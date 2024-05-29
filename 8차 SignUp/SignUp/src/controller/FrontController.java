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

    private EventController mappedController;

    public FrontController(ControllerMapper controllerMapper){
        this.controllerMapper = controllerMapper;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        mappedController = controllerMapper.getMappedController(e);

        if(mappedController != null)  mappedController.handleButtonEvent(e);
    }
}
