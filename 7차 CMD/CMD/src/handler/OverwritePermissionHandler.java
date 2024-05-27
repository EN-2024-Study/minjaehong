package handler;

import constant.OverwriteEnum;
import model.DTO.OverwritePermissionDTO;

// String 값 받고 그걸 분석해서 OverwritePermissionDTO return
public class OverwritePermissionHandler {

    public OverwritePermissionDTO handleOverWritePermission(String input){

        OverwriteEnum overwritePermission;

        if(input.isEmpty()){
            overwritePermission = OverwriteEnum.WRONG_INPUT;
            return new OverwritePermissionDTO(overwritePermission);
        }

        input = input.toUpperCase();

        char firstCh = input.charAt(0);

        switch(firstCh){
            case 'Y':
                overwritePermission = OverwriteEnum.YES;
                break;
            case 'N':
                overwritePermission = OverwriteEnum.NO;
                break;
            case 'A':
                overwritePermission = OverwriteEnum.ALL;
                break;
            default:
                overwritePermission = OverwriteEnum.WRONG_INPUT;
        }

        return new OverwritePermissionDTO(overwritePermission);
    }
}
