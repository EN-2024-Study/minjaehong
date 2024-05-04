import Service.ImageService;
import Service.LogService;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.util.ArrayList;

class MyListener implements ActionListener{

    View view;

    MyListener(View view){
        this.view = view;
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if(e.getSource()==view.searchBtn){
            if(view.searchTextField.getText().isEmpty()) return;
            else{
                String keyWord = view.searchTextField.getText();

                view.elementArr = view.imageService.GetKeywordImages(keyWord);

                // VIEW 바꿔주기
                view.changeToSearchMode();
                view.AddToCenterPanel(view.elementArr);

                view.howMany.setVisible(true);
                view.howMany.setSelectedIndex(0);

                view.logService.AddLog(keyWord);
            }
        }

        if(e.getSource() == view.logBtn){
            view.elementArr = view.logService.GetAllLogs();

            // VIEW 바꿔주기
            view.changeToLogMode();
            view.AddToCenterPanel(view.elementArr);
        }

        if(e.getSource()==view.howMany){
            JComboBox comboBox = (JComboBox) e.getSource();
            String curHowMany = comboBox.getSelectedItem().toString();
            if (curHowMany == "10") view.HideImage(10);
            else if (curHowMany == "20") view.HideImage(20);
            else if(curHowMany=="30") view.HideImage(30);
        }

        if(e.getSource()==view.backToHomeBtn){
            view.elementArr.clear();
            // VIEW 바꿔주기
            view.AddToCenterPanel(view.elementArr);
            view.changeToHomeMode();
        }

        if(e.getSource()==view.deleteAllLogBtn){
            view.logService.DeleteAllLogs();
            view.centerPanel.removeAll();
            view.centerPanel.repaint();
        }
    }
}

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

    ArrayList<JLabel> elementArr;

    JDialog dialog;

    ImageService imageService;
    LogService logService;

    MyListener myListener;

    public View() {

        myListener = new MyListener(this);

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

        searchBtn.addActionListener(myListener);
        howMany.addActionListener(myListener);
        logBtn.addActionListener(myListener);
        backToHomeBtn.addActionListener(myListener);
        deleteAllLogBtn.addActionListener(myListener);

        this.add(topPanel, BorderLayout.NORTH);
        this.add(centerPanel, BorderLayout.CENTER);
        this.add(bottomPanel, BorderLayout.SOUTH);

        Font mainFont = new Font("Consolas",Font.BOLD, 13);

        homeModeLayout = new FlowLayout(FlowLayout.CENTER, 50, 50);
        //searchModeLayout = new FlowLayout(FlowLayout.CENTER, 50, 50);
        searchModeLayout = new FlowLayout(FlowLayout.CENTER, 50, 50);
        logModeLayout = new GridLayout(10, 1);

        // imageArr 초기화
        elementArr = new ArrayList<JLabel>();
        for(int i=0;i<30;i++) elementArr.add(new JLabel());

        imageService = new ImageService();
        logService = new LogService();

        changeToHomeMode();

        dialog = new JDialog();

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
    }

    public void InitializeCenterPanel() {
        centerPanel = new JPanel();
        //centerPanel.add(dialog);
    }

    public void InitializeBottomPanel() {
        bottomPanel = new JPanel();
        bottomPanel.setLayout(logModeLayout);
        bottomPanel.setBackground(Color.YELLOW);

        deleteAllLogBtn = new JButton("DELETE ALL LOG");

        bottomPanel.add(deleteAllLogBtn);
        bottomPanel.setBackground(Color.WHITE);
    }

    //======================== 사실 상 Controller 에 있어야할 작업들임 ========================//

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

    public void AddToCenterPanel(ArrayList<JLabel> elementArr){
        for(int i=0;i<elementArr.size();i++){
            centerPanel.add(elementArr.get(i));
        }
    }
}