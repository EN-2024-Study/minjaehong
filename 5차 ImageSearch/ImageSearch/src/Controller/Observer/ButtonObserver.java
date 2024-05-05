package Controller.Observer;

import View.MainView;
import Controller.Controller;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

// 얘는 VIEW COMPONENT 들에게 주입되는 ActionListener 임
// 그래서 e.getSource() 를 판단하기 위해 VIEW COMPONENT 를 참조할 수 있어야하고
// 그에 따른 Controller 에게 메세지를 전달하기 위해 CONTROLLER 를 호출할 수 있어야함
// 그래서 VIEW CONTROLLER 참조변수를 둘 다 넘겨줘야함
public class ButtonObserver implements ActionListener {

    MainView mainView;
    Controller controller;

    public ButtonObserver(MainView mainView, Controller controller){
        this.mainView = mainView;
        this.controller = controller;
    }

    @Override
    public void actionPerformed(ActionEvent e) {

        if(e.getSource()==mainView.getSearchBtn()) controller.searchImage();
        else if(e.getSource() == mainView.getLogBtn()) controller.getAllLog();
        else if(e.getSource()==mainView.getHowMany()) controller.applyHowMany(e);
        else if(e.getSource()==mainView.getBackToHomeBtn()) controller.goBackToHome();
        else if(e.getSource()==mainView.getDeleteAllLogBtn()) controller.deleteAllLog();
    }
}
