package view;

import javax.swing.*;

public class MyTextField extends JTextField {
    private String name;

    private int minLength;
    private int maxLength;

    private String regex;
    private JPanel parentPanel;

    private String lengthMessage;
    private String regexMessage;

    public MyTextField(String name, int minLength, int maxLength, String regex, JPanel panel){
        this.name = name;
        this.minLength = minLength;
        this.maxLength = maxLength;
        this.regex = regex;
        this.parentPanel = panel;

        this.lengthMessage = name + " 길이는 " + minLength + " ~ " + maxLength + "입니다";
        this.regexMessage = name + "서식을 지켜주세요";
    }

    public boolean checkValidity(){

        if(this.getText().length() < minLength || this.getText().length() > maxLength){
            JOptionPane.showMessageDialog(parentPanel, lengthMessage);
            return false;
        }

        if(!super.getText().matches(regex)) {
            JOptionPane.showMessageDialog(parentPanel, regexMessage);
            return false;
        }

        return true;
    }
}
