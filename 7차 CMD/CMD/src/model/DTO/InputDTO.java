package model.DTO;

import java.util.List;

// InputHandler가 command랑 parameters로 생성해서
// controller에게 줌
public class InputDTO {

    private String command;
    private List<String> parameters;

    public InputDTO(String command, List<String> parameters){
        this.command = command;
        this.parameters = parameters;
    }

    public String getCommand(){
        return command;
    }
    public List<String> getParameters(){ return parameters; }
}