import Service.ImageService;
import Service.LogService;

// 자동정렬 ctrl alt shift L
class ImageSearchApp{

    public static void main(String[] args){

        new Controller(new View(), new ImageService(), new LogService());
    }
}