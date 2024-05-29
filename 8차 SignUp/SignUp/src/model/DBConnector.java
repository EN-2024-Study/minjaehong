package model;

import java.sql.Connection;
import java.sql.DriverManager;

public class DBConnector {
    public Connection getConnection(){
        Connection newConnectionObject = null;

        try{
            // 2. Driver Manager에게 새로운 Connection 객체를 달라고 요청
            // 위 코드로 runtime에 등록된 DriverManager 가지고 getConnection 해달라는 거임
            // 위에 코드때문에 DriverManager 사용할 수 있는거
            // 저거로 사용할 Driver 를 미리 등록했기 때문
            newConnectionObject = DriverManager.getConnection(AppConfig.DB_URL,AppConfig.DB_USERNAME,AppConfig.DB_PASSWORD);

            System.out.println("[SUCCESS] GOT NEW CONNECTION FROM DRIVERMANAGER");
        }catch(Exception e){
            e.printStackTrace();
            System.out.println("[FAIL] GET CONNECTION");
        }

        return newConnectionObject;
    }
}
