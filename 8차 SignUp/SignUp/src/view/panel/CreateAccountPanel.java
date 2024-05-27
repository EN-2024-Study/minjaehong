package view.panel;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;

public class CreateAccountPanel extends JPanel {

    JLabel idLabel;
    JTextField idTextField;
    public JButton idCheckButton;
    JLabel idInstructionLabel;

    JLabel pwLabel;
    JTextField pwTextField;
    JLabel pwInstructionLabel;

    JLabel nameLabel;
    JTextField nameTextField;
    JLabel nameInstructionLabel;

    JLabel phoneNumLabel;
    JTextField phoneNumTextField;
    public JButton phoneNumCheckButton;

    JLabel birthdayLabel;
    JTextField birthdayTextField;
    JLabel birthdayInstructionLabel;

    JLabel emailLabel;
    JTextField emailTextField;

    JLabel addressLabel;
    JTextField addressTextField;

    JLabel zipCodeLabel;
    JTextField zipCodeTextField;
    public JButton zipCodeCheckButton;

    public JButton submitButton;
    public JButton cancelButton;

    public CreateAccountPanel() {
        createComponents();
        initializePanel();
    }

    private void createComponents() {
        idLabel = new JLabel("ID", SwingConstants.RIGHT);
        idLabel.setBorder(new LineBorder(Color.BLACK));
        idTextField = new JTextField(15);
        idCheckButton = new JButton("중복확인");
        idCheckButton.setActionCommand("createAccountPanel_checkId");
        idInstructionLabel = new JLabel("only 14 words allowed");

        pwLabel = new JLabel("PW", SwingConstants.RIGHT);
        pwLabel.setBorder(new LineBorder(Color.BLACK));
        pwTextField = new JTextField(15);
        pwInstructionLabel = new JLabel("only 14 words allowed");

        nameLabel = new JLabel("NAME", SwingConstants.RIGHT);
        nameLabel.setBorder(new LineBorder(Color.BLACK));
        nameTextField = new JTextField(15);
        nameInstructionLabel = new JLabel("only 2-10 words allowed");

        phoneNumLabel = new JLabel("Phone Number", SwingConstants.RIGHT);
        phoneNumLabel.setBorder(new LineBorder(Color.BLACK));
        phoneNumTextField = new JTextField(15);
        phoneNumCheckButton = new JButton("중복확인");
        phoneNumCheckButton.setActionCommand("createAccountPanel_checkPhoneNum");

        birthdayLabel = new JLabel("Birthday", SwingConstants.RIGHT);
        birthdayLabel.setBorder(new LineBorder(Color.BLACK));
        birthdayTextField = new JTextField(15);
        birthdayInstructionLabel = new JLabel("ex) 010225");

        emailLabel = new JLabel("Email", SwingConstants.RIGHT);
        emailLabel.setBorder(new LineBorder(Color.BLACK));
        emailTextField = new JTextField(15);

        zipCodeLabel = new JLabel("ZipCode", SwingConstants.RIGHT);
        zipCodeLabel.setBorder(new LineBorder(Color.BLACK));
        zipCodeTextField = new JTextField(15);
        zipCodeCheckButton = new JButton("주소찾기");
        zipCodeCheckButton.setActionCommand("createAccountPanel_checkZipCode");

        addressLabel = new JLabel("Address", SwingConstants.RIGHT);
        addressLabel.setBorder(new LineBorder(Color.BLACK));
        addressTextField = new JTextField(15);

        submitButton = new JButton("CREATE ACCOUNT");
        submitButton.setActionCommand("createAccountPanel_createAccount");

        cancelButton = new JButton("CANCEL");
        cancelButton.setActionCommand("createAccountPanel_cancel");
    }

    private void initializePanel() {
        GridBagLayout grid = new GridBagLayout();
        this.setLayout(grid);

        GridBagConstraints gbc = new GridBagConstraints();
        gbc.gridheight = 1;
        gbc.anchor = GridBagConstraints.CENTER;

        // Center the components by calculating the center of a 10x10 grid
        int centerX = 5;

        // Adding the ID label, text field, check button, and instruction label
        gbc.gridx = centerX - 1;
        gbc.gridy = 2;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.insets = new Insets(10, 100, 5, 10); // Padding
        this.add(idLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 2;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 5, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(idTextField, gbc);

        gbc.gridx = centerX + 1;
        gbc.gridy = 2;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 5, 100); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(idCheckButton, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 3;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(5, 10, 10, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(idInstructionLabel, gbc);

        // Adding the password label, text field, and instruction label
        gbc.gridx = centerX - 1;
        gbc.gridy = 4;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 5, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(pwLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 4;
        gbc.gridwidth = 2;
        gbc.weightx = 0.8;
        gbc.insets = new Insets(10, 10, 5, 100); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(pwTextField, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 5;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(5, 10, 10, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(pwInstructionLabel, gbc);

        // Adding the name label, text field, and instruction label
        gbc.gridx = centerX - 1;
        gbc.gridy = 6;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 5, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(nameLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 6;
        gbc.gridwidth = 2;
        gbc.weightx = 0.8;
        gbc.insets = new Insets(10, 10, 5, 100); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(nameTextField, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 7;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(5, 10, 10, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(nameInstructionLabel, gbc);

        // Adding the phone number label, text field, and check button
        gbc.gridx = centerX - 1;
        gbc.gridy = 8;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 10, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(phoneNumLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 8;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 10, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(phoneNumTextField, gbc);

        gbc.gridx = centerX + 1;
        gbc.gridy = 8;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 10, 100); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(phoneNumCheckButton, gbc);

        // Adding the birthday label, text field, and instruction label
        gbc.gridx = centerX - 1;
        gbc.gridy = 9;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 5, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(birthdayLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 9;
        gbc.gridwidth = 2;
        gbc.weightx = 0.8;
        gbc.insets = new Insets(10, 10, 5, 100); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(birthdayTextField, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 10;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(5, 10, 10, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(birthdayInstructionLabel, gbc);

        // Adding the email label and text field
        gbc.gridx = centerX - 1;
        gbc.gridy = 11;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 10, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(emailLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 11;
        gbc.gridwidth = 2;
        gbc.weightx = 0.8;
        gbc.insets = new Insets(10, 10, 10, 100); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(emailTextField, gbc);

        // Adding the zip code label, text field, and check button
        gbc.gridx = centerX - 1;
        gbc.gridy = 12;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 10, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(zipCodeLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 12;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 10, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(zipCodeTextField, gbc);

        gbc.gridx = centerX + 1;
        gbc.gridy = 12;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 10, 100); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(zipCodeCheckButton, gbc);

        // Adding the address label and text field
        gbc.gridx = centerX - 1;
        gbc.gridy = 13;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 10, 10); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(addressLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 13;
        gbc.gridwidth = 2;
        gbc.weightx = 0.8;
        gbc.insets = new Insets(10, 10, 10, 100); // Padding
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(addressTextField, gbc);

        // Adding the create account button
        gbc.gridx = centerX - 1;
        gbc.gridy = 14;
        gbc.gridwidth = 3;
        gbc.weightx = 1.0;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.insets = new Insets(10, 100, 10, 100); // Padding
        this.add(submitButton, gbc);

        // Adding the cancel button
        gbc.gridx = centerX - 1;
        gbc.gridy = 15;
        gbc.gridwidth = 3;
        gbc.weightx = 1.0;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.insets = new Insets(10, 100, 10, 100); // Padding
        this.add(cancelButton, gbc);
    }
}
