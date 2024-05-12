import Controller.EraserEventController;
import Controller.NumberEventController;
import Controller.OperatorEventController;
import Listener.KeyBoardListener;
import Listener.MouseListener;
import View.MainView;

import javax.swing.*;
import java.util.ArrayDeque;

public class Calculator{

    private MainView mainView;

    private KeyBoardListener keyBoardListener;
    private MouseListener mouseListener;

    private NumberEventController numberEventController;
    private OperatorEventController operatorEventController;
    private EraserEventController eraserEventController;

    private ArrayDeque<String> numberDeque;
    private ArrayDeque<String> operatorDeque;

    private void initializeComponents(){

        this.mainView = new MainView();

        // numberDeque 는 default 상태가 0이 들어가 있는 상태
        this.numberDeque = new ArrayDeque<>();
        this.numberDeque.add("0");

        this.operatorDeque = new ArrayDeque<>();

        this.numberEventController = new NumberEventController(numberDeque, operatorDeque, mainView);
        this.operatorEventController = new OperatorEventController(numberDeque, operatorDeque, mainView);
        this.eraserEventController = new EraserEventController(numberDeque, operatorDeque, mainView);

        this.mouseListener = new MouseListener(numberEventController, operatorEventController, eraserEventController);
        this.keyBoardListener = new KeyBoardListener(numberEventController, operatorEventController, eraserEventController);
    }

    // 시작하기 전 해야할 작업 수행
    public Calculator() {
        // Calculator 구성하는 모든 Component 객체 생성
        initializeComponents();

        // 돌아가기 위한 Listener 연결시켜주기
        BindListenersToButtonPanel();
    }

    public void run(){
        // Frame 띄우는게 run 하는 명령임
        mainView.setVisible(true);
    }

    private void BindListenersToButtonPanel(){

        mainView.getButtonPanel().addKeyListener(keyBoardListener);

        // key event 받을 수 있는 조건 = focus 가 주어졌을 경우
        mainView.getButtonPanel().setFocusable(true);
        mainView.getButtonPanel().requestFocus();

        // button 들 mouseListener 달아주기
        JButton[] buttonArr = mainView.getButtonPanel().getButtonArray();
        for(int i=0;i<buttonArr.length;i++){
            buttonArr[i].addActionListener(mouseListener);
        }
    }
}