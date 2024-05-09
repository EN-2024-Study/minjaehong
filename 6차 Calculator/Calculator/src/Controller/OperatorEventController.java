package Controller;

import View.Panel.ResultPanel;

import javax.swing.*;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayDeque;

public class OperatorEventController {
    private ArrayDeque<String> numberDeque;
    private ArrayDeque<String> operatorDeque;
    private ResultPanel resultPanel;

    public OperatorEventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, ResultPanel resultPanel) {
        this.numberDeque = numberDeque;
        this.operatorDeque = operatorDeque;

        this.resultPanel = resultPanel;
    }

    // operatorDeque 랑 numberDeque 만 변경해주면 됨
    // 이에 따른 Label Rendering 은 MainController 가 알아서 해줄거임
    // 들어온 연산자에 따라 ArrayDeque 조작만 해주면 됨
    public void handleOperatorInput(String newOperator) {

        JLabel bigLabel = resultPanel.getBigLabel();
        JLabel smallLabel = resultPanel.getSmallLabel();

        // 일단 무조건 operatorDeque 에 PUSH 하고 판단하기
        operatorDeque.add(newOperator);

        printStartMatrix(numberDeque, operatorDeque);

        // 이번꺼가 등호일때
        // 등호일때는 무조건 계산해줘야함
        if (newOperator.equals("=")) {
            // 전꺼가 등호일때
            if (operatorDeque.size()==2 && operatorDeque.getFirst().equals("=")) {
                String lastEquation = smallLabel.getText();
                String[] arr = lastEquation.split(" ");

                // 전꺼가 4 = 이런 형식이었으면
                if (arr.length == 2) {
                    operatorDeque.removeFirst(); // (4 = =) -> (4 =)
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
                //return;
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
            else if (numberDeque.size() == 1 && operatorDeque.size() == 2) {
                operatorDeque.removeFirst();
                renderSmallLabel();
            }
            // 3. 숫자 2개 연산자 2개 = 계산 (잘됨)
            else if (numberDeque.size() == 2 && operatorDeque.size() == 2) {
                String result = calculate(); // 계산하면 numDeque 비고 optDeque 에 = 아직 남아있음
                numberDeque.add(result); // 계산된 값 push
                renderSmallLabel();
                renderBigLabel(); // 계산된 값 출력
            }
        }
        printEndMatrix(numberDeque, operatorDeque);

    }

    // 등호 연산 들어왔을때 + 연산자 두 개 채워지면
    // 값 진짜로 계산하기
    // 이거하면 numberDeque 에 result 값 하나랑
    // operatorDeque 에 지금 들어온 operator 하나 남음
    // 즉 결과값 / newOperator 이렇게 남음
    // 연산자 무조건 남기기
    public String calculate() {
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

    private void renderBigLabel() {
        resultPanel.getBigLabel().setText(numberDeque.getLast());
    }

    private void renderSmallLabel() {
        Object[] numberArr = numberDeque.toArray();
        Object[] operatorArr = operatorDeque.toArray();

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < numberArr.length; i++) {
            sb.append(numberArr[i]);
            sb.append(" ");

            sb.append(operatorArr[i]);
            sb.append(" ");
        }
        resultPanel.getSmallLabel().setText(sb.toString());
    }

    private void printStartMatrix(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque){
        Object[] numberArr = numberDeque.toArray();
        Object[] operatorArr = operatorDeque.toArray();

        System.out.println("======[START]======");
        for(int i=0;i<numberArr.length;i++) System.out.print(numberArr[i]+" ");
        System.out.println();
        for(int i=0;i<operatorArr.length;i++) System.out.print(operatorArr[i]+" ");
        System.out.println();
        System.out.println("===================");
    }

    private void printEndMatrix(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque){
        Object[] numberArr = numberDeque.toArray();
        Object[] operatorArr = operatorDeque.toArray();

        System.out.println("===================");
        for(int i=0;i<numberArr.length;i++) System.out.print(numberArr[i]+" ");
        System.out.println();
        for(int i=0;i<operatorArr.length;i++) System.out.print(operatorArr[i]+" ");
        System.out.println();
        System.out.println("=======[END]=======");
    }
}