package Listener;

import Controller.EventController;

import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;

// system 전반적인 keyboard event 를 얘가 모두 받음
// 받아서 actionCommand 까보고 그에 알맞는 eventController 로 전달해줌
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

        // SHIFT 예외처리
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
            case KeyEvent.VK_MINUS:
                operatorEventController.handleEvent("-");
                break;
            case KeyEvent.VK_MULTIPLY:
                operatorEventController.handleEvent("×");
                break;
            case KeyEvent.VK_DIVIDE:
            case KeyEvent.VK_SLASH:
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