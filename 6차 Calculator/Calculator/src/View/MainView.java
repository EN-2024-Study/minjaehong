package View;

import View.Panel.ButtonPanel;
import View.Panel.LogPanel;
import View.Panel.ResultPanel;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ComponentAdapter;
import java.awt.event.ComponentEvent;

public class MainView extends JFrame {

    private ButtonPanel buttonPanel;
    private LogPanel logPanel;
    private ResultPanel resultPanel;

    private void createComponents(){
        resultPanel = new ResultPanel();
        buttonPanel = new ButtonPanel();
        logPanel = new LogPanel();
    }

    private void initializeMainForm(){
        GridBagLayout grid = new GridBagLayout();
        setLayout(grid);

        GridBagConstraints gbc = new GridBagConstraints();
        gbc.gridx = 0;
        gbc.gridy = 0;
        gbc.gridwidth = 1;
        gbc.gridheight = 1;
        gbc.weightx = 1.0;
        gbc.weighty = 0.4;
        gbc.fill = GridBagConstraints.BOTH;
        this.add(resultPanel,gbc);

        gbc.gridx = 0;
        gbc.gridy = 1;
        gbc.gridwidth = 1;
        gbc.gridheight = 1;
        gbc.weightx = 1.0;
        gbc.weighty = 0.6;
        this.add(buttonPanel,gbc);

        gbc.gridx = 1;
        gbc.gridy = 0;
        gbc.gridwidth = 1;
        gbc.gridheight = 2;
        gbc.weightx = 1.0;
        gbc.weighty = 1.0;
        this.add(logPanel,gbc);

        // logPanel visible effect 추가
        // 람다 뭔지 모름 > 나중에 공부
        this.addComponentListener(new ComponentAdapter() {
            @Override
            public void componentResized(ComponentEvent e) {
                int width = getWidth();
                if (width < 500) {
                    logPanel.setVisible(false);
                }else{
                    logPanel.setVisible(true);
                }
            }
        });
    }

    public MainView(){
        setTitle("Windows Controller.Calculator");
        setSize(new Dimension(400,600));

        // 상대경로 지정
        ImageIcon img = new ImageIcon("src/Images/AppIcon.png");
        setIconImage(img.getImage());

        setMinimumSize(new Dimension(400,600));
        this.setDefaultCloseOperation(EXIT_ON_CLOSE);

        createComponents();
        initializeMainForm();
    }

    public ResultPanel getResultPanel() {
        return resultPanel;
    }

    public LogPanel getLogPanel(){
        return logPanel;
    }

    public String getLastEquation(){
        return resultPanel.getSmallLabel().getText();
    }

    public ButtonPanel getButtonPanel() {
        return buttonPanel;
    }

    public void renderSmallLabel(String newText){

        // smallLabel 크기 줄어드는거 방지
        if(newText.isEmpty()) newText=" ";

        resultPanel.getSmallLabel().setText(newText);
    }

    public void renderBigLabel(String newText){
        resultPanel.getBigLabel().setText(newText);
    }

    public boolean isBigLabelFull(){
        String curText = resultPanel.getBigLabel().getText();

        // 소수일때
        if(curText.contains(".")){
            // 0으로 시작하는 소수면 18개
            if(curText.charAt(0)=='0' && curText.length()==18) return true;
            // 0이 아닌 수로 시작하는 소수면
            else if(curText.length()==17) return true;
        }else{
            if(curText.length()==21) return true;
        }
        return false;
    }
}
