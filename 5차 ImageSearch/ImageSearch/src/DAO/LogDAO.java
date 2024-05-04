package DAO;

import java.util.ArrayList;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

// JAVA > JDBC API > JDBC DRIVER > DBMS(각종 DB들)
// JDBC API는 JDK에 포함되어있으며 DBMS에 상관없이 사용할 수 있는 API를 제공한다
// JDK에 이미 포함이 되어있기 때문에 따로 다운로드하거나 설치할 필요는 없다
public class LogDAO {

    // 1. JDBC 드라이버 : DBMS(사용하는 실제 DB)에 맞는 드라이버를 runtime 때 로딩할 때 필요함
    // "com.mysql.cj.jdbc.Driver" == 최신 버전 (MySQLConnector/J 버전)
    // "com.mysql.jdbc.Driver" == 구 버전
    // 나는 8.036 버전이라 cj 꺼를 써야함
    // 쉽게 말하면 runtime 에 자기가 쓸 DB에 맞는 JDBC 드라이버 로딩하기 위해 필요한 JDBC DRIVER URL임
    // 얘를 Class.forName 할때 인자로 넘기면 jdbcURL에 맞는 드라이버를 자동으로 등록해줌
    private String driverURL = "com.mysql.cj.jdbc.Driver";

    // 2. URL : DBMS(사용하는 실제 DB)와 연결하기 위한 URL
    // 사용하는 DB에 따라서 형식이 다르기 때문에 DB에 따라서 입력값이 달라짐
    // 얘는 위 jdbcURL을 통해 얻은 Driver의 인자로 넘겨주면 되는거임
    private String jdbcURL = "jdbc:mysql://localhost/ensharp";

    Connection conn = null;

    public ArrayList<String> getAllLogs(){
        ArrayList<String> logList = new ArrayList<>();

        try{
            // 1. dynamically loads JDBC driver class from certain URL
            // JDBC driver 는 default 로 로딩되는게 아니라 runtime 에 로딩되서 그럼
            // triggers the static initializer block of the JDBC driver class
            // 해당 JDBC driver class를 static 으로 알아서 올려줌
            //Class.forName(driverURL);
            Class.forName("com.mysql.cj.jdbc.Driver");

            System.out.println("GOT DRIVER IN RUNTIME");

            // 2. Driver Manager에게 Connection 객체를 달라고 요청
            // 위 코드로 runtime에 등록된 DriverManager 가지고 getConnection 해달라는 거임
            // 위에 코드때문에 DriverManager 사용할 수 있는거
            // 저거로 사용할 Driver 를 미리 등록했기 때문
            conn = DriverManager.getConnection(jdbcURL,"root","1234");

            System.out.println("CONNECT SUCCESS");
        }catch(ClassNotFoundException e){
            System.out.print("CLASS NOT FOUND EXCEPTION");
        }catch(SQLException e){
            System.out.print("GET CONNECTION EXCEPTION");
        }

        return logList;
    }
}
