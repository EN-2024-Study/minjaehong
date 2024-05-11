package View.Panel;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

public class LogPanel extends JPanel{

    // Label 들이 올라갈 labelPanel 따로 생성
    JPanel labelPanel;
    JScrollPane scrollPane;

    JButton trashCanButton;

    public LogPanel(){
        createComponents();
        initializeLogPanel();
    }

    private void createComponents(){
        labelPanel = new JPanel();
        labelPanel.setLayout(new BoxLayout(labelPanel, BoxLayout.Y_AXIS));

        scrollPane = new JScrollPane(labelPanel);
        scrollPane.setVerticalScrollBarPolicy(JScrollPane.VERTICAL_SCROLLBAR_AS_NEEDED);

        trashCanButton = new JButton("TRASHCAN");
        trashCanButton.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                labelPanel.removeAll();
                labelPanel.revalidate();
                labelPanel.repaint();
            }
        });
    }

    private void initializeLogPanel() {

        this.setLayout(new BorderLayout());

        scrollPane.setBorder(null);

        this.add(trashCanButton,BorderLayout.SOUTH);

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

        // label 들 붙일 labelPanel 을 들고 있는 scrollPane 을 LogPanel 에 붙이기
        this.add(scrollPane, BorderLayout.CENTER);

        labelPanel.setVisible(true);
        trashCanButton.setVisible(false);

        setVisible(true);
    }

    // 로그 넣을때도 콤마 처리 해야함
    public void addNewLogLabel(String equationString, String result){

        // 여기 하다 감
        JPanel newLogPanel = new JPanel();
        newLogPanel.setLayout(new GridLayout(2, 1));

        JTextArea equationTextArea = new JTextArea(equationString);
        equationTextArea.setEditable(false);
        equationTextArea.setFont(new Font("Consolas", Font.BOLD, 15));
        equationTextArea.setAlignmentX(Component.RIGHT_ALIGNMENT);
        newLogPanel.add(equationTextArea);

        JTextArea resultTextArea = new JTextArea(result);
        resultTextArea.setEditable(false);
        resultTextArea.setFont(new Font("Consolas", Font.BOLD, 30));
        resultTextArea.setAlignmentX(Component.RIGHT_ALIGNMENT);
        newLogPanel.add(resultTextArea);

        newLogPanel.addMouseListener(new MouseAdapter() {
            @Override
            public void mouseEntered(MouseEvent e) {
                setBackground(Color.ORANGE);
                revalidate();
                repaint();
            }

            @Override
            public void mouseClicked(MouseEvent e) {
                System.out.println("mouse clicked");
            }
        });

        newLogPanel.requestFocus();

        // 새로 추가된 것은 항상 위쪽에 추가
        labelPanel.add(newLogPanel, 0);
    }
}
