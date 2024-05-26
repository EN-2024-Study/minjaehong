package main;

import controller.CmdController;

import java.io.IOException;

public class Main {
    public static void main(String[] args) throws IOException {

        CmdController cmdController = new CmdController();
        cmdController.run();
    }
}