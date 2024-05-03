import javax.swing.*;
import javax.swing.border.Border;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.LinkedList;

// MVC 패턴이 아님 - Delegate 패턴??
// 여기가 View 랑 Controller 작업을 모두 수행함
// 그래서 얘랑 Model 만 필요함
// 여기서 바로 DAO 호출하고 바로 DTO 로 받기
class ImageSearchApp extends JFrame{

    JPanel topPanel;
    JPanel centerPanel;
    JPanel bottomPanel; // 왜 로그모드 갔을때 안보임??

    JTextField searchTextField;
    JButton searchBtn;
    JComboBox howMany;
    JButton logBtn;
    JButton backToHomeBtn;
    JButton deleteAllLogBtn;

    JLabel imageLabel;

    FlowLayout searchModeLayout;
    GridLayout logModeLayout;

    JButton[] buttonArr;

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
        imageLabel = new JLabel();
        buttonArr = new JButton[10];

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

        //requestFocus();
        //setFocusable(true);
        //setVisible(true);
    }

    public void InitializeCenterPanel(){
        centerPanel = new JPanel();
        centerPanel.setLayout(searchModeLayout);
    }

    public void InitializeBottomPanel(){
        bottomPanel = new JPanel();
        bottomPanel.setLayout(searchModeLayout);

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
        //bottomPanel.setVisible(false);
    }

    public void changeToSearchMode(){
        // TOP PANEL
        logBtn.setVisible(false);
        howMany.setVisible(true);
        backToHomeBtn.setVisible(true);

        // CENTER PANEL
        //centerPanel.setBackground(Color.GREEN);
        centerPanel.setLayout(searchModeLayout);

        for(int i=0;i<10;i++){
            buttonArr[i] = new JButton(""+i);
            buttonArr[i].setPreferredSize(new Dimension(150,150));
            centerPanel.add(buttonArr[i]);
        }

        // BOTTOM PANEL
        //bottomPanel.setVisible(false);

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
        centerPanel.setBackground(Color.BLACK);
        centerPanel.setLayout(logModeLayout);

        // BOTTOM PANEL
        // deleteAllLogBtn.setVisible(true);
        // bottomPanel.setVisible(true);
        this.add(bottomPanel, BorderLayout.SOUTH);

        requestFocus();
        setFocusable(true);
        setVisible(true);
    }

    public void AddDummyData(){

        /*
        centerPanel.add(imageLabel);

        ImageIcon icon = new ImageIcon("C://Users//USER//Pictures//Saved Pictures//mario.png");

        Image img = icon.getImage();
        Image updateImg = img.getScaledInstance(50,50,Image.SCALE_SMOOTH);
        ImageIcon updateIcon = new ImageIcon(updateImg);

        imageLabel.setIcon(updateIcon);

        imageLabel.setBounds();
        */
    }

    public static void main(String[] args){
        ImageSearchApp app = new ImageSearchApp();
    }
}