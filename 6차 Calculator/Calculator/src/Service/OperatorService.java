package Service;

import View.Panel.ResultPanel;

import javax.swing.*;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayDeque;

public class OperatorService {
    private ArrayDeque<String> numberDeque;
    private ArrayDeque<String> operatorDeque;
    private ResultPanel resultPanel;

    public OperatorService(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, ResultPanel resultPanel) {
        this.numberDeque = numberDeque;
        this.operatorDeque = operatorDeque;

        this.resultPanel = resultPanel;
    }

    // operatorDeque 랑 numberDeque 만 변경해주면 됨
    // 이에 따른 Label Rendering 은 Controller 가 알아서 해줄거임
    // 들어온 연산자에 따라 ArrayDeque 조작만 해주면 됨
    public void handleOperatorInput(String newOperator) {

        JLabel bigLabel = resultPanel.getBigLabel();
        JLabel smallLabel = resultPanel.getSmallLabel();

        // 일단 무조건 operatorDeque 에 PUSH 하고 판단하기
        operatorDeque.add(newOperator);

        // 만약 마지막 연산이 등호연산이고
        if (smallLabel.getText().contains("=")) {

            // 이번꺼도 등호연산이면
            if (newOperator.equals("=")) {
                // 저번 식 고대로 다시 해줘야함
                String lastEquation = smallLabel.getText();
                String[] arr = lastEquation.split(" ");
                operatorDeque.addFirst(arr[1]);
                numberDeque.add(arr[2]);
                operatorDeque.add("=");
                renderSmallLabel();

                String result = calculate();
                operatorDeque.removeFirst();
                numberDeque.add(result);
                renderBigLabel();
            }
            // 다른 연산자이면
            else {
                // 마지막꺼가 등호연산이었으면
                // 지금 무조건 숫자 1개 연산자 1개임(등호, 새연산자)
                // 왜냐하면 위에서 무조건 add(newOperator) 해줬기 때문
                // 그래서 smallLabel render 만 해주면 됨
                renderSmallLabel();
            }
            return;
        }

        // 마지막 연산이 등호연산이 아니고
        // 이번에 들어온게 등호연산이면
        if (newOperator.equals("=")) {

            // 만약 연산자가 등호 1개이면
            // ex) 4 =
            if(operatorDeque.size()==1){
                renderSmallLabel(); // 4 =
                operatorDeque.removeFirst(); // 들어온 = 빼주기
                return;
            }

            // 만약 연산자가 중복(일반 + 등호)으로 들어온거면
            // ex) 4 + = 이렇게 들어오면 4 + 4 = 으로 만들어줘야함
            // numberDeque 에 똑같은 수 하나 추가해줘야함
            if(numberDeque.size()==1 && operatorDeque.size()==2){
                numberDeque.add(numberDeque.getLast());
            }

            renderSmallLabel(); // 등호까지 모두 출력됨 ex) 1 + 2 =
            String result = calculate(); // 계산하면 numDeque 비고 optDeque 에 = 아직 남아있음

            operatorDeque.removeFirst(); // 들어온 = pop

            numberDeque.add(result); // 계산된 값 push
            renderBigLabel();
            return;
        }
        // 들어온게 일반 연산자이면
        else {
            if (numberDeque.size()==1 && operatorDeque.size()==2) {
                // 숫자 개수 1개 연산자 개수 2개면
                // 기존 연산자 바꾸는 작업임
                operatorDeque.removeFirst();
                renderSmallLabel();
            }
            else if (numberDeque.size()==1 && operatorDeque.size() == 1) {
                // 만약 이번꺼가 첫번째 연산자일때
                // 계산은 하지말고 smallLabel 만 최신화해주기
                renderSmallLabel();
            }
            else if (numberDeque.size()==2 && operatorDeque.size() == 2) {
                // 만약 숫자 개수도 2개 연산자 개수도 2개이면
                // 전자인 연산자 토대로 계산해줘야함
                String result = calculate(); // 이거 끝나면 숫자 1개 연산자 1개임

                /*
                if (result.equals("impossible")) {
                    buttonPanel.getDotButton().setEnabled(false);
                    buttonPanel.getDivButton().setEnabled(false);
                    buttonPanel.getAddButton().setEnabled(false);
                    buttonPanel.getMulButton().setEnabled(false);
                    buttonPanel.getSubButton().setEnabled(false);
                    buttonPanel.getNegateButton().setEnabled(false);
                }
                */

                // 계산결과 push
                numberDeque.add(result);

                renderSmallLabel();
                renderBigLabel();
            }
        }
    }

    // 값 진짜로 계산하기
    public String calculate () {
        BigDecimal num1 = new BigDecimal(numberDeque.removeFirst());
        String opt = operatorDeque.removeFirst();
        BigDecimal num2 = new BigDecimal(numberDeque.removeFirst());

        BigDecimal result = BigDecimal.ZERO;

        if (opt == "÷" && num2.equals(BigDecimal.ZERO)) {
            return "impossible";
        }

        switch (opt) {
            case "+":
                result = num1.add(num2);
                break;
            case "-":
                result = num1.subtract(num2);
                break;
            case "×":
                result = num1.multiply(num2);
                break;
            case "÷":
                // 16자리까지만 표기하고 반올림 해줌
                // 금융권에서 쓰는 정책? 이라고 한다
                result = num1.divide(num2, 16, RoundingMode.HALF_EVEN);
                break;
        }
        return result.stripTrailingZeros().toPlainString();
    }

    private void renderBigLabel () {
        resultPanel.getBigLabel().setText(numberDeque.getLast());
    }

    private void renderSmallLabel() {
        Object[] numberArr = numberDeque.toArray();
        Object[] operatorArr = operatorDeque.toArray();

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < numberArr.length; i++) {
            sb.append(numberArr[i]);
            sb.append(" ");
            if (i < operatorArr.length) {
                sb.append(operatorArr[i]);
                sb.append(" ");
            }
        }
        resultPanel.getSmallLabel().setText(sb.toString());
    }
}