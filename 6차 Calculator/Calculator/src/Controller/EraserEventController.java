package Controller;

import View.MainView;
import java.util.ArrayDeque;

public class EraserEventController extends EventController{

    public EraserEventController(ArrayDeque<String> numberDeque, ArrayDeque<String> operatorDeque, MainView mainView) {
        super(numberDeque, operatorDeque, mainView);
    }

    @Override
    public void handleEvent(String input){

        // cant divide by zero 상태이면 다시 정상화시키기
        if(checkIfCantDivideByZeroState()){
            changeToNormalState();
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
}
