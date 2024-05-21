package main;

import controller.MainController;
import model.VO.InputVO;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.nio.file.FileSystem;
import java.nio.file.FileSystems;

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


// INPUT VO TEST CODE
//    InputVO testVO = new InputVO("cd a.txt");
//        System.out.println(testVO.getCommand());
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
