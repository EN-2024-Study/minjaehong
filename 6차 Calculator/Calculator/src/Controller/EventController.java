package Controller;

import View.MainView;

import java.awt.*;
import java.util.ArrayDeque;

public abstract class EventController{
    protected ArrayDeque<String> numberDeque;
    protected ArrayDeque<String> operatorDeque;

    protected MainView mainView;

    protected EventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, MainView mainView) {
        this.numberDeque = numberDeque;
        this.operatorDeque = operatorDeque;

        this.mainView = mainView;
    }

    // 무조건 자식클래스에서 구현되어야함
    public abstract void handleEvent(String userInput);

    protected void renderBigLabel(){
        mainView.renderBigLabel(numberDeque.getLast());
    }

    protected void renderSmallLabel() {

        /* 이거 없어도 되긴함
        if(numberDeque.size()==0 && operatorDeque.size()==0) {
            mainView.renderSmallLabel("");
            return;
        }
        */
        
        Object[] numberArr = numberDeque.toArray();
        Object[] operatorArr = operatorDeque.toArray();

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < numberArr.length; i++) {
            sb.append(numberArr[i]);
            sb.append(" ");

            sb.append(operatorArr[i]);
            sb.append(" ");
        }

        mainView.renderSmallLabel(sb.toString());
    }

    // 현재 결과가 cant divide by zero 인지 확인해줌
    protected boolean checkIfCantDivideByZeroState(){
        if(mainView.getResultPanel().getBigLabel().getText().equals("cant divide by zero!")) return true;
        else return false;
    }

    // cant divide by zero 로 disabled 된 button 들을 다시 enabled 시켜줌
    // 이건 모든 자식 controller 들이 가져야할 기능이라 부모 클래스에 있어야함
    protected void changeToNormalState(){
        
        // disabled 된 button 다시 enable
        mainView.getButtonPanel().getDotButton().setEnabled(true);
        mainView.getButtonPanel().getDotButton().setBackground(Color.WHITE);
        mainView.getButtonPanel().getDivButton().setEnabled(true);
        mainView.getButtonPanel().getDivButton().setBackground(Color.WHITE);
        mainView.getButtonPanel().getAddButton().setEnabled(true);
        mainView.getButtonPanel().getAddButton().setBackground(Color.WHITE);
        mainView.getButtonPanel().getMulButton().setEnabled(true);
        mainView.getButtonPanel().getMulButton().setBackground(Color.WHITE);
        mainView.getButtonPanel().getSubButton().setEnabled(true);
        mainView.getButtonPanel().getSubButton().setBackground(Color.WHITE);
        mainView.getButtonPanel().getNegateButton().setEnabled(true);
        mainView.getButtonPanel().getNegateButton().setBackground(Color.WHITE);
        
        // deque 다 비우고 default 0 집어넣어주기
        numberDeque.clear();
        operatorDeque.clear();
        renderSmallLabel();
        numberDeque.add("0");
        renderBigLabel();
    }
}
