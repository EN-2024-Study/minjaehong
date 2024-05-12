package Listener;

import Controller.EventController;

import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;

public class KeyBoardListener extends KeyAdapter {
    private EventController numberEventController;
    private EventController operatorEventController;
    private EventController erasorEventController;

    public KeyBoardListener(EventController[] eventControllers) {
        this.numberEventController = eventControllers[0];
        this.operatorEventController = eventControllers[1];
        this.erasorEventController = eventControllers[2];
    }

    @Override
    public void keyPressed(KeyEvent e) {

        int key = e.getKeyCode();

        // shift 예외 상황 처리
        if (e.isShiftDown()) {
            switch (key) {
                // shift + "="
                case KeyEvent.VK_EQUALS:
                    operatorEventController.handleEvent("+");
                    break;
                // shift + 8
                case KeyEvent.VK_8:
                    operatorEventController.handleEvent("×");
                    break;
            }
            return;
        }

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
                numberEventController.handleEvent(String.valueOf((char) key));
                break;
            case KeyEvent.VK_PERIOD:
                numberEventController.handleEvent(".");
                break;
            case KeyEvent.VK_ADD:
                operatorEventController.handleEvent("+");
                break;
            case KeyEvent.VK_SUBTRACT:
                operatorEventController.handleEvent("-");
                break;
            case KeyEvent.VK_MULTIPLY:
                operatorEventController.handleEvent("×");
                break;
            case KeyEvent.VK_DIVIDE:
                operatorEventController.handleEvent("÷");
                break;
            case KeyEvent.VK_ENTER:
            case KeyEvent.VK_EQUALS:
                operatorEventController.handleEvent("=");
                break;
            case KeyEvent.VK_BACK_SPACE:
                erasorEventController.handleEvent("<");
                break;
            case KeyEvent.VK_DELETE:
                erasorEventController.handleEvent("CE");
                break;
            case KeyEvent.VK_ESCAPE:
                erasorEventController.handleEvent("C");
                break;
        }
    }
}