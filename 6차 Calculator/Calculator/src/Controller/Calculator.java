package Controller;

import Listener.ButtonEventListener;
import Listener.MouseEventListener;
import View.MainView;
import View.Panel.ButtonPanel;
import View.Panel.ResultPanel;

import javax.swing.*;
import java.util.ArrayDeque;

public class Calculator {

    private MainView mainView;

    private MouseEventListener mouseEventListener;
    private ButtonEventListener buttonObserver;

    private NumberEventController numberEventController;
    private OperatorEventController operatorEventController;
    private EraserEventController eraserEventController;

    private ArrayDeque<String> numberDeque;
    private ArrayDeque<String> operatorDeque;

    private void initializeController(){

        // numberDeque 는 default 상태가 0이 들어가 있는 상태
        this.numberDeque = new ArrayDeque<>();
        this.numberDeque.add("0");

        this.operatorDeque = new ArrayDeque<>();

        // eventController 자기 자신한테 해야할 일을 전달할 수 있게끔 자기 자신을 인자로 주기
        mouseEventListener = new MouseEventListener(this);
        buttonObserver = new ButtonEventListener(this);

        numberEventController = new NumberEventController(numberDeque, operatorDeque, mainView);
        operatorEventController = new OperatorEventController(numberDeque, operatorDeque, mainView);
        eraserEventController = new EraserEventController(numberDeque, operatorDeque, mainView);
    }

    public Calculator(){

        mainView = new MainView();
        mainView.setVisible(true);

        initializeController();

        BindObserversToButtonPanel();
    }

    /*
    public void handleInput(String newInput){
        if()
    }
    */

    public void numberButtonClicked(String newNum){
        numberEventController.handleNumberInput(newNum);
    }

    public void operatorButtonClicked(String newOperator){
        operatorEventController.handleOperatorInput(newOperator);
    }

    public void eraseBtnClicked(String input){
        eraserEventController.handleEraseEvent(input);
    }

    private void BindObserversToButtonPanel(){

        mainView.getButtonPanel().addKeyListener(buttonObserver);
        // key event 받을 수 있는 조건 = focus 가 주어졌을 경우
        mainView.getButtonPanel().setFocusable(true);
        mainView.getButtonPanel().requestFocus();

        mainView.getButtonPanel().getNum0Button().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getNum1Button().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getNum2Button().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getNum3Button().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getNum4Button().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getNum5Button().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getNum6Button().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getNum7Button().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getNum8Button().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getNum9Button().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getDotButton().addActionListener(mouseEventListener);

        mainView.getButtonPanel().getClearEntryButton().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getClearButton().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getBackSpaceButton().addActionListener(mouseEventListener);

        mainView.getButtonPanel().getAddButton().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getSubButton().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getMulButton().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getDivButton().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getEqualButton().addActionListener(mouseEventListener);
        mainView.getButtonPanel().getNegateButton().addActionListener(mouseEventListener);
    }
}