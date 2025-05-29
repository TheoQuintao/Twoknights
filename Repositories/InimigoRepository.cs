using MySqlConnector;
using System.Collections.Generic;

public class InimigoRepository
{
    public List<Inimigo> GetAll()
    {
        var inimigos = new List<Inimigo>();
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("SELECT id, nome, hp, nivel, parry, armas_id FROM inimigos", conn))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                inimigos.Add(new Inimigo
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Hp = reader.GetInt32("hp"),
                    Nivel = reader.GetInt32("nivel"),
                    Parry = reader.GetDecimal("parry"),
                    ArmasId = reader.GetInt32("armas_id")
                });
            }
        }
        return inimigos;
    }

    public void Add(Inimigo inimigo)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("INSERT INTO inimigos (nome, hp, nivel, parry, armas_id) VALUES (@nome, @hp, @nivel, @parry, @armas_id)", conn))
        {
            cmd.Parameters.AddWithValue("@nome", inimigo.Nome);
            cmd.Parameters.AddWithValue("@hp", inimigo.Hp);
            cmd.Parameters.AddWithValue("@nivel", inimigo.Nivel);
            cmd.Parameters.AddWithValue("@parry", inimigo.Parry);
            cmd.Parameters.AddWithValue("@armas_id", inimigo.ArmasId);
            cmd.ExecuteNonQuery();
        }
    }

    public void Delete(int id)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("DELETE FROM inimigos WHERE id = @id", conn))
        {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }
}