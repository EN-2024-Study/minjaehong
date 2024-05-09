package Listener;

import Controller.MainController;

import java.awt.event.*;

public class MouseEventListener implements ActionListener {
    MainController mainController;

    // MainController 참조하기 위해 MainController 필요함
    public MouseEventListener(MainController mainController){
        this.mainController = mainController;
    }

    // ButtonPanel 에서 event 가 호출되면
    // mainController 에게 해야할 일을 알려줌
    @Override
    public void actionPerformed(ActionEvent e) {

        String ac = e.getActionCommand();

        if (ac.equals("0") || ac.equals("1") || ac.equals("2") ||
                ac.equals("3") || ac.equals("4") || ac.equals("5") ||
                ac.equals("6") || ac.equals("7") || ac.equals("8") ||
                ac.equals("9") || ac.equals(".") || ac.equals("+/-")) {
            mainController.numBtnClicked(ac);
        }

        if (ac.equals("+") || ac.equals("-") || ac.equals("×") || ac.equals("÷") || ac.equals("=")) {
            mainController.optBtnClicked(ac);
        }

        if (ac.equals("CE") || ac.equals("C") || ac.equals("<")) {
            mainController.eraseBtnClicked(ac);
        }
    }
}
