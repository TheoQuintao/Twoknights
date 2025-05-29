using MySqlConnector;

public class Database
{
    private const string ConnectionString = "Server=localhost;Database=twoknights;User ID=root;Password=2507Theo@;";

    public static MySqlConnection GetConnection()
    {
        var connection = new MySqlConnection(ConnectionString);
        connection.Open();
        return connection;
    }
}