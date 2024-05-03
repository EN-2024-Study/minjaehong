package DAO;

import java.io.BufferedReader;
import java.io.BufferedWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.MalformedURLException;
import java.net.URL;
import java.net.URLEncoder;

import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;
import org.json.simple.parser.ParseException;

// 인증방식 REST API KEY
// 다음 검색 서비스에서 질의어로 이미지를 검색합니다
// 1. REST API 키를 HEADER 에 담아 GET 으로 요청해야함
// 2. 원하는 검색어와 함께 결과 형식 파라미터를 선택적으로 추가할 수 있음
// 3. Response Body 는 meta, documents 로 구성된 "JSON 객체"임

public class KakaoAPI {

    public static void getAPITest(){
        URL url = null;
        String readLine = null;
        StringBuilder buffer = null;
        BufferedReader bufferedReader = null;
        BufferedWriter bufferedWriter = null;
        HttpURLConnection urlConnection = null;

        // 이거 왜 쓰이는거임? 단위 알아보기
        int connTimeout = 5000;
        int readTimeout = 3000;

        String x = "mbappe";

        String RESTAPIKey = "bd02cf38bea539c77e8e9e7622711517";

        try {
            // 1. 날릴 URL 설정하기
            x = URLEncoder.encode(x,"UTF-8");

            String apiUrl = "https://dapi.kakao.com/v2/search/image";
            String queryParameters = "?query="+x;
            apiUrl+=queryParameters;
            
            // 2. REQUEST MESSAGE 객체 만들기. REQUEST MESSAGE 설정정보 구성
            url = new URL(apiUrl + "&sort=accuracy&size=10");
            urlConnection = (HttpURLConnection) url.openConnection();

            // request method POST GET 중 하나 선택
            urlConnection.setRequestMethod("GET");
            urlConnection.setConnectTimeout(connTimeout);
            urlConnection.setReadTimeout(readTimeout);

            // REQUEST MESSAGE HEADER 에 추가하는 작업??
            // HEADER에 "KakaoAk + 내 API 키"로 넣어줘야함
            // QUERY PARAMETER 로 USER 가 입력한 검색어 넣어줘야함
            urlConnection.setRequestProperty("Authorization", "KakaoAK " + RESTAPIKey);
            urlConnection.setRequestProperty("Accept", "application/json");

            buffer = new StringBuilder();
            if (urlConnection.getResponseCode() == HttpURLConnection.HTTP_OK) {
                bufferedReader = new BufferedReader(new InputStreamReader(urlConnection.getInputStream(), "UTF-8"));
                while ((readLine = bufferedReader.readLine()) != null) {
                    buffer.append(readLine).append("\n");
                }
            } else {
                buffer.append("code : ");
                buffer.append(urlConnection.getResponseCode()).append("\n");
                buffer.append("message : ");
                buffer.append(urlConnection.getResponseMessage()).append("\n");
            }

            String totalJSONstring = buffer.toString();

            // JSONString 을 JSON Objects 로 파싱해줌
            JSONParser parser = new JSONParser();

            // 가장 큰 JSONObject 가져오기. Documents 랑 Meta
            // 일단 Documents / MetaData 이렇게 두 개로 나누기
            JSONObject jsonObject = (JSONObject) parser.parse(totalJSONstring);

            // Documents JSONObject들만 추출
            JSONArray documentArray = (JSONArray) jsonObject.get("documents");

            for (int i = 0; i < documentArray.size(); i++) {
                JSONObject curDocument = (JSONObject) documentArray.get(i);

                String imageUrl = (String) curDocument.get("image_url");
                System.out.println(imageUrl);
            }
        }catch(Exception e){
            e.printStackTrace();
        }
    }
}
