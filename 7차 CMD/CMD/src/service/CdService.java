package service;

import model.VO.OutputVO;
import utility.Validator;

import java.io.IOException;
import java.nio.file.Path;
import java.util.AbstractMap;
import java.util.List;
import java.util.Map;

public class CdService extends CmdService{

    public CdService(Validator validator){
        super(validator);
    }

    public Map.Entry<String, OutputVO> handleCd(String curDirectory, List<String> parameters) throws IOException {

        if(parameters.size()==0){
            return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO(curDirectory));
        }

        if(parameters.size()>2){
            return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO("지정된 경로를 찾을 수 없습니다"));
        }

        String destination = parameters.get(0);
        return cd(curDirectory, destination);
    }

    private Map.Entry<String, OutputVO> cd(String curDirectory, String destination) throws IOException {

        Path changedDirectoryPath = getNormalizedPath(curDirectory, destination);

        if(validator.checkIfDirectoryExists(changedDirectoryPath)){

            if(validator.isDirectory(changedDirectoryPath)){
                return new AbstractMap.SimpleEntry<>(changedDirectoryPath.toString(), new OutputVO(""));
            }

            return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO("디렉터리 이름이 올바르지 않습니다."));
        }

        return new AbstractMap.SimpleEntry<>(curDirectory, new OutputVO("지정된 경로를 찾을 수 없습니다."));
    }


    // 사용 안하는 부모쪽 구현해야할 함수
    // CD는 데이터를 가져오는 작업이 아니라 그냥 해당 디렉토리의 존재성을 확인하고
    // controller의 curDirectory만 바꿔주면 됨
    @Override
    public OutputVO handleCommand(String curDirectory, List<String> parameters) throws IOException {
        return null;
    }
}