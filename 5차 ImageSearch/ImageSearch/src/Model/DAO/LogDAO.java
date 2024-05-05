package Model.DAO;

import Model.VO.LogListVO;
import Model.AppConfig;

import java.sql.*;
import java.util.ArrayList;

// JAVA > JDBC API > JDBC DRIVER > DBMS(각종 DB들)
// JDBC API는 DBMS(실제 DB들)에 상관없이 사용할 수 있는 API를 제공함
// 얘내는 JDK에 이미 포함이 되어있기 때문에 따로 다운로드하거나 설치할 필요가 없음

// 여기서 ConnectionPool 못씀??
// 매번 JDBC 드라이버에서 Connection 을 불러오는 대신 Connection Pool 사용
// 커넥션 풀에서 미리 준비되어 있는 Connection 객체를 받아 쓰기만 하면 됨
public class LogDAO {

    public static LogDAO instance;

    // 얘는 return을 DB에 적용된 row 개수로 해줌
    // 이걸로 성공했는지 판단가능
    // 얘내는 ? 로 매개변수 지정 가능
    private PreparedStatement preparedStatement;

    // 얘내는 매개변수 없는 sql 문
    // select 같은거에 사용
    private Statement statement;

    // select 한 rows 를 말그대로 set 형식으로 받음
    // 얘내 method 사용해서 받은 row를 조회가능
    private ResultSet resultSet;

    // singleton 으로 해놔 conn 변수가 딱 한번만 초기화되게 하자
    private LogDAO(){
        try {
            // dynamically loads JDBC driver class from certain URL
            // JDBC driver 는 default 로 로딩되는게 아니라 runtime 에 로딩되서 그럼
            // triggers the static initializer block of the JDBC driver class
            // 해당 JDBC driver class를 static 으로 알아서 올려줌
            Class.forName(AppConfig.JDBC_DRIVER_URL);

            System.out.println("[SUCCESS] GOT DRIVERMANAGER IN RUNTIME");
        }catch(Exception e){

        }
    }

    public static LogDAO GetInstance(){
        if(instance==null){
            instance = new LogDAO();
        }
        return instance;
    }

    //=========================== SINGLETON =============================//

    // 매번 JDBC DRIVER 에서 Connection 객체를 생성해서 반환함
    // conn.Close() 하면 매번 다시 JDBC DRIVER 에서 새로운 Connection 객체를 받아와야함
    // 1. 그냥 열어놓기
    // 2. 매번 Connection 객체 받아오기
    private Connection GetConnection(){
        Connection newConnectionObject = null;

        try{
            // 2. Driver Manager에게 새로운 Connection 객체를 달라고 요청
            // 위 코드로 runtime에 등록된 DriverManager 가지고 getConnection 해달라는 거임
            // 위에 코드때문에 DriverManager 사용할 수 있는거
            // 저거로 사용할 Driver 를 미리 등록했기 때문
            newConnectionObject = DriverManager.getConnection(AppConfig.DB_URL,AppConfig.DB_USERNAME,AppConfig.DB_PASSWORD);

            System.out.println("[SUCCESS] GOT NEW CONNECTION FROM DRIVERMANAGER");
        }catch(Exception e){
            System.out.println("[FAIL] GET CONNECTION");
        }

        return newConnectionObject;
    }

    //=========================== ADD LOG =============================//

    public void AddLog(String keyWord){

        // AddLog 할때 사용할 Connection 객체를 참조할 Connection 참조변수
        // 매번 GetConnection 으로 받아줘야함
        Connection conn = null;

        try{
            conn = GetConnection();

            statement = conn.createStatement();

            String addLogQuery = "INSERT INTO imagesearchlogdb VALUES (?)";

            preparedStatement = conn.prepareStatement(addLogQuery);
            preparedStatement.setString(1, keyWord);
            int success = preparedStatement.executeUpdate();

            if (success > 0) System.out.println("[SUCCESS] ADD LOG");
            else System.out.println("[FAIL] ADD LOG");

            // 자원반환
            preparedStatement.close();
            conn.close();

        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    //=========================== GET ALL LOG =============================//

    public LogListVO GetLogs(){
        Connection conn = null;
        ArrayList<String> logList = new ArrayList<>();

        try{
            // 새로운 Connection 객체 받기
            conn = GetConnection();

            statement = conn.createStatement();

            String selectLogQuery = "SELECT keyword FROM imagesearchlogdb";

            // select 문으로 조회된 row를 다 받기
            resultSet = statement.executeQuery(selectLogQuery);

            while (resultSet.next()) {
                String curLog = resultSet.getString("keyword");
                logList.add(curLog);
                System.out.println("[LOG KEYWORD] "+curLog);
            }

            // 자원반환
            conn.close();
            statement.close();
            resultSet.close();

        }catch (SQLException e) {
            e.printStackTrace();
        }

        return new LogListVO(logList);
    }

    //=========================== DELETE ALL LOG =============================//
    public void DeleteAll(){
        Connection conn = null;

        try{
            // 새로운 Connection 객체 받기
            conn = GetConnection();

            statement = conn.createStatement();

            String deleteLogQuery = "DELETE FROM imagesearchlogdb";

            // 로그 전체 삭제 실행
            statement.executeUpdate(deleteLogQuery);

            System.out.println("[SUCCESS] LOG DELETE");
            // 자원반환
            conn.close();
            statement.close();
        }catch (SQLException e) {
            e.printStackTrace();
        }
    }
}
