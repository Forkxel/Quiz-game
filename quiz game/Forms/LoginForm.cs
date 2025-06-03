using quiz_game.Database;
using System.Drawing;
using System.Text.RegularExpressions;

namespace quiz_game.Forms;

/// <summary>
/// Class for login form 
/// </summary>
public partial class LoginForm : Form
{
    private DatabaseServices services;
    public static string LoggedInUser { get; set; }
    private Label userLabel;
    private Button signOutButton;
    private Button changePasswordButton;
    private TextBox currentPasswordTextBox;
    private TextBox newPasswordTextBox;
    private Button confirmChangePasswordButton;
    private Button closeChangePasswordButton;
    private const int MaxLength = 50;
    
    public LoginForm()
    {
        InitializeComponent();
        services = new DatabaseServices();
        MaximizeBox = false;
        
        userLabel = new Label
        {
            Location = new Point(75, 20),
            Name = "userLabel",
            Size = new Size(200, 27),
            Text = "",
            Visible = false
        };

        signOutButton = new Button
        {
            Location = new Point(75, 60),
            Name = "signOutButton",
            Size = new Size(137, 27),
            Text = "Sign Out",
            Visible = false,
            Cursor = Cursors.Hand
        };

        signOutButton.Click += SignOutButton_Click;

        changePasswordButton = new Button
        {
            Location = new Point(75, 100),
            Name = "changePasswordButton",
            Size = new Size(137, 27),
            Text = "Change Password",
            Visible = false,
            Cursor = Cursors.Hand
        };
        changePasswordButton.Click += ChangePasswordButton_Click;

        currentPasswordTextBox = new TextBox
        {
            Location = new Point(45, 60),
            Name = "currentPasswordTextBox",
            Size = new Size(200, 27),
            PasswordChar = '*',
            PlaceholderText = "Current Password",
            Visible = false
        };

        newPasswordTextBox = new TextBox
        {
            Location = new Point(45, 100),
            Name = "newPasswordTextBox",
            Size = new Size(200, 27),
            PasswordChar = '*',
            PlaceholderText = "New Password",
            Visible = false
        };

        confirmChangePasswordButton = new Button
        {
            Location = new Point(40, 140),
            Name = "confirmChangePasswordButton",
            Size = new Size(90, 27),
            Text = "Confirm",
            Visible = false,
            Cursor = Cursors.Hand
        };
        confirmChangePasswordButton.Click += ConfirmChangePasswordButton_Click;

        closeChangePasswordButton = new Button
        {
            Location = new Point(160, 140),
            Name = "closeChangePasswordButton",
            Size = new Size(90, 27),
            Text = "Close",
            Visible = false,
            Cursor = Cursors.Hand
        };
        closeChangePasswordButton.Click += CloseChangePasswordButton_Click;

        Controls.Add(userLabel);
        Controls.Add(signOutButton);
        Controls.Add(changePasswordButton);
        Controls.Add(currentPasswordTextBox);
        Controls.Add(newPasswordTextBox);
        Controls.Add(confirmChangePasswordButton);
        Controls.Add(closeChangePasswordButton);
        
        if (!string.IsNullOrEmpty(LoggedInUser))
        {
            ShowUserControls();
        }
        else
        {
            ShowLoginControls();
        }
    }

    /// <summary>
    /// Method to validate if user exist based on password and username
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void loginButton_Click(object sender, EventArgs e)
    {
        string username = usernameTextBox.Text.Trim();
        string password = passwordTextBox.Text.Trim();
        
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter both username and password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        
        string storedPassword = services.GetPasswordForUser(username);
        
        if (string.IsNullOrEmpty(storedPassword))
        {
            MessageBox.Show("User does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            passwordTextBox.Text = string.Empty;
            return;
        }
        
        if (password != PasswordEncryption.Decrypt(storedPassword))
        {
            MessageBox.Show("Incorrect password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            passwordTextBox.Text = string.Empty;
            return;
        }
        
        LoggedInUser = username;
        MessageBox.Show("Login successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        ShowUserControls();
    }
    
    /// <summary>
    /// Method to add new user 
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void signUpButton_Click(object sender, EventArgs e)
    {
        string username = usernameTextBox.Text.Trim();
        string password = passwordTextBox.Text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter both username and password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (username.Contains(" "))
        {
            MessageBox.Show("Username must not contain spaces.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        
        if (!Regex.IsMatch(username, @"^[\p{L}\p{N}\p{P}\p{S}]*$"))
        {
            MessageBox.Show("Username contains invalid characters. Emoji and other special symbols are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            passwordTextBox.Text = string.Empty;
            return;
        }

        if (password.Contains(" "))
        {
            MessageBox.Show("Password must not contain spaces.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            passwordTextBox.Text = string.Empty;
            return;
        }

        if (password.Length < 5)
        {
            MessageBox.Show("Password must be at least 5 characters long.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            passwordTextBox.Text = string.Empty;    
            return;
        }
        
        if (!Regex.IsMatch(password, @"^[\p{L}\p{N}\p{P}\p{S}]*$"))
        {
            MessageBox.Show("Password contains invalid characters. Emoji and other special symbols are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            passwordTextBox.Text = string.Empty;
            return;
        }

        if (services.UserExists(username))
        {
            MessageBox.Show("Username already exists. Please choose another.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        
        if (username.Length > MaxLength)
        {
            MessageBox.Show($"Username must not exceed {MaxLength} characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        if (password.Length > MaxLength)
        {
            MessageBox.Show($"Password must not exceed {MaxLength} characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            passwordTextBox.Text = string.Empty;
            return;
        }

        string encryptedPassword = PasswordEncryption.Encrypt(password);
        if (services.AddUser(username, encryptedPassword))
        {
            LoggedInUser = username;
            MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ShowUserControls();
        }
        else
        {
            MessageBox.Show("Failed to register user. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    /// <summary>
    /// Method to sign out
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void SignOutButton_Click(object sender, EventArgs e)
    {
        LoggedInUser = null;
        
        usernameTextBox.Text = string.Empty;
        passwordTextBox.Text = string.Empty;
        
        ShowLoginControls();
    }
    
    /// <summary>
    /// Method to change form after user is logged in
    /// </summary>
    private void ShowUserControls()
    {
        userLabel.Text = $"Logged in as: {LoggedInUser}";
        userLabel.Visible = true;
        signOutButton.Visible = true;
        changePasswordButton.Visible = true;

        usernameTextBox.Visible = false;
        passwordTextBox.Visible = false; 
        loginButton.Visible = false; 
        signUpButton.Visible = false; 
    }
    
    /// <summary>
    /// Method to change form when user is not logged in
    /// </summary>
    private void ShowLoginControls()
    {
        usernameTextBox.Visible = true;
        passwordTextBox.Visible = true;
        loginButton.Visible = true;
        signUpButton.Visible = true;

        userLabel.Visible = false;
        signOutButton.Visible = false;
        changePasswordButton.Visible = false;
    }

    /// <summary>
    /// Method to change form to display textboxes and buttons to change password
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void ChangePasswordButton_Click(object sender, EventArgs e)
    {
        userLabel.Visible = false;
        signOutButton.Visible = false;
        changePasswordButton.Visible = false;

        currentPasswordTextBox.Visible = true;
        newPasswordTextBox.Visible = true;
        confirmChangePasswordButton.Visible = true;
        closeChangePasswordButton.Visible = true;
    }

    /// <summary>
    /// Method for confirm button to update password
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void ConfirmChangePasswordButton_Click(object sender, EventArgs e)
    {
        string currentPassword = currentPasswordTextBox.Text.Trim();
        string newPassword = newPasswordTextBox.Text.Trim();

        if (string.IsNullOrEmpty(currentPassword) || string.IsNullOrEmpty(newPassword))
        {
            MessageBox.Show("Both fields are required.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        string storedPassword = services.GetPasswordForUser(LoggedInUser);
        string decryptedStoredPassword = PasswordEncryption.Decrypt(storedPassword);

        if (decryptedStoredPassword != currentPassword)
        {
            MessageBox.Show("Current password is incorrect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            currentPasswordTextBox.Text = string.Empty;
            newPasswordTextBox.Text = string.Empty;
            return;
        }

        if (newPassword == currentPassword)
        {
            MessageBox.Show("New password must be different from the current password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            currentPasswordTextBox.Text = string.Empty;
            newPasswordTextBox.Text = string.Empty;
            return;
        }
        
        if (newPassword.Contains(" "))
        {
            MessageBox.Show("Password must not contain spaces.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            currentPasswordTextBox.Text = string.Empty;
            newPasswordTextBox.Text = string.Empty;
            return;
        }

        if (newPassword.Length < MaxLength)
        {
            MessageBox.Show($"Password must not exceed {MaxLength} characters.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            newPasswordTextBox.Text = string.Empty;
            return;
        }
        
        if (!Regex.IsMatch(newPassword, @"^[\p{L}\p{N}\p{P}\p{S}]*$"))
        {
            MessageBox.Show("Password contains invalid characters. Emoji and other special symbols are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            newPasswordTextBox.Text = string.Empty;
            currentPasswordTextBox.Text = string.Empty;
            return;
        }
        
        if (!Regex.IsMatch(currentPassword, @"^[\p{L}\p{N}\p{P}\p{S}]*$"))
        {
            MessageBox.Show("Password contains invalid characters. Emoji and other special symbols are not allowed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            newPasswordTextBox.Text = string.Empty;
            currentPasswordTextBox.Text = string.Empty;
            return;
        }


        string encryptedNewPassword = PasswordEncryption.Encrypt(newPassword);
        if (services.UpdatePassword(LoggedInUser, encryptedNewPassword))
        {
            MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            HidePasswordChangeControls();
        }
        else
        {
            MessageBox.Show("Failed to change password. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>
    /// Method to Close change password
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void CloseChangePasswordButton_Click(object sender, EventArgs e)
    {
        HidePasswordChangeControls();
    }

    /// <summary>
    /// Method to hide change password state
    /// </summary>
    private void HidePasswordChangeControls()
    {
        currentPasswordTextBox.Visible = false;
        newPasswordTextBox.Visible = false;
        confirmChangePasswordButton.Visible = false;
        closeChangePasswordButton.Visible = false;

        ShowUserControls();

        currentPasswordTextBox.Text = string.Empty;
        newPasswordTextBox.Text = string.Empty;
    }
}