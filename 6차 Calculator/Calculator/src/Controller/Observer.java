package Controller;

import View.Panel.ButtonPanel;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class Observer implements ActionListener {
    Controller controller;

    // Controller 참조하기 위해 Controller 필요함
    public Observer(Controller controller){
        this.controller = controller;
    }

    // ButtonPanel 에서 event 가 호출되면
    // controller 에게 해야할 일을 알려줌
    @Override
    public void actionPerformed(ActionEvent e) {

        String ac = e.getActionCommand();

        if (ac.equals("0") || ac.equals("1") || ac.equals("2") ||
                ac.equals("3") || ac.equals("4") || ac.equals("5") ||
                ac.equals("6") || ac.equals("7") || ac.equals("8") ||
                ac.equals("9") || ac.equals(".") || ac.equals("+/-")) {
            controller.numBtnClicked(ac);
        }

        if (ac.equals("+") || ac.equals("-") || ac.equals("×") || ac.equals("÷") || ac.equals("=")) {
            controller.optBtnClicked(ac);
        }

        if (ac.equals("CE")) {
            controller.clearEntryBtnClicked();
        }

        if (ac.equals("C")) {
            controller.clearBtnClicked();
        }

        if(ac.equals("<")) {
            
        }
    }
}
