package controller.eventController;

import view.panel.EditAccountPanel;

import javax.swing.*;
import java.awt.event.ActionEvent;

public class EditAccountController implements EventController {

    private EditAccountPanel editAccountPanel;

    public EditAccountController(EditAccountPanel editAccountPanel){
        this.editAccountPanel = editAccountPanel;
    }

    @Override
    public void handleButtonEvent(ActionEvent e) {
        String actionCommand = e.getActionCommand();
        String inputText;

        switch(actionCommand){
            case "editAccountPanel_checkId":
                inputText = editAccountPanel.idTextField.getText();
                JOptionPane.showMessageDialog(editAccountPanel, inputText + " available!");
                editAccountPanel.idTextField.setEnabled(false);
                break;

            case "editAccountPanel_checkPhoneNum":
                inputText = editAccountPanel.phoneNumTextField.getText();
                JOptionPane.showMessageDialog(editAccountPanel, inputText + " available!");
                editAccountPanel.phoneNumTextField.setEnabled(false);
                break;// panel 에서 값 받아오기

            case "editAccountPanel_checkZipCode":
                inputText = editAccountPanel.zipCodeTextField.getText();
                JOptionPane.showMessageDialog(editAccountPanel, inputText + " available!");
                editAccountPanel.zipCodeTextField.setEnabled(false);
                break;// panel 에서 값 받아오기

            case "editAccountPanel_submit":
                System.out.println("submit");
                // service 통해서 실제로 account 하나 생성해주기

                break;
            case "editAccountPanel_cancel":
                System.out.println("cancel");
                // panel만 바꿔주기
                break;
        }
    }
}
