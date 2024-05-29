package model.dto;

public class UpdateInfoDTO {
    private String userPW;
    private String useremail;
    private String userzipcode;
    private String useraddress;

    public UpdateInfoDTO(String userPW, String useremail, String userzipcode, String useraddress){
        this.userPW = userPW;
        this.useremail = useremail;
        this.userzipcode = userzipcode;
        this.useraddress = useraddress;
    }

    public String getUserPW() {
        return userPW;
    }

    public String getUserEmail() {
        return useremail;
    }

    public String getUserZipcode() {
        return userzipcode;
    }

    public String getUserAddress() {
        return useraddress;
    }
}
