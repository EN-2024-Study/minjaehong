package Listener;

import Controller.EventController;

import java.awt.event.*;

public class MouseListener implements ActionListener{
    private EventController numberEventController;
    private EventController operatorEventController;
    private EventController erasorEventController;

    // Calculator 참조하기 위해 Calculator 필요함
    public MouseListener(EventController numberEventController, EventController operatorEventController, EventController erasorEventController){
        this.numberEventController = numberEventController;
        this.operatorEventController = operatorEventController;
        this.erasorEventController = erasorEventController;
    }

    @Override
    public void actionPerformed(ActionEvent e) {

        String ac = e.getActionCommand();

        if (ac.equals("0") || ac.equals("1") || ac.equals("2") ||
                ac.equals("3") || ac.equals("4") || ac.equals("5") ||
                ac.equals("6") || ac.equals("7") || ac.equals("8") ||
                ac.equals("9") || ac.equals(".") || ac.equals("+/-")) {
            numberEventController.handleEvent(ac);
        }

        if (ac.equals("+") || ac.equals("-") || ac.equals("×") || ac.equals("÷") || ac.equals("=")) {
            operatorEventController.handleEvent(ac);
        }

        if (ac.equals("CE") || ac.equals("C") || ac.equals("<")) {
            erasorEventController.handleEvent(ac);
        }
    }
}
