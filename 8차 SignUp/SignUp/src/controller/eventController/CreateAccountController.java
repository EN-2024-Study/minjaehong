package controller.eventController;

import model.dto.AccountDTO;
import model.dto.TextFieldDTO;
import service.SignUpService;
import utility.ViewHandler;
import view.panel.CreateAccountPanel;

import javax.swing.*;
import java.awt.event.ActionEvent;

public class CreateAccountController extends EventController {

    private CreateAccountPanel createAccountPanel;

    public CreateAccountController(CreateAccountPanel createAccountPanel, ViewHandler viewHandler){
        this.createAccountPanel = createAccountPanel;
        this.viewHandler = viewHandler;
        this.signUpService = new SignUpService();
    }

    @Override
    public void handleButtonEvent(ActionEvent e) {
        String actionCommand = e.getActionCommand();

        switch(actionCommand){
            case "createAccountPanel_checkId":
                checkID();
                break;

            case "createAccountPanel_checkPhoneNum":
                checkPhoneNum();
                break;

            case "createAccountPanel_checkZipCode":
                break;

            case "createAccountPanel_submit":
                if(submit()){
                    JOptionPane.showMessageDialog(createAccountPanel, "createAccount success!");
                    viewHandler.handleButtonEvent(e);
                }
                break;

            case "createAccountPanel_cancel":
                viewHandler.handleButtonEvent(e);
                break;
        }
    }

    private void checkID(){
        String inputText = createAccountPanel.idTextField.getText();
        if(!createAccountPanel.idTextField.checkValidity()){
            return;
        }
        boolean exists = signUpService.checkIfIDExists(new TextFieldDTO(inputText));
        showCheckResultMessage(createAccountPanel, createAccountPanel.idTextField, exists);
    }

    private void checkPhoneNum(){
        String inputText = createAccountPanel.phoneNumTextField.getText();
        if(!createAccountPanel.phoneNumTextField.checkValidity()){
            return;
        }
        boolean exists = signUpService.checkIfIDExists(new TextFieldDTO(inputText));
        showCheckResultMessage(createAccountPanel, createAccountPanel.phoneNumTextField, exists);
    }

    private boolean submit(){
        if(!createAccountPanel.returnTextFieldsValidity()){
            return false;
        }

        String userID = createAccountPanel.idTextField.getText();
        String userPW = createAccountPanel.pwTextField.getText();
        String userName = createAccountPanel.nameTextField.getText();
        String userPhoneNum = createAccountPanel.phoneNumTextField.getText();
        String userBirth = createAccountPanel.birthdayTextField.getText();
        String userEmail = createAccountPanel.emailTextField.getText();
        String userZipcode = createAccountPanel.zipCodeTextField.getText();
        String userAddress = createAccountPanel.addressTextField.getText();

        signUpService.addAccount(new AccountDTO(userID, userPW, userName, userPhoneNum, userBirth, userEmail, userZipcode, userAddress));
        return true;
    }
}
