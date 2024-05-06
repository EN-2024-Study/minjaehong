package View.Panel;

import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.LineBorder;
import javax.swing.plaf.ButtonUI;
import java.awt.*;

// curNumberLabel에 지금입력한 값이 들어갈때가 있고
// 지금까지 입력한 결과값이 들어갈때가 있음

public class ResultPanel extends JPanel {

    JLabel curEquationLabel;
    JLabel curNumberLabel;
    JButton showLogButton;

    public ResultPanel(){
        createComponents();
        initializeResultPanel();
    }

    private void createComponents(){
        curEquationLabel = new JLabel("curEquation");
        //curEquationLabel.setBorder(BorderFactory.createLineBorder(Color.BLUE,3));

        curNumberLabel = new JLabel("curNumber");
        //curNumberLabel.setBorder(BorderFactory.createLineBorder(Color.BLUE,3));

        showLogButton = new JButton("X");
        //curNumberLabel.setBorder(BorderFactory.createLineBorder(Color.GREEN,3));
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
        add(curEquationLabel, gbc);

        gbc.gridx = 0;
        gbc.gridy = 2;
        gbc.gridwidth = 1;
        gbc.gridheight = 1;
        gbc.weightx = 1.0;
        gbc.weighty = 0.6;
        gbc.fill = GridBagConstraints.BOTH;
        add(curNumberLabel, gbc);
    }
}
