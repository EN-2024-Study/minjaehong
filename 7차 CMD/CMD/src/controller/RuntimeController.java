package controller;

import Constants.OverwriteEnum;
import model.VO.MessageVO;
import model.VO.OverwritePermissionVO;
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

    public OverwriteEnum handleOverwritePermission(File sourceFile, Path destinationPath) throws IOException {

        mainView.printMessageVO(new MessageVO(sourceFile.getAbsolutePath()+"\n"));
        mainView.printMessageVO(new MessageVO(destinationPath.toString()+"을(를) 덮어쓰시겠습니까? (Yes/No/All) "));

        OverwritePermissionVO overwritePermissionVO = mainView.getOverwritePermission();

        return overwritePermissionVO.getOverwritePermission();
    }
}
