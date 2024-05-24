package main;

import controller.MainController;

import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import java.util.LinkedList;

public class Main {
    // Windows 도 Unix 랑 동일하게 모든 것을 파일 취급함
    public static void main(String[] args) throws IOException, InterruptedException {

        System.out.print(System.getProperty("os.name"));
        System.out.println(System.getProperty(" [version os.version]"));
        System.out.println("(c) Microsoft Corporation. All rights reserved.");

        MainController mainController = new MainController();
        mainController.run();
    }
}

//                INPUT VO TEST CODE
//                InputVO testVO = new InputVO("cd a.txt");
//                System.out.println(testVO.getCommand());
//                System.out.println(testVO.getParameters());
//
//                testVO = new InputVO("cd      a.txt");
//                System.out.println(testVO.getCommand());
//                System.out.println(testVO.getParameters());
//
//                testVO = new InputVO("copy a.txt b.txt");
//                System.out.println(testVO.getCommand());
//                System.out.println(testVO.getParameters());
//
//                testVO = new InputVO("cd \"here we go.txt\"");
//                System.out.println(testVO.getCommand());
//                System.out.println(testVO.getParameters());
//
//                testVO = new InputVO("move \"a and b and c and d.txt\"     \"here we go.txt\"");
//                System.out.println(testVO.getCommand());
//                System.out.println(testVO.getParameters());
//
//                testVO = new InputVO("cd \"mancity       arsenal      chelsea    and me.txt\"");
//                System.out.println(testVO.getCommand());
//                System.out.println(testVO.getParameters());
//
//                testVO = new InputVO("move \"what are you doing .txt\"     \" i am going out .txt\" \" hi what     is your  name.txt\" \" my name is mintuchel.txt\"");
//                System.out.println(testVO.getCommand());
//                System.out.println(testVO.getParameters());


//    File source = new File("C:\\Users\\USER\\Desktop\\aaa.txt");
//    LinkedList<File> sourceFileList;
//        if(source.isFile()){
//                sourceFileList = new LinkedList<>();
//        sourceFileList.add(source);
//        }else{
//        sourceFileList = new LinkedList<>(Arrays.asList(source.listFiles()));
//        }
//        System.out.println(sourceFileList);
//        System.out.println("\n");
//
//        File destination = new File("C:\\Users\\USER\\Desktop\\temp");
//        LinkedList<File> destinationFileList;
//        if(source.isFile()){
//        destinationFileList = new LinkedList<>();
//        destinationFileList.add(destination);
//        }else{
//        destinationFileList = new LinkedList<>(Arrays.asList(destination.listFiles()));
//        }
//        System.out.println(destinationFileList);
//        System.out.println("\n");
//
//        int copiedCnt = 0;
//        LinkedList<String> fileNameList = new LinkedList<>(Arrays.asList(destination.list()));
//        System.out.println(fileNameList);
//        System.out.println("\n");
