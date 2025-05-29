using MySqlConnector;

public class Database
{
    private const string ConnectionString = "Server=localhost;Database=twoknights;User ID=root;Password=senha;";

    public static MySqlConnection GetConnection()
    {
        var connection = new MySqlConnection(ConnectionString);
        connection.Open();
        return connection;
    }
}