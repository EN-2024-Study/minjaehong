package handler;

import controller.command.CdController;
import controller.command.CommandController;
import utility.Validator;
import view.CmdView;

import java.util.ArrayList;

// command를 보고 그에 알맞는 controller를 반환해줌
public class ControllerMapper {

    private final ArrayList<CommandController> controllerList;

    public ControllerMapper(ArrayList<CommandController> controllerList) {
        this.controllerList = controllerList;
    }

    public CommandController getMappedController(String command) {

        switch (command) {
            case "cd":
                return new CdController(new CmdView(), new Validator());
            case "dir":
                return controllerList.get(1);
            case "copy":
                return controllerList.get(2);
            case "move":
                return controllerList.get(3);
            case "help":
                return controllerList.get(4);
            case "cls":
                return controllerList.get(5);
            case "exit":
                return controllerList.get(6);
            default:
                return null;
        }
    }
}
