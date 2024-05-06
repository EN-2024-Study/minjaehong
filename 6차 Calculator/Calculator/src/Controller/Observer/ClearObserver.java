package Controller.Observer;

import Controller.Controller;
import View.Panel.ButtonPanel;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

// CE C <- 이렇게 3개 처리
public class ClearObserver implements ActionListener {
    ButtonPanel buttonPanel;
    Controller controller;

    public ClearObserver(ButtonPanel buttonPanel, Controller controller){
        this.buttonPanel = buttonPanel;
        this.controller = controller;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        System.out.println("ClearObserver called");
    }
}
