import Controller.MainController;
import View.MainView;

// 변수명 일괄변경 = shift + f6
// 주석처리 = ctrl shift alt + /
// 자동정렬 = 정렬할 부분 지정하고 ctrl shift alt + l
public class CalculatorApp {
    public static void main(String[] args) {

        // 모든 Panel 붙어있는 MainView 생성해놓기
        MainView mainView = new MainView();
        mainView.setVisible(true);
        MainController mainController = new MainController(mainView);
    }
}
