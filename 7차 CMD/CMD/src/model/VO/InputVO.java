package model.VO;

public class InputVO {

    private String command;
    private String parameters;

    public InputVO(String input){
        initializeInputDTO(input);
    }

    private void initializeInputDTO(String input){

        // 빈 문자열일때
        if(input.length()==0){
            command = " ";
            parameters = "";
            return;
        }

        int spaceIdx = input.indexOf(" ");

        // 공백이 없을때
        if(spaceIdx==-1){
            command = input;
            parameters = "";
        }
        // 공백이 있으면 첫번째 공백을 기준으로 나눠주기
        else{
            command = input.substring(0,spaceIdx);
            parameters = input.substring(spaceIdx+1);
        }
    }

    public String getCommand(){
        return command;
    }

    public String getParameters(){
        return parameters;
    }
}
