package DTO;

import java.util.ArrayList;

public class ImageDTO {
    private ArrayList<String> imageURLs;

    public ImageDTO(ArrayList<String> imageURLs){
        this.imageURLs = imageURLs;
    }

    public ArrayList<String> GetImageURLs(){
        return imageURLs;
    }
}
