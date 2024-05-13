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

        // 예외면
        if(newNum.equals("cant divide by zero!")) {
            mainView.getResultPanel().getBigLabel().setText(newNum);
        }
        // 아니면 string format 후 bigLabel 에 출력해주기
        else{
            newNum = changeToFormattedString(newNum);
            mainView.getResultPanel().getBigLabel().setText(newNum);
        }
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

    // 결과에 콤마 추가한 결과를 반환
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
    protected boolean checkIfCantDivideByZeroState(){
        if(mainView.getResultPanel().getBigLabel().getText().equals("cant divide by zero!")) {
            return true;
        }
        else {
            return false;
        }
    }

    // cant divide by zero 로 disabled 된 button 들을 다시 enabled 시켜줌
    // 이건 모든 자식 controller 들이 가져야할 기능이라 부모 클래스에 있어야함
    protected void changeToNormalState(){
        
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
}