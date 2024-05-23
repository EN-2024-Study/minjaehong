package model.DAO;

import model.VO.DirVO;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.util.Arrays;
import java.util.Date;
import java.util.LinkedList;

public class DirDAO{

    private void addParentFolders(LinkedList<File> fileList, File sourceDirectory){
        // rootDirectory 일때 예외처리
        // Parent가 존재할때만 fileList에 추가
        if(sourceDirectory.getParentFile()!= null) {
            fileList.add(0, sourceDirectory.getParentFile());
        }
        // 현재 directory fileList에 추가
        fileList.add(0, sourceDirectory);
    }

    // 여기서부터는 진짜로 dir 작업 수행
    // 존재성이 확인된 애들만 호출됨
    // 한개의 폴더에 대한 탐색임
    public DirVO dir(String curDirectory, Path sourcePath) throws IOException {

        File source = sourcePath.toFile();

        // 우선 존재하는 파일이니까 curDirectory sourcePath.toString() 으로 기본 정보 초기화해주면서 객체 생성
        DirVO resultVO = new DirVO(curDirectory, sourcePath.toString());

        // 1. source가 폴더일때
        if (source.isDirectory()) {

            // 폴더 안에 있는 파일들 모두 불러오기
            LinkedList<File> fileList = new LinkedList<>(Arrays.asList(source.listFiles()));

            addParentFolders(fileList, source);

            for (int i=0;i<fileList.size();i++) {

                File file = fileList.get(i);

                // 숨겨진 파일이면 skip
                // canRead canWrite 로도 skip 해야함??
                if(file.isHidden()) continue;
                if(Files.isReadable(file.toPath())==false) continue;

                // DATE
                Date lastModifiedDate = new Date();
                lastModifiedDate.setTime(file.lastModified());

                // DIRECTORY OR NOT
                boolean isDirectory = file.isDirectory();

                // FILE SIZE
                long fileSize = file.length();

                // FILE NAME
                String fileName = file.getName();
                if (file.equals(source)) {
                    fileName = ".";
                } else if (file.equals(source.getParentFile())) {
                    fileName = "..";
                }

                resultVO.addNewFileInfo(lastModifiedDate, isDirectory, fileSize, fileName);
            }
        }
        // 2. source가 파일일때는 그냥 자기 자신에 대한 정보만 저장하면 됨
        else {
            Date lastModifiedDate = new Date();
            lastModifiedDate.setTime(source.lastModified());

            boolean isFolder = source.isDirectory();

            long fileSize = source.length();

            String fileName = source.getName();
            resultVO.addNewFileInfo(lastModifiedDate, isFolder, fileSize, fileName);
        }

        return resultVO;
    }
}