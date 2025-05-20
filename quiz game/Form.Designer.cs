namespace quiz_game;

partial class Form
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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
        panel = new System.Windows.Forms.Panel();
        startButton = new System.Windows.Forms.Button();
        difficultyCombo = new System.Windows.Forms.ComboBox();
        categoriesCombo = new System.Windows.Forms.ComboBox();
        title = new System.Windows.Forms.Label();
        panel.SuspendLayout();
        SuspendLayout();
        // 
        // panel
        // 
        panel.Controls.Add(startButton);
        panel.Controls.Add(difficultyCombo);
        panel.Controls.Add(categoriesCombo);
        panel.Controls.Add(title);
        panel.Dock = System.Windows.Forms.DockStyle.Fill;
        panel.Location = new System.Drawing.Point(0, 0);
        panel.Name = "panel";
        panel.Size = new System.Drawing.Size(800, 450);
        panel.TabIndex = 0;
        // 
        // startButton
        // 
        startButton.Location = new System.Drawing.Point(285, 283);
        startButton.Name = "startButton";
        startButton.Size = new System.Drawing.Size(231, 56);
        startButton.TabIndex = 3;
        startButton.Text = "Start";
        startButton.UseVisualStyleBackColor = true;
        startButton.Click += startButton_Click;
        // 
        // difficultyCombo
        // 
        difficultyCombo.FormattingEnabled = true;
        difficultyCombo.Location = new System.Drawing.Point(285, 166);
        difficultyCombo.Name = "difficultyCombo";
        difficultyCombo.Size = new System.Drawing.Size(231, 23);
        difficultyCombo.TabIndex = 2;
        // 
        // categoriesCombo
        // 
        categoriesCombo.FormattingEnabled = true;
        categoriesCombo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        categoriesCombo.Location = new System.Drawing.Point(285, 84);
        categoriesCombo.Name = "categoriesCombo";
        categoriesCombo.Size = new System.Drawing.Size(231, 23);
        categoriesCombo.TabIndex = 1;
        // 
        // title
        // 
        title.Dock = System.Windows.Forms.DockStyle.Top;
        title.Location = new System.Drawing.Point(0, 0);
        title.Name = "title";
        title.Size = new System.Drawing.Size(800, 81);
        title.TabIndex = 0;
        title.Text = "Quiz game";
        title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // Form
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(panel);
        Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Load += Form_Load;
        panel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.ComboBox difficultyCombo;
    private System.Windows.Forms.Button startButton;

    private System.Windows.Forms.ComboBox categoriesCombo;

    private System.Windows.Forms.Label title;

    private System.Windows.Forms.Panel panel;

    #endregion
}