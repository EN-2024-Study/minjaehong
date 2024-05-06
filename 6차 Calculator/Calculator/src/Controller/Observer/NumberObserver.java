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

        String str="";

        if (e.getSource() == buttonPanel.getNum0Button()) {
            str ="0";
        } else if (e.getSource() == buttonPanel.getNum1Button()) {
            str = "1";
        } else if (e.getSource() == buttonPanel.getNum2Button()) {
            str = "2";
        } else if (e.getSource() == buttonPanel.getNum3Button()) {
            str = "3";
        } else if (e.getSource() == buttonPanel.getNum4Button()) {
            str = "4";
        } else if (e.getSource() == buttonPanel.getNum5Button()) {
            str = "5";
        } else if (e.getSource() == buttonPanel.getNum6Button()) {
            str = "6";
        } else if (e.getSource() == buttonPanel.getNum7Button()) {
            str = "7";
        } else if (e.getSource() == buttonPanel.getNum8Button()) {
            str = "8";
        } else if (e.getSource() == buttonPanel.getNum9Button()) {
            str = "9";
        }

        //controller.addToCurNumberText(str);
    }
}
