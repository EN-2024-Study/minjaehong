package View.Panel;

import javax.swing.*;
import javax.swing.border.Border;
import java.awt.*;

public class LogPanel extends JPanel{

    private JLabel logLabel;

    public LogPanel(){
        createComponents();
        initializeLogPanel();
    }

    private void createComponents(){
        logLabel = new JLabel("ALL LOGS");
        //Border border = BorderFactory.createLineBorder(Color.RED,5); // Create a LineBorder with default color (black)
        //setBorder(border);
    }

    private void initializeLogPanel(){
        add(logLabel);
    }

    public JLabel getLogLabel() {
        return logLabel;
    }
}
