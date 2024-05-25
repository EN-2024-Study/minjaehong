package service;

import utility.Validator;

import java.io.File;
import java.io.IOException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.List;

// CmdService > CdService = DirService
// CmdService > ActionCmdService > MvService = CpService
public abstract class CmdService<T> {

    protected Validator validator;

    protected CmdService(Validator validator){
        this.validator = validator;
    }

    // 상대경로나 절대경로로 들어온걸 정규경로로 return 해줌
    protected final Path getNormalizedPath(String curDirectory, String directoryPath) throws IOException {

        Path retPath = Paths.get("");

        // 1. root 부터 시작하는 놈이면 바로 Path 객체 만들어주기
        // 얘는 C:/ 부터 시작하는 놈인거임
        if (validator.checkIfStartingFromRootDirectory(directoryPath)) {
            retPath = Paths.get(directoryPath);
        }
        
        // /// 이렇게 들어온거 예외처리해야함
        // curDirectory에 붙었다가 Paths.get에서 터지는 놈들 잡아야함
        
        // 2. 상대경로로 들어왔으면 curDirectory 이용해서 절대경로로 만들어주기
        // C:/A/B/C/../X/Y 형식으로 만들고
        else {
            retPath = Paths.get(curDirectory, directoryPath);
        }

        // normalize 해주기 (. .. 이런거 다 빼주기)
        retPath = retPath.normalize();
        // uSer 같은거 실제로 USER 일때 대비해서 File 로 한번 바꿔주고 내보내야함
        File file = new File(retPath.toString());
        retPath = Paths.get(file.getCanonicalPath());

        return retPath;
    }

    public abstract T handleCommand(String curDirectory, List<String> parameters) throws IOException;
}
