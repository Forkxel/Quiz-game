using System.ComponentModel;

namespace quiz_game.Forms;

partial class LoginForm
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
        usernameTextBox = new System.Windows.Forms.TextBox();
        passwordTextBox = new System.Windows.Forms.TextBox();
        loginButton = new System.Windows.Forms.Button();
        signUpButton = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // usernameTextBox
        // 
        usernameTextBox.Location = new System.Drawing.Point(75, 57);
        usernameTextBox.Name = "usernameTextBox";
        usernameTextBox.PlaceholderText = "Username";
        usernameTextBox.Size = new System.Drawing.Size(137, 27);
        usernameTextBox.TabIndex = 0;
        // 
        // passwordTextBox
        // 
        passwordTextBox.Location = new System.Drawing.Point(75, 116);
        passwordTextBox.Name = "passwordTextBox";
        passwordTextBox.PasswordChar = '*';
        passwordTextBox.PlaceholderText = "Password";
        passwordTextBox.Size = new System.Drawing.Size(137, 27);
        passwordTextBox.TabIndex = 1;
        // 
        // loginButton
        // 
        loginButton.Location = new System.Drawing.Point(58, 190);
        loginButton.Name = "loginButton";
        loginButton.Size = new System.Drawing.Size(81, 29);
        loginButton.TabIndex = 2;
        loginButton.Text = "login";
        loginButton.UseVisualStyleBackColor = true;
        loginButton.Click += loginButton_Click;
        // 
        // signUpButton
        // 
        signUpButton.Location = new System.Drawing.Point(145, 190);
        signUpButton.Name = "signUpButton";
        signUpButton.Size = new System.Drawing.Size(81, 29);
        signUpButton.TabIndex = 3;
        signUpButton.Text = "sign up";
        signUpButton.UseVisualStyleBackColor = true;
        signUpButton.Click += signUpButton_Click;
        // 
        // LoginForm
        // 
        ClientSize = new System.Drawing.Size(282, 253);
        Controls.Add(signUpButton);
        Controls.Add(loginButton);
        Controls.Add(passwordTextBox);
        Controls.Add(usernameTextBox);
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        ResumeLayout(false);
        PerformLayout();
    }

    private System.Windows.Forms.TextBox usernameTextBox;
    private System.Windows.Forms.TextBox passwordTextBox;
    private System.Windows.Forms.Button loginButton;
    private System.Windows.Forms.Button signUpButton;

    #endregion
}