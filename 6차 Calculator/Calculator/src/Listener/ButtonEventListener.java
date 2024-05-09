package Listener;

import Controller.MainController;

import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;

public class ButtonEventListener extends KeyAdapter {
    private MainController mainController;

    public ButtonEventListener(MainController mainController){
        this.mainController = mainController;
    }

    @Override
    public void keyPressed(KeyEvent e) {

        int key = e.getKeyCode();

        switch (key) {
            case KeyEvent.VK_0:
            case KeyEvent.VK_1:
            case KeyEvent.VK_2:
            case KeyEvent.VK_3:
            case KeyEvent.VK_4:
            case KeyEvent.VK_5:
            case KeyEvent.VK_6:
            case KeyEvent.VK_7:
            case KeyEvent.VK_8:
            case KeyEvent.VK_9:
                mainController.numBtnClicked(String.valueOf((char)key));
                break;
            case KeyEvent.VK_PERIOD:
                mainController.numBtnClicked(".");
                break;
            case KeyEvent.VK_ADD:
                System.out.println("add clicked");
                mainController.optBtnClicked("+");
                break;
            case KeyEvent.VK_SUBTRACT:
                mainController.optBtnClicked("-");
                break;
            case KeyEvent.VK_MULTIPLY:
                mainController.optBtnClicked("×");
                break;
            case KeyEvent.VK_DIVIDE:
                mainController.optBtnClicked("÷");
                break;
            case KeyEvent.VK_ENTER:
                mainController.optBtnClicked("=");
                break;
            case KeyEvent.VK_BACK_SPACE:
                mainController.eraseBtnClicked("<");
                break;
            case KeyEvent.VK_DELETE:
                mainController.eraseBtnClicked("CE");
                break;
            case KeyEvent.VK_ESCAPE:
                mainController.eraseBtnClicked("C");
                break;
        }
    }
}