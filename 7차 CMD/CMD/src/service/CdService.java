package service;

import model.VO.MessageVO;
import utility.Validator;

import java.io.IOException;
import java.nio.file.Path;
import java.util.AbstractMap;
import java.util.List;
import java.util.Map;

public class CdService extends CmdService<Map.Entry<String,MessageVO>>{

    public CdService(Validator validator){
        super(validator);
    }

    @Override
    public Map.Entry<String, MessageVO> handleCommand(String curDirectory, List<String> parameters) throws IOException {
        if(parameters.size()==0){
            return new AbstractMap.SimpleEntry<>(curDirectory, new MessageVO(curDirectory));
        }

        if(parameters.size()>1){
            return new AbstractMap.SimpleEntry<>(curDirectory, new MessageVO("지정된 경로를 찾을 수 없습니다"));
        }

        String destination = parameters.get(0);
        return cd(curDirectory, destination);
    }

    private Map.Entry<String, MessageVO> cd(String curDirectory, String destination) throws IOException {

        Path changedDirectoryPath = getNormalizedPath(curDirectory, destination);

        if(validator.checkIfDirectoryExists(changedDirectoryPath)){

            if(validator.isDirectory(changedDirectoryPath)){
                return new AbstractMap.SimpleEntry<>(changedDirectoryPath.toString(), new MessageVO(""));
            }

            return new AbstractMap.SimpleEntry<>(curDirectory, new MessageVO("디렉터리 이름이 올바르지 않습니다."));
        }

        return new AbstractMap.SimpleEntry<>(curDirectory, new MessageVO("지정된 경로를 찾을 수 없습니다."));
    }
}