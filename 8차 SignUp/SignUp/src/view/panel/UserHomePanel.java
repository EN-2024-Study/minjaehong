package view.panel;

import javax.swing.*;

public class UserHomePanel extends JPanel {

    public JButton editButton;
    public JButton logOutButton;
    public JButton deleteButton;

    public UserHomePanel() {
        createComponents();
        initializePanel();
    }

    private void createComponents(){
        editButton = new JButton("EDIT");
        editButton.setActionCommand("userHomePanel_edit");

        logOutButton = new JButton("LOG OUT");
        logOutButton.setActionCommand("userHomePanel_logOut");

        deleteButton = new JButton("DELETE");
        deleteButton.setActionCommand("userHomePanel_delete");
    }

    private void initializePanel(){
        this.add(editButton);
        this.add(logOutButton);
        this.add(deleteButton);
    }
}
