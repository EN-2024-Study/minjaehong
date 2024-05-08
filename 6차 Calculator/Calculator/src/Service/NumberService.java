package Service;

import View.Panel.ResultPanel;

import javax.swing.*;
import java.util.ArrayDeque;

public class NumberService {

    private ArrayDeque<String> numberDeque;
    private ArrayDeque<String> operatorDeque;
    private ResultPanel resultPanel;

    public NumberService(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, ResultPanel resultPanel) {
        this.numberDeque = numberDeque;
        this.operatorDeque = operatorDeque;

        this.resultPanel = resultPanel;
    }

    // 1. Number Button (소수점  + negate 도 받음)
    // 일단 if return 으로 하고
    // 각자 함수로 쪼개든 공통 부분을 밖으로 빼내거든 하자
    // numberBtn 은 언제나 BigLabel 만 render (최신화)
    public void handleNumberInput(String newNum) {

        JLabel bigLabel = resultPanel.getBigLabel();
        JLabel smallLabel = resultPanel.getSmallLabel();

        // 소수점이면
        if (newNum == ".") {
            // 이미 소수점 존재하면
            if (bigLabel.getText().contains(newNum)) {
                // 아무것도 안해도 됨
            } else {
                // 아니면 추가
                String lastNum = numberDeque.removeLast();
                numberDeque.add(lastNum + newNum);
            }
            return;
        }

        // 마지막에 등호연산이었는데
        // 숫자 들어오면 덱 모두 비우고 새로운 연산 시작
        // 다시 시작하는거임
        if (smallLabel.getText().contains("=")) {
            numberDeque.clear();
            operatorDeque.clear();
            numberDeque.add(newNum);
        }

        // 숫자 0으로 시작하면
        // 빈 상태는 0임
        // CE 나 C도 다 빼고 0 넣어준게 default 상태
        if (numberDeque.getLast() == "0") {
            // 0으로 시작하고 소수점 들어오면 0. 만들어야함
            if (newNum == ".") {
                String lastNum = numberDeque.removeLast();
                numberDeque.add(lastNum + newNum);
            }
            // 소수점 아니면 0을 바꿔주면 됨
            else {
                numberDeque.removeLast();
                numberDeque.add(newNum);
            }
            return;
        }

        // 0이 아닌 숫자일때
        if (numberDeque.getLast() != "0") {
            // 한 숫자의 뒤에 추가되는 경우 == 숫자가 연산자보다 많을때임
            // 이거 소수점도 받을 수 있음
            if (numberDeque.size() > operatorDeque.size()) {
                String lastNum = numberDeque.removeLast();
                numberDeque.add(lastNum + newNum);
            } else {
                // 새 숫자가 추가되는 경우 == 숫자와 연산자 개수가 같을때 (둘 다 1개씩일때)
                numberDeque.add(newNum);
            }
            return;
        }
    }
}