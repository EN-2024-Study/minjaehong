package view.panel;

import javax.swing.*;

public class UserHomePanel extends JPanel {

    public JLabel userLabel;

    public JButton editButton;
    public JButton logOutButton;
    public JButton deleteButton;

    public String userId;

    public UserHomePanel() {
        createComponents();
        initializePanel();
    }

    public void setCookie(String userId){
        this.userId = userId;
        this.userLabel.setText(userId);
    }

    public void deleteCookie() {this.userId = "";}

    private void createComponents(){
        userLabel = new JLabel("");

        editButton = new JButton("EDIT");
        editButton.setActionCommand("userHomePanel_edit");

        logOutButton = new JButton("LOG OUT");
        logOutButton.setActionCommand("userHomePanel_logOut");

        deleteButton = new JButton("DELETE");
        deleteButton.setActionCommand("userHomePanel_delete");
    }

    private void initializePanel(){
        this.add(userLabel);
        this.add(editButton);
        this.add(logOutButton);
        this.add(deleteButton);
    }
}
