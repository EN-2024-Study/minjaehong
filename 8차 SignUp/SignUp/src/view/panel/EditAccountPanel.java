package view.panel;

public class EditAccountPanel extends CreateAccountPanel{

    public EditAccountPanel(){
        super();
        this.createComponents();
        this.initializePanel();
    }

    private void createComponents(){
        idCheckButton.setActionCommand("editAccountPanel_checkId");
        phoneNumCheckButton.setActionCommand("editAccountPanel_checkPhoneNum");
        zipCodeCheckButton.setActionCommand("editAccountPanel_checkPhoneNum");

        submitButton.setActionCommand("editAccountPanel_submit");
        cancelButton.setActionCommand("editAccountPanel_cancel");
    }

    private void initializePanel() {
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
}
