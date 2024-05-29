package controller.eventController;

import service.SignUpService;
import utility.ViewHandler;

import javax.swing.*;
import java.awt.event.ActionEvent;

public abstract class EventController {

    protected SignUpService signUpService;
    protected ViewHandler viewHandler;

    public abstract void handleButtonEvent(ActionEvent e);

    protected void showCheckResultMessage(JPanel panel, JTextField textField, boolean exists){
        String inputText = textField.getText();

        if(exists) {
            JOptionPane.showMessageDialog(panel, inputText + " already exists!");
        }else {
            JOptionPane.showMessageDialog(panel, inputText + " available!");
            textField.setEnabled(false);
        }
    }
}