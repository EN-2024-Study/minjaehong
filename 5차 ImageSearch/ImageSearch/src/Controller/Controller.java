package Controller;

import Service.ImageService;
import Service.LogService;

import Controller.Observer.ImageObserver;
import Controller.Observer.ButtonObserver;

import View.MainView;

import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.util.ArrayList;

public class Controller {
    private MainView view;

    private ImageService imageService;
    private LogService logService;

    private ButtonObserver ButtonObserver;
    private ImageObserver ImageObserver;

    public Controller(MainView view, ImageService imageService, LogService logService){
        this.view = view;
        this.imageService = imageService;
        this.logService = logService;

        ButtonObserver = new ButtonObserver(view, this);
        ImageObserver = new ImageObserver();

        // View 초기화
        //InitView();

        // Controller.Controller 가 View 영향끼칠 수 있게 ActionListener 를 Component 들에게 모두 주기
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
        view.getSearchBtn().addActionListener(ButtonObserver);
        view.getHowMany().addActionListener(ButtonObserver);
        view.getLogBtn().addActionListener(ButtonObserver);
        view.getBackToHomeBtn().addActionListener(ButtonObserver);

        // BOTTOM PANEL 에게 모두 바인딩
        view.getDeleteAllLogBtn().addActionListener(ButtonObserver);
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
        for (int i = 0; i < startIdx; i++) {
            view.getElementArr().get(i).setVisible(true);
        }
        for (int i = startIdx; i < 30; i++) {
            view.getElementArr().get(i).setVisible(false);
        }
    }

    private void addToCenterPanel(ArrayList<JLabel> elementArr){
        for(int i=0;i<elementArr.size();i++){
            view.getCenterPanel().add(elementArr.get(i));
        }
    }

    //========================== OBSERVER REACTION FUNCTIONS ==========================//

    public void searchImage(){
        if(view.getSearchTextField().getText().isEmpty()) return;
        else{
            // VIEW 에서 받아서
            String keyWord = view.getSearchTextField().getText();

            // SERVICE 통해서 기록
            logService.AddLog(keyWord);

            // SERVICE 한테 넘기기
            ArrayList<JLabel> newElementArr = imageService.GetKeywordImages(keyWord);

            // 각 JLABEL 에게 ImageObserver 바인딩하기
            // 이거 나중에 함수로 빼보기 Bind 뭐시기로
            for(int i=0;i<newElementArr.size();i++){
                newElementArr.get(i).addMouseListener(ImageObserver);
            }

            // VIEW 에 적용
            view.setElementArr(newElementArr);

            // VIEW 바꿔주기
            changeToSearchMode();
            addToCenterPanel(view.getElementArr());

            view.getHowMany().setVisible(true);
            view.getHowMany().setSelectedIndex(0);
        }
    }

    public void getAllLog(){
        // SERVICE 에서 받기
        ArrayList<JLabel> newElementArr = logService.GetAllLogs();

        // VIEW 에 적용하기 전에 ActionListener 달아주기
        /*
        for(int i=0;i<newElementArr.size();i++){
            // MouseListener 추가
            // newElementArr.get(i).addMouseListener((MouseListener) ButtonObserver);
        }
        */

        // VIEW 에 적용
        view.setElementArr(newElementArr);

        // VIEW 바꿔주기
        changeToLogMode();
        addToCenterPanel(view.getElementArr());
    }

    public void applyHowMany(ActionEvent e){
        // VIEW 정보 확인
        JComboBox comboBox = (JComboBox) e.getSource();
        String curHowMany = comboBox.getSelectedItem().toString();

        // VIEW 에 다시 적용
        if (curHowMany == "10") hideImage(10);
        else if (curHowMany == "20") hideImage(20);
        else if(curHowMany=="30") hideImage(30);
    }

    public void goBackToHome(){
        // VIEW ELEMENT 다 지워주기
        view.getElementArr().clear();

        // VIEW 바꿔주기
        changeToHomeMode();
        addToCenterPanel(view.getElementArr());
    }

    public void deleteAllLog(){
        // SERVICE 통해서 적용하고
        logService.DeleteAllLogs();

        // VIEW 에 적용
        view.getCenterPanel().removeAll();
        view.getCenterPanel().repaint();
    }
}
