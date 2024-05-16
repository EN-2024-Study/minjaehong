package Controller;

import View.Frame.MainFrame;

import javax.swing.*;
import java.awt.*;
import java.math.BigDecimal;
import java.math.MathContext;
import java.util.ArrayDeque;

public class OperatorEventController extends EventController {

    public OperatorEventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, MainFrame mainFrame) {
        super(numberDeque, operatorDeque, mainFrame);
    }

    //========================== IMPLEMENTED HANDLEEVENT FUNCTION ==========================//

    @Override
    public void handleEvent(String newOperator) {
        // cant divide by zero 상태이면 다시 정상화시키기
        if(checkIfCantDivideByZeroState()){
            changeToNormalState();
            return;
        }
        
        // operator 하나 들어갔다는 것은 그 전 숫자가 numberDeque 로 들어갔다는 것
        // 들어간 숫자들 trailingZeros 다 잘라주기
        refineCurrentNumbers();

        // 일단 무조건 operatorDeque 에 PUSH 하고 판단하기
        operatorDeque.add(newOperator);

        // 이번꺼가 등호일때
        if (newOperator.equals("=")) {
            handleEqualOperator();
        }
        // 이번꺼 등호 아니면
        else {
            handleNonEqualOperators();
        }
    }

    //================================== PRIVATE FUNCTIONS =================================//

    private void handleEqualOperator(){
        // 전꺼가 등호일때
        if (operatorDeque.size()==2 && operatorDeque.getFirst().equals("=")) {
            // 마지막 식을 대상으로 판단해야함
            String lastEquation = mainFrame.getLatestSmallLabel();
            String[] arr = lastEquation.split(" ");

            // 전꺼가 4 = 이런 형식이었으면
            if (arr.length == 2) {
                operatorDeque.removeFirst(); // (4 = =) -> (4 =)

                String num1 = arr[0];
                // negate 면 negate 다 깐 값 추출해야함
                if(num1.contains("negate")) num1 = getValueFromNegateCapsuledString(num1);

                addNewLogButton(lastEquation, num1);
                renderSmallLabel();
            }
            // 전꺼가 2 + 3 = 5 이런 형식이었으면
            else {
                operatorDeque.clear();
                operatorDeque.add(arr[1]);
                numberDeque.add(arr[2]);
                operatorDeque.add("=");
                renderSmallLabel();

                String result = calculate();

                numberDeque.add(result);
                renderBigLabel();
            }
        }
        // 전꺼가 등호가 아닐때
        else {
            // 1. 숫자 1개 등호 1개 되면 (잘됨)
            // ex) 4 =
            if (numberDeque.size() == 1 && operatorDeque.size() == 1) {
                renderSmallLabel();
            }
            // 2. 숫자 1개 연산자 2개(일반 1개 등호 1개) (잘됨)
            // matrix 수동 작성하고 calculate
            else if (numberDeque.size() == 1 && operatorDeque.size() == 2) {
                numberDeque.add(numberDeque.getLast());
                renderSmallLabel();
                String result = calculate();

                numberDeque.add(result);
                renderBigLabel();
            }
            // 3. 숫자 2개 연산자 2개(일반 1개 등호 1개) (잘됨)
            // 진짜 calculate
            else if (numberDeque.size() == 2 && operatorDeque.size() == 2) {
                renderSmallLabel();
                String result = calculate();
                numberDeque.add(result);

                renderBigLabel();
            }
        }
    }

    private void handleNonEqualOperators(){
        // 1. 숫자 1개 연산자 1개 = 할게 없음. 잘 추가하기만 해주면 됨 (잘됨)
        if (numberDeque.size() == 1 && operatorDeque.size() == 1) {
            // nothing to do
        }
        // 2. 숫자 1개 연산자 2개 = 연산자 교체 (잘됨)
        // 전 연산이 등호였어도 잘됨
        else if (numberDeque.size() == 1 && operatorDeque.size() == 2) {
            operatorDeque.removeFirst();
        }
        // 3. 숫자 2개 연산자 2개 = 계산 (잘됨)
        else if (numberDeque.size() == 2 && operatorDeque.size() == 2) {
            renderSmallLabel();
            String result = calculate(); // 계산하면 numDeque 비고 optDeque 에 = 아직 남아있음
            numberDeque.add(result); // 계산된 값 push
            renderBigLabel(); // 계산된 값 출력
        }

        if(numberDeque.getLast().equals("cant divide by zero!")) return;

        renderSmallLabel();
    }

    // 등호 연산 들어왔을때 or 연산자 두 개 채워지면 값 진짜로 계산하기
    // 이거하면
    // 무조건 numberDeque 는 size = 1 이고 결과값 들어감
    // 무조건 operatorDeque 는 size = 1 이고 마지막 연산 들어감
    // 연산자 무조건 남기기 등호여도 남기기
    private String calculate() {
        // 연산에 필요한 숫자 2개 + 연산자 1개 추출
        String str1 = numberDeque.removeFirst();
        String operator = operatorDeque.removeFirst();
        String str2 = numberDeque.removeFirst();

        // negate 이면 negate 다 깐 숫자 받기
        if(str1.contains("negate")) str1 = getValueFromNegateCapsuledString(str1);
        if(str2.contains("negate")) str2 = getValueFromNegateCapsuledString(str2);

        BigDecimal num1 = new BigDecimal(str1);
        BigDecimal num2 = new BigDecimal(str2);
        BigDecimal result = BigDecimal.ZERO;

        // 나누기 0이면 cant divide by zero state 로 만들어주기
        if (operator.equals("÷") && num2.equals(BigDecimal.ZERO)) {
            changeToCantDivideByZeroState();
            return "cant divide by zero!";
        }
        else {
            switch (operator) {
                case "+":
                    result = num1.add(num2, MathContext.DECIMAL128);
                    break;
                case "-":
                    result = num1.subtract(num2, MathContext.DECIMAL128);
                    break;
                case "×":
                    result = num1.multiply(num2, MathContext.DECIMAL128);
                    break;
                case "÷":
                    // windows 는 손실을 싫어함
                    // 연산하고 저장할때 바로 RoundingMode 쓰면 안됨
                    // 그걸 여기서 쓰면 8/9*9 했을때 8이 안나오게 되는거임
                    // 그래서 renderBigLabel 할때 그때 RoundingMode 로 잘라줘야하는거임
                    result = num1.divide(num2, MathContext.DECIMAL128);
                    break;
            }

            // logButton 작성을 위한 string 변환
            str1 = changeToEngineeredString(str1);
            str2 = changeToEngineeredString(str2);
            String curSmallLabelString = str1 + " " + operator + " " + str2 + " = ";
            String curBigLabelString = changeToEngineeredString(result.toString());

            // 새로운 logButton 추가
            addNewLogButton(curSmallLabelString, curBigLabelString);

            return curBigLabelString;
        }
    }

    // smallLabel 로 올라가기 전 정제시켜주기
    // negate capsule화 되어있으면 정제시켜주기
    private void refineCurrentNumbers(){
        BigDecimal refinedBigDecimal;

        if(numberDeque.getFirst().contains("negate")==false){
            refinedBigDecimal = new BigDecimal(numberDeque.removeFirst());
            numberDeque.addFirst(refinedBigDecimal.stripTrailingZeros().toPlainString());
        }
        
        if(numberDeque.getLast().contains("negate")==false){
            refinedBigDecimal = new BigDecimal(numberDeque.removeLast());
            numberDeque.addLast(refinedBigDecimal.stripTrailingZeros().toPlainString());
        }
    }

    // 0으로 나누기를 진행했을때 호출되는 함수
    private void changeToCantDivideByZeroState(){
        mainFrame.getButtonPanel().getDotButton().setEnabled(false);
        mainFrame.getButtonPanel().getDotButton().setBackground(Color.RED);
        mainFrame.getButtonPanel().getDivButton().setEnabled(false);
        mainFrame.getButtonPanel().getDivButton().setBackground(Color.RED);
        mainFrame.getButtonPanel().getAddButton().setEnabled(false);
        mainFrame.getButtonPanel().getAddButton().setBackground(Color.RED);
        mainFrame.getButtonPanel().getMulButton().setEnabled(false);
        mainFrame.getButtonPanel().getMulButton().setBackground(Color.RED);
        mainFrame.getButtonPanel().getSubButton().setEnabled(false);
        mainFrame.getButtonPanel().getSubButton().setBackground(Color.RED);
        mainFrame.getButtonPanel().getNegateButton().setEnabled(false);
        mainFrame.getButtonPanel().getNegateButton().setBackground(Color.RED);
    }

    // 등호 연산 될때마다 로그에 찍힘
    private void addNewLogButton(String logEquationString, String result){
        
        String html =
                "<html>" +
                        "<div style = 'text-align:right;'>" +
                        logEquationString + "<br>" + result +
                        "</div>" +
                        "</html>";

        JButton newLogButton = new JButton(html);
        newLogButton.setBackground(Color.WHITE);
        newLogButton.setHorizontalAlignment(SwingConstants.RIGHT);

        mainFrame.getLogPanel().addNewLog(newLogButton);
    }
}