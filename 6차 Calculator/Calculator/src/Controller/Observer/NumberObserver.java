package Controller.Observer;

import Controller.Controller;
import View.MainView;
import View.Panel.ButtonPanel;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

// 0~9 입력 처리
public class NumberObserver implements ActionListener {
    Controller controller;
    ButtonPanel buttonPanel;

    // ButtonPanel ELEMENTS 참조하기 위해 MainView 필요함
    // Controller 참조하기 위해 Controller 필요함
    public NumberObserver(ButtonPanel buttonPanel, Controller controller){
        this.buttonPanel = buttonPanel;
        this.controller = controller;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        System.out.println("NumberObserver called");
        
        String newNum = "";
        if (e.getSource() == buttonPanel.getNum0Button()) {
            newNum ="0";
        } else if (e.getSource() == buttonPanel.getNum1Button()) {
            newNum = "1";
        } else if (e.getSource() == buttonPanel.getNum2Button()) {
            newNum = "2";
        } else if (e.getSource() == buttonPanel.getNum3Button()) {
            newNum = "3";
        } else if (e.getSource() == buttonPanel.getNum4Button()) {
            newNum = "4";
        } else if (e.getSource() == buttonPanel.getNum5Button()) {
            newNum = "5";
        } else if (e.getSource() == buttonPanel.getNum6Button()) {
            newNum = "6";
        } else if (e.getSource() == buttonPanel.getNum7Button()) {
            newNum = "7";
        } else if (e.getSource() == buttonPanel.getNum8Button()) {
            newNum = "8";
        } else if (e.getSource() == buttonPanel.getNum9Button()) {
            newNum = "9";
        }

        controller.numBtnClicked(newNum);
    }
}
