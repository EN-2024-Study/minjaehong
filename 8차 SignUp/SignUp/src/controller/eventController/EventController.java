package controller.eventController;

import model.dto.TextFieldDTO;
import service.SignUpService;

import javax.swing.*;
import java.awt.event.ActionEvent;

public abstract class EventController {

    protected SignUpService signUpService;

    public abstract void handleButtonEvent(ActionEvent e);

    protected void showCheckResultMessage(JPanel panel, JTextField textField, boolean exists){
        String inputText = textField.getText();

        if(exists) {
            JOptionPane.showMessageDialog(panel, inputText + " already exists!");
            textField.setEnabled(false);
        }else {
            JOptionPane.showMessageDialog(panel, inputText + " available!");
            textField.setEnabled(false);
        }
    }
}