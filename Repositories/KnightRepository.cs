using MySqlConnector;
using System.Collections.Generic;

public class KnightRepository
{
    public List<Knight> GetAll()
    {
        var knights = new List<Knight>();
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("SELECT id, nome, hp, xp, nivel, parry, user_id, armas_id, atributos_id FROM knights", conn))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                knights.Add(new Knight
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Hp = reader.GetInt32("hp"),
                    Xp = reader.GetInt32("xp"),
                    Nivel = reader.GetInt32("nivel"),
                    Parry = reader.GetDecimal("parry"),
                    UserId = reader.GetInt32("user_id"),
                    ArmasId = reader.GetInt32("armas_id"),
                    AtributosId = reader.GetInt32("atributos_id")
                });
            }
        }
        return knights;
    }

    public void Add(Knight knight)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand(
            "INSERT INTO knights (nome, hp, xp, nivel, parry, user_id, armas_id, atributos_id) " +
            "VALUES (@nome, @hp, @xp, @nivel, @parry, @user_id, @armas_id, @atributos_id)", conn))
        {
            cmd.Parameters.AddWithValue("@nome", knight.Nome);
            cmd.Parameters.AddWithValue("@hp", knight.Hp);
            cmd.Parameters.AddWithValue("@xp", knight.Xp);
            cmd.Parameters.AddWithValue("@nivel", knight.Nivel);
            cmd.Parameters.AddWithValue("@parry", knight.Parry);
            cmd.Parameters.AddWithValue("@user_id", knight.UserId);
            cmd.Parameters.AddWithValue("@armas_id", knight.ArmasId);
            cmd.Parameters.AddWithValue("@atributos_id", knight.AtributosId);
            cmd.ExecuteNonQuery();
        }
    }
}