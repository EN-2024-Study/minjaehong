package Controller.Observer;

import Controller.Controller;
import View.Panel.ButtonPanel;
import View.Panel.ResultPanel;

import javax.xml.transform.Result;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

// CE C <- 이렇게 3개 처리
public class ClearObserver implements ActionListener {
    ButtonPanel buttonPanel;
    ResultPanel resultPanel;
    Controller controller;

    public ClearObserver(ButtonPanel buttonPanel, Controller controller){
        this.buttonPanel = buttonPanel;
        this.controller = controller;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if(e.getSource()==buttonPanel.getClearEntryButton()){
            controller.clearBtnClicked();;
        }
        if(e.getSource()==buttonPanel.getClearButton()){
            controller.clearBtnClicked();
        }
    }
}
