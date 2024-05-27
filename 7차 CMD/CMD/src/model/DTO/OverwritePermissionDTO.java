package model.DTO;

import constant.OverwriteEnum;

public class OverwritePermissionDTO {

    private OverwriteEnum overwritePermission;

    public OverwritePermissionDTO(OverwriteEnum overwritePermission){ this.overwritePermission = overwritePermission; }
    
    public OverwriteEnum getOverwritePermission() {
        return overwritePermission;
    }
}
