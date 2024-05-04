package VO;

import java.util.ArrayList;

public class ImageListVO {
    // O(1) O(1)
    private ArrayList<String> imageURLList;

    public ImageListVO(ArrayList<String> imageURLs){
        this.imageURLList = imageURLs;
    }

    public ArrayList<String> GetImageURLList(){
        return imageURLList;
    }
}
