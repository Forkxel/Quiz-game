﻿using System.ComponentModel;

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
        label1.Location = new System.Drawing.Point(183, 71);
        label1.Name = "label1";
        label1.Size = new System.Drawing.Size(267, 39);
        label1.TabIndex = 3;
        label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label2
        // 
        label2.Location = new System.Drawing.Point(183, 110);
        label2.Name = "label2";
        label2.Size = new System.Drawing.Size(267, 39);
        label2.TabIndex = 4;
        label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label3
        // 
        label3.Location = new System.Drawing.Point(183, 149);
        label3.Name = "label3";
        label3.Size = new System.Drawing.Size(267, 39);
        label3.TabIndex = 5;
        label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label4
        // 
        label4.Location = new System.Drawing.Point(183, 188);
        label4.Name = "label4";
        label4.Size = new System.Drawing.Size(267, 39);
        label4.TabIndex = 6;
        label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label5
        // 
        label5.Location = new System.Drawing.Point(183, 227);
        label5.Name = "label5";
        label5.Size = new System.Drawing.Size(267, 39);
        label5.TabIndex = 7;
        label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // ScoreBoardForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        BackColor = System.Drawing.SystemColors.Control;
        ClientSize = new System.Drawing.Size(500, 384);
        Controls.Add(label5);
        Controls.Add(label4);
        Controls.Add(label3);
        Controls.Add(label2);
        Controls.Add(label1);
        Controls.Add(closeButton);
        Controls.Add(topPlayers);
        Location = new System.Drawing.Point(19, 19);
        Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        ResumeLayout(false);
    }

    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label5;

    private System.Windows.Forms.Button closeButton;

    private System.Windows.Forms.Label topPlayers;

    #endregion
}