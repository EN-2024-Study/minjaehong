package View.Panel;

import javax.swing.*;
import javax.swing.border.Border;
import java.awt.*;

public class ButtonPanel extends JPanel {

    private JButton[] buttonArr;
    private String[] buttonName={
            "CE","C","<-","/",
            "7","8","9","X",
            "4","5","6","-",
            "1","2","3","+",
            "+/-","0",".","="
    };
    
    private void createComponents() {
        buttonArr = new JButton[20];
        for(int i=0;i<20;i++) {
            buttonArr[i] = new JButton(buttonName[i]);
            buttonArr[i].setBackground(Color.ORANGE);
        }
    }

    private void initializeButtonPanel(){
        createComponents();

        GridBagLayout grid = new GridBagLayout();
        setLayout(grid);

        GridBagConstraints gbc = new GridBagConstraints();

        for (int row = 0; row < 5; row++) {
            for (int col = 0; col < 4; col++) {
                gbc.gridx = col;
                gbc.gridy = row;
                gbc.weightx = 1.0;
                gbc.weighty = 1.0;
                gbc.fill = GridBagConstraints.BOTH;
                gbc.insets = new Insets(5, 5, 5, 5); // Padding
                add(buttonArr[row*4+col], gbc);
            }
        }
    }

    public ButtonPanel() {
        createComponents();
        initializeButtonPanel();
    }
}
