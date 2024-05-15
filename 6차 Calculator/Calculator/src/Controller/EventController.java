package Controller;

import View.Frame.MainView;

import java.awt.*;
import java.math.BigDecimal;
import java.text.DecimalFormat;
import java.util.ArrayDeque;

// 추상클래스니까 클래스만 직접 생성불가
// protected 로 된 것들은 모두 자식클래스만 참조 가능
// public 은 다른 곳에서 호출 가능

// 자식 클래스에서 상속받아 받는 event 를 어떻게 다룰 건지 handleEvent 함수만 구현해주면 됨
public abstract class EventController{
    protected ArrayDeque<String> numberDeque;
    protected ArrayDeque<String> operatorDeque;

    protected MainView mainView;

    protected EventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, MainView mainView) {
        this.numberDeque = numberDeque;
        this.operatorDeque = operatorDeque;

        this.mainView = mainView;
    }

    //================ functions that have to be implemented in child classes ===============//

    // 무조건 자식클래스에서 구현되어야함
    // 만약 EventController 클래스로 자식클래스의 handleEvent 를 호출하면
    // abstract function 은 동적 바인딩되므로 자식클래스의 구현된 handleEvent 함수를 호출할 수 있음
    public abstract void handleEvent(String userInput);

    //============= functions that don't have to be implemented in child classes =============//

    protected void renderBigLabel(){
        String newNum = numberDeque.getLast();

        // cant divide by zero 예외면
        if(newNum.equals("cant divide by zero!")) {
            mainView.getResultPanel().getBigLabel().setText(newNum);
        }
        
        // negate capsuled string 이면
        // negate 다 까줘야함

        else if(newNum.contains("negate")){

            // 몇 번 capsuled 됐는지
            int capsuledCount = getNegateCapsuledCount(newNum);
            // capsuledCount 가지고 실제 숫자 추출
            int lastIdx = newNum.length() - 1;
            char realNum = newNum.charAt(lastIdx - capsuledCount);

            BigDecimal temp = new BigDecimal(realNum);
            if(capsuledCount%2!=0){
                temp = temp.negate();
            }
            mainView.getResultPanel().getBigLabel().setText(temp.toString());

            return;
        }

        // 아니면 string format 후 bigLabel 에 출력해주기
        else{
            newNum = changeToFormattedString(newNum);
            mainView.getResultPanel().getBigLabel().setText(newNum);
        }
    }

    private int getNegateCapsuledCount(String curString){
        int lastIdx = curString.length()-1;

        int capsuledCount = 0;
        while(curString.charAt(lastIdx)==')'){
            capsuledCount++;
            lastIdx--;
        }

        return capsuledCount;
    }

    protected void renderSmallLabel(){

        String newText;

        // "0." 으로 시작할때 예외처리
        if(operatorDeque.size()==0 && numberDeque.size()==1 && numberDeque.getFirst().equals("0.")){
            newText=" ";
        }else {
            Object[] numberArr = numberDeque.toArray();
            Object[] operatorArr = operatorDeque.toArray();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < numberArr.length; i++) {
                sb.append(numberArr[i]);
                sb.append(" ");

                sb.append(operatorArr[i]);
                sb.append(" ");
            }

            newText = sb.toString();

            // smallLabel 크기 줄어드는거 방지
            if (newText.isEmpty()) newText = " ";
        }

        mainView.getResultPanel().getSmallLabel().setText(newText);
    }

    // renderBigLabel 에서만 쓰임
    // 받은 문자열을 view 에 출력하기 전에 formatting 하려고 쓰임
    private String changeToFormattedString(String originalString){

        String formattedString;

        DecimalFormat df = new DecimalFormat("#,###");

        int decimalPointIndex = originalString.indexOf(".");

        if(decimalPointIndex==-1){
            BigDecimal originalNum = new BigDecimal(originalString);
            formattedString = df.format(originalNum);
        }else{
            String integerPart = originalString.substring(0, decimalPointIndex);
            String decimalPart = originalString.substring(decimalPointIndex);

            BigDecimal integerNum = new BigDecimal(integerPart);
            formattedString = df.format(integerNum) + decimalPart;
        }

        return formattedString;
    }

    // 현재 결과가 cant divide by zero 인지 확인해줌
    protected final boolean checkIfCantDivideByZeroState(){
        if(mainView.getResultPanel().getBigLabel().getText().equals("cant divide by zero!")) return true;
        else return false;
    }

    // cant divide by zero 로 disabled 된 button 들을 다시 enabled 시켜줌
    // 이건 모든 자식 controller 들이 가져야할 기능이라 부모 클래스에 있어야함
    protected final void changeToNormalState(){
        
        // disabled 된 button 다시 enable
        mainView.getButtonPanel().getDotButton().setEnabled(true);
        mainView.getButtonPanel().getDotButton().setBackground(Color.WHITE);
        mainView.getButtonPanel().getDivButton().setEnabled(true);
        mainView.getButtonPanel().getDivButton().setBackground(Color.WHITE);
        mainView.getButtonPanel().getAddButton().setEnabled(true);
        mainView.getButtonPanel().getAddButton().setBackground(Color.WHITE);
        mainView.getButtonPanel().getMulButton().setEnabled(true);
        mainView.getButtonPanel().getMulButton().setBackground(Color.WHITE);
        mainView.getButtonPanel().getSubButton().setEnabled(true);
        mainView.getButtonPanel().getSubButton().setBackground(Color.WHITE);
        mainView.getButtonPanel().getNegateButton().setEnabled(true);
        mainView.getButtonPanel().getNegateButton().setBackground(Color.WHITE);

        // 매끄러운 진행을 위한 matrix 조작
        // deque 다 비우고 default 0 집어넣어주기
        numberDeque.clear();
        operatorDeque.clear();
        renderSmallLabel();
        numberDeque.add("0");
        renderBigLabel();
    }

    // numberDeque 의 마지막 값을 인자로 주면 됨?
    protected String getNegateCapsuledString(String originalString){
        String capsuledString = "negate("+originalString+")";
        return capsuledString;
    }

    protected String getValueFromNegateCapsuledString(String capsuledString){
        int count = getNegateCapsuledCount(capsuledString);

        int idx = capsuledString.length() - count - 1;
        BigDecimal value = new BigDecimal(capsuledString.charAt(idx));

        if(count%2!=0) value = value.negate();

        return value.toString();
    }
}