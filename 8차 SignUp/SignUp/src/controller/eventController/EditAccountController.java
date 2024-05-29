package controller.eventController;

import model.dto.ValueDTO;
import service.SignUpService;
import utility.ViewHandler;
import view.panel.EditAccountPanel;

import javax.swing.*;
import java.awt.event.ActionEvent;

public class EditAccountController extends EventController {

    private EditAccountPanel editAccountPanel;

    public EditAccountController(EditAccountPanel editAccountPanel, ViewHandler viewHandler){
        this.editAccountPanel = editAccountPanel;
        this.viewHandler = viewHandler;
        this.signUpService = new SignUpService();
    }

    @Override
    public void handleButtonEvent(ActionEvent e) {
        String actionCommand = e.getActionCommand();

        switch(actionCommand){
            case "editAccountPanel_checkId":
                checkID();
                break;

            case "editAccountPanel_checkPhoneNum":
                checkPhoneNum();
                break;

            case "editAccountPanel_checkZipCode":
                break;

            case "editAccountPanel_submit":
                submit(e);
                break;
            case "editAccountPanel_cancel":
                cancel(e);
                break;
        }
    }

    private void checkID(){
        String inputText = editAccountPanel.idTextField.getText();
        boolean exists = signUpService.checkIfIDExists(new ValueDTO(inputText));
        showCheckResultMessage(editAccountPanel, editAccountPanel.idTextField, exists);
    }

    private void checkPhoneNum(){
        String inputText = editAccountPanel.phoneNumTextField.getText();
        boolean exists = signUpService.checkIfPhoneNumExists(new ValueDTO(inputText));
        showCheckResultMessage(editAccountPanel, editAccountPanel.phoneNumTextField, exists);
    }

    private void submit(ActionEvent e){
        if(editAccountPanel.returnTextFieldsValidity()){
            // signUpService.updateAccount();
            JOptionPane.showMessageDialog(editAccountPanel, "EDIT SUCCESS!");
            viewHandler.handleButtonEvent(e);
        }
    }

    private void cancel(ActionEvent e){
        viewHandler.handleButtonEvent(e);
    }
}
