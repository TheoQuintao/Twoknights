using MySqlConnector;

public class Database
{
    private const string ConnectionString = "Server=localhost;Database=twoknights;User ID=seu_usuario;Password=sua_senha;";

    public static MySqlConnection GetConnection()
    {
        var connection = new MySqlConnection(ConnectionString);
        connection.Open();
        return connection;
    }
}