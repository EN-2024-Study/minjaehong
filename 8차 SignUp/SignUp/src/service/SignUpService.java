package service;

import model.dao.AccountDAO;
import model.dto.AccountDTO;
import model.dto.TextFieldDTO;

public class SignUpService {

    private AccountDAO accountDAO;

    public SignUpService(){
        this.accountDAO = AccountDAO.GetInstance();
    }

    // READ
    public boolean checkIfIDExists(TextFieldDTO textFieldDTO){
        if(accountDAO.checkIfIDExists(textFieldDTO)) return true;
        return false;
    }

    public boolean checkIfPhoneNumExists(TextFieldDTO textFieldDTO){
        if(accountDAO.checkIfPhoneNumExists(textFieldDTO)) return true;
        return false;
    }

    public boolean checkIfValidLogin(TextFieldDTO textFieldDTO){
        if(accountDAO.checkIfValidLogin(textFieldDTO)) return true;
        return false;
    }

    public TextFieldDTO getPWOfCertainID(TextFieldDTO textFieldDTO){
        
        // ID가 존재자체를 안하면 깡통 DTO 보내기
        if(!checkIfIDExists(textFieldDTO)){
            return new TextFieldDTO("");
        }
        
        return accountDAO.getPWOfCertainID(textFieldDTO);
    }

    // CREATE
    public void addAccount(AccountDTO accountDTO){
        accountDAO.Add(accountDTO);
    }

    // UPDATE
    public boolean updateAccount(AccountDTO accountDTO){
        return true;
    }

    // DELETE
    public void deleteAccount(TextFieldDTO textFieldDTO){
        accountDAO.Delete(textFieldDTO);
    }
}