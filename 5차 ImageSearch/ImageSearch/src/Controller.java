import Service.ImageService;
import Service.LogService;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.awt.event.*;
import java.util.ArrayList;

public class Controller {
    private View view;

    private ImageService imageService;
    private LogService logService;

    private MyButtonListener myButtonListener;
    private MyMouseListener myMouseListener;

    public Controller(View view, ImageService imageService, LogService logService){
        this.view = view;
        this.imageService = imageService;
        this.logService = logService;

        myButtonListener = new MyButtonListener();
        myMouseListener = new MyMouseListener();

        // View 초기화
        //InitView();

        // Controller 가 View 영향끼칠 수 있게 ActionListener 를 Component 들에게 모두 주기
        BindButtonListenerToViewComponents();

        // 다 준비되었으면 HomeMode 로 시작하기
        changeToHomeMode();
    }

    public void InitView(){
        view.InitializeTopPanel();
        view.InitializeCenterPanel();
        view.InitializeBottomPanel();
        changeToHomeMode(); // ?
    }

    private void BindButtonListenerToViewComponents(){
        // TOP PANEL 에게 모두 바인딩
        view.getSearchBtn().addActionListener(myButtonListener);
        view.getHowMany().addActionListener(myButtonListener);
        view.getLogBtn().addActionListener(myButtonListener);
        view.getBackToHomeBtn().addActionListener(myButtonListener);

        // BOTTOM PANEL 에게 모두 바인딩
        view.getDeleteAllLogBtn().addActionListener(myButtonListener);
    }

    private void changeToHomeMode() {
        // TOP PANEL
        view.getSearchTextField().setText("");
        view.getSearchTextField().setVisible(true);
        view.getSearchBtn().setVisible(true);
        view.getHowMany().setVisible(false);
        view.getLogBtn().setVisible(true);
        view.getBackToHomeBtn().setVisible(false);

        // CENTER PANEL
        view.getCenterPanel().removeAll();
        view.getCenterPanel().repaint();
        view.getCenterPanel().setBackground(Color.WHITE);

        // BOTTOM PANEL
        view.getBottomPanel().setVisible(false);
    }

    private void changeToSearchMode() {
        // TOP PANEL
        view.getLogBtn().setVisible(false);
        view.getBackToHomeBtn().setVisible(true);

        // CENTER PANEL
        view.getCenterPanel().removeAll();
        view.getCenterPanel().repaint();
        view.getCenterPanel().setBackground(Color.WHITE);
        view.getCenterPanel().setLayout(view.getSearchModeLayout());

        // BOTTOM PANEL
        view.getBottomPanel().setVisible(false);
    }

    private void changeToLogMode() {
        // TOP PANEL
        view.getSearchTextField().setVisible(false);
        view.getSearchBtn().setVisible(false);
        view.getLogBtn().setVisible(false);
        view.getBackToHomeBtn().setVisible(true);

        // CENTER PANEL
        view.getCenterPanel().removeAll();
        view.getCenterPanel().repaint();
        view.getCenterPanel().setBackground(Color.WHITE);
        view.getCenterPanel().setLayout(view.getLogModeLayout());

        // BOTTOM PANEL
        view.getBottomPanel().setLayout(view.getLogModeLayout());
        view.getBottomPanel().setVisible(true);
    }

    private void hideImage(int startIdx) {
        for (int i = 0; i < startIdx; i++) view.getElementArr().get(i).setVisible(true);
        for (int i = startIdx; i < 30; i++) view.getElementArr().get(i).setVisible(false);
    }

    private void addToCenterPanel(ArrayList<JLabel> elementArr){
        for(int i=0;i<elementArr.size();i++){
            view.getCenterPanel().add(elementArr.get(i));
        }
    }

    //================================= CONTROLLER LISTENER =================================//

    private class MyButtonListener implements ActionListener{

        @Override
        public void actionPerformed(ActionEvent e) {

            if(e.getSource()==view.getSearchBtn()) searchImage();
            else if(e.getSource() == view.getLogBtn()) getAllLog();
            else if(e.getSource()==view.getHowMany()) applyHowMany(e);
            else if(e.getSource()==view.getBackToHomeBtn()) goBackToHome();
            else if(e.getSource()==view.getDeleteAllLogBtn()) deleteAllLog();
        }
    }

    // 얘는 ImageIcon 이 붙어있는 JLabel 들한테만 달리기 때문에
    // mouseClicked 안에서 이게 ImageIcon 붙어있는 JLabel 인지 확인할 필요가 없음
    // 그냥 ImageIcon 이라고 확신하고 들어가는거임 ImageIcon 이라는 것은 보장이 돼있음
    private class MyMouseListener extends MouseAdapter {

        // 기존 ImageIcon 을 받아서 더 4배의 ImageIcon 으로 만들어서 return 해줌
        private ImageIcon changeToBiggerImageIcon(ImageIcon originalIcon, int width, int height){
            Image originalImage = originalIcon.getImage();
            Image scaledImage = originalImage.getScaledInstance(width * 4, height * 4, Image.SCALE_SMOOTH);
            return new ImageIcon(scaledImage);
        }

        @Override
        public void mouseClicked(MouseEvent e) {
            if (e.getClickCount() == 1) {
                JLabel curLabel = (JLabel)e.getSource();
                ImageIcon curImageIcon = (ImageIcon)curLabel.getIcon();

                ImageIcon scaledImageIcon = changeToBiggerImageIcon(curImageIcon, curLabel.getWidth(), curLabel.getHeight());

                // view 를 기준으로 생성해서 보여주기
                JOptionPane.showMessageDialog(view, scaledImageIcon, "ENLARGED IMAGE", JOptionPane.PLAIN_MESSAGE);
            }
        }
    }

    //========================== ACTION LISTENER REACTION FUNCTIONS ==========================//

    private void searchImage(){
        if(view.getSearchTextField().getText().isEmpty()) return;
        else{
            // VIEW 에서 받아서
            String keyWord = view.getSearchTextField().getText();

            // SERVICE 한테 넘기기
            ArrayList<JLabel> newElementArr = imageService.GetKeywordImages(keyWord);

            // 각 JLABEL 에게 myMouseListener 바인딩하기
            // 이거 나중에 함수로 빼보기 Bind 뭐시기로
            for(int i=0;i<newElementArr.size();i++){
                newElementArr.get(i).addMouseListener(myMouseListener);
            }

            // VIEW 에 적용
            view.setElementArr(newElementArr);

            // VIEW 바꿔주기
            changeToSearchMode();
            addToCenterPanel(view.getElementArr());

            view.getHowMany().setVisible(true);
            view.getHowMany().setSelectedIndex(0);

            // SERVICE 통해서 기록
            logService.AddLog(keyWord);
        }
    }

    private void getAllLog(){
        // SERVICE 에서 받기
        ArrayList<JLabel> newElementArr = logService.GetAllLogs();

        // VIEW 에 적용하기 전에 ActionListener 달아주기
        for(int i=0;i<newElementArr.size();i++){
            // MouseListener 추가
            // newElementArr.get(i).addMouseListener((MouseListener) myButtonListener);
        }

        // VIEW 에 적용
        view.setElementArr(newElementArr);

        // VIEW 바꿔주기
        changeToLogMode();
        addToCenterPanel(view.getElementArr());
    }

    private void applyHowMany(ActionEvent e){
        // VIEW 정보 확인
        JComboBox comboBox = (JComboBox) e.getSource();
        String curHowMany = comboBox.getSelectedItem().toString();

        // VIEW 에 다시 적용
        if (curHowMany == "10") hideImage(10);
        else if (curHowMany == "20") hideImage(20);
        else if(curHowMany=="30") hideImage(30);
    }

    private void goBackToHome(){
        // VIEW ELEMENT 다 지워주기
        view.getElementArr().clear();

        // VIEW 바꿔주기
        changeToHomeMode();
        addToCenterPanel(view.getElementArr());
    }

    private void deleteAllLog(){
        // SERVICE 통해서 적용하고
        logService.DeleteAllLogs();

        // VIEW 에 적용
        view.getCenterPanel().removeAll();
        view.getCenterPanel().repaint();
    }
}
