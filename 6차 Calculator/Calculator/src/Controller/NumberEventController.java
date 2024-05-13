package Controller;

import View.Frame.MainView;
import java.math.BigDecimal;
import java.util.ArrayDeque;

// 무조건 BigLabel 만 변경
public class NumberEventController extends EventController{

    public NumberEventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, MainView mainView) {
        super(numberDeque, operatorDeque, mainView);
    }

    @Override
    public void handleEvent(String newNum) {

        // cant divide by zero 상태이면 다시 정상화시키기
        if(checkIfCantDivideByZeroState()){
            changeToNormalState();
        }

        switch (newNum){
            case "+/-":
                // 1. +/- 예외처리
                handleNegate();
                break;
            case ".":
                // 2. 소수점 예외처리
                handleDecimalPoint();
                break;
            default:
                handleNumber(newNum);
                break;
        }

        // 위 switch 문에서 항상 deque 에 원소를 추가하고 끝남
        // 따라서 numberDeque 는 size > 0 일 수 밖에 없음
        // 이미 numberDeque 에 반영된 상태에서 render 함수가 호출되기 때문에
        // render 함수에서 한 작업은 기존 Deque 에 반영이 안됨
        // 즉 진짜 프론트 작업 맘대로 해도 된다는 의미
        // formatting 처리를 이 함수 안에서 해도 해당 결과는 numberDeque 에 반영이 안된다
        renderBigLabel();
    }

    private void handleNegate(){

        // 0인데 negate 들어왔으면 아무것도 안하면 됨
        if(numberDeque.getLast().equals("0")) return;

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