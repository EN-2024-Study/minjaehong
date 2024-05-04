import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class Controller {
    View view;

    //Controller(MainFrame view, MainSerice service)
    public Controller(View view){
        this.view = view;
        InitView();
    }

    public void InitView(){
        view.InitializeTopPanel();
        view.InitializeCenterPanel();
        view.InitializeBottomPanel();
    }

    public void InitController(){
        BindActionListenersToViewComponents();
    }

    public void BindActionListenersToViewComponents(){

    }

    private class MyListener implements ActionListener{
        private final Controller controller;

        public MyListener(Controller c){
            this.controller = c;
        }

        @Override
        public void actionPerformed(ActionEvent e) {
            //if(e.getSource())
        }
    }
}
