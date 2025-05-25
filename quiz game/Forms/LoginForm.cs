using quiz_game.Database;

namespace quiz_game.Forms;

public partial class LoginForm : Form
{
    private DatabaseServices services;
    public static string LoggedInUser { get; set; }
    
    public LoginForm()
    {
        InitializeComponent();
        services = new DatabaseServices();
    }

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
            return;
        }
        
        if (password != PasswordEncryption.Decrypt(storedPassword))
        {
            MessageBox.Show("Incorrect password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        
        LoggedInUser = username;
        MessageBox.Show("Login successful!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        DialogResult = DialogResult.OK;
        Close();
    }

    private void signUpButton_Click(object sender, EventArgs e)
    {
        string username = usernameTextBox.Text.Trim();
        string password = passwordTextBox.Text.Trim();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            MessageBox.Show("Please enter both username and password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (services.UserExists(username))
        {
            MessageBox.Show("Username already exists. Please choose another.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }

        string encryptedPassword = PasswordEncryption.Encrypt(password);
        if (services.AddUser(username, encryptedPassword))
        {
            LoggedInUser = username;
            MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
        else
        {
            MessageBox.Show("Failed to register user. Try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}