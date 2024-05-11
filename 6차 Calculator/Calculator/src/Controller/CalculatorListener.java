package Controller;

public interface CalculatorListener {
    public void numberButtonClicked(String newNum);

    public void operatorButtonClicked(String newOperator);

    public void eraseBtnClicked(String eraser);
}