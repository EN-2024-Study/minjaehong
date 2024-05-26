package utility;

import AAA.Constants;
import AAA.OverwriteEnum;
import model.VO.MessageVO;
import model.VO.OverwritePermissionVO;
import view.CmdView;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;

public class RuntimeExceptionHandler {

    private CmdView cmdView;

    public RuntimeExceptionHandler(CmdView cmdView){
        this.cmdView = cmdView;
    }

    public void showCurWorkingFile(String curWorkingFile) throws IOException {
        cmdView.printMessageVO(new MessageVO(curWorkingFile));
    }

    public OverwriteEnum handleOverwritePermission(File sourceFile, Path destinationPath) throws IOException {

        cmdView.printMessageVO(new MessageVO(destinationPath.toString() + Constants.ASK_OVERWRITE));

        OverwritePermissionVO overwritePermissionVO = cmdView.getOverwritePermission();

        return overwritePermissionVO.getOverwritePermission();
    }
}
