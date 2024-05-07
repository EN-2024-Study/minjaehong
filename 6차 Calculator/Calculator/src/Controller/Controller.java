package Controller;

import Controller.Observer.*;
import View.MainView;
import View.Panel.ButtonPanel;
import View.Panel.LogPanel;
import View.Panel.ResultPanel;

import javax.swing.*;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;

public class Controller {
    ButtonPanel buttonPanel;
    ResultPanel resultPanel;

    NumberObserver numberObserver;
    ClearObserver clearObserver;
    OperationObserver operationObserver;

    JLabel smallLabel;
    JLabel bigLabel;

    Double finalResult;
    boolean isOperations;

    public Controller(MainView mainView){
        finalResult = (double)0;
        isOperations = false;

        buttonPanel = mainView.getButtonPanel();
        resultPanel = mainView.getResultPanel();

        smallLabel= resultPanel.getSmallLabel();
        bigLabel = resultPanel.getBigLabel();

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

    // 1. Number Button
    public void numBtnClicked(String newNum){

        if(isOperations || (bigLabel.getText()=="0")){
            bigLabel.setText("");
        }

        isOperations = false;

        System.out.println(bigLabel.getText()+newNum);

        // VIEW 최신화
        resultPanel.setBigLabel(bigLabel.getText()+newNum);
    }

    // 2. Operation Button
    public void optBtnClicked(String curOperator){
        // 연산자는 무조건 STACK 에 PUSH 됨

        System.out.println(bigLabel.getText());

        // 마지막 연산이 0이 아니었으면
        if(finalResult!=0) {
            // 계산한거 받아주고
            Double result = calculate(curOperator);
            bigLabel.setText(result+"");
            finalResult = result;
            smallLabel.setText("");
            bigLabel.setText(result+"");
        }

        finalResult = Double.parseDouble(bigLabel.getText());
        smallLabel.setText(finalResult+" "+curOperator);
        isOperations = true;

        // 연산자는 무조건 inputLabel 결과로 최신화됨
        //resultPanel.setCurInputLabel(Integer.toString(result));
        // 연산자는 무조건 equationLabel 최신화됨
        //resultPanel.setCurEquationLabel();
    }

    // 값 진짜로 계산하기
    public double calculate(String opt){
        double result = (double)0;
        switch(opt){
            case "+":
                result = finalResult + Double.parseDouble(bigLabel.getText());
                break;
            case "-":
                result = finalResult - Double.parseDouble(bigLabel.getText());
                break;
            case "×":
                result = finalResult * Double.parseDouble(bigLabel.getText());
                break;
            case"÷":
                result = finalResult / Double.parseDouble(bigLabel.getText());
                break;
        }
        return result;
    }

    // bigLabel 0으로 만듬
    public void clearBtnClicked(){
        bigLabel.setText("0");
    }

    // smallLabel bigLabel 모두 0으로 만듬
    public void clearAllBtnClicked(){
        bigLabel.setText("0");
        smallLabel.setText("");
        finalResult = 0.0;
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
