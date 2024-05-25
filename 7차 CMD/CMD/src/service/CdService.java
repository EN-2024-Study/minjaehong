package service;

import model.VO.MessageVO;
import utility.Validator;

import java.io.IOException;
import java.nio.file.Path;
import java.util.AbstractMap;
import java.util.List;
import java.util.Map;

// CD는 예외적으로 DAO가 없음
// 그냥 해당 PATH가 존재하는지 안하는지 확인만 하면 되기 때문
// Pair 객체로 return (바뀔 디렉터리, MessageVO)
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
            return new AbstractMap.SimpleEntry<>(curDirectory, new MessageVO("지정된 경로를 찾을 수 없습니다.\n"));
        }

        String destination = parameters.get(0);
        return executeCD(curDirectory, destination);
    }

    private Map.Entry<String, MessageVO> executeCD(String curDirectory, String destination) throws IOException {

        Path changedDirectoryPath = getNormalizedPath(curDirectory, destination);

        // 만약 해당 경로가 존재한다면
        if(validator.checkIfDirectoryExists(changedDirectoryPath)){

            if(validator.checkIfDirectory(changedDirectoryPath)){
                return new AbstractMap.SimpleEntry<>(changedDirectoryPath.toString(), new MessageVO(""));
            }

            return new AbstractMap.SimpleEntry<>(curDirectory, new MessageVO("디렉터리 이름이 올바르지 않습니다.\n"));
        }

        return new AbstractMap.SimpleEntry<>(curDirectory, new MessageVO("지정된 경로를 찾을 수 없습니다.\n"));
    }
}