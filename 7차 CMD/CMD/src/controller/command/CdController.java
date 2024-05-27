package controller.command;

import model.DTO.MessageDTO;
import service.CdService;
import utility.Validator;
import view.CmdView;

import java.io.IOException;
import java.util.List;
import java.util.Map;

public class CdController extends CommandController{

    private CdService cdService;

    public CdController(CmdView cmdView, Validator validator){
        super(cmdView);
        this.cdService = new CdService(validator);
    }

    @Override
    public String executeCommand(String curDirectory, List<String> parameters) throws IOException {
        Map.Entry<String, MessageDTO> cdResult = cdService.handleCommand(curDirectory, parameters);

        // getValue 로 message 추출 후 view 에 전달
        MessageDTO messageDTO = cdResult.getValue();
        cmdView.printMessageDTO(messageDTO);

        // getKey 로 바뀐 directory 추출 후 changedDirectory return 해서 curDirectory 최신화
        String changedDirectory = cdResult.getKey();
        return changedDirectory;
    }
}