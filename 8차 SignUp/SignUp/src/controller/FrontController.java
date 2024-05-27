package controller;

import Utility.ControllerMapper;
import controller.panelcontroller.*;
import view.frame.MainFrame;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.List;

// FrontController 는 ControllerMapper 를 통해서 ActionEvent e를 보낼 Controller 를 받음
public class FrontController implements ActionListener {

    private ControllerMapper controllerMapper;

    private Executable mappedController;
    private PanelChangeController panelChangeController;

    public FrontController(List<Executable> controllerList){
        controllerMapper = new ControllerMapper(controllerList);
        //panelChangeController = new PanelChangeController();
    }

    // 화면바꾸기를 여기서??
    @Override
    public void actionPerformed(ActionEvent e) {
        mappedController = controllerMapper.getMappedController(e);
        mappedController.execute(e);

        // 위에서 boolean으로 바꾸고 true 면 바꾸게 하기
        // panel 바꿔야하면 바꾸기
        panelChangeController.execute(e);
    }
}
