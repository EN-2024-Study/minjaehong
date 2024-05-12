package View.Panel;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.math.BigDecimal;
import java.text.DecimalFormat;

public class LogPanel extends JPanel{
    
    JPanel labelPanel; // Log label 들이 올라갈 labelPanel
    JScrollPane scrollPane; // labelPanel 을 대상으로 만들 JScrollPane 객체

    JButton trashCanButton;
    JPanel trashCanButtonPanel; // trashCanButton 을 붙일 flowlayout 형식의 panel

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

        labelPanel.addContainerListener(new ContainerListener() {
            @Override
            public void componentAdded(ContainerEvent e) {
                if(labelPanel.getComponentCount()>0){
                    trashCanButton.setVisible(true);
                }
            }

            @Override
            public void componentRemoved(ContainerEvent e) {
                if(labelPanel.getComponentCount()==0){
                    trashCanButton.setVisible(false);
                }
            }
        });
    }

    private void createScrollPane(){
        labelPanel = new JPanel();
        labelPanel.setLayout(new BoxLayout(labelPanel, BoxLayout.Y_AXIS));

        scrollPane = new JScrollPane(labelPanel,JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED, JScrollPane.HORIZONTAL_SCROLLBAR_NEVER);
        //scrollPane.setVerticalScrollBarPolicy(JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED);
        scrollPane.setBorder(null);
    }

    private void createTrashCanButton(){
        ImageIcon trashCanIcon = new ImageIcon("src/Images/TrashcanIcon.png");

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

    // 로그 넣을때도 콤마 처리 해야함
    public void addNewLogLabel(JButton newLogButton){

        // 새로 추가된 것은 항상 위쪽에 추가
        labelPanel.add(newLogButton, 0);
    }
}
