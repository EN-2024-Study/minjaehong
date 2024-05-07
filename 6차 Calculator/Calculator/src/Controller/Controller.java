package Controller;

import View.MainView;
import View.Panel.ButtonPanel;
import View.Panel.ResultPanel;

import javax.swing.*;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.util.ArrayDeque;

public class Controller {
    ButtonPanel buttonPanel;
    ResultPanel resultPanel;

    Observer observer;

    JLabel smallLabel;
    JLabel bigLabel;

    ArrayDeque<String> numberDeque;
    ArrayDeque<String> operatorDeque;

    public Controller(MainView mainView){

        numberDeque = new ArrayDeque<>();
        numberDeque.add("0");
        operatorDeque = new ArrayDeque<>();

        buttonPanel = mainView.getButtonPanel();
        resultPanel = mainView.getResultPanel();

        smallLabel= resultPanel.getSmallLabel();
        bigLabel = resultPanel.getBigLabel();

        observer = new Observer(buttonPanel, this);

        mainView.addComponentListener(new ComponentAdapter() {
            @Override
            public void componentResized(ComponentEvent e) {
                int width = mainView.getWidth();
                if (width < 600) {
                    mainView.getLogPanel().setVisible(false);
                }else{
                    mainView.getLogPanel().setVisible(true);
                }
            }
        });

        BindNumberObserverToButtonPanel();
    }

    // 1. Number Button
    // 이거 소수점도 받음
    public void numBtnClicked(String newNum){

        if(bigLabel.getText().contains(".") && newNum==".") {
            //System.out.println("소수점 이미 있어!!!");
            return;
        }

        // 숫자 0으로 시작할때 예외처리
        // 빈 상태는 0임. CE 나 C도 다 빼고 0 넣어준게 default 상태
        if(numberDeque.getLast()=="0"){
            if(newNum=="."){
                String lastNum = numberDeque.removeLast();
                numberDeque.add(lastNum + newNum);
            }else{
                numberDeque.removeLast();
                numberDeque.add(newNum);
            }
        }
        // 0이 아닌 숫자로 시작할때
        else {

            // 한 숫자의 뒤에 추가되는 경우
            // 이거 소수점도 받을 수 있음
            if (numberDeque.size() > operatorDeque.size()) {
                String lastNum = numberDeque.removeLast();
                numberDeque.add(lastNum + newNum);
            }
            // 새 숫자가 추가되는 경우
            // 두 덱의 개수가 같을때
            else {
                numberDeque.add(newNum);
            }
        }
        // VIEW 최신화
        // numberBtn 은 BigLabel 만 render
        resultPanel.setBigLabel(numberDeque.getLast());
    }

    // 2. Operation Button
    // 기본적인 사칙연산 + - x /
    // 등호 연산
    //
    public void optBtnClicked(String curOperator){

        // 이미 소수점 있으면 return

        // 일단 무조건 operatorDeque 에 PUSH 하고 판단하기
        operatorDeque.add(curOperator);

        // 만약 = 이 들어왔으면
        if(curOperator=="="){
            // 연산자 한 개 일때랑
            if(operatorDeque.size()==1){

            }
            // 두개 일때랑 다름
            if(operatorDeque.size()==2){

            }
        }

        if(numberDeque.size()<operatorDeque.size()){
            operatorDeque.removeFirst();
            renderSmallLabel();
        }

        if (operatorDeque.size()==1) {
            renderSmallLabel();
        }

        // 만약 일반 연산자이면
        if(operatorDeque.size()==2){
            String result = calculate();
            // result 추가
            numberDeque.add(result);

            // calculate 에서 이미 계산하면서 Deque 값들을 다 빼냄

            renderSmallLabel();
            // 연산자 들어오면 BigLabel 항상 최신화
            renderBigLabel();
        }
    }

    // 값 진짜로 계산하기
    public String calculate(){
        Double num1 = Double.parseDouble(numberDeque.removeFirst());
        String opt = operatorDeque.removeFirst();
        Double num2 = Double.parseDouble(numberDeque.removeFirst());

        double result = 0;

        switch(opt){
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
            case "×":
                result = num1 * num2;
                break;
            case"÷":
                result = num1 / num2;
                break;
        }
        return Double.toString(result);
    }

    // bigLabel 0으로 만듬
    public void clearEntryBtnClicked(){
        // 맨 마지막에 추가한거 빼주기
        numberDeque.removeLast();
        numberDeque.add("0");
        renderBigLabel();
        // renderBig to 0 always
    }

    // smallLabel bigLabel 모두 0으로 만듬
    public void clearBtnClicked(){
        numberDeque.clear();
        operatorDeque.clear();
        // numberDeque에는 무조건 숫자 하나 들어가 있어야함
        // 실제로 숫자 넣지 말기?? 그냥 0으로 보이게만 하기??
        // numberDeque.add(0.0);

        numberDeque.add("0");
        renderBigLabel();
        renderSmallLabel();
    }

    private void renderSmallLabel(){
        Object[] numberArr = numberDeque.toArray();
        Object[] operatorArr = operatorDeque.toArray();

        StringBuilder sb = new StringBuilder();
        for(int i=0;i<numberArr.length;i++){
            sb.append(numberArr[i]);
            sb.append(" ");
            if(i<operatorArr.length) {
                sb.append(operatorArr[i]);
                sb.append(" ");
            }
        }
        smallLabel.setText(sb.toString());
    }

    private void renderBigLabel(){
        bigLabel.setText(numberDeque.getLast());
    }

    private void BindNumberObserverToButtonPanel(){
        buttonPanel.getNum0Button().addActionListener(observer);
        buttonPanel.getNum1Button().addActionListener(observer);
        buttonPanel.getNum2Button().addActionListener(observer);
        buttonPanel.getNum3Button().addActionListener(observer);
        buttonPanel.getNum4Button().addActionListener(observer);
        buttonPanel.getNum5Button().addActionListener(observer);
        buttonPanel.getNum6Button().addActionListener(observer);
        buttonPanel.getNum7Button().addActionListener(observer);
        buttonPanel.getNum8Button().addActionListener(observer);
        buttonPanel.getNum9Button().addActionListener(observer);
        buttonPanel.getDotButton().addActionListener(observer);

        buttonPanel.getClearEntryButton().addActionListener(observer);
        buttonPanel.getClearButton().addActionListener(observer);
        buttonPanel.getBackSpaceButton().addActionListener(observer);

        buttonPanel.getAddButton().addActionListener(observer);
        buttonPanel.getSubButton().addActionListener(observer);
        buttonPanel.getMulButton().addActionListener(observer);
        buttonPanel.getDivButton().addActionListener(observer);
        buttonPanel.getEqualButton().addActionListener(observer);
        buttonPanel.getSignButton().addActionListener(observer);
    }

}
