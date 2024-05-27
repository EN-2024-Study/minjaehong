package controller.panelcontroller;

import view.panel.CreateAccountPanel;

import java.awt.event.ActionEvent;

public class CreateAccountController implements Executable{

    private CreateAccountPanel createAccountPanel;

    public CreateAccountController(CreateAccountPanel createAccountPanel){
        this.createAccountPanel = createAccountPanel;
    }

    @Override
    public void execute(ActionEvent e) {
        String actionCommand = e.getActionCommand();

        switch(actionCommand){
            case "createAccountPanel_checkId":
                System.out.println("checkID");

                // panel에서 값 받아오기
                
                // Service 통해서 중복 체크
                //createAccountService.checkID();

                // 화면에 JoptionPane으로 띄워주기
                
                // 값 비워주기
                
                break;
            case "createAccountPanel_checkPhoneNum":
                System.out.println("checkPhonenum");
                // panel에서 값 받아오기
                
                // service 통해서 중복체크
                
                // 화면에 JoptionPane으로 띄워주기
                
                // 값 비워주기
                
                break;
            case "createAccountPanel_checkZipCode":
                System.out.println("checkZipCode");
                // service 띄워주기만 하기
                
                break;
            case "createAccountPanel_createAccount":
                System.out.println("createAccount");
                // service 통해서 실제로 account 하나 생성해주기
                
                break;
            case "createAccountPanel_cancel":
                System.out.println("cancel");
                // panel만 바꿔주기
                break;
        }
    }
}
