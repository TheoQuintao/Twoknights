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

    public void Delete(int id)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("DELETE FROM knights WHERE id = @id", conn))
        {
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }
    }

    public Knight? GetById(int id)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("SELECT id, nome, hp, xp, nivel, parry, user_id, armas_id, atributos_id FROM knights WHERE id = @id", conn))
        {
            cmd.Parameters.AddWithValue("@id", id);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Knight
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
                    };
                }
            }
        }
        return null;
    }

    public void AtualizarKnight(Knight knight)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand(
            "UPDATE knights SET nome = @nome, hp = @hp, xp = @xp, nivel = @nivel, parry = @parry, user_id = @user_id, armas_id = @armas_id, atributos_id = @atributos_id WHERE id = @id", conn))
        {
            cmd.Parameters.AddWithValue("@nome", knight.Nome);
            cmd.Parameters.AddWithValue("@hp", knight.Hp);
            cmd.Parameters.AddWithValue("@xp", knight.Xp);
            cmd.Parameters.AddWithValue("@nivel", knight.Nivel);
            cmd.Parameters.AddWithValue("@parry", knight.Parry);
            cmd.Parameters.AddWithValue("@user_id", knight.UserId);
            cmd.Parameters.AddWithValue("@armas_id", knight.ArmasId);
            cmd.Parameters.AddWithValue("@atributos_id", knight.AtributosId);
            cmd.Parameters.AddWithValue("@id", knight.Id);
            cmd.ExecuteNonQuery();
        }
    }
}