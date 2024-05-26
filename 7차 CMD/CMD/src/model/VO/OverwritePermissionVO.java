package model.VO;

import AAA.OverwriteEnum;

public class OverwritePermissionVO {

    private OverwriteEnum overwritePermission;

    public OverwritePermissionVO(String input){
        initializeVO(input);
    }

    private void initializeVO(String input){

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
