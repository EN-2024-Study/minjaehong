package Controller;

import View.Frame.MainView;

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
    public void handleEvent(String log) {

        String head = "<html><div style = 'text-align:right;'>";
        String body = "<br>";
        String tail = "</div></html>";

        log = log.replace(head, "").replace(tail, "");

        String[] logDataArr = log.split("<br>");

        String newSmallLabel = logDataArr[0];
        String newBigLabel = logDataArr[1];

        numberDeque.clear();
        operatorDeque.clear();

        numberDeque.add(newBigLabel);
        operatorDeque.add("=");

        renderBigLabel();
        mainView.getResultPanel().getSmallLabel().setText(newSmallLabel);
    }
}
