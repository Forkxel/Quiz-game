namespace quiz_game;

public partial class ScoreBoardForm : Form
{
    private DatabaseServices services;
    private List<Label> scoreLabels;
    private string selectedDifficulty = null;
    private int selectedCategoryId = -1;
    private Button activeCategoryButton = null;
    private Button activeDifficultyButton = null;
    
    public ScoreBoardForm()
    {
        InitializeComponent();
        services = new DatabaseServices();
        scoreLabels = new List<Label> { label1, label2, label3, label4, label5 };
        LoadScores();
    }

    private void LoadScores()
    {
        foreach (var label in scoreLabels)
        {
            label.Text = string.Empty;
        }
        
        var scores = services.GetScoresByCategoryAndDifficulty(
            selectedCategoryId == -1 ? null : selectedCategoryId, 
            selectedDifficulty
        );
        
        int count = 1;
        foreach (var score in scores.OrderByDescending(s => s.Value).Take(5))
        {
            scoreLabels[count - 1].Text = $"{count}. {score.Key} - {score.Value}";
            count++;
        }
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
        Close();
    }

    private void geographyButton_Click(object sender, EventArgs e)
    {
        selectedCategoryId = 1;
        UpdateButtonState((Button)sender, ref activeCategoryButton);
        LoadScores();
    }

    private void historyButton_Click(object sender, EventArgs e)
    {
        selectedCategoryId = 2;
        UpdateButtonState((Button)sender, ref activeCategoryButton);
        LoadScores();
    }

    private void biologyButton_Click(object sender, EventArgs e)
    {
        selectedCategoryId = 3;
        UpdateButtonState((Button)sender, ref activeCategoryButton);
        LoadScores();
    }

    private void filmsButton_Click(object sender, EventArgs e)
    {
        selectedCategoryId = 4;
        UpdateButtonState((Button)sender, ref activeCategoryButton);
        LoadScores();
    }

    private void mixedButton_Click(object sender, EventArgs e)
    {
        selectedCategoryId = -1;
        UpdateButtonState((Button)sender, ref activeCategoryButton);
        LoadScores();
    }

    private void easyButton_Click(object sender, EventArgs e)
    {
        selectedDifficulty = "Easy";
        UpdateButtonState((Button)sender, ref activeDifficultyButton);
        LoadScores();
    }

    private void mediumButton_Click(object sender, EventArgs e)
    {
        selectedDifficulty = "Medium";
        UpdateButtonState((Button)sender, ref activeDifficultyButton);
        LoadScores();
    }

    private void hardButton_Click(object sender, EventArgs e)
    {
        selectedDifficulty = "Hard";
        UpdateButtonState((Button)sender, ref activeDifficultyButton);
        LoadScores();
    }
    
    private void UpdateButtonState(Button newActiveButton, ref Button currentActiveButton)
    {
        if (currentActiveButton != null)
        {
            currentActiveButton.BackColor = DefaultBackColor; // Reset previous button color
            currentActiveButton.ForeColor = DefaultForeColor; // Reset previous button text color
        }

        if (newActiveButton != null)
        {
            newActiveButton.BackColor = Color.LightBlue; // Active button background color
            newActiveButton.ForeColor = Color.DarkBlue;  // Active button text color
        }

        currentActiveButton = newActiveButton;
    }
}