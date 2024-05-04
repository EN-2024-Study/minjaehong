package DTO;

import java.util.ArrayList;

public class ImageListDTO {
    private ArrayList<String> imageURLList;

    public ImageListDTO(ArrayList<String> imageURLs){
        this.imageURLList = imageURLs;
    }

    public ArrayList<String> GetImageURLList(){
        return imageURLList;
    }
}
