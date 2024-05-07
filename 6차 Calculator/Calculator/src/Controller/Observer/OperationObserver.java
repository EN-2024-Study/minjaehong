package Controller.Observer;

import Controller.Controller;
import View.MainView;
import View.Panel.ButtonPanel;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

// + - x / (+/-) = 이렇게 6개 처리
public class OperationObserver implements ActionListener {
    ButtonPanel buttonPanel;
    Controller controller;

    public OperationObserver(ButtonPanel buttonPanel, Controller controller){
        this.buttonPanel = buttonPanel;
        this.controller = controller;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        System.out.println("OperationObserver called");

        String str="";
        if(e.getSource()==buttonPanel.getAddButton()) {
            controller.optBtnClicked("+");
        }
        if(e.getSource()==buttonPanel.getSubButton()){
            controller.optBtnClicked("-");
        }
        if(e.getSource()==buttonPanel.getMulButton()){
            controller.optBtnClicked("×");
        }
        if(e.getSource()==buttonPanel.getDivButton()){
            controller.optBtnClicked("÷");
        }
        if(e.getSource()==buttonPanel.getSignButton()){
            controller.optBtnClicked("+/-");
        }
    }
}
