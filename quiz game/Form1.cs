using System.Data.SqlClient;
using quiz_game.Tables;

namespace quiz_game;

public partial class Form1 : Form
{
    private SqlConnection connection = DatabaseConnection.GetInstance();
    
    public Form1()
    {
        InitializeComponent();
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
        string query = "SELECT questionText, correctAnswer, option1, option2, option3, c.name, d.name" +
                       " FROM Questions" +
                       "inner join Category c on cat_id = c.id" +
                       "inner join Difficulty d on dif_id = d.id" +
                       "where q.cat_id = @CategoryId and q.dif_id = @DifficultyId";
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
                CategoryId = Convert.ToInt32(reader["cat_id"]),
                DifficultyId = Convert.ToInt32(reader["diff_id"])
            });
        }
        reader.Close();
    }

    private void Form1_Load(object sender, EventArgs e)
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

    private void DisplayQuestionOnPanel(Question question)
    {
        
    }
}