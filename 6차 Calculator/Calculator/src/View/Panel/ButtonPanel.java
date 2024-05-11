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

    public JButton[] getButtonArray() { return buttonArr; }

    // 0 으로 나눌 수 없습니다 뜰때 enabled = false 시켜야되서 필요한 getter 들
    public JButton getDivButton() { return buttonArr[3]; }
    public JButton getMulButton() { return buttonArr[7]; }
    public JButton getSubButton() { return buttonArr[11]; }
    public JButton getAddButton() { return buttonArr[15]; }
    public JButton getNegateButton() { return buttonArr[16]; }
    public JButton getDotButton() { return buttonArr[18]; }
}
