using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using quiz_game.Tables;

namespace quiz_game;

public partial class Form : System.Windows.Forms.Form
{
    private SqlConnection connection = DatabaseConnection.GetInstance();
    private Panel quizPanel;
    private List<Question> currentQuestions = new();
    public static int CurrentQuestionIndex { get; set; }
    private const int MaxQuestions = 5;
    private int score = 0;
    private Label scoreLabel;
    private RadioButton selectedOption = null;
    
    public Form()
    {
        InitializeComponent();
        InitializeQuestionPanel();
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
    }

    private void DisplayNextQuestion()
    {
        if (CurrentQuestionIndex < currentQuestions.Count)
        {
            var currentQuestion = currentQuestions[CurrentQuestionIndex];
            currentQuestion.Display(quizPanel, isCorrect =>
            {
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
            DisplayFinalScorePanel();
            quizPanel.Visible = false;
        }
    }
    
    private void InitializeQuestionPanel()
    {
        quizPanel = new Panel
        {
            Visible = false,
            Dock = DockStyle.Fill
        };
        
        scoreLabel = new Label
        {
            Text = "Score: 0",
            Font = new Font("Arial", 12, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleRight,
            Dock = DockStyle.Top,
            Height = 40,
            Padding = new Padding(0, 10, 10, 0),
            Visible = false
        };
        Controls.Add(scoreLabel);

        Controls.Add(quizPanel);
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

    private void Form_Load(object sender, EventArgs e)
    {
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