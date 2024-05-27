package handler;

import constant.OverwriteEnum;
import model.DTO.OverwritePermissionDTO;

public class OverwritePermissionHandler {

    OverwriteEnum overwritePermission;

    public OverwritePermissionDTO handleOverWritePermission(String input){

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
