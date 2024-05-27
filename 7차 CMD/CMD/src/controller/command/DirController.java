package controller.command;

import constant.Constants;
import model.DTO.DirDTO;
import model.DTO.MessageDTO;
import service.DirService;
import utility.Validator;
import view.CmdView;

import java.io.IOException;
import java.util.BitSet;
import java.util.List;

public class DirController extends CommandController{

    private DirService dirService;

    public DirController(CmdView cmdView, Validator validator){
        super(cmdView);
        this.dirService = new DirService(validator);
    }

    @Override
    public String executeCommand(String curDirectory, List<String> parameters) throws IOException {
        if (parameters.size()==0) parameters.add(curDirectory);

        int parameterLength = parameters.size();

        BitSet bitset = new BitSet(parameterLength);

        cmdView.printDriveInfo();

        for(int i=0;i<parameterLength;i++) {
            DirDTO dirDTO = dirService.handleCommand(curDirectory, parameters);

            bitset.set(i, dirDTO.checkIfDirectoryExists());

            // 상황에 따른 파일을 찾을 수 없습니다 출력 #1
            if((i>1 && bitset.get(i)==true && bitset.get(i-1)==false)){
                cmdView.printMessageDTO(new MessageDTO(Constants.CANT_FIND_FILE));
            }

            cmdView.printDirDTO(dirDTO);
            parameters.remove(0);

            // 상황에 따른 파일을 찾을 수 없습니다 출력 #2
            if(bitset.get(i)==false && i==parameterLength-1){
                cmdView.printMessageDTO(new MessageDTO(Constants.CANT_FIND_FILE));
            }
        }

        return curDirectory;
    }
}
