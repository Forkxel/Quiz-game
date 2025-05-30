using System.ComponentModel;

namespace quiz_game.Forms;

partial class MainForm
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
        panel = new System.Windows.Forms.Panel();
        categoryTitle = new System.Windows.Forms.Label();
        difficultyTitle = new System.Windows.Forms.Label();
        scoreBoardButton = new System.Windows.Forms.Button();
        loginButton = new System.Windows.Forms.Button();
        startButton = new System.Windows.Forms.Button();
        difficultyCombo = new System.Windows.Forms.ComboBox();
        categoriesCombo = new System.Windows.Forms.ComboBox();
        title = new System.Windows.Forms.Label();
        panel.SuspendLayout();
        SuspendLayout();
        // 
        // panel
        // 
        panel.Controls.Add(categoryTitle);
        panel.Controls.Add(difficultyTitle);
        panel.Controls.Add(scoreBoardButton);
        panel.Controls.Add(loginButton);
        panel.Controls.Add(startButton);
        panel.Controls.Add(difficultyCombo);
        panel.Controls.Add(categoriesCombo);
        panel.Controls.Add(title);
        panel.Dock = System.Windows.Forms.DockStyle.Fill;
        panel.Location = new System.Drawing.Point(0, 0);
        panel.Name = "panel";
        panel.Size = new System.Drawing.Size(1000, 650);
        panel.TabIndex = 0;
        // 
        // categoryTitle
        // 
        categoryTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        categoryTitle.Location = new System.Drawing.Point(383, 214);
        categoryTitle.Name = "categoryTitle";
        categoryTitle.Size = new System.Drawing.Size(231, 32);
        categoryTitle.TabIndex = 8;
        categoryTitle.Text = "Category";
        categoryTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // difficultyTitle
        // 
        difficultyTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        difficultyTitle.Location = new System.Drawing.Point(383, 357);
        difficultyTitle.Name = "difficultyTitle";
        difficultyTitle.Size = new System.Drawing.Size(231, 32);
        difficultyTitle.TabIndex = 7;
        difficultyTitle.Text = "Difficulty";
        difficultyTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // scoreBoardButton
        // 
        scoreBoardButton.BackColor = System.Drawing.Color.Transparent;
        scoreBoardButton.Cursor = System.Windows.Forms.Cursors.Hand;
        scoreBoardButton.Location = new System.Drawing.Point(862, 93);
        scoreBoardButton.Name = "scoreBoardButton";
        scoreBoardButton.Size = new System.Drawing.Size(112, 36);
        scoreBoardButton.TabIndex = 6;
        scoreBoardButton.Text = "Score board";
        scoreBoardButton.UseVisualStyleBackColor = false;
        scoreBoardButton.Click += scoreBoardButton_Click;
        // 
        // loginButton
        // 
        loginButton.Cursor = System.Windows.Forms.Cursors.Hand;
        loginButton.Location = new System.Drawing.Point(862, 51);
        loginButton.Name = "loginButton";
        loginButton.Size = new System.Drawing.Size(112, 36);
        loginButton.TabIndex = 4;
        loginButton.Text = "Profile";
        loginButton.UseVisualStyleBackColor = true;
        loginButton.Click += loginButton_Click;
        // 
        // startButton
        // 
        startButton.Cursor = System.Windows.Forms.Cursors.Hand;
        startButton.Location = new System.Drawing.Point(383, 517);
        startButton.Name = "startButton";
        startButton.Size = new System.Drawing.Size(231, 56);
        startButton.TabIndex = 3;
        startButton.Text = "Start";
        startButton.UseVisualStyleBackColor = true;
        startButton.Click += startButton_Click;
        // 
        // difficultyCombo
        // 
        difficultyCombo.Cursor = System.Windows.Forms.Cursors.Hand;
        difficultyCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        difficultyCombo.FormattingEnabled = true;
        difficultyCombo.Location = new System.Drawing.Point(383, 392);
        difficultyCombo.Name = "difficultyCombo";
        difficultyCombo.Size = new System.Drawing.Size(231, 28);
        difficultyCombo.TabIndex = 2;
        // 
        // categoriesCombo
        // 
        categoriesCombo.Cursor = System.Windows.Forms.Cursors.Hand;
        categoriesCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        categoriesCombo.FormattingEnabled = true;
        categoriesCombo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
        categoriesCombo.Location = new System.Drawing.Point(383, 249);
        categoriesCombo.Name = "categoriesCombo";
        categoriesCombo.Size = new System.Drawing.Size(231, 28);
        categoriesCombo.TabIndex = 1;
        // 
        // title
        // 
        title.BackColor = System.Drawing.Color.LightGray;
        title.Dock = System.Windows.Forms.DockStyle.Top;
        title.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
        title.Location = new System.Drawing.Point(0, 0);
        title.Name = "title";
        title.Size = new System.Drawing.Size(1000, 146);
        title.TabIndex = 0;
        title.Text = "Quiz game";
        title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(1000, 650);
        Controls.Add(panel);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
        Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Quiz Game";
        Load += Form_Load;
        panel.ResumeLayout(false);
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label categoryTitle;

    private System.Windows.Forms.Label difficultyTitle;

    private System.Windows.Forms.Button scoreBoardButton;

    private System.Windows.Forms.Button loginButton;

    private System.Windows.Forms.ComboBox difficultyCombo;
    private System.Windows.Forms.Button startButton;

    private System.Windows.Forms.ComboBox categoriesCombo;

    private System.Windows.Forms.Label title;

    private System.Windows.Forms.Panel panel;

    #endregion
}