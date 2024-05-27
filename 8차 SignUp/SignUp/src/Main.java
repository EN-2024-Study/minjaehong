import view.frame.MainFrame;
import view.panel.EditAccountPanel;

import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class Main {
    public static void main(String[] args) {
        JFrame frame = new JFrame("Input JOptionPane Example");

        EditAccountPanel panel = new EditAccountPanel();
        frame.add(panel);
        frame.setVisible(true);

        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(800, 800);

        //SignUpInitializer initializer = new SignUpInitializer();
        //initializer.run();
    }
}
        /*
    // Create a JFrame
    JFrame frame = new JFrame("Input JOptionPane Example");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(300, 200);
        frame.setLayout(null);

    // Create a JButton
    JButton button = new JButton("Show Dialogs");
        button.setBounds(50, 50, 200, 30);
        frame.add(button);

    // Add ActionListener to the button
        button.addActionListener(new ActionListener() {
        @Override
        public void actionPerformed(ActionEvent e) {
            // Show the first JOptionPane input dialog
            String userInput = JOptionPane.showInputDialog(frame, "Enter some input:");

            // If the user provided input (i.e., didn't cancel the dialog)
            if (userInput != null) {
                // Show the second JOptionPane dialog with the user input
                JOptionPane.showMessageDialog(frame, "You entered: " + userInput);
            }
        }
    });

    // Set the frame visibility
        frame.setVisible(true);
        */
