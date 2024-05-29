package view.panel;

import view.MyTextField;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;

public class CreateAccountPanel extends JPanel {

    public JLabel idLabel;
    public MyTextField idTextField;
    public JButton idCheckButton;
    public JLabel idInstructionLabel;

    public JLabel pwLabel;
    public MyTextField pwTextField;
    public JLabel pwInstructionLabel;

    public JLabel nameLabel;
    public MyTextField nameTextField;
    public JLabel nameInstructionLabel;

    public JLabel phoneNumLabel;
    public MyTextField phoneNumTextField;
    public JButton phoneNumCheckButton;

    public JLabel birthdayLabel;
    public MyTextField birthdayTextField;
    public JLabel birthdayInstructionLabel;

    public JLabel emailLabel;
    public MyTextField emailTextField;

    public JLabel addressLabel;
    public MyTextField addressTextField;

    public JLabel zipCodeLabel;
    public MyTextField zipCodeTextField;
    public JButton zipCodeCheckButton;

    public JButton submitButton;
    public JButton cancelButton;

    public boolean returnTextFieldsValidity(){
        if(!idTextField.checkValidity()) return false;
        if(!pwTextField.checkValidity()) return false;
        if(!nameTextField.checkValidity()) return false;
        if(!phoneNumTextField.checkValidity()) return false;
        if(!birthdayTextField.checkValidity()) return false;
        if(!emailTextField.checkValidity()) return false;
        if(!addressTextField.checkValidity()) return false;
        if(!zipCodeTextField.checkValidity()) return false;

        return true;
    }

    public CreateAccountPanel() {
        createComponents();
        initializePanel();
    }

    private void createComponents() {
        idLabel = new JLabel("ID", SwingConstants.RIGHT);
        idLabel.setBorder(new LineBorder(Color.BLACK));
        idTextField = new MyTextField("ID",8,12,"^([a-zA-Z0-9]{8,12})$",this);
        idCheckButton = new JButton("중복 체크");
        idCheckButton.setActionCommand("createAccountPanel_checkId");
        idInstructionLabel = new JLabel("영문 대소문자 + 숫자 (8~12글자)");

        pwLabel = new JLabel("PW", SwingConstants.RIGHT);
        pwLabel.setBorder(new LineBorder(Color.BLACK));
        pwTextField = new MyTextField("PW",8,12,"^([a-zA-Z0-9~!@#]{8,12})$",this);
        pwInstructionLabel = new JLabel("영어 대소문자 + 숫자 + 특수기호 8~12글자");

        nameLabel = new JLabel("NAME", SwingConstants.RIGHT);
        nameLabel.setBorder(new LineBorder(Color.BLACK));
        nameTextField = new MyTextField("NAME",4,10,"^([a-zA-Z가-힣]{4,10})$",this);
        nameInstructionLabel = new JLabel("이름을 적어주세요");

        phoneNumLabel = new JLabel("PHONENUM", SwingConstants.RIGHT);
        phoneNumLabel.setBorder(new LineBorder(Color.BLACK));
        phoneNumTextField = new MyTextField("PHONENUM",13,13, "^(010-(\\d{4})-\\d{4})$",this);
        phoneNumCheckButton = new JButton("중복 체크");
        phoneNumCheckButton.setActionCommand("createAccountPanel_checkPhoneNum");

        birthdayLabel = new JLabel("BIRTHDAY", SwingConstants.RIGHT);
        birthdayLabel.setBorder(new LineBorder(Color.BLACK));
        birthdayTextField = new MyTextField("NAME",6,6,"^([0-9]{6})$",this);
        birthdayInstructionLabel = new JLabel("6자리 생년월일 적어주세요");

        emailLabel = new JLabel("EMAIL", SwingConstants.RIGHT);
        emailLabel.setBorder(new LineBorder(Color.BLACK));
        emailTextField = new MyTextField("EMAIL",10,20,"^([a-zA-Z0-9]+@[a-zA-Z0-9]+\\.[a-z]{2,3}+)$",this);

        zipCodeLabel = new JLabel("ZIPCODE", SwingConstants.RIGHT);
        zipCodeLabel.setBorder(new LineBorder(Color.BLACK));
        zipCodeTextField = new MyTextField("ZIPCODE",5,5,"^([0-9]{5,5})$",this);
        zipCodeCheckButton = new JButton("주소 찾기");
        zipCodeCheckButton.setActionCommand("createAccountPanel_checkZipCode");

        addressLabel = new JLabel("ADDRESS", SwingConstants.RIGHT);
        addressLabel.setBorder(new LineBorder(Color.BLACK));
        addressTextField = new MyTextField("ADDRESS",10,20,"^([가-힣]{10,20})$",this);

        submitButton = new JButton("SUBMIT");
        submitButton.setActionCommand("createAccountPanel_submit");

        cancelButton = new JButton("CANCEL");
        cancelButton.setActionCommand("createAccountPanel_cancel");
    }

    private void initializePanel() {
        GridBagLayout grid = new GridBagLayout();
        this.setLayout(grid);

        GridBagConstraints gbc = new GridBagConstraints();
        gbc.gridheight = 1;
        gbc.anchor = GridBagConstraints.CENTER;

        int centerX = 5;

        gbc.gridx = centerX - 1;
        gbc.gridy = 2;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.insets = new Insets(10, 100, 5, 10);
        this.add(idLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 2;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 5, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(idTextField, gbc);

        gbc.gridx = centerX + 1;
        gbc.gridy = 2;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 5, 100);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(idCheckButton, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 3;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(5, 10, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(idInstructionLabel, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 4;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 5, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(pwLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 4;
        gbc.gridwidth = 2;
        gbc.weightx = 0.8;
        gbc.insets = new Insets(10, 10, 5, 100);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(pwTextField, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 5;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(5, 10, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(pwInstructionLabel, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 6;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 5, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(nameLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 6;
        gbc.gridwidth = 2;
        gbc.weightx = 0.8;
        gbc.insets = new Insets(10, 10, 5, 100);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(nameTextField, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 7;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(5, 10, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(nameInstructionLabel, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 8;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(phoneNumLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 8;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(phoneNumTextField, gbc);

        gbc.gridx = centerX + 1;
        gbc.gridy = 8;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 10, 100);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(phoneNumCheckButton, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 9;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 5, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(birthdayLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 9;
        gbc.gridwidth = 2;
        gbc.weightx = 0.8;
        gbc.insets = new Insets(10, 10, 5, 100);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(birthdayTextField, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 10;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(5, 10, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(birthdayInstructionLabel, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 11;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(emailLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 11;
        gbc.gridwidth = 2;
        gbc.weightx = 0.8;
        gbc.insets = new Insets(10, 10, 10, 100);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(emailTextField, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 12;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(zipCodeLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 12;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(zipCodeTextField, gbc);

        gbc.gridx = centerX + 1;
        gbc.gridy = 12;
        gbc.gridwidth = 1;
        gbc.weightx = 0.4;
        gbc.insets = new Insets(10, 10, 10, 100);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(zipCodeCheckButton, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 13;
        gbc.gridwidth = 1;
        gbc.weightx = 0.2;
        gbc.insets = new Insets(10, 100, 10, 10);
        gbc.fill = GridBagConstraints.HORIZONTAL;
        this.add(addressLabel, gbc);

        gbc.gridx = centerX;
        gbc.gridy = 13;
        gbc.gridwidth = 2;
        gbc.weightx = 0.8;
        gbc.insets = new Insets(10, 10, 10, 100);
        this.add(addressTextField, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 14;
        gbc.gridwidth = 3;
        gbc.weightx = 1.0;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.insets = new Insets(10, 100, 10, 100);
        this.add(submitButton, gbc);

        gbc.gridx = centerX - 1;
        gbc.gridy = 15;
        gbc.gridwidth = 3;
        gbc.weightx = 1.0;
        gbc.fill = GridBagConstraints.HORIZONTAL;
        gbc.insets = new Insets(10, 100, 10, 100);
        this.add(cancelButton, gbc);
    }
}
