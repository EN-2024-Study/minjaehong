package service;

import model.dao.AccountDAO;
import model.dto.AccountDTO;
import model.dto.LoginDTO;
import model.dto.ValueDTO;

public class SignUpService {

    private AccountDAO accountDAO;

    public SignUpService(){
        this.accountDAO = AccountDAO.GetInstance();
    }

    // READ
    public boolean checkIfIDExists(ValueDTO valueDTO){
        if(accountDAO.checkIfIDExists(valueDTO)) return true;
        return false;
    }

    public boolean checkIfPhoneNumExists(ValueDTO valueDTO){
        if(accountDAO.checkIfPhoneNumExists(valueDTO)) return true;
        return false;
    }

    public boolean checkIfValidLogin(LoginDTO loginDTO){
        if(accountDAO.checkIfValidLogin(loginDTO)) return true;
        return false;
    }

    public ValueDTO getPWOfCertainID(ValueDTO valueDTO){
        
        // ID가 존재자체를 안하면 깡통 DTO 보내기
        if(!checkIfIDExists(valueDTO)){
            return new ValueDTO("");
        }
        
        return accountDAO.getPWOfCertainID(valueDTO);
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
    public void deleteAccount(ValueDTO valueDTO){
        accountDAO.Delete(valueDTO);
    }
}