using System.Data.SqlClient;
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

    private void textBox1_TextChanged(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void panel1_Paint_1(object sender, PaintEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void label1_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void startButton_Click(object sender, EventArgs e)
    {
        string selectedCategory = ((dynamic)categoriesCombo.SelectedItem)?.Id?.ToString();
        string selectedDifficulty = ((dynamic)difficultyCombo.SelectedItem)?.Id?.ToString();
        List<Question> questions = new List<Question>();
        string query = "SELECT questionText, correctAnswer, option1, option2, option3, cat_id, diff_id," +
                       " c.nameCategory as category, d.nameDifficulty as difficulty" +
                       " FROM Questions " +
                       "inner join Category c on cat_id = c.id " +
                       "inner join Difficulty d on diff_id = d.id " +
                       "where cat_id = @CategoryId and diff_id = @DifficultyId";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@CategoryId", selectedCategory);
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
            
            Console.WriteLine(questions[0]);
        }
        reader.Close();

        DisplayQuestionOnPanel(questions[0]);

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
        
        RadioButton option1 = new RadioButton
        {
            Text = question.Option1,
            Dock = DockStyle.Top,
            Padding = new Padding(10),
            AutoSize = true
        };
        RadioButton option2 = new RadioButton
        {
            Text = question.Option2,
            Dock = DockStyle.Top,
            Padding = new Padding(10),
            AutoSize = true
        };
        RadioButton option3 = new RadioButton
        {
            Text = question.Option3,
            Dock = DockStyle.Top,
            Padding = new Padding(10),
            AutoSize = true
        };

        quizPanel.Controls.Add(option3);
        quizPanel.Controls.Add(option2);
        quizPanel.Controls.Add(option1);

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
            
        var querryDif = "select id, nameDifficulty from difficulty";
        using (SqlCommand command = new SqlCommand(querryDif, connection))
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
    }
}   