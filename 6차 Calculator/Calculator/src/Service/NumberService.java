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

        /*
        if(newNum.equals("+/-") && numberDeque.getLast()!="0"){
            String lastNum = numberDeque.removeLast();
            if(lastNum.startsWith("-")){
                numberDeque.add(lastNum.substring(1));
            }else{
                numberDeque.add("-"+lastNum);
            }
            return;
        }
        */

        // 소수점이면
        if (newNum.equals(".")) {
            // 이미 현재 값에 소수점 존재하면
            if (bigLabel.getText().contains(newNum)) {
                // 아무것도 안해도 됨
            }
            else {
                // 아니면 소수점 추가
                String lastNum = numberDeque.removeLast();
                numberDeque.add(lastNum + newNum);
            }
            return;
        }

        // 마지막에 등호연산이었는데 숫자들어옴
        if (smallLabel.getText().contains("=")) {
            // 덱 모두 비우고 새로운 연산 시작
            numberDeque.clear();
            operatorDeque.clear();
            numberDeque.add(newNum);
            // smallLabel 비워주기
            smallLabel.setText("");
            return;
        }

        // 전 숫자가 0이면
        // 1. 계산에 쓰이는 0인지
        // 2. 시작상태 OR CE OR C 의 default 로 들어가있는 0인지 판별해야함
        /*
        if(numberDeque.getLast()=="0"){
            // 기존에 있는 0이 default가 아닌 계산에 쓰이는 0이면
            // 0 x 3 이면 연산자 x가 들어왔기 때문에 기존 0은 연산에 쓰이는 0임
            if(operatorDeque.size()!=0){

            }
        }
        */

        // 계산에 쓰이는 0 판단
        // 0 x 3 이면 연산자 x 가 들어왔기 때문에 기존 0은 연산에 쓰이는 0임
        if(numberDeque.getLast()=="0" && numberDeque.size()==1 && operatorDeque.size()==1){
            numberDeque.add(newNum);
            return;
        }

        // 아직 안쓰이는 0 판단
        // default 상태의 0 + 두번째로 0 들어갔다가 다른 숫자 들어갔을때
        // 시작상태 or CE or C 후 상태가 numberDeque 에 0 하나만 들어가 있는 상태임
        if (numberDeque.getLast() == "0") {
            // 0으로 시작하고 소수점 들어오면 0. 만들어야함
            if (newNum == ".") {
                String lastNum = numberDeque.removeLast();
                numberDeque.add(lastNum + newNum);
            }
            // 소수점 아니면 default 0을 바꿔주면 됨
            else {
                System.out.println("changing default 0 to newNum");
                numberDeque.removeLast();
                numberDeque.add(newNum);
            }
            return;
        }

        // 0이 아닌 숫자일때
        else if (numberDeque.getLast() != "0") {
            // 한 숫자의 뒤에 추가되는 경우 == 숫자가 연산자보다 많을때임
            // 숫자 1개 연산자 0개 or 숫자 2개 연산자 1개일때
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