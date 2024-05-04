import javax.imageio.ImageIO;
import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.image.BufferedImage;
import java.net.URL;
import java.util.ArrayList;

import DAO.ImageDAO;
import DTO.ImageDTO;

import static DAO.ImageDAO.getImage;

// revalidate repaint 써보기
// 쓰레드처럼 돌아가고 있는 Swing의 EventListener를 호출해줘서 rendering하게 강제한다??
// 쨌든 뭐 변한거 안뜨면 얘 호출하면 된다고 함
// 그냥 add 했는데 안뜨는 이유가 add 는 EventListener 에게 영향을 주지 않기 때문임

// MVC 패턴이 아님 - Delegate 패턴??
// Swing 은 자체적으로 Delegate 패턴이라고 함
// 얘가 View 랑 Controller 작업을 모두 수행함
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

    ImageDTO imageDTO;
    JLabel[] imageArr;

    ImageSearchApp(){
        setSize(800,800);
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

        // JLabels for showing Images
        imageArr = new JLabel[30];
        for(int i=0;i<30;i++) imageArr[i] = new JLabel();

        changeToHomeMode();

        // 처음에 생성되고 setVisible 호출해서 쓰레드처럼 돌아가고 있는
        // Swing 의 EventListener 에게 rendering 하라고 하기
        setVisible(true);
    }

    // 여기에 이 프로그램의 TopPanel 에 필요한거 다 넣어놓기
    // 어차피 visible false true 로 상황에 따라 뺄거임
    public void InitializeTopPanel(){
        // TopPanel 생성
        topPanel = new JPanel();
        topPanel.setLayout(new FlowLayout());

        // TopPanel Components 생성
        searchTextField = new JTextField(20);
        searchBtn = new JButton("SEARCH");
        String s1[] = {"10","20","30"};
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
                String keyword = searchTextField.getText();
                if(!keyword.isEmpty()){
                    changeToSearchMode(keyword);
                }
                // DAO로 데이터 불러와서 뿌려주기
                // 이 함수 안에 DAO 작업 들어가야함
            }
        });

        logBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                changeToLogMode();
            }
        });

        backToHomeBtn.addActionListener(new ActionListener() {
            @Override
            public void actionPerformed(ActionEvent e) {
                changeToHomeMode();
            }
        });
    }

    public void InitializeCenterPanel(){
        centerPanel = new JPanel();
    }

    public void InitializeBottomPanel(){
        bottomPanel = new JPanel();

        deleteAllLogBtn = new JButton("DELETE ALL LOG");

        bottomPanel.add(deleteAllLogBtn);
        bottomPanel.setBackground(Color.CYAN);
    }

    public void changeToHomeMode(){
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

    public void changeToSearchMode(String keyWord){
        // TOP PANEL
        logBtn.setVisible(false);
        howMany.setVisible(true);
        howMany.setSelectedIndex(0);
        backToHomeBtn.setVisible(true);

        // CENTER PANEL
        centerPanel.setBackground(Color.WHITE);
        centerPanel.setLayout(searchModeLayout);

        imageDTO = ImageDAO.getImage(keyWord);
        ArrayList<String> imageURLs = imageDTO.GetImageURLs();

        // API 로 가져와서 여기에 이렇게 뿌려주기
        
        URL url;
        Image curImage; // BufferedImage??
        for(int i=0;i<30;i++){
            try {
                url = new URL(imageURLs.get(i));
                // URL 통해서 Image 가져오기
                curImage = ImageIO.read(url);
                curImage = curImage.getScaledInstance(150,150,Image.SCALE_SMOOTH);
                //imageArr[i].setBorder(new LineBorder(Color.BLACK, 5,false));
                //imageArr[i].setPreferredSize(new Dimension(150,150));
                imageArr[i] = new JLabel(new ImageIcon(curImage));
                centerPanel.add(imageArr[i]);
                //imageURLs[i].setText("label");
                //imageArr[i].setBorder(new LineBorder(Color.BLACK, 5,false));
                //imageArr[i].setPreferredSize(new Dimension(150,150));
                //centerPanel.add(imageArr[i]);
            }catch(Exception e){

            }
        }

        // BOTTOM PANEL
        bottomPanel.setVisible(false);
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
            imageArr[i].setText("label");
            imageArr[i].setBorder(new LineBorder(Color.BLACK, 1,false));
            centerPanel.add(imageArr[i]);
        }

        // BOTTOM PANEL
        bottomPanel.setVisible(true);
    }

    public static void main(String[] args){

        ImageSearchApp app = new ImageSearchApp();

        //KakaoAPI.getAPITest();
    }
}