using System.Configuration;
using System.Data.SqlClient;

namespace quiz_game.Database;

public class DatabaseConnection
{
    private static SqlConnection? conn = null;

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
            Console.WriteLine("Wrong App.config file.");
            Environment.Exit(0);
            throw;
        }
    }

    private static string ReadSetting(string key)
    {
        var appSettings = ConfigurationManager.AppSettings;
        string result = appSettings[key] ?? "Not Found";
        return result;
    }

}