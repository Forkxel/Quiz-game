namespace quiz_game;

partial class Form1
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
        title1 = new System.Windows.Forms.Label();
        questionPanel = new System.Windows.Forms.Panel();
        panel.SuspendLayout();
        SuspendLayout();
        // 
        // panel
        // 
        panel.Controls.Add(startButton);
        panel.Controls.Add(difficultyCombo);
        panel.Controls.Add(categoriesCombo);
        panel.Controls.Add(title1);
        panel.Controls.Add(questionPanel);
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
        difficultyCombo.Size = new System.Drawing.Size(231, 28);
        difficultyCombo.TabIndex = 2;
        // 
        // categoriesCombo
        // 
        categoriesCombo.FormattingEnabled = true;
        categoriesCombo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        categoriesCombo.Location = new System.Drawing.Point(285, 84);
        categoriesCombo.Name = "categoriesCombo";
        categoriesCombo.Size = new System.Drawing.Size(231, 28);
        categoriesCombo.TabIndex = 1;
        categoriesCombo.SelectedIndexChanged += Categories_SelectedIndexChanged;
        // 
        // title1
        // 
        title1.Dock = System.Windows.Forms.DockStyle.Top;
        title1.Location = new System.Drawing.Point(0, 0);
        title1.Name = "title1";
        title1.Size = new System.Drawing.Size(800, 81);
        title1.TabIndex = 0;
        title1.Text = "Quiz game";
        title1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        title1.Click += label1_Click;
        // 
        // questionPanel
        // 
        questionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
        questionPanel.Location = new System.Drawing.Point(0, 0);
        questionPanel.Name = "questionPanel";
        questionPanel.Size = new System.Drawing.Size(800, 450);
        questionPanel.TabIndex = 4;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 450);
        Controls.Add(panel);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Load += Form1_Load;
        panel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Panel questionPanel;

    private System.Windows.Forms.ComboBox difficultyCombo;
    private System.Windows.Forms.Button startButton;

    private System.Windows.Forms.ComboBox categoriesCombo;

    private System.Windows.Forms.Label title1;

    private System.Windows.Forms.Panel panel;

    #endregion
}