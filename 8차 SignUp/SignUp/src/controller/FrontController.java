package controller;

import utility.ControllerMapper;
import utility.ViewHandler;
import controller.eventController.*;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

// FrontController 는 ControllerMapper 를 통해서 ActionEvent e를 보낼 Controller 를 받음
// ViewHandler 는 ActionEvent e가 view 를 바꿔야하는 명령이면 view 를 바꿔줌
public class FrontController implements ActionListener {

    private ControllerMapper controllerMapper;
    private ViewHandler viewHandler;

    private EventController mappedController;

    public FrontController(ControllerMapper controllerMapper, ViewHandler viewHandler){
        this.controllerMapper = controllerMapper;
        this.viewHandler = viewHandler;
    }

    private boolean checkIfPanelChangeNeeded(ActionEvent e){
        String command = e.getActionCommand();
        if(command.equals("loginPanel_createAccount")||
                command.equals("loginPanel_login")||
                command.equals("createAccountPanel_submit") ||
                command.equals("createAccountPanel_cancel")||
                command.equals("userHomePanel_edit")||
                command.equals("userHomePanel_logOut")||
                command.equals("userHomePanel_delete")||
                command.equals("editAccountPanel_cancel") ||
                command.equals("editAccountPanel_submit")) {
            return true;
        }
        return false;
    }


    @Override
    public void actionPerformed(ActionEvent e) {
        mappedController = controllerMapper.getMappedController(e);

        if(mappedController != null)  mappedController.handleButtonEvent(e);

        if(checkIfPanelChangeNeeded(e)){
            viewHandler.handleButtonEvent(e);
        }
    }
}
