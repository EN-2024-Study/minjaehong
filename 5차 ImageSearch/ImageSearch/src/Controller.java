import Service.ImageService;
import Service.LogService;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.util.ArrayList;

public class Controller {
    View view;

    ImageService imageService;
    LogService logService;

    MyListener myListener;

    public Controller(View view, ImageService imageService, LogService logService){
        this.view = view;
        this.imageService = imageService;
        this.logService = logService;

        myListener = new MyListener();

        // View 초기화
        //InitView();
        // Controller 가 View 영향끼칠 수 있게 ActionListener 를 Component 들에게 모두 주기
        BindActionListenersToViewComponents();

        changeToHomeMode();
    }

    public void InitView(){
        view.InitializeTopPanel();
        view.InitializeCenterPanel();
        view.InitializeBottomPanel();
        changeToHomeMode();
    }

    public void BindActionListenersToViewComponents(){
        // TOP PANEL 에게 모두 바인딩
        view.getSearchBtn().addActionListener(myListener);
        view.getHowMany().addActionListener(myListener);
        view.getLogBtn().addActionListener(myListener);
        view.getBackToHomeBtn().addActionListener(myListener);

        // BOTTOM PANEL 에게 모두 바인딩
        view.getDeleteAllLogBtn().addActionListener(myListener);
    }

    public void changeToHomeMode() {
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

    public void changeToSearchMode() {
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

    public void changeToLogMode() {
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

    public void hideImage(int startIdx) {
        for (int i = 0; i < startIdx; i++) view.getElementArr().get(i).setVisible(true);
        for (int i = startIdx; i < 30; i++) view.getElementArr().get(i).setVisible(false);
    }

    public void addToCenterPanel(ArrayList<JLabel> elementArr){
        for(int i=0;i<elementArr.size();i++){
            view.getCenterPanel().add(elementArr.get(i));
        }
    }

    private class MyListener implements ActionListener{
        //final Controller controller;

        public MyListener(){

        }

        /*
        public MyListener(Controller c){
            this.controller = c;
        }
        */

        @Override
        public void actionPerformed(ActionEvent e) {
            if(e.getSource()==view.getSearchBtn()){
                if(view.getSearchTextField().getText().isEmpty()) return;
                else{
                    // VIEW에서 받아서
                    String keyWord = view.getSearchTextField().getText();

                    // SERVICE한테 넘기기
                    view.elementArr = imageService.GetKeywordImages(keyWord);

                    // VIEW바꿔주기
                    changeToSearchMode();
                    addToCenterPanel(view.getElementArr());

                    view.getHowMany().setVisible(true);
                    view.getHowMany().setSelectedIndex(0);

                    // SERVICE에 기록
                    logService.AddLog(keyWord);
                }
            }

            if(e.getSource() == view.logBtn){
                view.elementArr = view.logService.GetAllLogs();

                // VIEW 바꿔주기
                changeToLogMode();
                addToCenterPanel(view.getElementArr());
            }

            if(e.getSource()==view.howMany){
                JComboBox comboBox = (JComboBox) e.getSource();
                String curHowMany = comboBox.getSelectedItem().toString();
                if (curHowMany == "10") hideImage(10);
                else if (curHowMany == "20") hideImage(20);
                else if(curHowMany=="30") hideImage(30);
            }

            if(e.getSource()==view.backToHomeBtn){
                view.getElementArr().clear();
                // VIEW 바꿔주기
                addToCenterPanel(view.elementArr);
                changeToHomeMode();
            }

            if(e.getSource()==view.deleteAllLogBtn){
                logService.DeleteAllLogs();
                view.getCenterPanel().removeAll();
                view.getCenterPanel().repaint();
            }
        }
    }
}
