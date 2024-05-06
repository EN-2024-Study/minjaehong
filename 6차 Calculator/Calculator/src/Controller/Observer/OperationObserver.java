package Controller.Observer;

import Controller.Controller;
import View.MainView;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

// + - x / (+/-) = 이렇게 6개 처리
public class OperationObserver implements ActionListener {
    MainView mainView;
    Controller controller;

    public OperationObserver(MainView mainView, Controller controller){
        this.mainView = mainView;
        this.controller = controller;
    }

    @Override
    public void actionPerformed(ActionEvent e) {

    }
}
