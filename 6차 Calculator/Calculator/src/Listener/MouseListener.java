package Listener;

import Controller.Calculator;

import java.awt.event.*;

public class MouseListener implements ActionListener{
    Calculator calculator;

    // Controller.Calculator 참조하기 위해 Controller.Calculator 필요함
    public MouseListener(Calculator calculator){
        this.calculator = calculator;
    }

    // ButtonPanel 에서 event 가 호출되면
    // calculator 에게 해야할 일을 알려줌
    @Override
    public void actionPerformed(ActionEvent e) {

        String ac = e.getActionCommand();

        if (ac.equals("0") || ac.equals("1") || ac.equals("2") ||
                ac.equals("3") || ac.equals("4") || ac.equals("5") ||
                ac.equals("6") || ac.equals("7") || ac.equals("8") ||
                ac.equals("9") || ac.equals(".") || ac.equals("+/-")) {
            calculator.numberButtonClicked(ac);
        }

        if (ac.equals("+") || ac.equals("-") || ac.equals("×") || ac.equals("÷") || ac.equals("=")) {
            calculator.operatorButtonClicked(ac);
        }

        if (ac.equals("CE") || ac.equals("C") || ac.equals("<")) {
            calculator.eraseBtnClicked(ac);
        }
    }
}
