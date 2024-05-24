package controller;

import model.VO.MessageVO;
import view.MainView;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;

// enum 만들어서 yes no all 전달
public class RuntimeController {

    MainView mainView;

    public RuntimeController(){
        mainView = new MainView();
    }

    public boolean handleOverwrite(File sourceFile, Path destinationPath) throws IOException {

        mainView.printMessageVO(new MessageVO(sourceFile.getAbsolutePath()+"\n"));
        mainView.printMessageVO(new MessageVO(destinationPath.toString()+"을(를) 덮어쓰시겠습니까? (Yes/No/All) "));

        MessageVO messageVO = mainView.getRuntimeInput();

        String userInput = messageVO.getMessage();
        userInput = userInput.toLowerCase();

        if(userInput.startsWith("y")) return true;

        return false;
    }
}
