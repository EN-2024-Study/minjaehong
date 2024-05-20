package main;

import controller.MainController;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.nio.file.FileSystem;
import java.nio.file.FileSystems;

public class Main {
    // Windows 도 Unix 랑 동일하게 모든 것을 파일 취급함
    public static void main(String[] args) throws IOException {

        System.out.print(System.getProperty("os.name"));
        System.out.println(System.getProperty(" [version os.version]"));
        System.out.println("(c) Microsoft Corporation. All rights reserved.");

        MainController mainController = new MainController();
        mainController.run();
    }
}