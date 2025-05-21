using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace quiz_game;

partial class MyForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        panel = new Panel();
        startButton = new Button();
        difficultyCombo = new ComboBox();
        categoriesCombo = new ComboBox();
        title = new Label();
        panel.SuspendLayout();
        SuspendLayout();
        // 
        // panel
        // 
        panel.Controls.Add(startButton);
        panel.Controls.Add(difficultyCombo);
        panel.Controls.Add(categoriesCombo);
        panel.Controls.Add(title);
        panel.Dock = DockStyle.Fill;
        panel.Location = new Point(0, 0);
        panel.Name = "panel";
        panel.Size = new Size(800, 450);
        panel.TabIndex = 0;
        // 
        // startButton
        // 
        startButton.Location = new Point(285, 283);
        startButton.Name = "startButton";
        startButton.Size = new Size(231, 56);
        startButton.TabIndex = 3;
        startButton.Text = "Start";
        startButton.UseVisualStyleBackColor = true;
        startButton.Click += startButton_Click;
        // 
        // difficultyCombo
        // 
        difficultyCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        difficultyCombo.FormattingEnabled = true;
        difficultyCombo.Location = new Point(285, 166);
        difficultyCombo.Name = "difficultyCombo";
        difficultyCombo.Size = new Size(231, 23);
        difficultyCombo.TabIndex = 2;
        // 
        // categoriesCombo
        // 
        categoriesCombo.DropDownStyle = ComboBoxStyle.DropDownList;
        categoriesCombo.FormattingEnabled = true;
        categoriesCombo.ImeMode = ImeMode.NoControl;
        categoriesCombo.Location = new Point(285, 84);
        categoriesCombo.Name = "categoriesCombo";
        categoriesCombo.Size = new Size(231, 23);
        categoriesCombo.TabIndex = 1;
        // 
        // title
        // 
        title.Dock = DockStyle.Top;
        title.Location = new Point(0, 0);
        title.Name = "title";
        title.Size = new Size(800, 81);
        title.TabIndex = 0;
        title.Text = "Quiz game";
        title.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // Form
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(panel);
        Margin = new Padding(3, 2, 3, 2);
        StartPosition = FormStartPosition.CenterScreen;
        Load += Form_Load;
        panel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private ComboBox difficultyCombo;
    private Button startButton;

    private ComboBox categoriesCombo;

    private Label title;

    private Panel panel;

    #endregion
}