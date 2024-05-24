package controller;

import model.VO.MessageVO;
import view.MainView;

import java.io.IOException;
import java.nio.file.Path;

// enum 만들어서 yes no all 전달
public class RuntimeController {

    MainView mainView;

    public RuntimeController(){
        mainView = new MainView();
    }

    public boolean handleOverwrite(Path destinationPath) throws IOException {
        mainView.printMessageVO(new MessageVO(destinationPath.toString()+" 을(를) 덮어쓰시겠습니까? "));

        MessageVO messageVO = mainView.getRuntimeInput();

        String userInput = messageVO.getMessage();

        if(userInput.startsWith("y")) return true;

        return false;
    }
}
