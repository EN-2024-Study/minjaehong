package view.panel;

import view.MyTextField;

import javax.swing.*;
import java.awt.*;

public class LoginPanel extends JPanel {

    public JLabel idLabel;
    public MyTextField idTextField;

    public JLabel pwLabel;
    public MyTextField pwTextField;

    public JButton loginButton;
    public JButton findPasswordButton;
    public JButton createAccountButton;

    public LoginPanel() {
        createComponents();
        initializePanel();
    }

    private void createComponents(){
        idLabel = new JLabel("ID",SwingConstants.RIGHT);
        idTextField = new MyTextField("ID",8,12,"^([a-zA-Z0-9]{8,12})$",this);

        pwLabel = new JLabel("PW",SwingConstants.RIGHT);
        pwTextField = new MyTextField("PW",10,12,"^([a-zA-Z0-9~!@#]{8,12})$",this);

        loginButton = new JButton("LOGIN");
        loginButton.setActionCommand("loginPanel_login");

        findPasswordButton = new JButton("FIND PASSWORD");
        findPasswordButton.setActionCommand("loginPanel_findPassword");

        createAccountButton = new JButton("CREATE ACCOUNT");
        createAccountButton.setActionCommand("loginPanel_createAccount");
    }

    public boolean returnTextFieldsValidity(){
        if(!idTextField.checkValidity()) return false;
        if(!pwTextField.checkValidity()) return false;

        return true;
    }

    private void initializePanel(){
        GridBagLayout grid = new GridBagLayout();
        this.setLayout(grid);

        GridBagConstraints gbc = new GridBagConstraints();
        gbc.gridheight = 1;
        gbc.anchor = GridBagConstraints.CENTER;

        int centerX = 5;

        gbc.gridx = centerX - 1;
        gbc.gridy = 2;
        gbc.gridwidth = 1;
        gbc.weightx = 0.3;
        gbc.fill = GridBagConstraints.NONE;
        gbc.insets = new Insets(10, 100, 10, 30);
        this.add(idLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 2;
        gbc.gridwidth = 1;
        gbc.weightx = 0.7;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.insets = new Insets(10, 30, 10, 100);
        this.add(idTextField, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 3;
        gbc.gridwidth = 1;
        gbc.weightx = 0.3;
        gbc.insets = new Insets(10, 100, 10, 30);
        gbc.fill = GridBagConstraints.NONE;
        this.add(pwLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 3;
        gbc.gridwidth = 1;
        gbc.weightx = 0.7;
        gbc.insets = new Insets(10, 30, 10, 100);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(pwTextField, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 4;
        gbc.gridwidth = 2;
        gbc.weightx = 1.0;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.insets = new Insets(10, 100, 10, 100);
        this.add(loginButton, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 5;
        gbc.gridwidth = 2;
        gbc.weightx = 1.0;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.insets = new Insets(10, 100, 10, 100);
        this.add(findPasswordButton, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 6;
        gbc.gridwidth = 2;
        gbc.weightx = 1.0;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.insets = new Insets(10, 100, 10, 100);
        this.add(createAccountButton, gbc);
    }
}
