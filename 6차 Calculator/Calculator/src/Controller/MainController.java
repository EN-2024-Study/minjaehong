package Controller;

import Listener.ButtonEventListener;
import Listener.MouseEventListener;
import View.MainView;
import View.Panel.ButtonPanel;
import View.Panel.ResultPanel;

import javax.swing.*;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;
import java.util.ArrayDeque;

public class MainController {

    ButtonPanel buttonPanel;
    ResultPanel resultPanel;
    JLabel smallLabel;
    JLabel bigLabel;

    MouseEventListener mouseEventListener;
    ButtonEventListener buttonObserver;

    NumberEventController numberEventController;
    OperatorEventController operatorEventController;
    EraserEventController eraserEventController;

    ArrayDeque<String> numberDeque;
    ArrayDeque<String> operatorDeque;

    private void initializeController(){

        // numberDeque 는 default 상태가 0이 들어가 있는 상태
        numberDeque = new ArrayDeque<>();
        numberDeque.add("0");
        operatorDeque = new ArrayDeque<>();

        // eventController 자기 자신한테 해야할 일을 전달할 수 있게끔 자기 자신을 인자로 주기
        mouseEventListener = new MouseEventListener(this);
        buttonObserver = new ButtonEventListener(this);

        numberEventController = new NumberEventController(numberDeque, operatorDeque, resultPanel);
        operatorEventController = new OperatorEventController(numberDeque, operatorDeque, resultPanel);
        eraserEventController = new EraserEventController(numberDeque, operatorDeque, resultPanel);
    }

    public MainController(MainView mainView){

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
        numberEventController.handleNumberInput(newNum);
        if(newNum.equals("+/-")) return;
        resultPanel.setBigLabel(numberDeque.getLast()); // 이게 renderBigLabel 아님??
    }

    public void optBtnClicked(String newOperator){
        operatorEventController.handleOperatorInput(newOperator);
    }

    public void eraseBtnClicked(String input){
        eraserEventController.handleEraseEvent(input);
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

        buttonPanel.getNum0Button().addActionListener(mouseEventListener);
        buttonPanel.getNum1Button().addActionListener(mouseEventListener);
        buttonPanel.getNum2Button().addActionListener(mouseEventListener);
        buttonPanel.getNum3Button().addActionListener(mouseEventListener);
        buttonPanel.getNum4Button().addActionListener(mouseEventListener);
        buttonPanel.getNum5Button().addActionListener(mouseEventListener);
        buttonPanel.getNum6Button().addActionListener(mouseEventListener);
        buttonPanel.getNum7Button().addActionListener(mouseEventListener);
        buttonPanel.getNum8Button().addActionListener(mouseEventListener);
        buttonPanel.getNum9Button().addActionListener(mouseEventListener);
        buttonPanel.getDotButton().addActionListener(mouseEventListener);

        buttonPanel.getClearEntryButton().addActionListener(mouseEventListener);
        buttonPanel.getClearButton().addActionListener(mouseEventListener);
        buttonPanel.getBackSpaceButton().addActionListener(mouseEventListener);

        buttonPanel.getAddButton().addActionListener(mouseEventListener);
        buttonPanel.getSubButton().addActionListener(mouseEventListener);
        buttonPanel.getMulButton().addActionListener(mouseEventListener);
        buttonPanel.getDivButton().addActionListener(mouseEventListener);
        buttonPanel.getEqualButton().addActionListener(mouseEventListener);
        buttonPanel.getNegateButton().addActionListener(mouseEventListener);
    }
}