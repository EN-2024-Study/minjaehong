package view.panel;

public class EditAccountPanel extends CreateAccountPanel{

    public EditAccountPanel(){
        super();
        this.initializePanel();
    }

    private void initializePanel() {
        submitButton.setActionCommand("editAccountPanel_submit");
        cancelButton.setActionCommand("editAccountPanel_cancel");

        idLabel.setVisible(false);
        idTextField.setVisible(false);
        idCheckButton.setVisible(false);
        idInstructionLabel.setVisible(false);

        phoneNumLabel.setVisible(false);
        phoneNumCheckButton.setVisible(false);
        phoneNumTextField.setVisible(false);

        nameLabel.setVisible(false);
        nameInstructionLabel.setVisible(false);
        nameTextField.setVisible(false);

        birthdayLabel.setVisible(false);
        birthdayInstructionLabel.setVisible(false);
        birthdayTextField.setVisible(false);
    }

    public boolean returnTextFieldsValidity(){

        if(!pwTextField.checkValidity()) return false;
        if(!emailTextField.checkValidity()) return false;
        if(!addressTextField.checkValidity()) return false;
        if(!zipCodeTextField.checkValidity()) return false;

        return true;
    }
}
