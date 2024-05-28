package constant;

public class Querys {

    // READ
    public static final String checkIfIDExists ="SELECT EXISTS (SELECT TRUE FROM account WHERE userid = ?)";
    public static final String checkIfPhoneNumExists ="SELECT EXISTS (SELECT TRUE FROM account WHERE userphoneNum = ?)";
    public static final String checkIfValidLogin = "SELECT EXISTS (SELECT TRUE FROM account WHERE userid = ? AND userpw = ?)";
    public static final String getPWOfCertainID = "SELECT userpw FROM account WHERE userid = ?";

    // CREATE
    public static final String addAccount = "INSERT INTO account " +
            "(userid, userpw, username, userphonenum, userbirth, useremail, userzipcode, useraddress) " +
            "VALUES (?, ?, ?, ?, ?, ?, ?, ?)";

    // UPDATE
    public static final String updateAccount = "";

    // DELETE
    public static final String deleteAccount = "DELETE FROM account WHERE userid = ?";
}