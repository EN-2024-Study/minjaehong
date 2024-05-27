package service;

import constant.Constants;
import model.DTO.MessageDTO;
import utility.Validator;

import java.io.IOException;
import java.nio.file.Path;
import java.util.AbstractMap;
import java.util.List;
import java.util.Map;

// CD는 예외적으로 DAO가 없음
// 그냥 해당 PATH가 존재하는지 안하는지 확인만 하면 되기 때문
// Pair 객체로 return (바뀔 디렉터리, MessageDTO)
public class CdService extends CmdService<Map.Entry<String, MessageDTO>>{

    public CdService(Validator validator){
        super(validator);
    }

    @Override
    public Map.Entry<String, MessageDTO> handleCommand(String curDirectory, List<String> parameters) throws IOException {
        if(parameters.size()==0){
            return new AbstractMap.SimpleEntry<>(curDirectory, new MessageDTO(curDirectory));
        }

        if(parameters.size()>1){
            return new AbstractMap.SimpleEntry<>(curDirectory, new MessageDTO(Constants.CANT_FIND_CERTAIN_ROUTE));
        }

        String destination = parameters.get(0);
        return executeCD(curDirectory, destination);
    }

    private Map.Entry<String, MessageDTO> executeCD(String curDirectory, String destination) throws IOException {

        Path changedDirectoryPath = getNormalizedPath(curDirectory, destination);

        // 만약 해당 경로가 존재한다면
        if(validator.checkIfDirectoryExists(changedDirectoryPath)){

            if(validator.checkIfDirectory(changedDirectoryPath)){
                return new AbstractMap.SimpleEntry<>(changedDirectoryPath.toString(), new MessageDTO(""));
            }

            return new AbstractMap.SimpleEntry<>(curDirectory, new MessageDTO(Constants.WRONG_DIRECTORY_NAME));
        }

        return new AbstractMap.SimpleEntry<>(curDirectory, new MessageDTO(Constants.CANT_FIND_CERTAIN_ROUTE));
    }
}