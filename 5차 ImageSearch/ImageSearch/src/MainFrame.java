import DAO.*;
import VO.*;

import javax.imageio.ImageIO;
import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.net.URL;
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

    FlowLayout searchModeLayout;
    GridLayout logModeLayout;

    JLabel[] imageArr;

    LogDAO logDAO;
    ImageDAO imageDAO;

    ImageListVO imageListVO;
    LogListVO logListVO;

    public MainFrame() {
        setSize(800, 800);
        setTitle("ImagerSearcher");
        this.setDefaultCloseOperation(EXIT_ON_CLOSE);

        InitializeTopPanel();
        InitializeCenterPanel();
        InitializeBottomPanel();

        this.add(topPanel, BorderLayout.NORTH);
        this.add(centerPanel, BorderLayout.CENTER);
        this.add(bottomPanel, BorderLayout.SOUTH);

        searchModeLayout = new FlowLayout(FlowLayout.CENTER, 50, 50);
        logModeLayout = new GridLayout(0, 1);

        // imageArr 초기화
        imageArr = new JLabel[30];
        for(int i=0;i<30;i++) imageArr[i] = new JLabel();

        // 이때 처음이자 마지막으로 초기화됨
        imageDAO = ImageDAO.GetInstance();
        logDAO = LogDAO.GetInstance();

        changeToHomeMode();

        // 처음에 생성되고 setVisible 호출해서 쓰레드처럼 돌아가고 있는
        // Swing 의 EventListener 에게 rendering 하라고 하기
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
        searchBtn = new JButton("SEARCH");
        String s1[] = {"10", "20", "30"};
        howMany = new JComboBox(s1);
        logBtn = new JButton("SHOW LOGS");
        backToHomeBtn = new JButton("BACK TO HOME");

        // TopPanel Components 추가
        topPanel.add(searchTextField);
        topPanel.add(searchBtn);
        topPanel.add(howMany);
        topPanel.add(logBtn);
        topPanel.add(backToHomeBtn);
        topPanel.setBackground(Color.BLUE);

        // TopPanel ActionListener 추가
        searchBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                if(searchTextField.getText().isEmpty()) return;
                else{
                    // DAO로 데이터 불러와서 뿌려주기
                    // 이 함수 안에 DAO 작업 들어가야함
                    String keyWord = searchTextField.getText();

                    changeToSearchMode();
                    // DAO 작업
                    // 뿌려주기까지 하는데
                    // 이걸 그냥 데이터 받고 뿌려주는걸 따로 나누면 MVC 분리가 가능하지 않을까??
                    GetKeywordImages(keyWord);
                    logDAO.AddLog(keyWord);
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
                else HideImage(30);
            }
        });

        logBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                changeToLogMode();
                // 로그 가져와서 여기서 뿌려주기
                GetAllLogs();
            }
        });

        backToHomeBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                changeToHomeMode();
            }
        });
    }

    public void InitializeCenterPanel() {
        centerPanel = new JPanel();
    }

    public void InitializeBottomPanel() {
        bottomPanel = new JPanel();

        deleteAllLogBtn = new JButton("DELETE ALL LOG");

        bottomPanel.add(deleteAllLogBtn);
        bottomPanel.setBackground(Color.CYAN);
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
        centerPanel.setBackground(Color.RED);
        centerPanel.setLayout(searchModeLayout);
        centerPanel.removeAll();

        // BOTTOM PANEL
        bottomPanel.setVisible(false);
    }

    public void changeToSearchMode() {
        // TOP PANEL
        logBtn.setVisible(false);
        howMany.setVisible(true);
        howMany.setSelectedIndex(0);
        backToHomeBtn.setVisible(true);

        // CENTER PANEL
        centerPanel.setBackground(Color.WHITE);
        centerPanel.setLayout(searchModeLayout);

        // BOTTOM PANEL
        bottomPanel.setVisible(false);
    }

    public void changeToLogMode() {
        // TOP PANEL
        searchTextField.setVisible(false);
        searchBtn.setVisible(false);
        howMany.setVisible(false);
        logBtn.setVisible(false);
        backToHomeBtn.setVisible(true);

        // CENTER PANEL
        centerPanel.setBackground(Color.WHITE);
        centerPanel.setLayout(logModeLayout);
        
        // BOTTOM PANEL
        bottomPanel.setVisible(true);
    }

    public void HideImage(int startIdx) {
        for (int i = 0; i < startIdx; i++) imageArr[i].setVisible(true);
        for (int i = startIdx; i < 30; i++) imageArr[i].setVisible(false);
    }

    //======================== 사실 상 Controller 에 있어야할 작업들임 ========================//

    public void GetKeywordImages(String keyWord){
        
        // 싱글톤 DAO로 받기
        imageListVO = imageDAO.GetImageURLs(keyWord);

        ArrayList<String> imageURLList = imageListVO.GetImageURLList();

        // API 로 가져와서 여기에 이렇게 뿌려주기
        URL url;
        Image curImage; // BufferedImage??

        for (int i = 0; i < 30; i++) {
            try {
                url = new URL(imageURLList.get(i));
                // URL 통해서 Image 가져오기
                curImage = ImageIO.read(url);
                curImage = curImage.getScaledInstance(150, 150, Image.SCALE_SMOOTH);

                // 새 이미지 화면에 추가하기
                imageArr[i] = new JLabel(new ImageIcon(curImage));

                // panel 에도 추가
                // 이 작업이 여기 가있는게 맞는걸까
                centerPanel.add(imageArr[i]);

                System.out.println("DONE!");
            } catch (Exception e) {

            }
        }
    }

    public void GetAllLogs(){

        // 싱글톤 DAO로 받기
        logListVO = logDAO.GetLogs();

        // API 로 가져와서 여기에 이렇게 뿌려주기
        URL url;
        Image curImage; // BufferedImage??

        ArrayList<String> logList = logListVO.GetLogList();

        String curLog;
        for (int i = 0; i < logList.size(); i++) {
            curLog = logList.get(i);

            // 새 이미지 화면에 추가하기
            imageArr[i] = new JLabel(curLog);

            // panel 에도 추가
            // 이 작업이 여기 가있는게 맞는걸까
            centerPanel.add(imageArr[i]);
        }
    }
}