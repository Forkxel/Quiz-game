using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using quiz_game.Tables;
using Timer = System.Windows.Forms.Timer;

namespace quiz_game;

public partial class Form : System.Windows.Forms.Form
{
    private SqlConnection connection = DatabaseConnection.GetInstance();
    private Panel quizPanel;
    private List<Question> currentQuestions = new();
    public static int CurrentQuestionIndex { get; set; }
    private const int MaxQuestions = 8;
    private int score = 0;
    private Label scoreLabel;
    private Label timerLabel;
    private RadioButton selectedOption = null;
    private Timer questionTimer;
    private int timeLeft;
    private Panel infoPanel;
    
    public Form()
    {
        InitializeComponent();
        InitializeLayout();
    }
    
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

    private void startButton_Click(object sender, EventArgs e)
    {
        object selectedCategory = ((dynamic)categoriesCombo.SelectedItem)?.Id;
        string selectedDifficulty = ((dynamic)difficultyCombo.SelectedItem)?.Id?.ToString();
        List<Question> questions = new List<Question>();
        currentQuestions.Clear();
        CurrentQuestionIndex = 0;

        string query = "SELECT questionText, correctAnswer, option1, option2, option3, cat_id, diff_id," +
                       " c.nameCategory as category, d.nameDifficulty as difficulty" +
                       " FROM Questions " +
                       "INNER JOIN Category c ON cat_id = c.id " +
                       "INNER JOIN Difficulty d ON diff_id = d.id " +
                       "WHERE (@CategoryId IS NULL OR cat_id = @CategoryId) " +
                       "AND diff_id = @DifficultyId";

        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@CategoryId", selectedCategory ?? DBNull.Value);
        command.Parameters.AddWithValue("@DifficultyId", selectedDifficulty);

        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            questions.Add(new SingleChoiceQuestion
            {
                QuestionText = reader["questionText"].ToString(),
                CorrectAnswer = reader["correctAnswer"].ToString(),
                Option1 = reader["option1"].ToString(),
                Option2 = reader["option2"].ToString(),
                Option3 = reader["option3"].ToString(),
                Category = reader["category"].ToString(),
                Difficulty = reader["difficulty"].ToString()
            });
        }
        reader.Close();
        
        var random = new Random();
        currentQuestions = questions
            .DistinctBy(q => q.QuestionText) 
            .OrderBy(q => random.Next()) 
            .Take(MaxQuestions)        
            .ToList();
        DisplayNextQuestion();

        panel.Visible = false;
        scoreLabel.Visible = true;
        timerLabel.Visible = true;
        infoPanel.Visible = true;
    }

    private void DisplayNextQuestion()
    {
        if (CurrentQuestionIndex < currentQuestions.Count)
        {
            timeLeft = 30;
            timerLabel.Text = $"Time: {timeLeft}";
            questionTimer.Start();
            
            var currentQuestion = currentQuestions[CurrentQuestionIndex];
            currentQuestion.Display(quizPanel, isCorrect =>
            {
                questionTimer.Stop();
                if (isCorrect)
                {
                    score++;
                    scoreLabel.Text = $"Score: {score}";
                }
                CurrentQuestionIndex++;
                DisplayNextQuestion();
            });
        }
        else
        {
            questionTimer.Stop();
            DisplayFinalScorePanel();
            quizPanel.Visible = false;
        }
    }
    
    private void DisplayFinalScorePanel()
    {
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
        Controls.Clear();
        Controls.Add(finalScorePanel); 
    }
    
    private void QuestionTimer_Tick(object sender, EventArgs e)
    {
        timeLeft--;
        timerLabel.Text = $"Time: {timeLeft}";

        if (timeLeft <= 0)
        {
            timeLeft = 0;
            timerLabel.Text = "Time: 0";
            questionTimer.Stop();
            foreach (var control in quizPanel.Controls)
            {
                if (control is RadioButton rb)
                {
                    rb.ForeColor = Color.Red;
                }
            }
            CurrentQuestionIndex++;
            DisplayNextQuestion();
        }
    }

    private void Form_Load(object sender, EventArgs e)
    {
        questionTimer = new Timer
        {
            Interval = 1000
        };
        questionTimer.Tick += QuestionTimer_Tick;
        
        var queryCat = "select id, nameCategory from category";
        using (SqlCommand command = new SqlCommand(queryCat, connection))
        {
            SqlDataReader readerCat = command.ExecuteReader();

            while (readerCat.Read())
            {
                categoriesCombo.Items.Add(new { Id = readerCat["id"], Name = readerCat["nameCategory"] });
            }
            readerCat.Close();
        }
            
        var queryDiff = "select id, nameDifficulty from difficulty";
        using (SqlCommand command = new SqlCommand(queryDiff, connection))
        {
            SqlDataReader readerDif = command.ExecuteReader();

             while (readerDif.Read())
            {
                difficultyCombo.Items.Add(new { Id = readerDif["id"], Name = readerDif["nameDifficulty"] });
            }
            readerDif.Close();
        }
            
        categoriesCombo.DisplayMember = "Name";
        difficultyCombo.DisplayMember = "Name";
        categoriesCombo.Items.Add(new { Id = (object)null, Name = "Mixed" });
    }
}   