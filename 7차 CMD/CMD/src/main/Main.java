package main;

import controller.FrontController;

import java.io.IOException;

public class Main {
    public static void main(String[] args) throws IOException {
        FrontController frontController = new FrontController();
        frontController.run();
    }
}