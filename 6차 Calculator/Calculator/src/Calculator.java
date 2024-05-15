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
        BindListenersToComponents(); // 돌아가기 위한 Listener 연결시켜주기
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

    private void BindListenersToComponents() {

        // 1. ButtonPanel 에 keyBoardListener 달기
        mainView.getButtonPanel().addKeyListener(keyBoardListener);
        mainView.getButtonPanel().setFocusable(true);
        mainView.getButtonPanel().requestFocus();

        // 2. MainView 자체에 사이즈조절 및 showLogPanel 반응형 달기
        mainView.addComponentListener(new ComponentAdapter() {
            @Override
            public void componentResized(ComponentEvent e) {

                if(mainView.getButtonPanel().isVisible()==false){
                    GridBagConstraints gbc = new GridBagConstraints();

                    gbc.gridx = 1;
                    gbc.gridy = 0;
                    gbc.gridwidth = 1;
                    gbc.gridheight = 2;
                    gbc.weightx = 1.0;
                    gbc.weighty = 1.0;
                    gbc.fill = GridBagConstraints.BOTH;
                    mainView.add(mainView.getLogPanel(),gbc);

                    mainView.getResultPanel().setBackground(Color.WHITE);
                    mainView.getLogPanel().setVisible(false);
                    mainView.getButtonPanel().setVisible(true);
                    mainView.getResultPanel().getShowLogButton().setEnabled(true);
                }

                int width = mainView.getWidth();
                if (width < 500) {
                    mainView.getLogPanel().setVisible(false);
                    mainView.getResultPanel().getShowLogButton().setVisible(true);
                }else{
                    mainView.getLogPanel().setVisible(true);
                    mainView.getResultPanel().getShowLogButton().setVisible(false);
                }
            }
        });

        // 3. ButtonPanel 에 ButtonListener 달아주기
        JButton[] buttonArr = mainView.getButtonPanel().getButtonArray();
        for (int i = 0; i < buttonArr.length; i++) {
            buttonArr[i].addActionListener(buttonListener);
        }

        // 4. showLogButton 에 buttonListener 달아주기
        JButton showLogButton = mainView.getResultPanel().getShowLogButton();
        showLogButton.addActionListener(buttonListener);

        // 5. 새로운 logLabel 들 생길때마다 기존 buttonListener 달아주기
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

        // 6. resultPanel 에 log 보여질때 반응형 달기
        JPanel resultPanel = mainView.getResultPanel();
        resultPanel.addMouseListener(new MouseAdapter() {
            GridBagConstraints gbc = new GridBagConstraints();

            @Override
            public void mouseClicked(MouseEvent e) {
                if(!mainView.getButtonPanel().isVisible()){
                    gbc.gridx = 1;
                    gbc.gridy = 0;
                    gbc.gridwidth = 1;
                    gbc.gridheight = 2;
                    gbc.weightx = 1.0;
                    gbc.weighty = 1.0;
                    gbc.fill = GridBagConstraints.BOTH;
                    mainView.add(mainView.getLogPanel(),gbc);

                    mainView.getResultPanel().setBackground(Color.WHITE);
                    mainView.getLogPanel().setVisible(false);
                    mainView.getButtonPanel().setVisible(true);
                    mainView.getResultPanel().getShowLogButton().setEnabled(true);
                }
            }
        });

        JButton trashCanButton = mainView.getLogPanel().getTrashCanButton();
        logLabelPanel.addContainerListener(new ContainerListener() {
            @Override
            public void componentAdded(ContainerEvent e) {
                if(logLabelPanel.getComponentCount()>0){
                    trashCanButton.setVisible(true);
                }

                // 20개 이상 누적되었으면
                // 맨 첫번째꺼 지워주기
                if(logLabelPanel.getComponentCount()>20){
                    logLabelPanel.remove(20);
                }
            }

            @Override
            public void componentRemoved(ContainerEvent e) {
                if(logLabelPanel.getComponentCount()==0){
                    trashCanButton.setVisible(false);
                }
            }
        });
    }

    // Calculator 진짜로 실행하기
    public void run() {
        mainView.setVisible(true);
    }
}