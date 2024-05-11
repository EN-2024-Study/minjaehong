package View.Panel;

import javax.swing.*;
import java.awt.*;

public class LogPanel extends JPanel{

    JPanel labelPanel;

    public LogPanel(){
        createComponents();
        initializeLogPanel();
    }

    private void createComponents(){
        //logLabel = new JLabel("ALL LOGS");
        //Border border = BorderFactory.createLineBorder(Color.RED,5); // Create a LineBorder with default color (black)
        //setBorder(border);
    }

    private void initializeLogPanel() {
        setLayout(new BorderLayout());

        labelPanel = new JPanel();
        labelPanel.setLayout(new BoxLayout(labelPanel, BoxLayout.Y_AXIS)); // Set vertical BoxLayout

        /*
        for (int i = 0; i < 20; i++) {
            JLabel label = new JLabel("" + i);
            label.setFont(new Font("Consolas", Font.BOLD, 45));
            label.setAlignmentX(Component.RIGHT_ALIGNMENT); // Align JLabels to the left
            labelPanel.add(label);
        }
        */

        JScrollPane scrollPane = new JScrollPane(labelPanel);
        scrollPane.setVerticalScrollBarPolicy(JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED);

        add(scrollPane, BorderLayout.CENTER);

        labelPanel.setVisible(true);
        setVisible(true);
    }

    public void addNewLogLabel(String equationString, String result){
        /*
        JLabel newLogLabel = new JLabel();
        newLogLabel.setLayout(new BoxLayout(newLogLabel, BoxLayout.Y_AXIS)); // Set vertical BoxLayout

        JTextField smallLog = new JTextField(1);
        smallLog.setText("small LOG!");
        JTextField bigLog = new JTextField(1);
        bigLog.setText(result);

        //newLogLabel.add(new JLabel("Row 1:"));
        newLogLabel.add(smallLog);
        //newLogLabel.add(new JLabel("Row 2:"));
        newLogLabel.add(bigLog);

        labelPanel.add(newLogLabel);
        */

        /*
        JLabel smallLabel = new JLabel(result);
        smallLabel.setFont(new Font("Consolas", Font.BOLD, 15));
        smallLabel.setAlignmentX(Component.RIGHT_ALIGNMENT); // Align JLabels to the left

        JLabel bigLabel = new JLabel(result);
        bigLabel.setFont(new Font("Consolas", Font.BOLD, 30));
        bigLabel.setAlignmentX(Component.RIGHT_ALIGNMENT); // Align JLabels to the left

        JLabel label = new JLabel();
        label.add(smallLabel);
        label.add(bigLabel);
        label.setVisible(true);
        */

        JLabel label = new JLabel(result);
        label.setFont(new Font("Consolas", Font.BOLD, 30));
        label.setAlignmentX(Component.RIGHT_ALIGNMENT);
        // 새로 추가된 것은 항상 위쪽에 추가
        labelPanel.add(label, 0);

        JLabel equationlabel = new JLabel(equationString);
        equationlabel.setFont(new Font("Consolas", Font.BOLD, 15));
        equationlabel.setAlignmentX(Component.RIGHT_ALIGNMENT);
        // 새로 추가된 것은 항상 위쪽에 추가
        labelPanel.add(equationlabel, 0);

        revalidate();
        repaint();
    }
}
