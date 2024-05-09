package Controller;

import Service.NumberService;
import Service.OperatorService;
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

    MouseObserver mouseObserver;
    ButtonObserver buttonObserver;

    NumberService numberService;
    OperatorService operatorService;

    JLabel smallLabel;
    JLabel bigLabel;

    ArrayDeque<String> numberDeque;
    ArrayDeque<String> operatorDeque;

    private void initializeController(){

        // numberDeque 는 default 상태가 0이 들어가 있는 상태
        numberDeque = new ArrayDeque<>();
        numberDeque.add("0");
        operatorDeque = new ArrayDeque<>();

        // controller 자기 자신한테 해야할 일을 전달할 수 있게끔 자기 자신을 인자로 주기
        mouseObserver = new MouseObserver(this);
        buttonObserver = new ButtonObserver(this);

        numberService = new NumberService(numberDeque, operatorDeque, resultPanel);
        operatorService = new OperatorService(numberDeque, operatorDeque, resultPanel);
    }

    public Controller(MainView mainView){

        buttonPanel = mainView.getButtonPanel();
        resultPanel = mainView.getResultPanel();

        smallLabel= resultPanel.getSmallLabel();
        bigLabel = resultPanel.getBigLabel();

        initializeController();

        // LogPanel visible 처리
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

        BindObserversToButtonPanel();
    }

    public void numBtnClicked(String newNum){
        numberService.handleNumberInput(newNum);
        if(newNum=="+/-") return;
        resultPanel.setBigLabel(numberDeque.getLast()); // 이게 renderBigLabel 아님??
    }

    public void optBtnClicked(String newOperator){
        operatorService.handleOperatorInput(newOperator);
    }

    // bigLabel 0으로 만듬
    public void clearEntryBtnClicked(){
        // 맨 마지막에 추가한거 빼주기
        numberDeque.removeLast();
        numberDeque.add("0");
        renderBigLabel();
    }

    // smallLabel bigLabel 모두 0으로 만듬
    public void clearBtnClicked(){
        numberDeque.clear();
        operatorDeque.clear();
        renderSmallLabel();
        numberDeque.add("0");
        renderBigLabel();
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
        bigLabel.setText(numberDeque.getLast());
    }

    private void BindObserversToButtonPanel(){
        buttonPanel.addKeyListener(buttonObserver);
        // key event 받을 수 있는 조건 = focus 가 주어졌을 경우
        buttonPanel.setFocusable(true);
        buttonPanel.requestFocus();

        buttonPanel.getNum0Button().addActionListener(mouseObserver);
        buttonPanel.getNum1Button().addActionListener(mouseObserver);
        buttonPanel.getNum2Button().addActionListener(mouseObserver);
        buttonPanel.getNum3Button().addActionListener(mouseObserver);
        buttonPanel.getNum4Button().addActionListener(mouseObserver);
        buttonPanel.getNum5Button().addActionListener(mouseObserver);
        buttonPanel.getNum6Button().addActionListener(mouseObserver);
        buttonPanel.getNum7Button().addActionListener(mouseObserver);
        buttonPanel.getNum8Button().addActionListener(mouseObserver);
        buttonPanel.getNum9Button().addActionListener(mouseObserver);
        buttonPanel.getDotButton().addActionListener(mouseObserver);

        buttonPanel.getClearEntryButton().addActionListener(mouseObserver);
        buttonPanel.getClearButton().addActionListener(mouseObserver);
        buttonPanel.getBackSpaceButton().addActionListener(mouseObserver);

        buttonPanel.getAddButton().addActionListener(mouseObserver);
        buttonPanel.getSubButton().addActionListener(mouseObserver);
        buttonPanel.getMulButton().addActionListener(mouseObserver);
        buttonPanel.getDivButton().addActionListener(mouseObserver);
        buttonPanel.getEqualButton().addActionListener(mouseObserver);
        buttonPanel.getNegateButton().addActionListener(mouseObserver);
    }
}