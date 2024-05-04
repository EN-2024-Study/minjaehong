import Model.DAO.*;
import Model.VO.*;
import Service.ImageService;
import Service.LogService;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

class MainFrame extends JFrame {

    JPanel topPanel;
    JPanel centerPanel;
    JPanel bottomPanel;

    JTextField searchTextField;
    JButton searchBtn;
    JComboBox howMany;
    JButton logBtn;
    JButton backToHomeBtn;
    JButton deleteAllLogBtn;

    Font mainFont;

    FlowLayout homeModeLayout;
    FlowLayout searchModeLayout;
    GridLayout logModeLayout;

    ArrayList<JLabel> elementArr;

    ImageService imageService;
    LogService logService;

    public MainFrame() {
        //setSize(1000, 800);
        //setMinimumSize(getSize());
        setExtendedState(JFrame.MAXIMIZED_BOTH);
        
        setTitle("ImagerSearcher");
        this.setDefaultCloseOperation(EXIT_ON_CLOSE);

        InitializeTopPanel();
        InitializeCenterPanel();
        InitializeBottomPanel();

        this.add(topPanel, BorderLayout.NORTH);
        this.add(centerPanel, BorderLayout.CENTER);
        this.add(bottomPanel, BorderLayout.SOUTH);

        Font mainFont = new Font("Consolas",Font.BOLD, 13);

        homeModeLayout = new FlowLayout(FlowLayout.CENTER, 50, 50);
        //searchModeLayout = new FlowLayout(FlowLayout.CENTER, 50, 50);
        searchModeLayout = new FlowLayout(FlowLayout.CENTER, 50, 50);
        logModeLayout = new GridLayout(0, 1);

        // imageArr 초기화
        elementArr = new ArrayList<JLabel>();
        for(int i=0;i<30;i++) elementArr.add(new JLabel());

        imageService = new ImageService();
        logService = new LogService();

        changeToHomeMode();

        setVisible(true);
    }

    // 여기에 이 프로그램의 TopPanel 에 필요한거 다 넣어놓기
    // 어차피 visible false true 로 상황에 따라 뺄거임
    public void InitializeTopPanel() {
        // TopPanel 생성
        topPanel = new JPanel();
        topPanel.setLayout(new FlowLayout());

        // TopPanel Components 생성
        searchTextField = new JTextField(20);

        searchTextField.setPreferredSize(new Dimension(80,28));

        searchBtn = new JButton("SEARCH");
        searchBtn.setFont(mainFont);
        String s1[] = {"10", "20", "30"};
        howMany = new JComboBox(s1);
        logBtn = new JButton("SHOW LOGS");
        logBtn.setFont(mainFont);
        backToHomeBtn = new JButton("BACK TO HOME");
        backToHomeBtn.setFont(mainFont);

        // TopPanel Components 추가
        topPanel.add(searchTextField);
        topPanel.add(searchBtn);
        topPanel.add(howMany);
        topPanel.add(logBtn);
        topPanel.add(backToHomeBtn);
        topPanel.setBackground(Color.WHITE);

        // TopPanel ActionListener 추가
        searchBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                if(searchTextField.getText().isEmpty()) return;
                else{
                    String keyWord = searchTextField.getText();

                    elementArr = imageService.GetKeywordImages(keyWord);

                    // VIEW 바꿔주기
                    changeToSearchMode();
                    AddToCenterPanel(elementArr);

                    howMany.setVisible(true);
                    howMany.setSelectedIndex(0);

                    logService.AddLog(keyWord);
                }
            }
        });

        howMany.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                JComboBox comboBox = (JComboBox) e.getSource();
                String curHowMany = comboBox.getSelectedItem().toString();
                if (curHowMany == "10") HideImage(10);
                else if (curHowMany == "20") HideImage(20);
                else if(curHowMany=="30") HideImage(30);
            }
        });

        logBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                // 로그 가져와서 여기서 뿌려주기
                elementArr = logService.GetAllLogs();

                // VIEW 바꿔주기
                changeToLogMode();
                AddToCenterPanel(elementArr);
            }
        });

        backToHomeBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                elementArr.clear();

                // VIEW 바꿔주기
                AddToCenterPanel(elementArr);
                changeToHomeMode();
            }
        });
    }

    public void InitializeCenterPanel() {
        centerPanel = new JPanel();
    }

    public void InitializeBottomPanel() {
        bottomPanel = new JPanel();
        bottomPanel.setLayout(logModeLayout);
        bottomPanel.setBackground(Color.YELLOW);

        deleteAllLogBtn = new JButton("DELETE ALL LOG");

        bottomPanel.add(deleteAllLogBtn);
        bottomPanel.setBackground(Color.WHITE);

        // Bottom Panel ActionListener 추가
        deleteAllLogBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                logService.DeleteAllLogs();
                centerPanel.removeAll();
                centerPanel.repaint();
            }
        });
    }

    public void changeToHomeMode() {
        // TOP PANEL
        searchTextField.setText("");
        searchTextField.setVisible(true);
        searchBtn.setVisible(true);
        howMany.setVisible(false);
        logBtn.setVisible(true);
        backToHomeBtn.setVisible(false);

        // CENTER PANEL
        centerPanel.removeAll();
        centerPanel.repaint();
        centerPanel.setBackground(Color.WHITE);

        // BOTTOM PANEL
        bottomPanel.setVisible(false);
    }

    public void changeToSearchMode() {
        // TOP PANEL
        logBtn.setVisible(false);
        backToHomeBtn.setVisible(true);

        // CENTER PANEL
        centerPanel.removeAll();
        centerPanel.repaint();
        centerPanel.setBackground(Color.WHITE);
        centerPanel.setLayout(searchModeLayout);

        // BOTTOM PANEL
        bottomPanel.setVisible(false);
    }

    public void changeToLogMode() {
        // TOP PANEL
        searchTextField.setVisible(false);
        searchBtn.setVisible(false);
        logBtn.setVisible(false);
        backToHomeBtn.setVisible(true);

        // CENTER PANEL
        centerPanel.removeAll();
        centerPanel.repaint();
        centerPanel.setBackground(Color.WHITE);
        centerPanel.setLayout(logModeLayout);

        // BOTTOM PANEL
        bottomPanel.setLayout(logModeLayout);
        bottomPanel.setVisible(true);
    }

    public void HideImage(int startIdx) {
        for (int i = 0; i < startIdx; i++) elementArr.get(i).setVisible(true);
        for (int i = startIdx; i < 30; i++) elementArr.get(i).setVisible(false);
    }

    //======================== 사실 상 Controller 에 있어야할 작업들임 ========================//

    public void AddToCenterPanel(ArrayList<JLabel> elementArr){
        for(int i=0;i<elementArr.size();i++){
            centerPanel.add(elementArr.get(i));
        }
    }
}