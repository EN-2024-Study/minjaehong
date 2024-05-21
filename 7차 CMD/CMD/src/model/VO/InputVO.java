package model.VO;

import java.util.ArrayList;
import java.util.List;
import java.util.StringTokenizer;

public class InputVO {

    private String command;
    private List<String> parameters;
    
    public InputVO(String input){
        initializeInputDTO(input);
    }

    // O(N)
    private String modifyColonCombinedInput(String input){
        StringBuilder sb = new StringBuilder();

        boolean insideColon = false;
        for(int i=0;i<input.length();i++){
            char ch = input.charAt(i);

            if(ch == '"'){
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

    // 공백포함된 파일명 폴더명일 경우 "" 로 감싸서 들어옴
    private void initializeInputDTO(String input){

        String parameterString;
        parameters = new ArrayList<>();

        // 빈 문자열일때
        if(input.length()==0){
            command = " ";
            return;
        }

        // cd. cd.. cd\ cd/ 로 시작하는거 예외처리
        if(input.startsWith("cd.") || input.startsWith("cd..") || input.startsWith("cd/") || input.startsWith("cd\\")){
            input = "cd " + input.substring(2);
        }
        
        // 만약 " 콜론을 포함한다면 콜론 없는 문자열로 변환
        if(input.contains("\"")){
            input = modifyColonCombinedInput(input);
        }

        // 여기서부터는 따옴표는 없고 공백이랑 따옴표 내 공백은 물음표 처리된 문자열 처리하는거임
        int spaceIdx = input.indexOf(" ");

        // 공백이 없을때
        if(spaceIdx==-1){
            command = input;
            return;
        }
        // 공백이 있으면 첫번째 공백을 기준으로 command 랑 parameterString 으로 나눠주기
        else{
            command = input.substring(0,spaceIdx);
            parameterString = input.substring(spaceIdx+1);
        }

        // cd a.txt
        // cd a?and?b?and?c.txt
        // cd aaa????bcd.txt
        StringTokenizer tokenizer = new StringTokenizer(parameterString, " ");
        while(tokenizer.hasMoreTokens()){
            String curToken = tokenizer.nextToken();
            parameters.add(curToken.replace("?"," "));
        }
    }

    public String getCommand(){
        return command;
    }

    public List<String> getParameters(){
        return parameters;
    }
}