package Controller;

import Controller.Controller;
import View.MainView;
import View.Panel.ButtonPanel;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

// 0~9 입력 처리
public class Observer implements ActionListener {
    Controller controller;
    ButtonPanel buttonPanel;

    // ButtonPanel ELEMENTS 참조하기 위해 MainView 필요함
    // Controller 참조하기 위해 Controller 필요함
    public Observer(ButtonPanel buttonPanel, Controller controller){
        this.buttonPanel = buttonPanel;
        this.controller = controller;
    }

    @Override
    public void actionPerformed(ActionEvent e) {

        String ac = e.getActionCommand();

        if (ac.equals("0") || ac.equals("1") || ac.equals("2") ||
                ac.equals("3") || ac.equals("4") || ac.equals("5") ||
                ac.equals("6") || ac.equals("7") || ac.equals("8") ||
                ac.equals("9") || ac.equals(".") || ac.equals("+/-")) {
            controller.numBtnClicked(ac);
        }

        if (ac.equals("+") || ac.equals("-") || ac.equals("×") || ac.equals("÷")) {
            controller.optBtnClicked(ac);
        }

        if (ac.equals("CE")) {
            controller.clearEntryBtnClicked();
        }

        if (ac.equals("C")) {
            controller.clearBtnClicked();
        }
    }
}
