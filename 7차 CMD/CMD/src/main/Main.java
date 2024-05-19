package main;

import controller.MainController;

import java.io.File;
import java.io.IOException;
import java.nio.file.FileSystem;
import java.nio.file.FileSystems;
import java.nio.file.Path;
import java.util.Iterator;

public class Main {
    // Windows 도 Unix 랑 동일하게 모든 것을 파일 취급함
    public static void main(String[] args) throws IOException {
        MainController mainController = new MainController();
        mainController.run();

        // FileSystem Interface 를 통해 현재 OS의 파일시스템을 알 수 있음
        // FileSystem 참조객체는 getDefault 라는 static method로 받을 수 있음

        /*
        String newDirectory = "..";
        // File 인스턴스를 생성했다고 해서 파일이나 디렉토리가 생성되는 것은 아님
        // 1. 기존에 존재하는 파일 참조하거나
        // 2. 새 파일 생성하기 전에 미리 인스턴스 생성해놔야함
        File f = new File(newDirectory);
        System.out.println(f.getAbsolutePath());
        System.out.println(f.getCanonicalPath());
        System.out.println(f.getParent());
        System.out.println(f.getPath());
        System.out.println("==================");
        newDirectory = "\\";
        f = new File(newDirectory);
        System.out.println(f.getAbsolutePath());
        System.out.println(f.getCanonicalPath());
        System.out.println(f.getParent());
        System.out.println(f.getPath());
        System.out.println("==================");
        newDirectory = "/";
        f = new File(newDirectory);
        System.out.println(f.getAbsolutePath());
        System.out.println(f.getCanonicalPath());
        System.out.println(f.getParent());
        System.out.println(f.getPath());
         */
    }
}