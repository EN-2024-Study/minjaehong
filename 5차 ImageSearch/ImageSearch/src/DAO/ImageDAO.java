package DAO;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.util.ArrayList;

import VO.*;
import org.json.simple.JSONArray;
import org.json.simple.JSONObject;
import org.json.simple.parser.JSONParser;

// 인증방식 REST API KEY
// 다음 검색 서비스에서 질의어로 이미지를 검색합니다
// 1. REST API 키를 HEADER 에 담아 GET 으로 요청해야함
// 2. 원하는 검색어와 함께 결과 형식 파라미터를 선택적으로 추가할 수 있음
// 3. Response Body 는 meta, documents 로 구성된 "JSON 객체"임

public class ImageDAO {

    private static ImageDAO instance;

    private ImageDAO(){ }

    public static ImageDAO GetInstance(){
        if(instance==null){
            instance = new ImageDAO();
        }
        return instance;
    }

    //=========================== SINGLETON =============================//

    public ImageListVO GetImageURLs(String keyWord){

        // returning list
        ArrayList<String> imageURLList = new ArrayList<>();

        URL url = null;
        String readLine = null;
        StringBuilder buffer = null;
        BufferedReader bufferedReader = null;
        HttpURLConnection urlConnection = null;

        String RESTAPIKey = "bd02cf38bea539c77e8e9e7622711517";

        try {
            // 1. 날릴 URL 설정하기
            keyWord = URLEncoder.encode(keyWord,"UTF-8");

            // 2. URL 에 query parameters 붙여주기
            String apiUrl = "https://dapi.kakao.com/v2/search/image";
            String queryParameters = "?query="+keyWord;
            apiUrl+=queryParameters;

            // 3. REQUEST MESSAGE 객체 만들기. REQUEST MESSAGE 설정정보 구성
            // 무조건 30개 불러오기
            url = new URL(apiUrl + "&sort=recency&size=30");
            urlConnection = (HttpURLConnection) url.openConnection();

            // 4. REQUEST METHOD 설정
            urlConnection.setRequestMethod("GET");

            // REQUEST MESSAGE HEADER 에 추가하는 작업??
            // REQUEST HEADER에 "KakaoAk + 내 API 키"로 넣어줘야함
            urlConnection.setRequestProperty("Authorization", "KakaoAK " + RESTAPIKey);
            urlConnection.setRequestProperty("Accept", "application/json");

            buffer = new StringBuilder();

            bufferedReader = new BufferedReader(new InputStreamReader(urlConnection.getInputStream(), "UTF-8"));
            while ((readLine = bufferedReader.readLine()) != null) {
                buffer.append(readLine).append("\n");
            }

            String totalJSONstring = buffer.toString();

            System.out.println("TOTAL JSON TO STRING DONE");

            // JSONString 을 JSON Objects 로 파싱해줌
            JSONParser parser = new JSONParser();

            // 가장 큰 JSONObject 가져오기. Documents 랑 Meta
            // 일단 Documents / MetaData 이렇게 두 개로 나누기
            JSONObject jsonObject = (JSONObject) parser.parse(totalJSONstring);

            // Documents JSONObject들만 추출
            JSONArray documentArray = (JSONArray) jsonObject.get("documents");

            for (int i = 0; i < documentArray.size(); i++) {
                JSONObject curDocument = (JSONObject) documentArray.get(i);

                String curURL = (String) curDocument.get("image_url");
                imageURLList.add(curURL); // O(1)
            }

            System.out.println("GET ALL IMAGES BY URLS DONE");
        }catch(Exception e){
            e.printStackTrace();
        }

        return new ImageListVO(imageURLList);
    }
}
