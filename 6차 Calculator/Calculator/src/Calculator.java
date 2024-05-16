import Controller.*;
import Listener.KeyBoardListener;
import Listener.ButtonListener;
import View.Frame.MainFrame;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.util.ArrayDeque;

public class Calculator {

    private MainFrame mainFrame;

    private ArrayDeque<String> numberDeque;
    private ArrayDeque<String> operatorDeque;

    private KeyBoardListener keyBoardListener; // system 전반적인 keyboard 입력 처리
    private ButtonListener buttonListener; // system 전반적인 button 클릭 처리

    // system 에 쓰이는 모든 event controller 의 자식클래스를 담는 배열
    private EventController[] eventControllerArr;

    // 시작하기 전 해야할 작업 수행
    public Calculator() {
        initializeComponents(); // Calculator 구성하는 모든 Component 객체 생성
        bindDefaultListenersToComponents(); // 기본적인 Listener 연결시켜주기
        bindRuntimeListenersToComponents(); // runtime 시 동적으로 작동하는 Listener 연결시켜주기
    }

    private void initializeComponents() {

        this.mainFrame = new MainFrame();

        // numberDeque 는 default 상태가 0이 들어가 있는 상태
        this.numberDeque = new ArrayDeque<>();
        this.numberDeque.add("0");

        this.operatorDeque = new ArrayDeque<>();

        this.eventControllerArr = new EventController[4];
        this.eventControllerArr[0] = new NumberEventController(numberDeque, operatorDeque, mainFrame);
        this.eventControllerArr[1] = new OperatorEventController(numberDeque, operatorDeque, mainFrame);
        this.eventControllerArr[2] = new EraserEventController(numberDeque, operatorDeque, mainFrame);
        this.eventControllerArr[3] = new LogEventController(numberDeque, operatorDeque, mainFrame);

        this.buttonListener = new ButtonListener(eventControllerArr);
        this.keyBoardListener = new KeyBoardListener(eventControllerArr);
    }

    private void bindDefaultListenersToComponents() {

        // 1. ButtonPanel 에 keyBoardListener 달기
        mainFrame.addKeyListener(keyBoardListener);
        mainFrame.setFocusable(true);
        mainFrame.requestFocus();
        
        // 2. ButtonPanel 에 ButtonListener 달아주기
        JButton[] buttonArr = mainFrame.getButtonPanel().getButtonArray();
        for (int i = 0; i < buttonArr.length; i++) buttonArr[i].addActionListener(buttonListener);

        // 3. showLogButton 에 buttonListener 달아주기
        mainFrame.getResultPanel().getShowLogButton().addActionListener(buttonListener);
    }

    private void bindRuntimeListenersToComponents(){

        GridBagConstraints gbc = new GridBagConstraints();

        // 1. mainFrame 자체에 runtime 시 사이즈조절 반응형 달아주기
        mainFrame.addComponentListener(new ComponentAdapter() {
            @Override
            public void componentResized(ComponentEvent e) {

                // 만약 로그창 떠있었으면 다시 제자리에 갖다 붙여주기
                if(mainFrame.getButtonPanel().isVisible()==false) putLogPanelBackToDefaultLocation();

                int width = mainFrame.getWidth();
                if (width < 550) {
                    mainFrame.getLogPanel().setVisible(false);
                    mainFrame.getResultPanel().getShowLogButton().setVisible(true);
                }else{
                    mainFrame.getLogPanel().setVisible(true);
                    mainFrame.getResultPanel().getShowLogButton().setVisible(false);
                }
            }
        });

        // 2. logLabelPanel에 runtime 시 log 추가 삭제에 따른 반응형 listener 달아주기
        JPanel logLabelPanel = mainFrame.getLogPanel().getLabelPanel();
        JButton trashCanButton = mainFrame.getLogPanel().getTrashCanButton();
        logLabelPanel.addContainerListener(new ContainerAdapter() {
            @Override
            public void componentAdded(ContainerEvent e) {

                // 첫번째 log 가 추가되어 총 logLabel 개수가 2개가 되면
                // no logs label 제거하고 trashCanButton visible 해놓기
                if(logLabelPanel.getComponentCount()>0){
                    trashCanButton.setVisible(true);
                }

                // 20개 초과 누적되었으면 맨 첫번째꺼 지워주기
                if(logLabelPanel.getComponentCount()>20){
                    logLabelPanel.remove(20);
                }

                // 새로운 label에 기존 buttonListener 달아주기
                Component component = logLabelPanel.getComponent(0);
                if (component instanceof JButton) {
                    JButton newButton = (JButton) component;
                    newButton.addActionListener(buttonListener);
                }
            }

            @Override
            public void componentRemoved(ContainerEvent e) {
                if(logLabelPanel.getComponentCount()==0){
                    trashCanButton.setVisible(false);
                }
            }
        });

        // 3. runtime 시 resultPanel 클릭에 따른 반응형 listener 달아주기
        mainFrame.getResultPanel().addMouseListener(new MouseAdapter() {
            @Override
            public void mouseClicked(MouseEvent e) {
                if(mainFrame.getButtonPanel().isVisible()==false){
                    putLogPanelBackToDefaultLocation();
                }
            }
        });
    }

    // LogPanel 제자리에 갖다 붙여줌
    private void putLogPanelBackToDefaultLocation(){
        GridBagConstraints gbc = new GridBagConstraints();

        gbc.gridx = 1;
        gbc.gridy = 0;
        gbc.gridwidth = 1;
        gbc.gridheight = 2;
        gbc.weightx = 1.0;
        gbc.weighty = 1.0;
        gbc.fill = GridBagConstraints.BOTH;
        mainFrame.add(mainFrame.getLogPanel(),gbc);

        mainFrame.getResultPanel().setBackground(Color.WHITE);
        mainFrame.getLogPanel().setVisible(false);
        mainFrame.getButtonPanel().setVisible(true);
        mainFrame.getResultPanel().getShowLogButton().setEnabled(true);
    }

    // Calculator 진짜로 실행하기
    public void run() {
        mainFrame.setVisible(true);
    }
}