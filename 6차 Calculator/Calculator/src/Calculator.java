import Controller.*;
import Listener.KeyBoardListener;
import Listener.ButtonListener;
import View.Frame.MainView;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.util.ArrayDeque;

public class Calculator {

    private MainView mainView;

    private ArrayDeque<String> numberDeque;
    private ArrayDeque<String> operatorDeque;

    // system 전반적인 keyboard 입력 처리
    private KeyBoardListener keyBoardListener;
    // system 전반적인 button 클릭 처리
    private ButtonListener buttonListener;

    // system 에 쓰이는 모든 event controller 의 자식클래스를 담는 배열
    private EventController[] eventControllerArr;

    // 시작하기 전 해야할 작업 수행
    public Calculator() {
        initializeComponents(); // Calculator 구성하는 모든 Component 객체 생성
        BindListenersToButtonPanel(); // 돌아가기 위한 Listener 연결시켜주기
    }

    private void initializeComponents() {

        this.mainView = new MainView();

        // numberDeque 는 default 상태가 0이 들어가 있는 상태
        this.numberDeque = new ArrayDeque<>();
        this.numberDeque.add("0");

        this.operatorDeque = new ArrayDeque<>();

        this.eventControllerArr = new EventController[4];

        this.eventControllerArr[0] = new NumberEventController(numberDeque, operatorDeque, mainView);
        this.eventControllerArr[1] = new OperatorEventController(numberDeque, operatorDeque, mainView);
        this.eventControllerArr[2] = new EraserEventController(numberDeque, operatorDeque, mainView);
        this.eventControllerArr[3] = new LogEventController(numberDeque, operatorDeque, mainView);

        this.buttonListener = new ButtonListener(eventControllerArr);
        this.keyBoardListener = new KeyBoardListener(eventControllerArr);
    }

    private void BindListenersToButtonPanel() {

        mainView.getButtonPanel().addKeyListener(keyBoardListener);

        // key event 받을 수 있는 조건 = focus 가 주어졌을 경우
        mainView.getButtonPanel().setFocusable(true);
        mainView.getButtonPanel().requestFocus();

        // button 들 buttonListener 달아주기
        JButton[] buttonArr = mainView.getButtonPanel().getButtonArray();
        for (int i = 0; i < buttonArr.length; i++) {
            buttonArr[i].addActionListener(buttonListener);
        }

        JPanel logLabelPanel = mainView.getLogPanel().getLabelPanel();
        logLabelPanel.addContainerListener(new ContainerAdapter() {
            @Override
            public void componentAdded(ContainerEvent e) {
                    Component component = logLabelPanel.getComponent(0);
                    if (component instanceof JButton) {
                        JButton newButton = (JButton) component;
                        newButton.addActionListener(buttonListener);
                    }
                }
        });
    }

    // Calculator 진짜로 실행하기
    public void run() {
        mainView.setVisible(true);
    }
}