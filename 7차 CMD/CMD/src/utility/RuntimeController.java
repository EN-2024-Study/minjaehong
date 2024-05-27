package utility;

import constant.Constants;
import constant.OverwriteEnum;
import handler.OverwritePermissionHandler;
import model.DTO.MessageDTO;
import model.DTO.OverwritePermissionDTO;
import view.CmdView;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;

public class RuntimeController {

    private final CmdView cmdView;
    private OverwritePermissionHandler overwritePermissionHandler;

    public RuntimeController(CmdView cmdView){
        this.cmdView = cmdView;
        overwritePermissionHandler = new OverwritePermissionHandler();
    }

    public void showCurWorkingFile(String curWorkingFile) throws IOException {
        cmdView.printMessageDTO(new MessageDTO(curWorkingFile));
    }

    public OverwriteEnum handleOverwritePermission(File sourceFile, Path destinationPath) throws IOException {

        cmdView.printMessageDTO(new MessageDTO(destinationPath.toString() + Constants.ASK_OVERWRITE));

        String input = cmdView.getOverwritePermission();
        OverwritePermissionDTO overwritePermissionDTO = overwritePermissionHandler.handleOverWritePermission(input);
        return overwritePermissionDTO.getOverwritePermission();
    }
}
