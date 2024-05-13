package Constants;

// 정적 상수는 public static final 로 선언
// [상수 관리 클래스 선언 방식]
// 1. final 클래스로 선언합니다
// 이 클래스는 상수 관리 목적 외 쓰임새로 확장되어선 안 된다는 설계입니다
// 2. 생성자는 private 으로 막아 둡니다
// 이 클래스는 인스턴스 생성이 불필요하며, 완전 유틸성 클래스임을 보장합니다

public final class ConstValue {
    public static final String KEYBOARD_ZERO = "0";
    public static final String KEYBOARD_ONE = "1";
    public static final String KEYBOARD_TWO = "2";
    public static final String KEYBOARD_THREE = "3";
    public static final String KEYBOARD_FOUR = "4";
    public static final String KEYBOARD_FIVE = "5";
    public static final String KEYBOARD_SIX = "6";
    public static final String KEYBOARD_SEVEN = "7";
    public static final String KEYBOARD_EIGHT = "8";
    public static final String KEYBOARD_NINE = "9";

    public static final String KEYBOARD_MULTIPLY = "×";
    public static final String KEYBOARD_DIVIDE = "÷";
    public static final String KEYBOARD_PLUS = "+";
    public static final String KEYBOARD_MINUS = "-";

    public static final String KEYBOARD_BACKSPACE = "<";
    public static final String KEYBOARD_C = "C";
    public static final String KEYBOARD_CE = "CE";
    public static final String KEYBOARD_DECIMAL_POINT = ".";
    public static final String KEYBOARD_EQUAL = "2";

    public static final String KEYBOARD_NEGATE = "+/-";

    public static final String DIVIDE_BY_ZERO_SIGN = "cant divide by zero!";

    public static String APP_ICON = "src/Images/AppIcon.png";
}
