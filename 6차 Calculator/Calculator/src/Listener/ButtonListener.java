package Listener;

import Controller.EventController;

import java.awt.event.*;

// system 전반적인 button click event 를 얘가 모두 받음
// 받아서 actionCommand 까보고 그에 알맞는 eventController 로 전달해줌
public class ButtonListener implements ActionListener{

    private EventController numberEventController;
    private EventController operatorEventController;
    private EventController erasorEventController;
    private EventController logEventController;

    public ButtonListener(EventController[] eventControllers) {
        this.numberEventController = eventControllers[0];
        this.operatorEventController = eventControllers[1];
        this.erasorEventController = eventControllers[2];
        this.logEventController = eventControllers[3];
    }

    @Override
    public void actionPerformed(ActionEvent e) {

        String buttonText = e.getActionCommand();

        if (buttonText.equals("0") || buttonText.equals("1") || buttonText.equals("2") ||
                buttonText.equals("3") || buttonText.equals("4") || buttonText.equals("5") ||
                buttonText.equals("6") || buttonText.equals("7") || buttonText.equals("8") ||
                buttonText.equals("9") || buttonText.equals(".") || buttonText.equals("+/-")) {
            numberEventController.handleEvent(buttonText);
        }

        else if (buttonText.equals("+") || buttonText.equals("-") || buttonText.equals("×") || buttonText.equals("÷") || buttonText.equals("=")) {
            operatorEventController.handleEvent(buttonText);
        }

        else if (buttonText.equals("CE") || buttonText.equals("C") || buttonText.equals("<")) {
            erasorEventController.handleEvent(buttonText);
        }

        // logEventController
        // <html> 로 시작하거나 showLogButton 이면 logEventController 한테 보내줘야함
        else if(buttonText.contains("<html>") || buttonText.equals("showLogButton")){
            logEventController.handleEvent(buttonText);
        }
    }
}
