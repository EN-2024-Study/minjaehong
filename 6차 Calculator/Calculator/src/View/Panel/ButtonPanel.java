package View.Panel;

import javax.swing.*;
import java.awt.*;

public class ButtonPanel extends JPanel {

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

    private JButton addButton;
    private JButton divButton;
    private JButton dotButton;
    private JButton equalButton;
    private JButton mulButton;
    private JButton negateButton;
    private JButton subButton;

    private JButton clearEntryButton;
    private JButton clearButton;
    private JButton backSpaceButton;

    private JScrollPane resultScrollPane;

    private Font mainFont;

    public ButtonPanel() {
        // 필요한 버튼 모두 생성
        createComponents();
        // Panel 의 맞는 위치에 붙이기
        initializeButtonPanel();
    }
    
    private void createComponents() {
        mainFont = new Font("Consolas",Font.BOLD,15);

        clearEntryButton = new JButton("CE");
        clearButton = new JButton("C");
        backSpaceButton = new JButton("<");
        addButton = new JButton("+");
        divButton = new JButton("/");
        dotButton = new JButton(".");

        equalButton = new JButton("=");
        mulButton = new JButton("*");
        negateButton = new JButton("+/-");
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

        clearEntryButton.setText("CE");
        clearEntryButton.setFont(mainFont);
        clearEntryButton.setBackground(Color.ORANGE);
        clearEntryButton.setFocusable(false);

        clearButton.setText("C");
        clearButton.setFont(mainFont);
        clearButton.setBackground(Color.ORANGE);
        clearButton.setFocusable(false);

        backSpaceButton.setText("<");
        backSpaceButton.setFont(mainFont);
        backSpaceButton.setBackground(Color.ORANGE);
        backSpaceButton.setFocusable(false);

        negateButton.setText("+/-");
        negateButton.setFont(mainFont);
        negateButton.setBackground(Color.ORANGE);
        negateButton.setFocusable(false);

        divButton.setText("÷");
        divButton.setFont(mainFont);
        divButton.setBackground(Color.ORANGE);
        divButton.setFocusable(false);

        mulButton.setText("×");
        mulButton.setFont(mainFont);
        mulButton.setBackground(Color.ORANGE);
        mulButton.setFocusable(false);

        subButton.setText("-");
        subButton.setFont(mainFont);
        subButton.setBackground(Color.ORANGE);
        subButton.setFocusable(false);

        addButton.setText("+");
        addButton.setFont(mainFont);
        addButton.setBackground(Color.ORANGE);
        addButton.setFocusable(false);

        equalButton.setText("=");
        equalButton.setFont(mainFont);
        equalButton.setBackground(Color.RED);
        equalButton.setFocusable(false);

        dotButton.setText(".");
        dotButton.setFont(mainFont);
        dotButton.setBackground(Color.ORANGE);
        dotButton.setFocusable(false);

        num1Button.setText("1");
        num1Button.setFont(mainFont);
        num1Button.setBackground(Color.WHITE);
        num1Button.setFocusable(false);

        num2Button.setText("2");
        num2Button.setFont(mainFont);
        num2Button.setBackground(Color.WHITE);
        num2Button.setFocusable(false);

        num3Button.setText("3");
        num3Button.setFont(mainFont);
        num3Button.setBackground(Color.WHITE);
        num3Button.setFocusable(false);

        num4Button.setText("4");
        num4Button.setFont(mainFont);
        num4Button.setBackground(Color.WHITE);
        num4Button.setFocusable(false);

        num5Button.setText("5");
        num5Button.setFont(mainFont);
        num5Button.setBackground(Color.WHITE);
        num5Button.setFocusable(false);

        num6Button.setText("6");
        num6Button.setFont(mainFont);
        num6Button.setBackground(Color.WHITE);
        num6Button.setFocusable(false);

        num7Button.setText("7");
        num7Button.setFont(mainFont);
        num7Button.setBackground(Color.WHITE);
        num7Button.setFocusable(false);

        num8Button.setText("8");
        num8Button.setFont(mainFont);
        num8Button.setBackground(Color.WHITE);
        num8Button.setFocusable(false);

        num9Button.setText("9");
        num9Button.setFont(mainFont);
        num9Button.setBackground(Color.WHITE);
        num9Button.setFocusable(false);

        num0Button.setText("0");
        num0Button.setFont(mainFont);
        num0Button.setBackground(Color.WHITE);
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
        gbc.insets = new Insets(1, 1, 1, 1); // Padding

        addButton(this, clearEntryButton, gbc, 0, 0);
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

        addButton(this, negateButton, gbc, 4, 0);
        addButton(this, num0Button, gbc, 4, 1);
        addButton(this, dotButton, gbc, 4, 2);
        addButton(this, equalButton, gbc, 4, 3);
    }

    private void addButton(JPanel panel, JButton button, GridBagConstraints gbc, int row, int col) {
        gbc.gridx = col;
        gbc.gridy = row;
        panel.add(button, gbc);
    }

    public JButton getNum0Button() { return num0Button; }
    public JButton getNum1Button() { return num1Button; }
    public JButton getNum2Button() { return num2Button; }
    public JButton getNum3Button() { return num3Button; }
    public JButton getNum4Button() { return num4Button; }
    public JButton getNum5Button() { return num5Button; }
    public JButton getNum6Button() { return num6Button; }
    public JButton getNum7Button() { return num7Button; }
    public JButton getNum8Button() { return num8Button; }
    public JButton getNum9Button() { return num9Button; }
    public JButton getDotButton() { return dotButton; }

    public JButton getClearEntryButton() { return clearEntryButton; }
    public JButton getClearButton() { return clearButton; }
    public JButton getBackSpaceButton() { return backSpaceButton; }

    public JButton getAddButton() { return addButton; }
    public JButton getSubButton() { return subButton; }
    public JButton getMulButton() { return mulButton; }
    public JButton getDivButton() { return divButton; }

    public JButton getEqualButton() { return equalButton; }

    public JButton getNegateButton() { return negateButton; }
}
