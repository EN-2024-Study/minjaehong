package handler;

import model.DTO.InputDTO;

import java.util.ArrayList;
import java.util.StringTokenizer;

// 클래스가 꼭 변수가 있어야하는건 아님!
// 클래스를 구조체라고 생각하면 안됨
// 그냥 특정 역할을 하는 함수만 있어도 됨!
// 이건 InputDTO를 받고 command랑 parameter로 분리해주는 작업을 함
public class InputHandler {
    // O(N)
    // 콜론을 빼주고 콜론 사이에 있는 공백을 ?로 바꿔서 내보냄
    private String modifyColonCombinedInput(String input){

        StringBuilder sb = new StringBuilder();
        boolean insideColon = false;

        for(int i=0;i<input.length();i++){
            char ch = input.charAt(i);

            if(ch == '\"'){
                insideColon = !insideColon;
                continue;
            }

            if(ch == ' ' && insideColon){
                sb.append('?');
            }else{
                sb.append(ch);
            }
        }

        return sb.toString();
    }

    // cd 일 경우 인자가 1개로 취급하기 때문에 공백 포함된게 ""로 안들어와도 됨
    // dir 일 경우 공백포함된 파일명 폴더명일 경우 "" 로 감싸서 들어옴
    public InputDTO handleInput(String input){

        String command;
        String parameterString;
        ArrayList<String> parameterList = new ArrayList<>();

        input = input.trim();
        input = input.toLowerCase();

        input = input.replace(',',' ');

        // 빈 문자열일때
        // controller에게 깡통 DTO return
        if(input.length()==0){
            command = " ";
            return new InputDTO("",parameterList);
        }

        if(input.startsWith("cd.") || input.startsWith("cd..") || input.startsWith("cd/") || input.startsWith("cd\\")){
            input = "cd " + input.substring(2);
        }

        // 만약 " 콜론을 포함한다면 콜론 없는 문자열로 변환
        if(input.contains("\"")){
            input = modifyColonCombinedInput(input);
        }

        // 여기서부터는 따옴표는 없고 공백이랑 따옴표 내 공백이 ?로 처리된 문자열 처리하면 되는거임
        int spaceIdx = input.indexOf(" ");

        // 공백이 없을때는 바로 return
        if(spaceIdx==-1){
            command = input;
            return new InputDTO(input, parameterList);
        }

        // 공백이 있으면 첫번째 공백을 기준으로 command 랑 parameterString 으로 나눠주기
        command = input.substring(0,spaceIdx);

        parameterString = input.substring(spaceIdx+1);

        // cd 일때는 따옴표 안들어가도 공백 인지 처리되는거 적용해주기 위해
        if(command.equals("cd")){
            parameterString = parameterString.trim();
            parameterString = parameterString.replace( ' ','?');
        }

        // dir copy move 에서 공백 포함된 문자열 들어왔을때를 위해
        StringTokenizer tokenizer = new StringTokenizer(parameterString, " ");
        while(tokenizer.hasMoreTokens()){
            String curToken = tokenizer.nextToken();
            parameterList.add(curToken.replace("?"," "));
        }

        // ?로 변환시키고 남은 공백 없애기
        for(int i=0;i< parameterList.size();i++){
            parameterList.set(i,parameterList.get(i).trim());
        }

        return new InputDTO(command, parameterList);
    }
}
