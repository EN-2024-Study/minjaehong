package model;

public class AppConfig {

    // 1. JDBC 드라이버 : DBMS(사용하는 실제 DB)에 맞는 드라이버를 runtime 때 로딩할 때 필요함
    // "com.mysql.cj.jdbc.Driver" == 최신 버전 (MySQLConnector/J 버전)
    // "com.mysql.jdbc.Driver" == 구 버전
    // 나는 8.036 버전이라 cj 꺼를 써야함
    // 쉽게 말하면 runtime 에 자기가 쓸 DB에 맞는 JDBC 드라이버 로딩하기 위해 필요한 JDBC DRIVER URL임
    // 얘를 Class.forName 할때 인자로 넘기면 jdbcURL에 맞는 드라이버를 자동으로 등록해줌
    // 이 jdbcDriverURL 가지고 Class.forName 쓰면 알아서 JDBC 드라이버를 등록해주는거임
    public static final String JDBC_DRIVER_URL = "com.mysql.cj.jdbc.Driver";

    // 2. DB URL : DBMS(사용하는 실제 DB)와 연결하기 위한 URL
    // 사용하는 DB에 따라서 형식이 다르기 때문에 DB에 따라서 입력값이 달라짐
    // 얘는 위 jdbcDriverURL을 통해 얻은 JDBCDriver의 인자로 넘겨주면 실제 DB Connection 반환해줌
    public static final String DB_URL = "jdbc:mysql://localhost/ensharp";

    // ENSHARP DB ID AND PW
    public static final String DB_USERNAME = "root";
    public static final String DB_PASSWORD = "1234";

}