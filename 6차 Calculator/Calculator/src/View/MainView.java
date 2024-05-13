package View;

import View.Panel.ButtonPanel;
import View.Panel.LogPanel;
import View.Panel.ResultPanel;
import static Constants.ConstValue.*;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.math.BigDecimal;
import java.math.RoundingMode;
import java.text.DecimalFormat;

public class MainView extends JFrame {

    private ImageIcon calculatorIcon;

    private ButtonPanel buttonPanel;
    private LogPanel logPanel;
    private ResultPanel resultPanel;

    private void createComponents(){
        // 상대경로 지정
        calculatorIcon = new ImageIcon(APP_ICON);

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

        // logPanel and logButton visible effect 추가
        // 람다 뭔지 모름 > 나중에 공부
        this.addComponentListener(new ComponentAdapter() {
            @Override
            public void componentResized(ComponentEvent e) {
                int width = getWidth();
                if (width < 500) {
                    logPanel.setVisible(false);
                    resultPanel.getLogButton().setVisible(true);
                }else{
                    logPanel.setVisible(true);
                    resultPanel.getLogButton().setVisible(false);
                }
            }
        });

        /*
        PopupFactory pf = new PopupFactory();
        Popup test = pf.getPopup(this, logPanel,10,10);
        */

        getResultPanel().getLogButton().addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                //resultPanel.getComponentPopupMenu();

                //test.show();

                //logPanel.setSize(getWidth(), getHeight());
                //logPanel.setLocation(getLocation().x, getLocation().y + getHeight());
                //logPanel.setVisible(true);
            }
        });
    }

    public MainView(){
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

    // 직접적으로 BigLabel 변수를 바꿔주는 함수
    // 모든 rendering 호출은 목적지가 얘임
    // 그래서 얘만 바꿔주면 됨

    // 사용자 입력한거는 0 자르면 안됨
    // 사용자 입력한 것도 setScale 하면 .0 같은거나 소수점 0 이 아예 표시가 안됨
    // 숫자로는 잘 들어가긴함
    public void renderBigLabel(String newNum){

        if(newNum.equals("cant divide by zero!")) {
            resultPanel.getBigLabel().setText(newNum);
            return;
        }

        BigDecimal curNum = new BigDecimal(newNum);
        curNum = curNum.setScale(16, RoundingMode.HALF_EVEN);

        newNum = curNum.stripTrailingZeros().toPlainString();

        newNum = getFormattedNumber(newNum);

        resultPanel.getBigLabel().setText(newNum);
    }

    private String getFormattedNumber(String targetNum){
        DecimalFormat df = new DecimalFormat("#,###");

        int decimalPointIndex = targetNum.indexOf(".");

        if(decimalPointIndex==-1){
            BigDecimal temp = new BigDecimal(targetNum);
            targetNum = df.format(temp);
        }else{
            String integerPart = targetNum.substring(0, decimalPointIndex);
            String decimalPart = targetNum.substring(decimalPointIndex);

            BigDecimal temp = new BigDecimal(integerPart);
            targetNum = df.format(temp) + decimalPart;
        }

        return targetNum;
    }
}