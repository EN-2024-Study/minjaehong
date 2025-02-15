package View.Panel;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.math.BigDecimal;
import java.text.DecimalFormat;

public class LogPanel extends JPanel{
    
    private JPanel logLabelPanel; // Log label 들이 올라갈 logLabelPanel
    private JScrollPane scrollPane; // labelPanel 을 대상으로 만들 JScrollPane 객체

    private JButton trashCanButton;
    private JPanel trashCanButtonPanel; // trashCanButton 을 붙일 flowlayout 형식의 panel

    public LogPanel(){
        createComponents();
        initializeLogPanel();
    }

    private void createComponents(){
        createScrollPane();

        createTrashCanButton();
        createTrashCanPanel();
    }

    private void initializeLogPanel() {
        createComponents();

        this.setLayout(new BorderLayout());
        this.add(trashCanButtonPanel,BorderLayout.SOUTH); // 아래 쓰레기통 붙이기

        // label 들 붙일 labelPanel 을 들고 있는 scrollPane 을 LogPanel 에 붙이기
        this.add(scrollPane, BorderLayout.CENTER);

        int fixedWidth = 400;
        int fixedHeight = 600;
        Dimension fixedSize = new Dimension(fixedWidth, fixedHeight);
        setPreferredSize(fixedSize);
        setMinimumSize(fixedSize);
        setMaximumSize(fixedSize);
    }

    private void createScrollPane(){
        logLabelPanel = new JPanel();
        logLabelPanel.setLayout(new BoxLayout(logLabelPanel, BoxLayout.Y_AXIS));

        scrollPane = new JScrollPane(logLabelPanel,JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_NEVER);
        scrollPane.setBorder(null);
    }

    private void createTrashCanButton(){
        ImageIcon trashCanIcon = new ImageIcon("src/Images/trashcanIcon.png");

        trashCanButton = new JButton(trashCanIcon);
        trashCanButton.setSize(20,20);
        trashCanButton.setBorderPainted(false);
        trashCanButton.setContentAreaFilled(false);
        trashCanButton.setVisible(false);

        trashCanButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                logLabelPanel.removeAll();
            }
        });
    }

    private void createTrashCanPanel(){
        trashCanButtonPanel = new JPanel();
        trashCanButtonPanel.setLayout(new FlowLayout(FlowLayout.RIGHT));
        trashCanButtonPanel.setBackground(Color.WHITE);

        trashCanButtonPanel.add(trashCanButton);
    }

    public JPanel getLabelPanel(){
        return logLabelPanel;
    }

    public JButton getTrashCanButton(){
        return trashCanButton;
    }

    public void addNewLog(JButton newLog){
        // 새로 추가된 것은 항상 위쪽에 추가
        logLabelPanel.add(newLog, 0);

        // logPanel 다시 그리기
        revalidate();
        repaint();
    }
}
