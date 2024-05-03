import DAO.KakaoAPI;
import org.json.simple.parser.ParseException;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.IOException;

// MVC 패턴이 아님 - Delegate 패턴??
// 여기가 View 랑 Controller 작업을 모두 수행함
// 그래서 얘랑 Model 만 필요함
// 여기서 바로 DAO 호출하고 바로 DTO 로 받기
class ImageSearchApp extends JFrame{

    JPanel topPanel;
    JPanel centerPanel;
    JPanel bottomPanel;

    JTextField searchTextField;
    JButton searchBtn;
    JComboBox howMany;
    JButton logBtn;
    JButton backToHomeBtn;
    JButton deleteAllLogBtn;

    FlowLayout searchModeLayout;
    GridLayout logModeLayout;

    JLabel[] labelArr;

    ImageSearchApp(){
        setSize(1000,1000);
        setTitle("ImagerSearcher");
        this.setDefaultCloseOperation(EXIT_ON_CLOSE);

        InitializeTopPanel();
        InitializeCenterPanel();
        InitializeBottomPanel();

        this.add(topPanel, BorderLayout.NORTH);
        this.add(centerPanel, BorderLayout.CENTER);
        this.add(bottomPanel, BorderLayout.SOUTH);

        searchModeLayout = new FlowLayout(FlowLayout.CENTER, 50,50);
        logModeLayout = new GridLayout(0,1);

        // test components
        labelArr = new JLabel[30];
        for(int i=0;i<20;i++) labelArr[i] = new JLabel();

        changeToHomeMode();

        requestFocus();
        setFocusable(true);
        setVisible(true);
    }

    // 여기에 이 프로그램의 TopPanel 에 필요한거 다 넣어놓기
    // 어차피 visible false true 로 상황에 따라 빼게 할거임
    public void InitializeTopPanel(){
        topPanel = new JPanel();
        topPanel.setLayout(new FlowLayout());

        searchTextField = new JTextField(20);
        searchBtn = new JButton("SEARCH");
        String s1[] = {"10","20","30"};
        howMany = new JComboBox(s1);
        logBtn = new JButton("SHOW LOGS");
        backToHomeBtn = new JButton("BACK TO HOME");

        logBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                changeToLogMode();
            }
        });

        searchBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                changeToSearchMode();
                // DAO로 데이터 불러와서 뿌려주기
                // 이 함수 안에 DAO 작업 들어가야함
            }
        });

        backToHomeBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                changeToHomeMode();
            }
        });

        topPanel.add(searchTextField);
        topPanel.add(searchBtn);
        topPanel.add(howMany);
        topPanel.add(logBtn);
        topPanel.add(backToHomeBtn);
        topPanel.setBackground(Color.BLUE);
    }

    public void InitializeCenterPanel(){
        centerPanel = new JPanel();
        centerPanel.setLayout(searchModeLayout);
    }

    public void InitializeBottomPanel(){
        bottomPanel = new JPanel();

        deleteAllLogBtn = new JButton("DELETE ALL LOG");

        bottomPanel.add(deleteAllLogBtn);
        bottomPanel.setBackground(Color.CYAN);
    }

    public void changeToHomeMode(){
        // TOP PANEL
        searchTextField.setVisible(true);
        searchBtn.setVisible(true);
        howMany.setVisible(false);
        logBtn.setVisible(true);
        backToHomeBtn.setVisible(false);

        // CENTER PANEL
        centerPanel.setBackground(Color.RED);
        centerPanel.setLayout(searchModeLayout);

        // 해당 패널에 있는거 다 지우고 가기
        centerPanel.removeAll();

        // BOTTOM PANEL
        bottomPanel.setVisible(false);
    }

    public void changeToSearchMode(){
        // TOP PANEL
        logBtn.setVisible(false);
        howMany.setVisible(true);
        backToHomeBtn.setVisible(true);

        // CENTER PANEL
        centerPanel.setBackground(Color.WHITE);
        centerPanel.setLayout(searchModeLayout);

        // API 로 가져와서 여기에 이렇게 뿌려주기
        for(int i=0;i<30;i++){
            labelArr[i].setText("label");
            labelArr[i].setBorder(new LineBorder(Color.BLACK, 5,false));
            labelArr[i].setPreferredSize(new Dimension(150,150));
            centerPanel.add(labelArr[i]);
        }

        // BOTTOM PANEL
        bottomPanel.setVisible(false);

        requestFocus();
        setFocusable(true);
        setVisible(true);
    }

    public void changeToLogMode(){
        // TOP PANEL
        searchTextField.setVisible(false);
        searchBtn.setVisible(false);
        howMany.setVisible(false);
        logBtn.setVisible(false);
        backToHomeBtn.setVisible(true);

        // CENTER PANEL
        centerPanel.setBackground(Color.WHITE);
        centerPanel.setLayout(logModeLayout);
        
        // Log 가져와서 여기에 뿌려주기
        for(int i=0;i<20;i++){
            labelArr[i].setText("label");
            labelArr[i].setBorder(new LineBorder(Color.BLACK, 1,false));
            centerPanel.add(labelArr[i]);
        }

        // BOTTOM PANEL
        bottomPanel.setVisible(true);

        requestFocus();
        setFocusable(true);
        setVisible(true);
    }

    public static void main(String[] args){

        //ImageSearchApp app = new ImageSearchApp();

        KakaoAPI.getAPITest();
    }
}