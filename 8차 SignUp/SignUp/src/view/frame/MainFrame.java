package view.frame;

import javax.swing.*;
import java.awt.*;

public class MainFrame extends JFrame {
    public MainFrame(){
        setTitle("SignUp");
        setSize(new Dimension(800,800));
        setMinimumSize(new Dimension(1000,1000));
        this.setDefaultCloseOperation(EXIT_ON_CLOSE);
    }
}
