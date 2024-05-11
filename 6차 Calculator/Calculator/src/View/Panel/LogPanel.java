package View.Panel;

import javax.swing.*;
import javax.swing.border.Border;
import java.awt.*;

// extends JScrollPane
//
public class LogPanel extends JPanel{

    private JLabel logLabel;

    public LogPanel(){
        createComponents();
        initializeLogPanel();
    }

    private void createComponents(){
        //logLabel = new JLabel("ALL LOGS");
        //Border border = BorderFactory.createLineBorder(Color.RED,5); // Create a LineBorder with default color (black)
        //setBorder(border);
    }

    private void initializeLogPanel(){
        JLabel logLabel = new JLabel("ALL LOGS");

        JPanel labelPanel = new JPanel();
        labelPanel.setBackground(Color.WHITE);
        labelPanel.add(logLabel);

        JScrollPane scrollPane = new JScrollPane(labelPanel);
        scrollPane.setVerticalScrollBarPolicy(JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED);

        setLayout(new BorderLayout());
        add(scrollPane, BorderLayout.CENTER);
    }
}
