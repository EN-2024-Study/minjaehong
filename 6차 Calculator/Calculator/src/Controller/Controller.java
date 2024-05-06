package Controller;

import Controller.Observer.*;
import View.MainView;
import View.Panel.ButtonPanel;
import View.Panel.LogPanel;
import View.Panel.ResultPanel;

import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;

// Controller 는 3개의 Observer 로부터 해야할 일을 명령받음
// 1. ClearObserver에게 연락이 왔을때
// CE

// 2. NumberObserver에게서 연락이 왔을때
// 그냥 숫자 더 추가함 -> curNumberLabel에 숫자 계속 추가
// 숫자추가되면서 따옴표 표시
// 숫자추가되면서 길어지면 숫자 크기 작아짐
// 건드릴 곳이 ResultPanel의 curNumberLabel 밖에 없음

// 3. OperationObserver에게서 연락이 왔을때
// +이면 -> 수식이 완성안된 경우에는 그냥 curEquation의 뒷자리를 +로 바꿈
// -이면 -> 수식이 완성안된 경우에는 그냥 curEquation의 뒷자리를 -로 바꿈
// x이면 -> 수식이 완성안된 경우에는 그냥 curEquation의 뒷자리를 x로 바꿈
// /이면 -> 수식이 완성안된 경우에는 그냥 curEquation의 뒷자리를 /로 바꿈


public class Controller {
    private String curNumberText;
    private String curEquationText;

    ButtonPanel buttonPanel;
    LogPanel logPanel;
    ResultPanel resultPanel;

    NumberObserver numberObserver;
    ClearObserver clearObserver;
    OperationObserver operationObserver;

    public Controller(MainView mainView){
        curNumberText="";
        curEquationText="";

        buttonPanel = mainView.getButtonPanel();

        numberObserver = new NumberObserver(buttonPanel, this);
        clearObserver = new ClearObserver(buttonPanel, this);
        operationObserver = new OperationObserver(buttonPanel, this);

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

    public void addToCurNumberText(String s){
        curNumberText+=s;
        System.out.println(curNumberText);
    }

    public void addToCurEquationText(String s){
        curEquationText+=s;
    }

    private void BindNumberObserverToButtonPanel(){
        // 숫자 버튼한테 numberObserver 바인딩
        buttonPanel.getNum0Button().addActionListener(numberObserver);
        buttonPanel.getNum1Button().addActionListener(numberObserver);
        buttonPanel.getNum2Button().addActionListener(numberObserver);
        buttonPanel.getNum3Button().addActionListener(numberObserver);
        buttonPanel.getNum4Button().addActionListener(numberObserver);
        buttonPanel.getNum5Button().addActionListener(numberObserver);
        buttonPanel.getNum6Button().addActionListener(numberObserver);
        buttonPanel.getNum7Button().addActionListener(numberObserver);
        buttonPanel.getNum8Button().addActionListener(numberObserver);
        buttonPanel.getNum9Button().addActionListener(numberObserver);

        // CE C BACKSPACE 한테 clearObserver 바인딩
        buttonPanel.getClearButton().addActionListener(clearObserver);
        buttonPanel.getClearAllButton().addActionListener(clearObserver);
        buttonPanel.getBackSpaceButton().addActionListener(clearObserver);

        // + - +/- = * / 한테 operationObserver 바인딩
        buttonPanel.getAddButton().addActionListener(operationObserver);
        buttonPanel.getSubButton().addActionListener(operationObserver);
        buttonPanel.getMulButton().addActionListener(operationObserver);
        buttonPanel.getDivButton().addActionListener(operationObserver);
        buttonPanel.getEqualButton().addActionListener(operationObserver);

        buttonPanel.getDotButton().addActionListener(operationObserver);
        buttonPanel.getSignButton().addActionListener(operationObserver);
    }
}
