package Controller;

import Service.NumberService;
import Service.OperatorService;
import View.MainView;
import View.Panel.ButtonPanel;
import View.Panel.ResultPanel;

import javax.swing.*;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.util.ArrayDeque;

public class Controller {
    ButtonPanel buttonPanel;
    ResultPanel resultPanel;

    Observer observer;

    NumberService numberService;
    OperatorService operatorService;

    JLabel smallLabel;
    JLabel bigLabel;

    ArrayDeque<String> numberDeque;
    ArrayDeque<String> operatorDeque;

    public Controller(MainView mainView){

        // numberDeque 는 default 상태가 0이 들어가 있는 상태
        numberDeque = new ArrayDeque<>();
        numberDeque.add("0");
        operatorDeque = new ArrayDeque<>();

        buttonPanel = mainView.getButtonPanel();
        resultPanel = mainView.getResultPanel();

        smallLabel= resultPanel.getSmallLabel();
        bigLabel = resultPanel.getBigLabel();

        // controller 자기 자신한테 해야할 일을 전달할 수 있게끔 자기 자신을 인자로 주기
        observer = new Observer(this);

        numberService = new NumberService(numberDeque, operatorDeque, resultPanel);
        operatorService = new OperatorService(numberDeque, operatorDeque, resultPanel);

        mainView.addComponentListener(new ComponentAdapter() {
            @Override
            public void componentResized(ComponentEvent e) {
                int width = mainView.getWidth();
                if (width < 500) {
                    mainView.getLogPanel().setVisible(false);
                }else{
                    mainView.getLogPanel().setVisible(true);
                }
            }
        });

        BindNumberObserverToButtonPanel();
    }

    // numberDeque.getLast() 를 Panel 에 최신화해주기만 하면 됨
    public void numBtnClicked(String newNum){
        numberService.handleNumberInput(newNum);
        resultPanel.setBigLabel(numberDeque.getLast());
        // 이게 renderBigLabel 아님??
    }

    // 2. Operation Button
    // 기본적인 사칙연산 + - x /
    // 등호 연산
    public void optBtnClicked(String newOperator){
        operatorService.handleOperatorInput(newOperator);
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

    public void backSpaceBtnClicked(){

        // 새 숫자를 입력할때만 써져야함
        // 이미 들어간 숫자 삭제는 불가능해야함
        // 그래서 숫자가 연산자 개수보다 많을때만 가능
        if(numberDeque.size() > operatorDeque.size()){
            int length = numberDeque.getLast().length();
            if(length > 1){
                String lastNum = numberDeque.removeLast();
                numberDeque.add(lastNum.substring(0, length-1));
            }
            else if(length==1){
                numberDeque.removeLast();
                numberDeque.add("0");
            }
            renderBigLabel();
        }
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
        System.out.println(numberDeque.getLast());
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
        buttonPanel.getNegateButton().addActionListener(observer);
    }
}