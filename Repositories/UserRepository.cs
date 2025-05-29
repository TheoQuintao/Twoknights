using MySqlConnector;
using System.Collections.Generic;

public class UserRepository
{
    public List<User> GetAll()
    {
        var users = new List<User>();
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("SELECT id, nome, senha FROM user", conn))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                users.Add(new User
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Senha = reader.GetString("senha")
                });
            }
        }
        return users;
    }

    public void Add(User user)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("INSERT INTO user (nome, senha) VALUES (@nome, @senha)", conn))
        {
            cmd.Parameters.AddWithValue("@nome", user.Nome);
            cmd.Parameters.AddWithValue("@senha", user.Senha);
            cmd.ExecuteNonQuery();
        }
    }

    public User? GetByNomeSenha(string nome, string senha)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("SELECT id, nome, senha FROM user WHERE nome = @nome AND senha = @senha", conn))
        {
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@senha", senha);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new User
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome"),
                        Senha = reader.GetString("senha")
                    };
                }
            }
        }
        return null;
    }

    public List<User> GetByNome(string nome)
    {
        var users = new List<User>();
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("SELECT id, nome, senha FROM user WHERE nome = @nome", conn))
        {
            cmd.Parameters.AddWithValue("@nome", nome);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(new User
                    {
                        Id = reader.GetInt32("id"),
                        Nome = reader.GetString("nome"),
                        Senha = reader.GetString("senha")
                    });
                }
            }
        }
        return users;
    }
}