package Controller;

import Controller.Observer.*;
import View.MainView;
import View.Panel.ButtonPanel;
import View.Panel.LogPanel;
import View.Panel.ResultPanel;

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
    }
}
