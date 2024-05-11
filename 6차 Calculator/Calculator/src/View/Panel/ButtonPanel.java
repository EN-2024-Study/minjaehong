package View.Panel;

import javax.swing.*;
import java.awt.*;

public class ButtonPanel extends JPanel {

    private Font mainFont;
    private JButton[] buttonArr;

    private final String[] buttonName={
            "CE","C","<","÷",
            "7","8","9","×",
            "4","5","6","-",
            "1","2","3","+",
            "+/-","0",".","="
    };

    public ButtonPanel() {
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
                this.add(buttonArr[curIdx], gbc);
            }
        }
    }

    private void createComponents() {
        // ButtonPanel 에 사용할 Font 생성
        mainFont = new Font("Consolas",Font.BOLD,15);

        // ButtonPanel 에 올려둘 Button 생성
        buttonArr = new JButton[20];

        // Button 이름 초기화
        for(int row=0;row<5;row++){
            for(int column=0;column<4;column++){
                int curIdx = row*4 + column;
                buttonArr[curIdx] = new JButton(buttonName[curIdx]);
                buttonArr[curIdx].setFont(mainFont);
                buttonArr[curIdx].setBackground(Color.WHITE);
                buttonArr[curIdx].setFocusable(false);
            }
        }
        
        // 등호 버튼 색깔 예외처리
        buttonArr[19].setBackground(new Color(140,140,140));
    }

    public JButton[] getButtonArray() { return buttonArr; }

    public JButton getClearEntryButton() { return buttonArr[0]; }
    public JButton getClearButton() { return buttonArr[1]; }
    public JButton getBackSpaceButton() { return buttonArr[2]; }
    public JButton getDivButton() { return buttonArr[3]; }

    public JButton getNum7Button() { return buttonArr[4]; }
    public JButton getNum8Button() { return buttonArr[5]; }
    public JButton getNum9Button() { return buttonArr[6]; }
    public JButton getMulButton() { return buttonArr[7]; }

    public JButton getNum4Button() { return buttonArr[8]; }
    public JButton getNum5Button() { return buttonArr[9]; }
    public JButton getNum6Button() { return buttonArr[10]; }
    public JButton getSubButton() { return buttonArr[11]; }

    public JButton getNum1Button() { return buttonArr[12]; }
    public JButton getNum2Button() { return buttonArr[13]; }
    public JButton getNum3Button() { return buttonArr[14]; }
    public JButton getAddButton() { return buttonArr[15]; }

    public JButton getNegateButton() { return buttonArr[16]; }
    public JButton getNum0Button() { return buttonArr[17]; }
    public JButton getDotButton() { return buttonArr[18]; }
    public JButton getEqualButton() { return buttonArr[19]; }
}
