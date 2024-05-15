package Controller;

import View.Frame.MainView;

import javax.swing.*;
import java.awt.*;
import java.math.BigDecimal;
import java.math.MathContext;
import java.util.ArrayDeque;

public class OperatorEventController extends EventController {

    public OperatorEventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, MainView mainView) {
        super(numberDeque, operatorDeque, mainView);
    }

    @Override
    public void handleEvent(String newOperator) {

        //printStartMatrix(numberDeque, operatorDeque);

        // cant divide by zero 상태이면 다시 정상화시키기
        if(checkIfCantDivideByZeroState()){
            changeToNormalState();
            return;
        }
        
        // operator 하나 들어갔다는 것은 그 전 숫자가 입력되었다는 것
        // 그 전 숫자들 trailingZeros 다 잘라주기
        refineCurrentNumbers();

        // 일단 무조건 operatorDeque 에 PUSH 하고 판단하기
        operatorDeque.add(newOperator);

        // 이번꺼가 등호일때
        // 등호일때는 무조건 계산해줘야함
        if (newOperator.equals("=")) {
            // 전꺼가 등호일때
            if (operatorDeque.size()==2 && operatorDeque.getFirst().equals("=")) {
                // 마지막 식을 대상으로 판단해야함
                String lastEquation = mainView.getLastEquation();
                String[] arr = lastEquation.split(" ");

                // 전꺼가 4 = 이런 형식이었으면
                if (arr.length == 2) {
                    operatorDeque.removeFirst(); // (4 = =) -> (4 =)
                    addNewLogButton(lastEquation, new BigDecimal(arr[0]));
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
                return;
            }
            // 전꺼가 등호가 아닐때
            else {
                // 1. 숫자 1개 등호 1개 되면
                // ex) 4 =
                if (numberDeque.size() == 1 && operatorDeque.size() == 1) {
                    renderSmallLabel();
                    //return;
                }
                // 2. 숫자 1개 연산자 2개(일반 1개 등호 1개)
                // matrix 수동 작성하고 calculate
                else if (numberDeque.size() == 1 && operatorDeque.size() == 2) {
                    numberDeque.add(numberDeque.getLast());
                    renderSmallLabel();
                    String result = calculate();

                    numberDeque.add(result);
                    renderBigLabel();
                    //return;
                }
                // 3. 숫자 2개 연산자 2개(일반 1개 등호 1개)
                // 진짜 calculate
                else if (numberDeque.size() == 2 && operatorDeque.size() == 2) {
                    renderSmallLabel();
                    String result = calculate();
                    numberDeque.add(result);

                    renderBigLabel();
                    return;
                }
            }
        }
        // 이번꺼가 등호가 아닐때
        else {
            // 1. 숫자 1개 연산자 1개 = 할게 없음. 잘 추가하기만 해주면 됨 (잘됨)
            if (numberDeque.size() == 1 && operatorDeque.size() == 1) {
                renderSmallLabel();
            }
            // 2. 숫자 1개 연산자 2개 = 연산자 교체 (잘됨)
            // 전 연산이 등호였어도 잘됨
            else if (numberDeque.size() == 1 && operatorDeque.size() == 2) {
                operatorDeque.removeFirst();
                renderSmallLabel();
            }
            // 3. 숫자 2개 연산자 2개 = 계산 (잘됨)
            else if (numberDeque.size() == 2 && operatorDeque.size() == 2) {
                renderSmallLabel();
                String result = calculate(); // 계산하면 numDeque 비고 optDeque 에 = 아직 남아있음
                numberDeque.add(result); // 계산된 값 push
                renderSmallLabel();
                renderBigLabel(); // 계산된 값 출력
            }
        }
        //printEndMatrix(numberDeque, operatorDeque);
    }

    // 등호 연산 들어왔을때 or 연산자 두 개 채워지면 값 진짜로 계산하기
    // 이거하면
    // 무조건 numberDeque 는 size = 1 이고 결과값 들어감
    // 무조건 operatorDeque 는 size = 1 이고 마지막 연산 들어감
    // 연산자 무조건 남기기 등호여도 남기기
    private String calculate() {

        String str1 = numberDeque.removeFirst();
        if(str1.contains("negate")) str1 = getValueFromNegateCapsuledString(str1);
        String str2 = numberDeque.removeFirst();
        if(str2.contains("negate")) str2 = getValueFromNegateCapsuledString(str2);

        BigDecimal num1 = new BigDecimal(str1);
        String operator = operatorDeque.removeFirst();
        BigDecimal num2 = new BigDecimal(str2);

        // log equation 저장을 위해 계산하기 전 미리 저장해놓기
        String logEquationString = num1 + " "+ operator + " " + num2 + " = ";

        BigDecimal result = BigDecimal.ZERO;

        // 0으로 나눴으면 cant divide by zero state 로 만들어주기
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

            addNewLogButton(logEquationString, result);

            return result.stripTrailingZeros().toPlainString();
        }
    }

    // smallLabel 로 올라가기 전 정제시켜주기
    private void refineCurrentNumbers(){
        BigDecimal refinedBigDecimal;
        
        // 만약 numberDeque 의 첫번째 원소가 negate 캡슐화가 안되어있으면
        // 정제할 수 있는 수이므로 정제해주기
        if(numberDeque.getFirst().contains("negate")==false){
            refinedBigDecimal = new BigDecimal(numberDeque.removeFirst());
            numberDeque.addFirst(refinedBigDecimal.stripTrailingZeros().toPlainString());
        }

        // 만약 numberDeque 의 두번째 원소가 negate 캡슐화가 안되어있으면
        // 정제할 수 있는 수이므로 정제해주기
        if(numberDeque.getLast().contains("negate")==false){
            refinedBigDecimal = new BigDecimal(numberDeque.removeLast());
            numberDeque.addLast(refinedBigDecimal.stripTrailingZeros().toPlainString());
        }
    }

    private void changeToCantDivideByZeroState(){
        mainView.getButtonPanel().getDotButton().setEnabled(false);
        mainView.getButtonPanel().getDotButton().setBackground(Color.RED);
        mainView.getButtonPanel().getDivButton().setEnabled(false);
        mainView.getButtonPanel().getDivButton().setBackground(Color.RED);
        mainView.getButtonPanel().getAddButton().setEnabled(false);
        mainView.getButtonPanel().getAddButton().setBackground(Color.RED);
        mainView.getButtonPanel().getMulButton().setEnabled(false);
        mainView.getButtonPanel().getMulButton().setBackground(Color.RED);
        mainView.getButtonPanel().getSubButton().setEnabled(false);
        mainView.getButtonPanel().getSubButton().setBackground(Color.RED);
        mainView.getButtonPanel().getNegateButton().setEnabled(false);
        mainView.getButtonPanel().getNegateButton().setBackground(Color.RED);
    }

    // 등호 연산 될때마다 로그에 찍힘
    private void addNewLogButton(String logEquationString, BigDecimal result){
        
        String html =
                "<html>" +
                        "<div style = 'text-align:right;'>" +
                        logEquationString + "<br>" + result +
                        "</div>" +
                        "</html>";

        JButton newLogButton = new JButton(html);
        newLogButton.setBackground(Color.WHITE);
        newLogButton.setHorizontalAlignment(SwingConstants.RIGHT);

        mainView.getLogPanel().addNewLogLabel(newLogButton);
    }
}