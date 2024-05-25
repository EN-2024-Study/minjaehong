package utility;

import constants.OverwriteEnum;
import model.VO.MessageVO;
import model.VO.OverwritePermissionVO;
import view.MainView;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;

public class RuntimeExceptionHandler {

    private MainView mainView;

    public RuntimeExceptionHandler(MainView mainView){
        this.mainView = mainView;
    }

    public void showCurWorkingFile(String curWorkingFile) throws IOException {
        mainView.printMessageVO(new MessageVO(curWorkingFile));
    }

    public OverwriteEnum handleOverwritePermission(File sourceFile, Path destinationPath) throws IOException {

        mainView.printMessageVO(new MessageVO(destinationPath.toString()+"을(를) 덮어쓰시겠습니까? (Yes/No/All) "));

        OverwritePermissionVO overwritePermissionVO = mainView.getOverwritePermission();

        return overwritePermissionVO.getOverwritePermission();
    }
}
