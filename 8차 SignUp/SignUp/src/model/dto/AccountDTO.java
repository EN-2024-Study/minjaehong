package model.dto;

public class AccountDTO {
    private final String userId;
    private final String userPw;
    private final String userName;
    private final String userPhoneNum;
    private final String userBirth;
    private final String userEmail;
    private final String userZipCode;
    private final String userAddress;

    public AccountDTO(String userId, String userPw, String userName, String userPhoneNum,
                      String userBirth, String userEmail, String userZipCode, String userAddress) {
        this.userId = userId;
        this.userPw = userPw;
        this.userName = userName;
        this.userPhoneNum = userPhoneNum;
        this.userBirth = userBirth;
        this.userEmail = userEmail;
        this.userZipCode = userZipCode;
        this.userAddress = userAddress;
    }

    public String getUserId() {
        return userId;
    }

    public String getUserPw() {
        return userPw;
    }

    public String getUserName() {
        return userName;
    }

    public String getUserPhoneNum() {
        return userPhoneNum;
    }

    public String getUserBirth() {
        return userBirth;
    }

    public String getUserEmail() {
        return userEmail;
    }

    public String getUserZipCode() {
        return userZipCode;
    }

    public String getUserAddress() {
        return userAddress;
    }
}
