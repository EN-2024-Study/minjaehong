import Service.ImageService;
import Service.LogService;

import javax.swing.*;
import javax.swing.border.LineBorder;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.MouseListener;
import java.util.ArrayList;

public class Controller {
    private View view;

    private ImageService imageService;
    private LogService logService;

    private MyListener myListener;

    public Controller(View view, ImageService imageService, LogService logService){
        this.view = view;
        this.imageService = imageService;
        this.logService = logService;

        myListener = new MyListener();

        // View 초기화
        //InitView();

        // Controller 가 View 영향끼칠 수 있게 ActionListener 를 Component 들에게 모두 주기
        BindActionListenerToViewComponents();

        // 다 준비되었으면 HomeMode로 시작하기
        changeToHomeMode();
    }

    public void InitView(){
        view.InitializeTopPanel();
        view.InitializeCenterPanel();
        view.InitializeBottomPanel();
        changeToHomeMode(); // ?
    }

    private void BindActionListenerToViewComponents(){
        // TOP PANEL 에게 모두 바인딩
        view.getSearchBtn().addActionListener(myListener);
        view.getHowMany().addActionListener(myListener);
        view.getLogBtn().addActionListener(myListener);
        view.getBackToHomeBtn().addActionListener(myListener);

        // BOTTOM PANEL 에게 모두 바인딩
        view.getDeleteAllLogBtn().addActionListener(myListener);
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

    //========================== ACTIONLISTENER ==========================//

    private void searchImage(){
        if(view.getSearchTextField().getText().isEmpty()) return;
        else{
            // VIEW 에서 받아서
            String keyWord = view.getSearchTextField().getText();

            // SERVICE 한테 넘기기
            view.setElementArr(imageService.GetKeywordImages(keyWord));

            // VIEW 에 적용
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
            // newElementArr.get(i).addMouseListener((MouseListener) myListener);
        }

        // VIEW 에 적용
        view.setElementArr(newElementArr);

        // VIEW 바꿔주기
        changeToLogMode();
        addToCenterPanel(view.getElementArr());
    }

    private void applyHowMany(String curHowMany){
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

    private class MyListener implements ActionListener{

        public MyListener(){

        }

        @Override
        public void actionPerformed(ActionEvent e) {
            if(e.getSource()==view.getSearchBtn()){
                searchImage();
            }

            if(e.getSource() == view.getLogBtn()){
                getAllLog();
            }

            if(e.getSource()==view.getHowMany()){
                JComboBox comboBox = (JComboBox) e.getSource();
                String curHowMany = comboBox.getSelectedItem().toString();

                applyHowMany(curHowMany);
            }

            if(e.getSource()==view.getBackToHomeBtn()){
                goBackToHome();
            }

            if(e.getSource()==view.getDeleteAllLogBtn()){
                deleteAllLog();
            }
        }
    }
}
