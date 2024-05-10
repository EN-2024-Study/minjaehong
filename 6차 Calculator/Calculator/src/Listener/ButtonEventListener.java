package Listener;

import Controller.Calculator;

import java.awt.event.KeyAdapter;
import java.awt.event.KeyEvent;

public class ButtonEventListener extends KeyAdapter {
    private Calculator calculator;

    public ButtonEventListener(Calculator calculator){
        this.calculator = calculator;
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
                calculator.numberButtonClicked(String.valueOf((char)key));
                break;
            case KeyEvent.VK_PERIOD:
                calculator.numberButtonClicked(".");
                break;
            case KeyEvent.VK_ADD:
                calculator.operatorButtonClicked("+");
                break;
            case KeyEvent.VK_SUBTRACT:
                calculator.operatorButtonClicked("-");
                break;
            case KeyEvent.VK_MULTIPLY:
                calculator.operatorButtonClicked("×");
                break;
            case KeyEvent.VK_DIVIDE:
                calculator.operatorButtonClicked("÷");
                break;
            case KeyEvent.VK_ENTER:
                calculator.operatorButtonClicked("=");
                break;
            case KeyEvent.VK_BACK_SPACE:
                calculator.eraseBtnClicked("<");
                break;
            case KeyEvent.VK_DELETE:
                calculator.eraseBtnClicked("CE");
                break;
            case KeyEvent.VK_ESCAPE:
                calculator.eraseBtnClicked("C");
                break;
        }
    }
}