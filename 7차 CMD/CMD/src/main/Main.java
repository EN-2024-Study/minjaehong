package main;

import controller.MainController;

import java.io.IOException;

public class Main {
    // Windows 도 Unix 랑 동일하게 모든 것을 파일 취급함
    public static void main(String[] args) throws IOException {
        MainController mainController = new MainController();
        mainController.run();
    }
}