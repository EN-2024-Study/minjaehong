import Service.ImageService;
import Service.LogService;

import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;

class View extends JFrame{

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

    LogService logService;
    ImageService imageService;

    ArrayList<JLabel> elementArr;

    public View() {

        logService = new LogService();
        imageService = new ImageService();

        setSize(new Dimension(1000,800));
        setMinimumSize(new Dimension(1000,1000));
        //setExtendedState(JFrame.MAXIMIZED_BOTH);
        // 아이콘 처리
        //setIconImage();

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
        searchModeLayout = new FlowLayout(FlowLayout.CENTER, 50, 50);
        logModeLayout = new GridLayout(10, 1);

        // imageArr 초기화
        elementArr = new ArrayList<JLabel>();
        for(int i=0;i<30;i++) elementArr.add(new JLabel());

        setVisible(true);
    }

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
    }

    //================================= GET FUNCTIONS FOR CONTROLLER =================================//

    // PANELS
    public JPanel getTopPanel(){ return topPanel; }
    public JPanel getCenterPanel() { return centerPanel; }
    public JPanel getBottomPanel() { return bottomPanel; }

    // COMPONENTS
    public JTextField getSearchTextField() {
        return searchTextField;
    }
    public JButton getSearchBtn(){
        return searchBtn;
    }
    public JComboBox getHowMany(){
        return howMany;
    }
    public JButton getLogBtn(){
        return logBtn;
    }
    public JButton getBackToHomeBtn(){
        return backToHomeBtn;
    }
    public JButton getDeleteAllLogBtn(){
        return deleteAllLogBtn;
    }

    // ELEMENT ARR
    public ArrayList<JLabel> getElementArr() { return elementArr; }

    // LAYOUTS
    public FlowLayout getHomeModeLayout() { return homeModeLayout; }
    public FlowLayout getSearchModeLayout() { return searchModeLayout; }
    public GridLayout getLogModeLayout(){ return logModeLayout; }
}