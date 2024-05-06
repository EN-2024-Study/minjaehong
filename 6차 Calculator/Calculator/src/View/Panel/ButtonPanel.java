package View.Panel;

import javax.swing.*;
import java.awt.*;

public class ButtonPanel extends JPanel {

    public enum KeyBoard {
        CLEAR_ALL, CLEAR, BACKSPACE, DIVIDE,
        SEVEN, EIGHT, NINE, MULTIPLY,
        FOUR, FIVE, SIX, MINUS,
        ONE, TWO, THREE, PLUS,
        SIGN, ZERO, DOT, EQUAL
    }

    private JButton[] buttonArr;

    private String[] buttonName={
            "CE","C","<-","/",
            "7","8","9","X",
            "4","5","6","-",
            "1","2","3","+",
            "+/-","0",".","="
    };

    private JButton clearAllButton;
    private JButton clearButton;
    private JButton backSpaceButton;
    private JButton addButton;
    private JButton divButton;
    private JButton dotButton;
    private JButton equalButton;
    private JButton mulButton;
    private JButton signButton;
    private JButton subButton;

    private JButton num0Button;
    private JButton num1Button;
    private JButton num2Button;
    private JButton num3Button;
    private JButton num4Button;
    private JButton num5Button;
    private JButton num6Button;
    private JButton num7Button;
    private JButton num8Button;
    private JButton num9Button;

    private JScrollPane resultScrollPane;

    private void createComponents() {
        clearAllButton = new JButton("CE");
        clearButton = new JButton("C");
        backSpaceButton = new JButton("<");
        addButton = new JButton("+");
        divButton = new JButton("/");
        dotButton = new JButton(".");

        equalButton = new JButton("=");
        mulButton = new JButton("*");
        signButton = new JButton("+/-");
        subButton = new JButton("-");

        num0Button = new JButton("0");
        num1Button = new JButton("1");
        num2Button = new JButton("2");
        num3Button = new JButton("3");
        num4Button = new JButton("4");
        num5Button = new JButton("5");
        num6Button = new JButton("6");
        num7Button = new JButton("7");
        num8Button = new JButton("8");
        num9Button = new JButton("9");

        clearAllButton.setText("CE");
        clearAllButton.setFocusable(false);

        clearButton.setText("C");
        clearButton.setFocusable(false);

        backSpaceButton.setText("<");
        backSpaceButton.setFocusable(false);

        signButton.setText("+/-");
        signButton.setFocusable(false);

        divButton.setText("÷");
        divButton.setFocusable(false);

        mulButton.setText("×");
        mulButton.setFocusable(false);

        subButton.setText("-");
        subButton.setFocusable(false);

        addButton.setText("+");
        addButton.setFocusable(false);

        equalButton.setText("=");
        equalButton.setFocusable(false);

        dotButton.setText(".");
        dotButton.setFocusable(false);

        num1Button.setText("1");
        num1Button.setFocusable(false);

        num2Button.setText("2");
        num2Button.setFocusable(false);

        num3Button.setText("3");
        num3Button.setFocusable(false);

        num4Button.setText("4");
        num4Button.setFocusable(false);

        num5Button.setText("5");
        num5Button.setFocusable(false);

        num6Button.setText("6");
        num6Button.setFocusable(false);

        num7Button.setText("7");
        num7Button.setFocusable(false);

        num8Button.setText("8");
        num8Button.setFocusable(false);

        num9Button.setText("9");
        num9Button.setFocusable(false);

        num0Button.setText("0");
        num0Button.setFocusable(false);
    }

    private void initializeButtonPanel(){
        createComponents();

        GridBagLayout grid = new GridBagLayout();
        setLayout(grid);
        
        GridBagConstraints gbc = new GridBagConstraints();
        gbc.weightx = 1.0;
        gbc.weighty = 1.0;
        gbc.fill = GridBagConstraints.BOTH;
        gbc.insets = new Insets(5, 5, 5, 5); // Padding

        addButton(this, clearAllButton, gbc, 0, 0);
        addButton(this, clearButton, gbc, 0, 1);
        addButton(this, backSpaceButton, gbc, 0, 2);
        addButton(this, divButton, gbc, 0, 3);

        addButton(this, num7Button, gbc, 1, 0);
        addButton(this, num8Button, gbc, 1, 1);
        addButton(this, num9Button, gbc, 1, 2);
        addButton(this, mulButton, gbc, 1, 3);

        addButton(this, num4Button, gbc, 2, 0);
        addButton(this, num5Button, gbc, 2, 1);
        addButton(this, num6Button, gbc, 2, 2);
        addButton(this, subButton, gbc, 2, 3);

        addButton(this, num1Button, gbc, 3, 0);
        addButton(this, num2Button, gbc, 3, 1);
        addButton(this, num3Button, gbc, 3, 2);
        addButton(this, addButton, gbc, 3, 3);

        addButton(this, signButton, gbc, 4, 0);
        addButton(this, num0Button, gbc, 4, 1);
        addButton(this, dotButton, gbc, 4, 2);
        addButton(this, equalButton, gbc, 4, 3);
    }

    private void addButton(JPanel panel, JButton button, GridBagConstraints gbc, int row, int col) {
        gbc.gridx = col;
        gbc.gridy = row;
        panel.add(button, gbc);
    }

    public ButtonPanel() {
        createComponents();
        initializeButtonPanel();
    }
}
