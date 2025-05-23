using System.ComponentModel;

namespace quiz_game;

partial class ScoreBoardForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
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
        topPlayers = new System.Windows.Forms.Label();
        closeButton = new System.Windows.Forms.Button();
        label1 = new System.Windows.Forms.Label();
        label2 = new System.Windows.Forms.Label();
        label3 = new System.Windows.Forms.Label();
        label4 = new System.Windows.Forms.Label();
        label5 = new System.Windows.Forms.Label();
        geographyButton = new System.Windows.Forms.Button();
        historyButton = new System.Windows.Forms.Button();
        biologyButton = new System.Windows.Forms.Button();
        filmsButton = new System.Windows.Forms.Button();
        mixedButton = new System.Windows.Forms.Button();
        line = new System.Windows.Forms.Label();
        easyButton = new System.Windows.Forms.Button();
        mediumButton = new System.Windows.Forms.Button();
        hardButton = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // topPlayers
        // 
        topPlayers.Dock = System.Windows.Forms.DockStyle.Top;
        topPlayers.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
        topPlayers.Location = new System.Drawing.Point(0, 0);
        topPlayers.Name = "topPlayers";
        topPlayers.Size = new System.Drawing.Size(500, 36);
        topPlayers.TabIndex = 0;
        topPlayers.Text = "Top Players";
        topPlayers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        // 
        // closeButton
        // 
        closeButton.Location = new System.Drawing.Point(193, 306);
        closeButton.Name = "closeButton";
        closeButton.Size = new System.Drawing.Size(120, 38);
        closeButton.TabIndex = 2;
        closeButton.Text = "Close";
        closeButton.UseVisualStyleBackColor = true;
        closeButton.Click += closeButton_Click;
        // 
        // label1
        // 
        label1.Location = new System.Drawing.Point(150, 71);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(267, 39);
        label1.TabIndex = 3;
        label1.Text = "label1";
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label2
        // 
        label2.Location = new System.Drawing.Point(150, 110);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(267, 39);
        label2.TabIndex = 4;
        label2.Text = "label2";
        label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label3
        // 
        label3.Location = new System.Drawing.Point(150, 149);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(267, 39);
        label3.TabIndex = 5;
        label3.Text = "label3";
        label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label4
        // 
        label4.Location = new System.Drawing.Point(150, 188);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(267, 39);
        label4.TabIndex = 6;
        label4.Text = "label4";
        label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label5
        // 
        label5.Location = new System.Drawing.Point(150, 227);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(267, 39);
        label5.TabIndex = 7;
        label5.Text = "label5";
        label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // geographyButton
        // 
        geographyButton.Location = new System.Drawing.Point(12, 40);
        geographyButton.Name = "geographyButton";
        geographyButton.Size = new System.Drawing.Size(95, 31);
        geographyButton.TabIndex = 8;
        geographyButton.Text = "Geography";
        geographyButton.UseVisualStyleBackColor = true;
        geographyButton.Click += geographyButton_Click;
        // 
        // historyButton
        // 
        historyButton.Location = new System.Drawing.Point(12, 79);
        historyButton.Name = "historyButton";
        historyButton.Size = new System.Drawing.Size(95, 31);
        historyButton.TabIndex = 9;
        historyButton.Text = "History";
        historyButton.UseVisualStyleBackColor = true;
        historyButton.Click += historyButton_Click;
        // 
        // biologyButton
        // 
        biologyButton.Location = new System.Drawing.Point(12, 118);
        biologyButton.Name = "biologyButton";
        biologyButton.Size = new System.Drawing.Size(95, 31);
        biologyButton.TabIndex = 10;
        biologyButton.Text = "Biology";
        biologyButton.UseVisualStyleBackColor = true;
        biologyButton.Click += biologyButton_Click;
        // 
        // filmsButton
        // 
        filmsButton.Location = new System.Drawing.Point(12, 157);
        filmsButton.Name = "filmsButton";
        filmsButton.Size = new System.Drawing.Size(95, 31);
        filmsButton.TabIndex = 11;
        filmsButton.Text = "Films";
        filmsButton.UseVisualStyleBackColor = true;
        filmsButton.Click += filmsButton_Click;
        // 
        // mixedButton
        // 
        mixedButton.Location = new System.Drawing.Point(12, 194);
        mixedButton.Name = "mixedButton";
        mixedButton.Size = new System.Drawing.Size(95, 31);
        mixedButton.TabIndex = 12;
        mixedButton.Text = "Mixed";
        mixedButton.UseVisualStyleBackColor = true;
        mixedButton.Click += mixedButton_Click;
        // 
        // line
        // 
        line.Location = new System.Drawing.Point(12, 228);
        line.Name = "line";
        line.Size = new System.Drawing.Size(95, 24);
        line.TabIndex = 13;
        line.Text = "______________";
        line.TextAlign = System.Drawing.ContentAlignment.TopCenter;
        // 
        // easyButton
        // 
        easyButton.Location = new System.Drawing.Point(12, 255);
        easyButton.Name = "easyButton";
        easyButton.Size = new System.Drawing.Size(95, 31);
        easyButton.TabIndex = 14;
        easyButton.Text = "Easy";
        easyButton.UseVisualStyleBackColor = true;
        easyButton.Click += easyButton_Click;
        // 
        // mediumButton
        // 
        mediumButton.Location = new System.Drawing.Point(12, 292);
        mediumButton.Name = "mediumButton";
        mediumButton.Size = new System.Drawing.Size(95, 31);
        mediumButton.TabIndex = 15;
        mediumButton.Text = "Medium";
        mediumButton.UseVisualStyleBackColor = true;
        mediumButton.Click += mediumButton_Click;
        // 
        // hardButton
        // 
        hardButton.Location = new System.Drawing.Point(12, 329);
        hardButton.Name = "hardButton";
        hardButton.Size = new System.Drawing.Size(95, 31);
        hardButton.TabIndex = 16;
        hardButton.Text = "Hard";
        hardButton.UseVisualStyleBackColor = true;
        hardButton.Click += hardButton_Click;
        // 
        // ScoreBoardForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.SystemColors.Control;
        ClientSize = new System.Drawing.Size(500, 384);
        Controls.Add(hardButton);
        Controls.Add(mediumButton);
        Controls.Add(easyButton);
        Controls.Add(line);
        Controls.Add(mixedButton);
        Controls.Add(filmsButton);
        Controls.Add(biologyButton);
        Controls.Add(historyButton);
        Controls.Add(geographyButton);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(closeButton);
        Controls.Add(topPlayers);
        Location = new System.Drawing.Point(19, 19);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button easyButton;
    private System.Windows.Forms.Button mediumButton;
    private System.Windows.Forms.Button hardButton;

    private System.Windows.Forms.Button geographyButton;
    private System.Windows.Forms.Button historyButton;
    private System.Windows.Forms.Button biologyButton;
    private System.Windows.Forms.Button filmsButton;
    private System.Windows.Forms.Button mixedButton;
    private System.Windows.Forms.Label line;

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;

    private System.Windows.Forms.Button closeButton;

    private System.Windows.Forms.Label topPlayers;

    #endregion
}