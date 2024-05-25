package model.VO;

// Service 나 Controller
public class MessageVO {
    private String message;

    public MessageVO(String message){
        this.message = message;
    }

    public String getMessage(){
        return message;
    }
}