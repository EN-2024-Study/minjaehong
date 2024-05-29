package controller.eventController;

import model.dto.LoginDTO;
import model.dto.ValueDTO;
import service.SignUpService;
import utility.ViewHandler;
import view.panel.LoginPanel;

import javax.swing.*;
import java.awt.event.ActionEvent;

public class LoginController extends EventController {

    private LoginPanel loginPanel;

    public LoginController(LoginPanel loginPanel, ViewHandler viewHandler){
        this.loginPanel = loginPanel;
        this.viewHandler = viewHandler;
        this.signUpService = new SignUpService();
    }

    @Override
    public void handleButtonEvent(ActionEvent e) {
        String actionCommand = e.getActionCommand();

        switch(actionCommand){
            case "loginPanel_login":
                login(e);
                break;

            case "loginPanel_findPassword":
                findPassword();
                break;

            case "loginPanel_createAccount":
                createAccount(e);
                break;
        }
    }

    private void login(ActionEvent e){
        if(!loginPanel.returnTextFieldsValidity()) {
            return;
        }

        String inputID = loginPanel.idTextField.getText();
        String inputPW = loginPanel.pwTextField.getText();

        if(signUpService.checkIfValidLogin(new LoginDTO(inputID, inputPW))) {
            JOptionPane.showMessageDialog(loginPanel, "로그인 성공!");
            viewHandler.setCookie(inputID);
            viewHandler.handleButtonEvent(e);
        }else{
            JOptionPane.showMessageDialog(loginPanel, "로그인 실패!");
        }
    }

    private void findPassword(){
        String userInput = JOptionPane.showInputDialog(loginPanel, "당신의 ID를 입력하세요");

        ValueDTO valueDTO = signUpService.getPWOfCertainID(new ValueDTO(userInput));
        String password = valueDTO.getValue();

        if (!password.isEmpty()) {
            JOptionPane.showMessageDialog(loginPanel, "당신의 비밀번호는 " + password +" 입니다!");
        }else{
            JOptionPane.showMessageDialog(loginPanel, "ID가 존재하지 않습니다!");
        }
    }

    private void createAccount(ActionEvent e){
        viewHandler.handleButtonEvent(e);
    }
}
