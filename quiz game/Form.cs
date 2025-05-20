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
        
        var random = new Random();
        var randomQuestion = questions[random.Next(0, questions.Count)];
        DisplayQuestionOnPanel(randomQuestion);

        panel.Visible = false;
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
            Font = new Font("Arial", 12, FontStyle.Bold),
            Dock = DockStyle.Top,
            Padding = new Padding(10),
            AutoSize = true
        };
        quizPanel.Controls.Add(questionLabel);

        Panel radioPanel = new Panel()
        {
            Visible = true,
            Dock = DockStyle.Fill
        };
        
        quizPanel.Controls.Add(radioPanel);
        
        RadioButton option1 = new RadioButton
        {
            Text = question.Option1,
            Dock = DockStyle.Bottom,
            Padding = new Padding(10),
            AutoSize = true
        };
        RadioButton option2 = new RadioButton
        {
            Text = question.Option2,
            Dock = DockStyle.Bottom,
            Padding = new Padding(10),
            AutoSize = true
        };
        RadioButton option3 = new RadioButton
        {
            Text = question.Option3,
            Dock = DockStyle.Bottom,
            Padding = new Padding(10),
            AutoSize = true
        };

        radioPanel.Controls.Add(option3);
        radioPanel.Controls.Add(option2);
        radioPanel.Controls.Add(option1);

        Button confirmButton = new Button
        {
            Text = "Submit",
            Dock = DockStyle.Bottom,
            Padding = new Padding(10),
            AutoSize = true
        };
        confirmButton.Click += (sender, e) => ConfirmAnswer(question, option1, option2, option3);
        quizPanel.Controls.Add(confirmButton);

        quizPanel.Visible = true;
    }

    private void ConfirmAnswer(Question question, RadioButton option1, RadioButton option2, RadioButton option3)
    {
        var selectedAnswer = "";

        if (option1.Checked) selectedAnswer = option1.Text;
        if (option2.Checked) selectedAnswer = option2.Text;
        if (option3.Checked) selectedAnswer = option3.Text;

        if (selectedAnswer == question.CorrectAnswer)
        {
            MessageBox.Show("Correct");
        }
        else
        {
            MessageBox.Show("Wrong");
        }

        quizPanel.Visible = false;
    }

    private void Form_Load(object sender, EventArgs e)
    {
        var querryCat = "select id, nameCategory from category";
        using (SqlCommand command = new SqlCommand(querryCat, connection))
        {
            SqlDataReader readerCat = command.ExecuteReader();

            while (readerCat.Read())
            {
                categoriesCombo.Items.Add(new { Id = readerCat["id"], Name = readerCat["nameCategory"] });
            }
            readerCat.Close();
        }
            
        var querryDiff = "select id, nameDifficulty from difficulty";
        using (SqlCommand command = new SqlCommand(querryDiff, connection))
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