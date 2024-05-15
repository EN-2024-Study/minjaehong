package Controller;

import View.Frame.MainView;

import java.awt.*;
import java.util.ArrayDeque;

// [LogEventController 작동 원리]
// 1. LogButton 이 눌린다
// 2. ButtonListener 가 log button click 된거 감지
// 3. ButtonListener 는 logEventController 한테 넘김
// 4. 그럼 얘가 smallLabel 이랑 bigLabel 을 바꿔줌
// 그니까 얘는 그냥 LogButton Text 값 받아서 파싱한 다음 smallLabel 이랑 bigLabel 최신화 해주면 됨
public class LogEventController extends EventController{

    public LogEventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, MainView mainView) {
        super(numberDeque, operatorDeque, mainView);
    }

    @Override
    public void handleEvent(String buttonText) {
        if(buttonText.startsWith("<html>")){
            handleLogHistory(buttonText);
        }else{
            handleShowLogButton();
        }
    }

    // showLogButton 눌렸을때
    private void handleShowLogButton(){

        GridBagConstraints gbc = new GridBagConstraints();

        mainView.getResultPanel().setBackground(Color.DARK_GRAY);
        mainView.getButtonPanel().setVisible(false);

        Dimension buttonPanelSize = mainView.getButtonPanel().getSize();
        mainView.getLogPanel().setSize(buttonPanelSize);

        gbc.gridx = 0;
        gbc.gridy = 1;
        gbc.gridwidth = 1;
        gbc.gridheight = 1;
        gbc.weightx = 1.0;
        gbc.weighty = 0.6;
        gbc.fill = GridBagConstraints.BOTH;
        mainView.add(mainView.getLogPanel(),gbc);

        mainView.getResultPanel().getShowLogButton().setEnabled(false);
        mainView.getButtonPanel().setVisible(false);
        mainView.getLogPanel().setVisible(true);
    }

    // LogPanel 에 있는 LogButton 눌렸을때
    // log 파싱하고 numberDeque 랑 operatorDeque 를 수동으로 조작해서
    // 다음 연산이 가능한 상태로 만들어줘야함
    private void handleLogHistory(String log){
        // log parsing 하기
        String head = "<html><div style = 'text-align:right;'>";
        String body = "<br>";
        String tail = "</div></html>";

        log = log.replace(head, "").replace(tail, "");

        String[] logDataArr = log.split("<br>");

        String newSmallLabel = logDataArr[0];
        String newBigLabel = logDataArr[1];

        // numberDeque operatorDeque 조작
        numberDeque.clear();
        operatorDeque.clear();
        numberDeque.add(newBigLabel);
        operatorDeque.add("=");

        // bigLabel smallLabel rendering
        renderBigLabel();
        mainView.getResultPanel().getSmallLabel().setText(newSmallLabel);

        // 다시 mainView 로 focus 주기
        mainView.setFocusable(true);
        mainView.requestFocus();
    }
}
