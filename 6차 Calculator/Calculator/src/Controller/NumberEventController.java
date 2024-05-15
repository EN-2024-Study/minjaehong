package Controller;

import View.Frame.MainFrame;

import java.math.BigDecimal;
import java.util.ArrayDeque;

// 무조건 BigLabel 만 변경
public class NumberEventController extends EventController{

    public NumberEventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, MainFrame mainFrame) {
        super(numberDeque, operatorDeque, mainFrame);
    }

    @Override
    public void handleEvent(String newNum) {

        // cant divide by zero 상태이면 다시 정상화시키기
        if(checkIfCantDivideByZeroState()){
            changeToNormalState();
        }
        // cant divide by zero 상태가 아닐때는 정상적으로 처리
        else {
            switch (newNum) {
                case "+/-":
                    handleNegate();
                    break;

                case ".":
                    handleDecimalPoint();
                    break;

                default:
                    handleNumber(newNum);
                    break;
            }
        }
        // number 가 들어왔을때는 항상 BigLabel render
        renderBigLabel();
    }
    
    // negate 일때는 smallLabel 이 최신화되는 경우가 있음
    // 그래서 필요에 따라 renderSmallLabel 함수를 넣어줘야함
    private void handleNegate(){

        // DEFAULT 0 이면 아무것도 안해도 됨
        if(numberDeque.getLast().equals("0") && operatorDeque.size()==0) return;

        //if(numberDeque.getLast().equals("0."))

        // negate capsule화가 처음일때는 
        // 숫자 1개 연산자 1개 채워져있을때 밖에 없음
        // 이때도 마지막 연산이 등호였는지 아닌지에 따라 경우가 나눠짐
        if(numberDeque.size()==1 && operatorDeque.size()==1) {

            String curNum = numberDeque.getFirst();
            
            // 만약 마지막 연산이 등호였으면
            // 새로운 시작이므로 numberDeque operatorDeque 모두 비워주기
            if (operatorDeque.getLast().equals("=")) {
                operatorDeque.clear();
                numberDeque.clear();
            }

            // 마지막 연산이 등호가 아니면
            // 이미 채워져있는 숫자의 negate 값을 두번째 수로 추가해줌
            numberDeque.add(getNegateCapsuledString(curNum));
            
            renderSmallLabel();
            return;
        }

        // 이미 negate capsule화가 되어있는 식이면
        // 캡슐화 한번 더 해서 넣어주기
        if(numberDeque.getLast().contains("negate")){
            String curNum = numberDeque.removeLast();
            numberDeque.add(getNegateCapsuledString(curNum));
            
            renderSmallLabel();
            return;
        }

        // 위의 경우가 아닐때는 그냥 일반적인 - 부호만 붙이는 처리해주면 됨
        BigDecimal lastNum = new BigDecimal(numberDeque.removeLast());
        lastNum = lastNum.negate();

        numberDeque.add(lastNum.toString());
    }

    private void handleDecimalPoint(){

        // 마지막 연산이 "=" 일때 or 다음 수 받아야할때
        if(operatorDeque.size() > 0){
            // 마지막 연산이 "=" 일때 예외처리
            // 0. 이 찍히고 smallLabel 모두 비워져야함
            if(operatorDeque.getFirst().equals("=")) {
                numberDeque.clear();
                operatorDeque.clear();
                numberDeque.add("0.");

                renderSmallLabel();
                return;
            }
            // 다음 수 받아야할때
            // 바로 . 으로 0. 받는거 예외처리
            else if(numberDeque.size()==1 && operatorDeque.size()==1){
                numberDeque.add("0.");
                return;
            }
        }

        // 이미 현재 값에 소수점 존재하면
        if (numberDeque.getLast().contains(".")) {
            // 아무것도 안해도 됨
        } else {
            // 아니면 소수점 추가
            String lastNum = numberDeque.removeLast();
            numberDeque.add(lastNum + ".");
        }
    }

    private void handleNumber(String newNum){

        // 연산자가 0개일때
        if (numberDeque.size() == 1 && operatorDeque.size() == 0) {
            // 만약 0이면 Default 0임. 지워지는 0임
            if (numberDeque.getLast().equals("0")) {
                numberDeque.removeLast();
                numberDeque.add(newNum);
            }
            // 0 아니면 뒤에 추가하는 수임
            else {
                if(isCurNumFull()) return;

                String lastNum = numberDeque.removeLast();
                numberDeque.add(lastNum + newNum);
            }
            return;
        }

        // 연산자 1개일때
        if (operatorDeque.size() == 1) {

            // 만약 남아있는 연산자가 "=" 연산자이면
            if(operatorDeque.getFirst().equals("=")){
                numberDeque.clear();
                operatorDeque.clear();
                numberDeque.add(newNum);
                return;
            }

            // 만약 이번에 새로운 수를 추가할 차례이면
            // 무지성으로 추가
            if (numberDeque.size() == 1) {
                numberDeque.add(newNum);
                return;
            }

            if (numberDeque.size() == 2) {
                // 만약 0이면 지워도 되는 0임
                if (numberDeque.getLast().equals("0")) {
                    numberDeque.removeLast();
                    numberDeque.add(newNum);
                }
                // 0 아니면 뒤에 추가하는 수임
                else {
                    if(isCurNumFull()) return;

                    String lastNum = numberDeque.removeLast();
                    numberDeque.add(lastNum + newNum);
                }
                return;
            }
        }
    }

    // 현재 숫자 꽉찼는지 판단
    // 새로운 number 추가하기 직전에 호출해서 판단
    private boolean isCurNumFull(){

        String curNum = numberDeque.getLast();

        // 소수점 있을때
        if(curNum.contains(".")){
            // 정수부가 0이고 17자리 OR 정수부가 0이 아니고 16자리
            if((curNum.startsWith("0") && curNum.length()-1==17) ||
                    (!curNum.startsWith("0") && curNum.length()-1==16)) return true;
        }
        // 소수점 없을때 16자리가 MAX
        else {
            if(curNum.length()==16) return true;
        }
        return false;
    }
}