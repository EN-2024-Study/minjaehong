import View.MainView;
import Controller.Controller;

public class CalculatorApp {
    public static void main(String[] args) {
        // 모든 Panel 다 붙여놓음
        MainView mainView = new MainView();
        //
        Controller controller = new Controller(mainView);
        mainView.setVisible(true);
    }
}
