package View.Panel;

import javax.swing.*;
import javax.swing.border.Border;
import javax.swing.border.LineBorder;
import java.awt.*;

public class ResultPanel extends JPanel {

    JLabel curEquation;
    JLabel curNumber;

    private void createComponents(){
        curEquation = new JLabel("curEquation");
        curEquation.setBorder(BorderFactory.createLineBorder(Color.BLUE,3));

        curNumber = new JLabel("curNumber");
        curNumber.setBorder(BorderFactory.createLineBorder(Color.BLUE,3));
    }

    private void initializeResultPanel(){

        createComponents();

        GridBagLayout grid = new GridBagLayout();
        setLayout(grid);

        GridBagConstraints gbc = new GridBagConstraints();

        // grid 내에서 [x][y] 위치
        gbc.gridx = 0;
        gbc.gridy = 0;
        // 컴포넌트의 디폴트 크기에 대한 폭과 높이의 소속 배율
        gbc.gridwidth = 1;
        gbc.gridheight = 1;
        // 컴포넌트 각 영역의 크기 비율
        gbc.weightx = 1.0;
        gbc.weighty = 0.3;
        // 영역을 채우기 위한 속성 지정
        gbc.fill = GridBagConstraints.BOTH;
        add(curEquation, gbc);

        gbc.gridx = 0;
        gbc.gridy = 1;
        gbc.gridwidth = 1;
        gbc.gridheight = 1;
        gbc.weightx = 1.0;
        gbc.weighty = 0.7;
        gbc.fill = GridBagConstraints.BOTH;
        add(curNumber, gbc);
    }

    public ResultPanel(){
        createComponents();
        initializeResultPanel();
    }
}
