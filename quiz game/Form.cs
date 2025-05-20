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
    private int currentQuestionIndex = 0;
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
        currentQuestionIndex = 0;

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
            questions.Add(new Question
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
        
        currentQuestions = questions
            .DistinctBy(q => q.QuestionText) 
            .OrderBy(q => Guid.NewGuid()) 
            .Take(MaxQuestions)        
            .ToList();
        DisplayNextQuestion();

        panel.Visible = false;
        scoreLabel.Visible = true;
    }

    private void DisplayNextQuestion()
    {
        if (currentQuestionIndex < currentQuestions.Count)
        {
            DisplayQuestionOnPanel(currentQuestions[currentQuestionIndex]);
            currentQuestionIndex++;
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

        Controls.Add(quizPanel);
    }
    
    private void DisplayQuestionOnPanel(Question question)
    {
        quizPanel.Controls.Clear();
        
        Label questionLabel = new Label
        {
            Text = question.QuestionText,
            Font = new Font("Arial", 14, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            Dock = DockStyle.Top,
            Height = 100,
            Margin = new Padding(0, 15, 0, 10),
            AutoSize = false
        };
        quizPanel.Controls.Add(questionLabel);
        
        FlowLayoutPanel radioPanel = new FlowLayoutPanel()
        {
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Anchor = AnchorStyles.None,
            Padding = new Padding(0),
            Margin = new Padding(0),
        };
        
        Panel centerPanel = new Panel()
        {
            Dock = DockStyle.Fill,
        };
        centerPanel.Controls.Add(radioPanel);

        quizPanel.Controls.Add(centerPanel);
        
        Font radioFont = new Font("Arial", 16, FontStyle.Regular);
        List<string> options = new List<string> { question.Option1, question.Option2, question.Option3 };
        
        var rnd = new Random();
        options = options.OrderBy(x => rnd.Next()).ToList();
        
        List<RadioButton> radioButtons = new List<RadioButton>();

        foreach (var opt in options)
        {
            RadioButton rb = new RadioButton
            {
                Text = opt,
                AutoSize = true,
                Margin = new Padding(0, 15, 0, 15),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = radioFont
            };
            radioButtons.Add(rb);
            radioPanel.Controls.Add(rb);
        }
        
        Button confirmButton = new Button
        {
            Text = "Submit",
            AutoSize = true,
            Anchor = AnchorStyles.Bottom,
            Padding = new Padding(10),
            Margin = new Padding(0, 10, 0, 20),
            Width = 160,
            Height = 60,
            TextAlign = ContentAlignment.MiddleCenter,
            Font = new Font("Arial", 16, FontStyle.Bold),
        };
        
        Panel buttonPanel = new Panel
        {
            Dock = DockStyle.Bottom,
            Height = 90
        };
        buttonPanel.Controls.Add(confirmButton);
        confirmButton.Location = new Point((buttonPanel.Width - confirmButton.Width) / 2, (buttonPanel.Height - confirmButton.Height) / 2);

        quizPanel.Controls.Add(buttonPanel);
        
        buttonPanel.Resize += (s, e) =>
        {
            confirmButton.Location = new Point((buttonPanel.Width - confirmButton.Width) / 2, (buttonPanel.Height - confirmButton.Height) / 2);
        };

        confirmButton.Click += (sender, e) => ConfirmAnswer(question, radioButtons, confirmButton);

        quizPanel.Visible = true;
    }

    private void ConfirmAnswer(Question question, List<RadioButton> radioButtons, Button confirmButton)
    {
        if (confirmButton.Text == "Submit")
        {
           var selectedAnswer = ""; 
           foreach (RadioButton rb in radioButtons)
           {
               if (rb.Checked)
               {
                   selectedAnswer = rb.Text;
                   selectedOption = rb;
               }
           }
           
           foreach (var rb in radioButtons)
           {
               if (rb.Text == question.CorrectAnswer)
               {
                   rb.ForeColor = Color.Green;
               }
               else if (rb.Text != question.CorrectAnswer)
               {
                   rb.ForeColor = Color.Red;
               }
           }
           
           if (selectedAnswer == question.CorrectAnswer)
           {
               score++;
               scoreLabel.Text = $"Score: {score}";
           }
           confirmButton.Text = "Next";
           
           foreach (var rb in radioButtons)
           {
               rb.Click -= DisableClick; // Zajistěte, že nebudou přidány duplicity
               rb.Click += DisableClick;
           }
        }
        else
        {
            DisplayNextQuestion();
        }
    }
    
    private void DisableClick(object sender, EventArgs e)
    {
        ((RadioButton)sender).Checked = false;
        selectedOption.Checked = true;
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