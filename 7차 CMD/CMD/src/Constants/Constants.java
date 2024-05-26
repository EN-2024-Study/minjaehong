package constants;

public final class Constants {

    // VIEW
    public static final String DIR_DATE_FORMAT = "yyyy-MM-dd a hh:mm";
    public static final String DIR_SYMBOL = "<DIR> ";
    public static final String DIR_DIRECTORY = " 디렉터리\n\n";
    public static final String DIR_FILE = " 파일    ";
    public static final String DIR_BYTE = " 바이트\n";
    public static final String DIR_BYTE_LEFT = " 바이트 남음\n";
    public static final String CLS_TEXT = "\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";

    public static final String HELP_TEXT = "특정 명령어에 대한 자세한 내용이 필요하면 HELP 명령어 이름을 입력하십시오.\n"
            + "CD       현재 디렉터리 이름을 보여주거나 바꿉니다.\n"
            + "CLS      화면을 지웁니다.\n"
            + "COPY     하나 이상의 파일을 다른 위치로 복사합니다.\n"
            + "DIR      디렉터리에 있는 파일과 하위 디렉터리 목록을 보여줍니다.\n"
            + "MOVE     하나 이상의 파일을 한 디렉터리에서 다른 디렉터리로 이동합니다.\n"
            + "HELP     Windows 명령에 대한 도움말 정보를 제공합니다.\n"
            + "EXIT     CMD.EXE 프로그램(명령 인터프리터)을 종료합니다.\n"
            + "도구에 대한 자세한 내용은 온라인 도움말의 명령줄 참조를 참조하십시오.\n";

    public static final String CANT_FIND_FILE = "\n파일을 찾을 수 없습니다.\n";

    // CONTROLLER
    public static final String MICROSOFT_LICENSE = "(c) Microsoft Corporation. All rights reserved.\n";
    public static final String CMD_EXE = "cmd.exe";
    public static final String EXECUTE = "/C";
    public static final String VER = "ver";
    public static final String VOL = "vol";
    public static final String USER_HOME = "user.home";

    public static final String WRONG_LABEL = "파일 이름, 디렉터리 이름 또는 볼륨 레이블 구문이 잘못되었습니다.\n";
    public static final String WRONG_CMD = "%s은(는) 내부 또는 외부 명령, 실행할 수 있는 프로그램, 또는 배치 파일이 아닙니다.\n";
    public static final String NOT_EXPECTED = "%s은(는) 예상되지 않았습니다.\n";

    // RUNTIMEEXCEPTIONHANDLER
    public static final String ASK_OVERWRITE = "을(를) 덮어쓰시겠습니까? (Yes/No/All) ";

    // SERVICE
    public static final String CANT_FIND_CERTAIN_FILE = "지정된 파일을 찾을 수 없습니다.\n";
    public static final String WRONG_COMMAND = "명령 구문이 올바르지 않습니다.\n";

    // DIRDAO
    public static final String TO_ME = ".";
    public static final String TO_PARENT = "..";

    // CDSERVICE
    public static final String CANT_FIND_CERTAIN_ROUTE = "지정된 경로를 찾을 수 없습니다.\n";
    public static final String WRONG_DIRECTORY_NAME = "디렉터리 이름이 올바르지 않습니다.\n";

    // CPSERVICE
    public static final String ZERO_FILE_COPIED = "0개 파일이 복사되었습니다.\n";
    public static final String ONE_FILE_COPIED = "1개 파일이 복사되었습니다.\n";
    public static final String CANT_COPY_TO_SAME_FILE = "같은 파일로 복사할 수 없습니다.\n0개 파일이 복사되었습니다.\n";
    public static final String N_FILE_COPIED = " 개 파일이 복사되었습니다.\n";

    // MVSERVICE
    public static final String ZERO_FILE_MOVED = "1개의 파일을 이동했습니다.\n";
    public static final String ONE_FILE_MOVED = "1개의 파일을 이동했습니다.\n";
    public static final String ZERO_DIRECTORY_MOVED = "0개 디렉터리를 이동했습니다.\n";
    public static final String ONE_DIRECTORY_MOVED = "1개 디렉터리를 이동했습니다.\n";
    public static final String ACCESS_DENIED = "액세스가 거부되었습니다.\n";
}
