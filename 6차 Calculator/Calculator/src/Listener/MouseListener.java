package Listener;

import Controller.EventController;

import java.awt.event.*;

public class MouseListener implements ActionListener{
    private EventController numberEventController;
    private EventController operatorEventController;
    private EventController erasorEventController;

    public MouseListener(EventController[] eventControllers) {
        this.numberEventController = eventControllers[0];
        this.operatorEventController = eventControllers[1];
        this.erasorEventController = eventControllers[2];
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
