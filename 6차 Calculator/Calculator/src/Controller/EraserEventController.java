package Controller;

import View.MainView;
import View.Panel.ResultPanel;

import javax.swing.*;
import java.awt.*;
import java.text.DecimalFormat;
import java.util.ArrayDeque;

public class EraserEventController {
    private ArrayDeque<String> numberDeque;
    private ArrayDeque<String> operatorDeque;

    private MainView mainView;

    public EraserEventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, MainView mainView) {
        this.numberDeque = numberDeque;
        this.operatorDeque = operatorDeque;

        this.mainView = mainView;
    }

    public void handleEraseEvent(String input){

        // impossible 이면 C 효과
        if(mainView.getResultPanel().getBigLabel().getText().equals("cant divide by zero!")){
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
            clearBtnClicked();
            return;
        }

        switch(input){
            case "CE":
                clearEntryBtnClicked();
                break;
            case "C":
                clearBtnClicked();
                break;
            case "<":
                backSpaceBtnClicked();
                break;
        }
    }

    // bigLabel 0으로 만듬
    private void clearEntryBtnClicked(){
        // 맨 마지막에 추가한거 빼주기
        numberDeque.removeLast();
        numberDeque.add("0");
        renderBigLabel();
    }

    // smallLabel bigLabel 모두 0으로 만듬
    private void clearBtnClicked(){
        numberDeque.clear();
        operatorDeque.clear();
        renderSmallLabel();
        numberDeque.add("0");
        renderBigLabel();
    }

    private void backSpaceBtnClicked(){

        // 새 숫자를 입력할때만 써져야함
        // 이미 들어간 숫자 삭제는 불가능해야함
        // 그래서 숫자가 연산자 개수보다 많을때만 가능
        if(numberDeque.size() > operatorDeque.size()){
            int length = numberDeque.getLast().length();
            if(length > 1){
                String lastNum = numberDeque.removeLast();
                numberDeque.add(lastNum.substring(0, length-1));
            }
            else if(length==1){
                numberDeque.removeLast();
                numberDeque.add("0");
            }
            renderBigLabel();
        }
    }

    private void renderSmallLabel(){
        Object[] numberArr = numberDeque.toArray();
        Object[] operatorArr = operatorDeque.toArray();

        StringBuilder sb = new StringBuilder();
        for(int i=0;i<numberArr.length;i++){
            sb.append(numberArr[i]);
            sb.append(" ");
            if(i<operatorArr.length) {
                sb.append(operatorArr[i]);
                sb.append(" ");
            }
        }

        mainView.renderSmallLabel(sb.toString());
    }

    private void renderBigLabel(){
        mainView.renderBigLabel(numberDeque.getLast());
    }
}
