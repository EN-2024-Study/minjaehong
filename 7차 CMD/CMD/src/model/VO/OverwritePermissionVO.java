package model.VO;

import Constants.OverwriteEnum;

public class OverwritePermissionVO {

    OverwriteEnum overwritePermission;

    public OverwritePermissionVO(String input){
        initialize(input);
    }

    private void initialize(String input){

        if(input.isEmpty()){
            overwritePermission = OverwriteEnum.WRONG_INPUT;
            return;
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
    }

    public OverwriteEnum getOverwritePermission() {
        return overwritePermission;
    }
}
