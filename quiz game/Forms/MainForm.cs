using quiz_game.Database;
using quiz_game.Tables;
using Timer = System.Windows.Forms.Timer;

namespace quiz_game.Forms;

/// <summary>
/// Class for Main form of the application
/// </summary>
public partial class MainForm : Form
{
    private Panel quizPanel;
    private List<Question> currentQuestions = new();
    public static int CurrentQuestionIndex { get; set; }
    private const int MaxQuestions = 6;
    private int score;
    private Label scoreLabel;
    private Label timerLabel;
    private RadioButton selectedOption = null;
    public Timer QuestionTimer { get; set; }
    private int timeLeft;
    private Panel infoPanel;
    private DatabaseServices services = new();
    private int currentTime;
    private bool mixedCategorySelected;
    
    public MainForm()
    {
        InitializeComponent();
        InitializeLayout();
        MaximizeBox = false;
    }
    
    /// <summary>
    /// Method to prepare layout for when the question is shown
    /// </summary>
    private void InitializeLayout()
    {
        infoPanel = new Panel
        {
            Dock = DockStyle.Top,
            Height = 50,
            Padding = new Padding(10),
            BackColor = Color.LightGray,
            Visible = false
        };
        
        quizPanel = new Panel
        {
            Visible = false,
            Dock = DockStyle.Fill
        };
        
        scoreLabel = new Label
        {
            Text = "Score: 0",
            Font = new Font("Arial", 12, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft,
            Dock = DockStyle.Left,
            Width = 150,
            Padding = new Padding(5),
            Visible = false
        };
        infoPanel.Controls.Add(scoreLabel);
        
        timerLabel = new Label
        {
            Text = "Time left: 30",
            Font = new Font("Arial", 12, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleRight,
            Dock = DockStyle.Right,
            Width = 150,
            Padding = new Padding(5),
            Visible = false
        };
        infoPanel.Controls.Add(timerLabel);

        Controls.Add(quizPanel);
        
        Controls.Add(infoPanel);    
    }

    /// <summary>
    /// Method where random questions are loaded when player click start button
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void startButton_Click(object sender, EventArgs e)
    {
        var selectedCategory = categoriesCombo.SelectedItem as Category;
        var selectedDifficulty = difficultyCombo.SelectedItem as Difficulty;
        
        if (selectedCategory == null && !IsMixedCategorySelected())
        {
            MessageBox.Show("Please select a valid category.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        if (selectedDifficulty == null)
        {
            MessageBox.Show("Please select a difficulty level.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return;
        }
        
        currentQuestions.Clear();
        CurrentQuestionIndex = 0;
        
        var singleQuestions = services.GetSingleQuestions(selectedCategory.Id, selectedDifficulty.Id.ToString());
        var writtenAnswer = services.GetWrittenQuestions(selectedCategory.Id, selectedDifficulty.Id.ToString());
        var multipleQuestions = services.GetMultipleQuestions(selectedCategory.Id, selectedDifficulty.Id.ToString());
        var trueFalseQuestions = services.GetTrueFalseQuestions(selectedCategory.Id, selectedDifficulty.Id.ToString());
        
        List<Question> questions = singleQuestions
            .Concat(writtenAnswer)
            .Concat(multipleQuestions)
            .Concat(trueFalseQuestions)
            .ToList();
        
        var random = new Random();
        var firstQuestion = singleQuestions.OrderBy(q => random.Next()).FirstOrDefault();
        questions.Remove(firstQuestion);
        
        var remainingQuestions = questions.OrderBy(q => random.Next()).Take(MaxQuestions - 1).ToList();
        
        currentQuestions = new List<Question> { firstQuestion }.Concat(remainingQuestions).ToList();
        
        DisplayNextQuestion();

        panel.Visible = false;
        scoreLabel.Visible = true;
        timerLabel.Visible = true;
        infoPanel.Visible = true;
    }

    /// <summary>
    /// Method that displays new question when player click next button
    /// </summary>
    private void DisplayNextQuestion()
    {
        if (CurrentQuestionIndex < currentQuestions.Count)
        {
            timeLeft = 30;
            timerLabel.Text = $"Time: {timeLeft}";
            QuestionTimer.Start();
            
            var currentQuestion = currentQuestions[CurrentQuestionIndex];
            currentQuestion.Display(quizPanel, isCorrect =>
            {
                if (isCorrect)
                {   
                    score += (currentTime / 2) + 1;
                    scoreLabel.Text = $"Score: {score}";
                }
                CurrentQuestionIndex++;
                DisplayNextQuestion();
            });
        }
        else
        {
            QuestionTimer.Stop();
            DisplayFinalScorePanel();
            quizPanel.Visible = false;
        }
    }
    
    /// <summary>
    /// Method that displays panel with final score and play again button
    /// </summary>
    private void DisplayFinalScorePanel()
    {
        Controls.Clear();
        
        Panel finalScorePanel = new Panel
        {
            Dock = DockStyle.Fill
        };

        Label finalScoreLabel = new Label
        {
            Text = $"Your final score: {score}",
            Font = new Font("Arial", 16, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Fill
        };

        finalScorePanel.Controls.Add(finalScoreLabel);
        
        Label highScoreLabel = new Label
        {
            Text = string.Empty,
            Font = new Font("Arial", 14, FontStyle.Italic),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 50
        };
        finalScorePanel.Controls.Add(highScoreLabel);

        if (!string.IsNullOrEmpty(LoginForm.LoggedInUser))
        {
            bool isNewScore = services.UpdateScore(LoginForm.LoggedInUser, score);
            if (isNewScore)
            {
                highScoreLabel.Text = "Congratulations! You've set a new high score!";
            }
        }
        else
        {
            highScoreLabel.Text = "Log in to save your high score.";
            highScoreLabel.ForeColor = Color.Blue;
        }
        
        Button playAgainButton = new Button
        {
            Text = "Play Again",
            Font = new Font("Arial", 12, FontStyle.Bold),
            Dock = DockStyle.Bottom,
            Height = 50,
            Cursor = Cursors.Hand
        };
        playAgainButton.Click += PlayAgainButton_Click;
        finalScorePanel.Controls.Add(playAgainButton);
        
        Controls.Clear();
        Controls.Add(finalScorePanel); 
    }

    /// <summary>
    /// Method that resets everything to start state when player click play again button
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void PlayAgainButton_Click(object? sender, EventArgs e)
    {
        score = 0;
        CurrentQuestionIndex = 0;

        Controls.Clear();
        InitializeComponent();
        InitializeLayout();
        LoadComboBoxes();

        panel.Visible = true;
        quizPanel.Visible = false;
        infoPanel.Visible = false;
        timerLabel.Visible = false;
        scoreLabel.Visible = false;
    }

    /// <summary>
    /// Method for timer to run
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void QuestionTimer_Tick(object sender, EventArgs e)
    {
        timeLeft--;
        timerLabel.Text = $"Time: {timeLeft}";
        currentTime = timeLeft;

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            timerLabel.Text = "Time: 0";
            QuestionTimer.Stop();

            if (CurrentQuestionIndex < currentQuestions.Count)
            {
                var question = currentQuestions[CurrentQuestionIndex];
                question.TimeOut(DisplayNextQuestion);
            }
        }
    }

    /// <summary>
    ///  Method where timer is started and combo boxes are filled
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void Form_Load(object sender, EventArgs e)
    {
        QuestionTimer = new Timer
        {
            Interval = 1000
        };
        QuestionTimer.Tick += QuestionTimer_Tick;
        
        LoadComboBoxes();
    }

    /// <summary>
    /// Method to switch to login form when login button is clicked
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void loginButton_Click(object sender, EventArgs e)
    {
        var loginForm = new LoginForm();
        loginForm.ShowDialog();
    }
    
    /// <summary>
    /// Method to fill combo boxes
    /// </summary>
    private void LoadComboBoxes()
    {
        var categories = services.GetCategories();
        var difficulties = services.GetDifficulties();

        categoriesCombo.Items.Clear();
        difficultyCombo.Items.Clear();

        categoriesCombo.Items.AddRange(categories.ToArray());
        difficultyCombo.Items.AddRange(difficulties.ToArray());

        categoriesCombo.DisplayMember = "Name";
        difficultyCombo.DisplayMember = "Name";

        categoriesCombo.Items.Add(new Category{ Id = null, Name = "Mixed" });
    }
    
    /// <summary>
    /// Method to find if category user selected is Mixed
    /// </summary>
    /// <returns>true if category is Mixed</returns>
    private bool IsMixedCategorySelected()
    {
        if (categoriesCombo.SelectedItem is Category selectedCategory && selectedCategory.Name == "Mixed")
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Method to show score board form when score board button is clicked
    /// </summary>
    /// <param name="sender">The source of the event</param>
    /// <param name="e">The event arguments</param>
    private void scoreBoardButton_Click(object sender, EventArgs e)
    {
        var scoreBoardForm = new ScoreBoardForm();
        scoreBoardForm.ShowDialog();
    }
}   