using System.Data.SqlClient;
using quiz_game.Tables;

namespace quiz_game;

public class DatabaseServices
{
    private SqlConnection connection = DatabaseConnection.GetInstance();
    
    public List<Question> GetSingleQuestions(object selectedCategory, string selectedDifficulty)
    {
        List<Question> questions = new();
        
        var query = "SELECT questionText, correctAnswer, option1, option2, option3, cat_id, diff_id," +
                       " c.nameCategory as category, d.nameDifficulty as difficulty" +
                       " FROM Questions " +
                       "INNER JOIN Category c ON cat_id = c.id " +
                       "INNER JOIN Difficulty d ON diff_id = d.id " +
                       "WHERE (@CategoryId IS NULL OR cat_id = @CategoryId) " +
                       "AND diff_id = @DifficultyId";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@CategoryId", selectedCategory ?? DBNull.Value);
            command.Parameters.AddWithValue("@DifficultyId", selectedDifficulty);

            using (SqlDataReader reader = command.ExecuteReader())
            {
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
            }
        }
        return questions;
    }
    
    public List<Question> GetWrittenQuestions(object selectedCategory, string selectedDifficulty)
    {
        List<Question> questions = new List<Question>();
        var query = "select questionText, correctAnswer, cat_id, diff_id, " +
                    "c.nameCategory as category, d.nameDifficulty as difficulty " +
                    "from WrittenQuestions " +
                    "inner join Category c on cat_id = c.id " +
                    "inner join Difficulty d on diff_id = d.id " +
                    "where (@CategoryId is null or cat_id = @CategoryId) " +
                    "and diff_id = @DifficultyId";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@CategoryId", selectedCategory ?? DBNull.Value);
            command.Parameters.AddWithValue("@DifficultyId", selectedDifficulty);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    questions.Add(new WrittenAnswer
                    {
                        QuestionText = reader["questionText"].ToString(),
                        CorrectAnswer = reader["correctAnswer"].ToString(),
                        Category = reader["category"].ToString(),
                        Difficulty = reader["difficulty"].ToString()
                    });
                }
            }
        }
        return questions;
    }

    public List<dynamic> GetCategories()
    {
        List<dynamic> categories = new();
        var query = "select id, nameCategory from category";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    categories.Add(new
                    {
                        Id = reader["id"],
                        Name = reader["nameCategory"].ToString()
                    });
                }
            }
        }
        return categories;
    }

    public List<dynamic> GetDifficulties()
    {
        List<dynamic> difficulties = new();
        var query = "select id, nameDifficulty from difficulty";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    difficulties.Add(new
                    {
                        Id = reader["id"],
                        Name = reader["nameDifficulty"].ToString()
                    });
                }
            }
        }

        return difficulties;
    }
}