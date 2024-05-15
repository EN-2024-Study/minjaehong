package View.Panel;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.math.BigDecimal;
import java.text.DecimalFormat;

public class LogPanel extends JPanel{
    
    private JPanel labelPanel; // Log label 들이 올라갈 labelPanel
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
    }

    private void createScrollPane(){
        labelPanel = new JPanel();
        labelPanel.setLayout(new BoxLayout(labelPanel, BoxLayout.Y_AXIS));

        scrollPane = new JScrollPane(labelPanel,JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_NEVER);
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
                labelPanel.removeAll();
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
        return labelPanel;
    }

    public JButton getTrashCanButton(){
        return trashCanButton;
    }

    // 로그 넣을때도 콤마 처리 해야함
    public void addNewLogLabel(JButton newLogButton){
        // 새로 추가된 것은 항상 위쪽에 추가
        labelPanel.add(newLogButton, 0);

        // logPanel 다시 그리기
        revalidate();
        repaint();
    }
}
