package view.panel;

import javax.swing.*;
import java.awt.*;

public class EditAccountPanel extends CreateAccountPanel{

    private JButton editButton;
    private JButton cancelEditButton;

    public EditAccountPanel(){
        super();
        this.createComponents();
        this.initializePanel();
    }

    private void createComponents(){
        editButton = new JButton("EDIT");
        cancelEditButton = new JButton("CANCEL EDIT");
    }

    private void initializePanel() {
        idLabel.setVisible(false);
        idTextField.setVisible(false);
        idCheckButton.setVisible(false);
        idInstructionLabel.setVisible(false);

        GridBagLayout layout = (GridBagLayout) this.getLayout();
        GridBagConstraints gbc = layout.getConstraints(submitButton);

        this.remove(submitButton);
        this.add(editButton, gbc);

        gbc = layout.getConstraints(cancelButton);

        this.remove(cancelButton);
        this.add(cancelEditButton, gbc);
    }
}
