package Controller;

import View.Panel.ResultPanel;

import javax.swing.*;
import java.util.ArrayDeque;

public class NumberEventController {

    private ArrayDeque<String> numberDeque;
    private ArrayDeque<String> operatorDeque;
    private ResultPanel resultPanel;

    private JLabel bigLabel;
    private JLabel smallLabel;

    public NumberEventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, ResultPanel resultPanel) {
        this.numberDeque = numberDeque;
        this.operatorDeque = operatorDeque;

        this.resultPanel = resultPanel;

        this.bigLabel = resultPanel.getBigLabel();
        this.smallLabel = resultPanel.getSmallLabel();
    }

    private void handleNegate(){
        String lastNum = numberDeque.removeLast();
        if (lastNum.startsWith("-")) {
            numberDeque.add(lastNum.substring(1));
        } else {
            numberDeque.add("-" + lastNum);
        }
        return;
    }

    private void handleDecimalPoint(){
        // 이미 현재 값에 소수점 존재하면
        if (numberDeque.getLast().contains(".")) {
            // 아무것도 안해도 됨
        } else {
            // 아니면 소수점 추가
            String lastNum = numberDeque.removeLast();
            numberDeque.add(lastNum + ".");
        }
        return;
    }

    private void handleNumber(String newNum){
        // 연산자가 0개일때
        if (numberDeque.size() == 1 && operatorDeque.size() == 0) {
            // 만약 0이면 Default 0임. 지워지는 0임
            if (numberDeque.getLast() == "0") {
                numberDeque.removeLast();
                numberDeque.add(newNum);
            }
            // 0 아니면 뒤에 추가하는 수임
            else {
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
                if (numberDeque.getLast() == "0") {
                    numberDeque.removeLast();
                    numberDeque.add(newNum);
                }
                // 0 아니면 뒤에 추가하는 수임
                else {
                    String lastNum = numberDeque.removeLast();
                    numberDeque.add(lastNum + newNum);
                }
                return;
            }
        }
    }

    // 1. +/- 일때
    // 2. 소수점일때
    // 3. 숫자일때 -> 그 전꺼에 따라 경우의 수 나눠짐
    public void handleNumberInput(String newNum) {

        // 1. +/- 예외처리
        if (newNum.equals("+/-") && numberDeque.getLast() != "0") {
            handleNegate();
        }

        // 2. 소수점이면
        else if (newNum.equals(".")) {
            handleDecimalPoint();
        }

        else{
            handleNumber(newNum);
        }
    }

    private void renderSmallLabel () {
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
        smallLabel.setText(sb.toString());
    }

    private void renderBigLabel () {
        bigLabel.setText(numberDeque.getLast());
    }

}