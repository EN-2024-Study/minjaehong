package controller.eventController;

import model.dto.TextFieldDTO;
import service.SignUpService;
import view.panel.EditAccountPanel;

import javax.swing.*;
import java.awt.event.ActionEvent;

public class EditAccountController extends EventController {

    private EditAccountPanel editAccountPanel;

    public EditAccountController(EditAccountPanel editAccountPanel){
        this.editAccountPanel = editAccountPanel;
        this.signUpService = new SignUpService();
    }

    private void checkID(){
        String inputText = editAccountPanel.idTextField.getText();
        boolean exists = signUpService.checkIfIDExists(new TextFieldDTO(inputText));
        showCheckResultMessage(editAccountPanel, editAccountPanel.idTextField, exists);
    }

    private void checkPhoneNum(){
        String inputText = editAccountPanel.phoneNumTextField.getText();
        boolean exists = signUpService.checkIfPhoneNumExists(new TextFieldDTO(inputText));
        showCheckResultMessage(editAccountPanel, editAccountPanel.phoneNumTextField, exists);
    }

    private void cancel(){
        clearPanel();
    }

    private void clearPanel(){
        editAccountPanel.idTextField.setText("");
        editAccountPanel.idTextField.setEnabled(true);
        editAccountPanel.phoneNumTextField.setText("");
        editAccountPanel.phoneNumTextField.setEnabled(true);
        editAccountPanel.zipCodeTextField.setText("");
        editAccountPanel.zipCodeTextField.setEnabled(true);
    }

    @Override
    public void handleButtonEvent(ActionEvent e) {
        String actionCommand = e.getActionCommand();
        String inputText;

        switch(actionCommand){
            case "editAccountPanel_checkId":
                checkID();
                break;

            case "editAccountPanel_checkPhoneNum":
                checkPhoneNum();
                break;

            case "editAccountPanel_checkZipCode":
                inputText = editAccountPanel.zipCodeTextField.getText();
                JOptionPane.showMessageDialog(editAccountPanel, inputText + " available!");
                editAccountPanel.zipCodeTextField.setEnabled(false);
                break;

            case "editAccountPanel_submit":

                break;
            case "editAccountPanel_cancel":

                break;
        }
    }
}
