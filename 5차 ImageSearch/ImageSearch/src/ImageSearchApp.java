import Controller.Controller;
import Service.ImageService;
import Service.LogService;
import View.MainView;

// 자동정렬 ctrl alt shift L
class ImageSearchApp{

    public static void main(String[] args){

        new Controller(new MainView(), new ImageService(), new LogService());
    }
}