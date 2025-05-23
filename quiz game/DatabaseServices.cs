using System.Data.SqlClient;
using quiz_game.Tables;

namespace quiz_game;

public class DatabaseServices
{
    private SqlConnection connection = DatabaseConnection.GetInstance();

    public DatabaseServices()
    {
        if (connection.State != System.Data.ConnectionState.Open)
        {
            connection.Open();
        }
    }
    
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
    
    public List<Question> GetMultipleQuestions(object selectedCategory, string selectedDifficulty)
    {
        List<Question> questions = new();
        var query = "SELECT questionText, option1, option2, option3, correctAnswer, cat_id, diff_id, " +
                    "c.nameCategory as category, d.nameDifficulty as difficulty " +
                    "FROM MultipleChoiceQuestion " +
                    "INNER JOIN Category c ON cat_id = c.id " +
                    "INNER JOIN Difficulty d ON diff_id = d.id " +
                    "WHERE (@CategoryId IS NULL OR cat_id = @CategoryId) AND diff_id = @DifficultyId";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@CategoryId", selectedCategory ?? DBNull.Value);
            command.Parameters.AddWithValue("@DifficultyId", selectedDifficulty);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    questions.Add(new MultipleChoiceQuestion
                    {
                        QuestionText = reader["questionText"].ToString(),
                        Option1 = reader["option1"].ToString(),
                        Option2 = reader["option2"].ToString(),
                        Option3 = reader["option3"].ToString(),
                        CorrectAnswers = reader["correctAnswer"].ToString().Split(';').ToList(),
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
    
    public bool UserExists(string username)
    {
        var query = "select count(*) from Player where username = @username";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@username", username);
            int count = (int)command.ExecuteScalar();
            return count > 0;
        }
    }
    
    public bool AddUser(string username, string password)
    {
        var query = "insert into Player (username, userPassword, score, diff_id, cat_id) values (@username, @password, 0, null, null)";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    
    public string GetPasswordForUser(string username)
    {
        var query = "select userPassword from Player where username = @username";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@username", username);
            object result = command.ExecuteScalar();
            return result != null ? result.ToString() : null;
        }
    }

    public bool UpdateScore(string username, int score)
    {
        var query = "update Player set score = @score where username = @username" +
                    " and (score is null or @score > score)";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@score", score);
            
            return command.ExecuteNonQuery() > 0;
        }
    }

    public Dictionary<string, int> GetTopScores()
    {
        Dictionary<string, int> topScores = new();
        var query = "SELECT TOP (5) username, score FROM Player WHERE score IS NOT NULL ORDER BY score DESC";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string username = reader["username"].ToString();
                    int score = (int)reader["score"];
                    topScores.Add(username, score);
                }
            }
        }

        return topScores;
    }
    
    public Dictionary<string, int> GetScoresByCategoryAndDifficulty(int? categoryId, string difficulty)
    {
        Dictionary<string, int> filteredScores = new();
        var query = @"
            SELECT username, score
            FROM Player
            WHERE (cat_id = @CategoryId OR @CategoryId IS NULL)
              AND (diff_id = (SELECT id FROM Difficulty WHERE nameDifficulty = @Difficulty) OR @Difficulty IS NULL)
              AND score IS NOT NULL
            ORDER BY score DESC";

        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@CategoryId", categoryId.HasValue ? (object)categoryId.Value : DBNull.Value);
            command.Parameters.AddWithValue("@Difficulty", string.IsNullOrEmpty(difficulty) ? DBNull.Value : difficulty);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string username = reader["username"].ToString();
                    int score = (int)reader["score"];
                    filteredScores.Add(username, score);
                }
            }
        }

        return filteredScores;
    }
}