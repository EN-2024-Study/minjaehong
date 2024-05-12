import Controller.EraserEventController;
import Controller.EventController;
import Controller.NumberEventController;
import Controller.OperatorEventController;
import Listener.KeyBoardListener;
import Listener.MouseListener;
import View.MainView;

import javax.swing.*;
import java.util.ArrayDeque;

public class Calculator{

    private MainView mainView;
    private ArrayDeque<String> numberDeque;
    private ArrayDeque<String> operatorDeque;

    private KeyBoardListener keyBoardListener;
    private MouseListener mouseListener;

    private EventController[] eventControllerArr;

    // 시작하기 전 해야할 작업 수행
    public Calculator() {
        initializeComponents(); // Calculator 구성하는 모든 Component 객체 생성
        BindListenersToButtonPanel(); // 돌아가기 위한 Listener 연결시켜주기
    }

    private void initializeComponents(){

        this.mainView = new MainView();

        // numberDeque 는 default 상태가 0이 들어가 있는 상태
        this.numberDeque = new ArrayDeque<>();
        this.numberDeque.add("0");

        this.operatorDeque = new ArrayDeque<>();

        this.eventControllerArr = new EventController[3];

        this.eventControllerArr[0] = new NumberEventController(numberDeque, operatorDeque, mainView);
        this.eventControllerArr[1] = new OperatorEventController(numberDeque, operatorDeque, mainView);
        this.eventControllerArr[2] = new EraserEventController(numberDeque, operatorDeque, mainView);

        this.mouseListener = new MouseListener(eventControllerArr);
        this.keyBoardListener = new KeyBoardListener(eventControllerArr);
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

    public void run(){
        mainView.setVisible(true);
    }
}