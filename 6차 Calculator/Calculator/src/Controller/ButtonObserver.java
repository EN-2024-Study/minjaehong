package Controller;

import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;

public class ButtonObserver extends KeyAdapter {
    Controller controller;

    public ButtonObserver(Controller controller ){
        this.controller = controller;
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
                controller.numBtnClicked(String.valueOf((char)key));
                break;
            case KeyEvent.VK_PERIOD:
                controller.numBtnClicked(".");
                break;
            case KeyEvent.VK_ADD:
                System.out.println("add clicked");
                controller.optBtnClicked("+");
                break;
            case KeyEvent.VK_SUBTRACT:
                controller.optBtnClicked("-");
                break;
            case KeyEvent.VK_MULTIPLY:
                controller.optBtnClicked("×");
                break;
            case KeyEvent.VK_DIVIDE:
                controller.optBtnClicked("÷");
                break;
            case KeyEvent.VK_ENTER:
                controller.optBtnClicked("=");
                break;
            case KeyEvent.VK_BACK_SPACE:
                controller.backSpaceBtnClicked();
                break;
            case KeyEvent.VK_DELETE:
                controller.clearEntryBtnClicked();
                break;
            case KeyEvent.VK_ESCAPE:
                controller.clearBtnClicked();
                break;
            default:
                break;
        }
    }
}
