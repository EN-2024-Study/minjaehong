package View.Frame;

import View.Panel.ButtonPanel;
import View.Panel.LogPanel;
import View.Panel.ResultPanel;

import javax.swing.*;
import java.awt.*;

public class MainFrame extends JFrame {

    private ImageIcon calculatorIcon;

    private ButtonPanel buttonPanel;
    private LogPanel logPanel;
    private ResultPanel resultPanel;

    private void createComponents(){
        // 상대경로 지정
        calculatorIcon = new ImageIcon("src/Images/appIcon.png");

        resultPanel = new ResultPanel();
        buttonPanel = new ButtonPanel();
        logPanel = new LogPanel();
    }

    private void initializeMainView(){

        // Icon 지정
        setIconImage(calculatorIcon.getImage());

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
    }

    public MainFrame(){
        setTitle("Windows Calculator");
        setSize(new Dimension(400,600));
        setMinimumSize(new Dimension(400,600));
        this.setDefaultCloseOperation(EXIT_ON_CLOSE);

        createComponents();
        initializeMainView();
    }

    public ResultPanel getResultPanel() {
        return resultPanel;
    }
    public ButtonPanel getButtonPanel() {
        return buttonPanel;
    }

    public void setSmallLabel(String newEquation){
        resultPanel.setSmallLabel(newEquation);
    }

    public void setBigLabel(String newResult){
        resultPanel.setBigLabel(newResult);
    }

    public LogPanel getLogPanel(){
        return logPanel;
    }

    public String getLatestSmallLabel(){
        return resultPanel.getSmallLabel().getText();
    }
    public String getLatestBigLabel() { return resultPanel.getBigLabel().getText(); }
}