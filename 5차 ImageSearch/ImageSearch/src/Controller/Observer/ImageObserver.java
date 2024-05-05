package Controller.Observer;

import javax.swing.*;
import java.awt.*;
import java.awt.event.MouseAdapter;
import java.awt.event.MouseEvent;

// 얘는 그냥 CONTROLLER 에서 생성해서 IMAGE JLABEL 들한테 주입만 해주면 됨
// VIEW 와 CONTROLLER 에 대한 참조변수가 필요가 없다

// 얘는 ImageIcon 이 붙어있는 JLabel 들한테만 달리기 때문에
// mouseClicked 안에서 이게 ImageIcon 붙어있는 JLabel 인지 확인할 필요가 없음
// 그냥 ImageIcon 이라고 확신하고 들어가는거임 ImageIcon 이라는 것은 보장이 돼있음
public class ImageObserver extends MouseAdapter {

    public ImageObserver(){

    }

    // 기존 ImageIcon 을 받아서 더 4배의 ImageIcon 으로 만들어서 return 해줌
    private ImageIcon changeToBiggerImageIcon(ImageIcon originalIcon, int width, int height){
        Image originalImage = originalIcon.getImage();
        Image scaledImage = originalImage.getScaledInstance(width * 4, height * 4, Image.SCALE_SMOOTH);
        return new ImageIcon(scaledImage);
    }

    @Override
    public void mouseClicked(MouseEvent e) {
        if (e.getClickCount() == 1) {
            JLabel curLabel = (JLabel)e.getSource();
            ImageIcon curImageIcon = (ImageIcon)curLabel.getIcon();

            ImageIcon scaledImageIcon = changeToBiggerImageIcon(curImageIcon, curLabel.getWidth(), curLabel.getHeight());

            // view 를 기준으로 생성해서 보여주기
            JOptionPane.showMessageDialog(null, scaledImageIcon, "ENLARGED IMAGE", JOptionPane.PLAIN_MESSAGE);
        }
    }
}
