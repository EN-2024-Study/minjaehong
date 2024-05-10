package View.Panel;

import javax.swing.*;
import java.awt.*;

public class ButtonPanel extends JPanel {

    private Font mainFont;
    private JButton[] buttons;

    private final String[] buttonName={
            "CE","C","<","÷",
            "7","8","9","×",
            "4","5","6","-",
            "1","2","3","+",
            "+/-","0",".","="
    };

    public ButtonPanel() {
        setBackground(Color.WHITE);
        createComponents();
        initializeButtonPanel();
    }

    private void initializeButtonPanel() {
        createComponents();

        GridBagLayout grid = new GridBagLayout();
        setLayout(grid);

        GridBagConstraints gbc = new GridBagConstraints();
        gbc.weightx = 1.0;
        gbc.weighty = 1.0;
        gbc.fill = GridBagConstraints.BOTH;
        gbc.insets = new Insets(1, 1, 1, 1); // Padding

        for (int row = 0; row < 5; row++) {
            for (int col = 0; col < 4; col++) {
                int curIdx = row * 4 + col;
                gbc.gridx = col;
                gbc.gridy = row;
                this.add(buttons[curIdx], gbc);
            }
        }
    }

    private void createComponents() {
        // ButtonPanel 에 사용할 Font 생성
        mainFont = new Font("Consolas",Font.BOLD,15);

        // ButtonPanel 에 올려둘 Button 생성
        buttons = new JButton[20];

        // Button 이름 초기화
        for(int row=0;row<5;row++){
            for(int column=0;column<4;column++){
                int curIdx = row*4 + column;
                buttons[curIdx] = new JButton(buttonName[curIdx]);
                buttons[curIdx].setFont(mainFont);
                buttons[curIdx].setBackground(Color.WHITE);
                buttons[curIdx].setFocusable(false);
            }
        }
        
        // 등호 버튼 색깔 예외처리
        buttons[19].setBackground(new Color(140,140,140));
    }

    public JButton getClearEntryButton() { return buttons[0]; }
    public JButton getClearButton() { return buttons[1]; }
    public JButton getBackSpaceButton() { return buttons[2]; }
    public JButton getDivButton() { return buttons[3]; }

    public JButton getNum7Button() { return buttons[4]; }
    public JButton getNum8Button() { return buttons[5]; }
    public JButton getNum9Button() { return buttons[6]; }
    public JButton getMulButton() { return buttons[7]; }

    public JButton getNum4Button() { return buttons[8]; }
    public JButton getNum5Button() { return buttons[9]; }
    public JButton getNum6Button() { return buttons[10]; }
    public JButton getSubButton() { return buttons[11]; }

    public JButton getNum1Button() { return buttons[12]; }
    public JButton getNum2Button() { return buttons[13]; }
    public JButton getNum3Button() { return buttons[14]; }
    public JButton getAddButton() { return buttons[15]; }

    public JButton getNegateButton() { return buttons[16]; }
    public JButton getNum0Button() { return buttons[17]; }
    public JButton getDotButton() { return buttons[18]; }
    public JButton getEqualButton() { return buttons[19]; }
}
