package Controller;

import View.Frame.MainFrame;

import java.awt.*;
import java.math.BigDecimal;
import java.text.DecimalFormat;
import java.util.ArrayDeque;

// 추상클래스니까 클래스만 직접 생성불가
// protected로 된 것들은 모두 자식클래스만 참조 가능
// public은 다른 곳에서 호출 가능

// 자식 클래스에서 상속받아 받는 event를 어떻게 다룰 건지 handleEvent 함수만 구현해주면 됨
public abstract class EventController{

    protected ArrayDeque<String> numberDeque;
    protected ArrayDeque<String> operatorDeque;

    protected MainFrame mainFrame;

    protected EventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, MainFrame mainFrame) {
        this.numberDeque = numberDeque;
        this.operatorDeque = operatorDeque;

        this.mainFrame = mainFrame;
    }

    //================ functions that have to be implemented in child classes ===============//

    // 무조건 자식클래스에서 구현되어야함
    // 만약 EventController 클래스로 자식클래스의 handleEvent 를 호출하면
    // abstract function은 동적 바인딩되므로 자식클래스의 구현된 handleEvent 함수를 호출할 수 있음
    public abstract void handleEvent(String userInput);

    //============= functions that don't have to be implemented in child classes =============//

    protected void renderBigLabel(){
        String newNum = numberDeque.getLast();

        // 1. cant divide by zero 이면 아무것도 안해줘도 됨
        if(newNum.equals("cant divide by zero!")) {

        }
        // 2. negate capsuled string이면 negate 다 까줘야함
        else if(newNum.contains("negate")){
            newNum = getValueFromNegateCapsuledString(newNum);
        }
        // 3. 그냥 일반적인 숫자면 format 처리해주기
        else{
            System.out.println("before : " + newNum);
            newNum = changeToEngineeredString(newNum);
            System.out.println("after engineered : " + newNum);
            newNum = changeToFormattedString(newNum);
            System.out.println("after formatted : " + newNum);
        }

        // 작업 다 끝나면 bigLabel에 적용해주기
        mainFrame.getResultPanel().getBigLabel().setText(newNum);
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
                sb.append(changeToEngineeredString(numberArr[i].toString()));
                sb.append(" ");

                if(i < operatorArr.length) {
                    sb.append(operatorArr[i]);
                    sb.append(" ");
                }
            }

            newText = sb.toString();

            // smallLabel 크기 줄어드는거 방지
            if (newText.isEmpty()) newText = " ";
        }

        mainFrame.getResultPanel().getSmallLabel().setText(newText);
    }

    // renderBigLabel 에서만 쓰임
    // 받은 문자열을 view 에 출력하기 전에 formatting 하려고 쓰임
    private String changeToFormattedString(String originalString){

        if(originalString.startsWith("-0.")) return originalString;

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
        if(mainFrame.getLastBigLabel().equals("cant divide by zero!")) return true;
        else return false;
    }

    // cant divide by zero 로 disabled 된 button 들을 다시 enabled 시켜줌
    // 이건 모든 자식 controller 들이 가져야할 기능이라 부모 클래스에 있어야함
    protected final void changeToNormalState(){
        
        // disabled 된 button 다시 enable
        mainFrame.getButtonPanel().getDotButton().setEnabled(true);
        mainFrame.getButtonPanel().getDotButton().setBackground(Color.WHITE);
        mainFrame.getButtonPanel().getDivButton().setEnabled(true);
        mainFrame.getButtonPanel().getDivButton().setBackground(Color.WHITE);
        mainFrame.getButtonPanel().getAddButton().setEnabled(true);
        mainFrame.getButtonPanel().getAddButton().setBackground(Color.WHITE);
        mainFrame.getButtonPanel().getMulButton().setEnabled(true);
        mainFrame.getButtonPanel().getMulButton().setBackground(Color.WHITE);
        mainFrame.getButtonPanel().getSubButton().setEnabled(true);
        mainFrame.getButtonPanel().getSubButton().setBackground(Color.WHITE);
        mainFrame.getButtonPanel().getNegateButton().setEnabled(true);
        mainFrame.getButtonPanel().getNegateButton().setBackground(Color.WHITE);

        // 매끄러운 진행을 위한 deque 조작
        // deque 다 비우고 default 0 집어넣어주기
        numberDeque.clear();
        operatorDeque.clear();
        renderSmallLabel();
        numberDeque.add("0");
        renderBigLabel();
    }

    // E 포함된 BigDecimal 로 바꿔줌
    protected String changeToEngineeredString(String curNum){
        BigDecimal standardNum = new BigDecimal("10E+14");
        String resultString;

        // 예외일때는 skip
        if(curNum.contains(".") || curNum.contains("negate") || curNum.equals("cant divide by zero!")) return curNum;

        BigDecimal testNum = new BigDecimal(curNum);
        if(testNum.abs().compareTo(standardNum) > 0) {
            resultString = testNum.stripTrailingZeros().toEngineeringString();
        }else{
            resultString = testNum.stripTrailingZeros().toPlainString();
        }
        return resultString;
    }
    
    //==================== functions for negate-capsuled string handling =====================//

    // negate capsule화를 한번 더 시킨 string을 return
    protected final String getNegateCapsuledString(String originalString){
        String capsuledString = "negate("+originalString+")";
        return capsuledString;
    }

    // negate capsule화된 횟수를 return
    private int getNegateCapsuledCount(String capsuledString){
        int lastIdx = capsuledString.length()-1;
        int capsuledCount = 0;

        while(capsuledString.charAt(lastIdx--)==')') capsuledCount++;

        return capsuledCount;
    }

    // negate capsule화된 string에서 negate 대상이 된 값을 return
    protected final String getValueFromNegateCapsuledString(String capsuledString){
        int capsuledCount = getNegateCapsuledCount(capsuledString);

        // 숫자 끝나는 idx 찾기
        int numberEndIdx = capsuledString.length() - capsuledCount - 1;

        // 숫자 시작 idx 찾기
        int numberStartIdx = numberEndIdx;
        while(capsuledString.charAt(numberStartIdx)!='(') numberStartIdx--;
        numberStartIdx+=1;

        // negate 된 대상 추출
        BigDecimal value = new BigDecimal(capsuledString.substring(numberStartIdx, numberEndIdx+1));

        // 홀수번 했으면 negate 해주기
        if(capsuledCount%2 != 0) value = value.negate();

        return value.toString();
    }
}