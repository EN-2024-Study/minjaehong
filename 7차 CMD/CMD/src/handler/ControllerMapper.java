package handler;

import controller.*;
import view.CmdView;

import java.util.ArrayList;

// command를 보고 그에 알맞는 controller를 반환해줌
public class ControllerMapper {

    ArrayList<CommandController> controllerList;

    public ControllerMapper(ArrayList<CommandController> controllerList) {
        this.controllerList = controllerList;
    }

    public CommandController getMappedController(String command) {

        switch (command) {
            case "cd":
                return controllerList.get(0);
            case "dir":
                return controllerList.get(1);
            case "copy":
                return controllerList.get(2);
            case "move":
                return controllerList.get(3);
            default:
                return null;
        }
    }
}
