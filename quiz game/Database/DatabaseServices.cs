using System.Data.SqlClient;
using quiz_game.Tables;

namespace quiz_game.Database;

/// <summary>
/// Class for every interaction with database
/// </summary>
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
    
    /// <summary>
    /// Method to get single answer question from database based on category and difficulty
    /// </summary>
    /// <param name="selectedCategory">Selected category</param>
    /// <param name="selectedDifficulty">Selected difficulty</param>
    /// <returns>List of single answer questions</returns>
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
    
    /// <summary>
    /// Method to get written answer question from database based on category and difficulty
    /// </summary>
    /// <param name="selectedCategory">Selected category</param>
    /// <param name="selectedDifficulty">Selected difficulty</param>
    /// <returns>List of written answer questions</returns>
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
    
    /// <summary>
    /// Method to get multiple answer question from database based on category and difficulty
    /// </summary>
    /// <param name="selectedCategory">Selected category</param>
    /// <param name="selectedDifficulty">Selected difficulty</param>
    /// <returns>List of multiple answer questions</returns>
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
    
    /// <summary>
    /// Method to get true or false answer question from database based on category and difficulty
    /// </summary>
    /// <param name="selectedCategory">Selected category</param>
    /// <param name="selectedDifficulty">Selected difficulty</param>
    /// <returns>List of true or false questions</returns>
    public List<Question> GetTrueFalseQuestions(object selectedCategory, string selectedDifficulty)
    {
        List<Question> questions = new();
        var query = "select questionText, correctAnswer, cat_id, diff_id, " +
                    "c.nameCategory as category, d.nameDifficulty as difficulty " +
                    "from TrueFalseQuestions " +
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
                    questions.Add(new TrueFalseQuestion
                    {
                        QuestionText = reader["questionText"].ToString(),
                        CorrectAnswer = (bool)reader["correctAnswer"],
                        Category = reader["category"].ToString(),
                        Difficulty = reader["difficulty"].ToString()
                    });
                }
            }
        }
        return questions;
    }

    /// <summary>
    /// Method to get categories from database
    /// </summary>
    /// <returns>List of categories</returns>
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

    /// <summary>
    /// Method to get difficulties from database
    /// </summary>
    /// <returns>List of difficulties</returns>
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
    
    /// <summary>
    /// Method to find if user exists based on the username
    /// </summary>
    /// <param name="username">username of the user</param>
    /// <returns>true if username already exists</returns>
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
    
    /// <summary>
    /// Method to add new user
    /// </summary>
    /// <param name="username">username of the user</param>
    /// <param name="password">encrypted password</param>
    /// <returns>true if the user is successfully added</returns>
    public bool AddUser(string username, string password)
    {
        var query = "insert into Player (username, userPassword) values (@username, @password)";
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            command.Parameters.AddWithValue("@username", username);
            command.Parameters.AddWithValue("@password", password);
            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected > 0;
        }
    }
    
    /// <summary>
    /// Method to get password for user from database
    /// </summary>
    /// <param name="username">username of the user</param>
    /// <returns>Returns encrypted password for the user or null if no user is found</returns>
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

    /// <summary>
    /// Method to update score for user
    /// </summary>
    /// <param name="username">username of the user</param>
    /// <param name="score">new score</param>
    /// <returns>true if the score is updated</returns>
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
    
    /// <summary>
    /// Method to get top 5 scores from database
    /// </summary>
    /// <returns>Dictionary with top 5 users as string and score as ints</returns>
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
}