using System.Configuration;
using System.Data.SqlClient;

namespace quiz_game.Database;

/// <summary>
/// Class to Connect to database using App.config
/// </summary>
public static class DatabaseConnection
{
    private static SqlConnection? conn = null;

    /// <summary>
    /// Method to connect to database
    /// </summary>
    /// <returns>A SqlConnection object representing the database connection</returns>
    public static SqlConnection GetInstance()
    {
        try
        {
            if (conn == null)
            {
                SqlConnectionStringBuilder consStringBuilder = new SqlConnectionStringBuilder();
                consStringBuilder.UserID = ReadSetting("Name");
                consStringBuilder.Password = ReadSetting("Password");
                consStringBuilder.InitialCatalog = ReadSetting("Database");
                consStringBuilder.DataSource = ReadSetting("DataSource");
                consStringBuilder.ConnectTimeout = 30;
                conn = new SqlConnection(consStringBuilder.ConnectionString);
                conn.Open();
            }

            return conn;
        }
        catch (Exception e)
        {
            MessageBox.Show("Wrong App.config file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(0);
            throw;
        }
    }

    /// <summary>
    /// Reads a specific setting from the App.config file based on the provided key.
    /// </summary>
    /// <param name="key"></param>
    /// <returns>The value associated with the key. If the key is not found, "Not Found" is returned.</returns>
    private static string ReadSetting(string key)
    {
        var appSettings = ConfigurationManager.AppSettings;
        string result = appSettings[key] ?? "Not Found";
        return result;
    }
}