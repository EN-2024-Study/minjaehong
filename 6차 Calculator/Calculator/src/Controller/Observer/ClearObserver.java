package Controller.Observer;

import View.MainView;
import Controller.Controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

// CE C <- 이렇게 3개 처리
public class ClearObserver implements ActionListener {
    MainView mainView;
    Controller controller;

    public ClearObserver(MainView mainView, Controller controller){
        this.mainView = mainView;
        this.controller = controller;
    }

    @Override
    public void actionPerformed(ActionEvent e) {

    }
}
