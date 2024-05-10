package View.Panel;

import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.LineBorder;
import javax.swing.plaf.ButtonUI;
import java.awt.*;

// curNumberLabel에 지금입력한 값이 들어갈때가 있고
// 지금까지 입력한 결과값이 들어갈때가 있음

public class ResultPanel extends JPanel {

    JLabel smallLabel; // 연산자 눌렸을때만 바뀜
    JLabel bigLabel; // 숫자 + 연산자 눌렀을때 둘 다 바뀜
    JButton showLogButton;

    public ResultPanel(){
        createComponents();
        initializeResultPanel();
    }

    private void createComponents(){

        setBackground(Color.WHITE);

        smallLabel = new JLabel(" ");
        smallLabel.setFont(new Font("Consolas", Font.BOLD, 16));
        smallLabel.setBackground(Color.WHITE);
        smallLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        smallLabel.setVerticalAlignment(SwingConstants.BOTTOM);
        //Border border = BorderFactory.createLineBorder(Color.RED,5); // Create a LineBorder with default color (black)
        //smallLabel.setBorder(border);

        bigLabel = new JLabel("0");
        bigLabel.setFont(new Font("Consolas", Font.BOLD, 36));
        bigLabel.setBackground(Color.WHITE);
        bigLabel.setHorizontalAlignment(SwingConstants.RIGHT);
        //border = BorderFactory.createLineBorder(Color.BLUE,5); // Create a LineBorder with default color (black)
        //bigLabel.setBorder(border);

        showLogButton = new JButton("X");
    }

    private void initializeResultPanel(){

        createComponents();

        GridBagLayout grid = new GridBagLayout();
        setLayout(grid);

        GridBagConstraints gbc = new GridBagConstraints();

        /*
        // 로그버튼
        gbc.gridx = 0;
        gbc.gridy = 0;
        // 컴포넌트의 디폴트 크기에 대한 폭과 높이의 소속 배율
        gbc.gridwidth = 1;
        gbc.gridheight = 1;
        // 컴포넌트 각 영역의 크기 비율
        gbc.weightx = 1.0;
        gbc.weighty = 0.15;
        // 영역을 채우기 위한 속성 지정
        add(showLogButton, gbc);
        */

        // grid 내에서 [x][y] 위치
        gbc.gridx = 0;
        gbc.gridy = 1;
        // 컴포넌트의 디폴트 크기에 대한 폭과 높이의 소속 배율
        gbc.gridwidth = 1;
        gbc.gridheight = 1;
        // 컴포넌트 각 영역의 크기 비율
        gbc.weightx = 1.0;
        gbc.weighty = 0.25;
        // 영역을 채우기 위한 속성 지정
        gbc.fill = GridBagConstraints.BOTH;
        add(smallLabel, gbc);

        gbc.gridx = 0;
        gbc.gridy = 2;
        gbc.gridwidth = 1;
        gbc.gridheight = 1;
        gbc.weightx = 1.0;
        gbc.weighty = 0.6;
        gbc.fill = GridBagConstraints.BOTH;
        add(bigLabel, gbc);
    }

    public JLabel getSmallLabel(){
        return smallLabel;
    }

    public JLabel getBigLabel() {
        return bigLabel;
    }

    public void setSmallLabel(String equation){
        smallLabel.setText(equation);
    }

    public void setBigLabel(String input){
        bigLabel.setText(input);
    }

    public boolean isFull(){
        if(bigLabel.getText().length()==21) return true;
        else return false;
    }
}
