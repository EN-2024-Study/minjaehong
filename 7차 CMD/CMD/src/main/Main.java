package main;

import controller.MainController;

import java.io.File;
import java.io.IOException;
import java.util.Arrays;
import java.util.LinkedList;

public class Main {
    public static void main(String[] args) throws IOException, InterruptedException {

        System.out.print(System.getProperty("os.name"));
        System.out.println(System.getProperty(" [version os.version]"));
        System.out.println("(c) Microsoft Corporation. All rights reserved.");

        MainController mainController = new MainController();
        mainController.run();
    }
}
