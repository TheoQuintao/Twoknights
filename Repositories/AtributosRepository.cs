using MySqlConnector;
using System.Collections.Generic;

public class AtributosRepository
{
    public List<Atributos> GetAll()
    {
        var atributos = new List<Atributos>();
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("SELECT id, força, destreza, Constituicao FROM atributos", conn))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                atributos.Add(new Atributos
                {
                    Id = reader.GetInt32("id"),
                    Forca = reader.GetInt32("força"),
                    Destreza = reader.GetInt32("destreza"),
                    Constituicao = reader.GetInt32("Constituicao")
                });
            }
        }
        return atributos;
    }

    public void Add(Atributos atributo)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("INSERT INTO atributos (força, destreza, Constituicao) VALUES (@forca, @destreza, @constituicao)", conn))
        {
            cmd.Parameters.AddWithValue("@forca", atributo.Forca);
            cmd.Parameters.AddWithValue("@destreza", atributo.Destreza);
            cmd.Parameters.AddWithValue("@constituicao", atributo.Constituicao);
            cmd.ExecuteNonQuery();
        }
    }

    public int AddAndReturnId(Atributos atributo)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("INSERT INTO atributos (força, destreza, Constituicao) VALUES (@forca, @destreza, @constituicao); SELECT LAST_INSERT_ID();", conn))
        {
            cmd.Parameters.AddWithValue("@forca", atributo.Forca);
            cmd.Parameters.AddWithValue("@destreza", atributo.Destreza);
            cmd.Parameters.AddWithValue("@constituicao", atributo.Constituicao);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }
    }

    public Atributos GetById(int id)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("SELECT id, força, destreza, Constituicao FROM atributos WHERE id = @id", conn))
        {
            cmd.Parameters.AddWithValue("@id", id);
            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return new Atributos
                    {
                        Id = reader.GetInt32("id"),
                        Forca = reader.GetInt32("força"),
                        Destreza = reader.GetInt32("destreza"),
                        Constituicao = reader.GetInt32("Constituicao")
                    };
                }
            }
        }
        throw new Exception("Atributos não encontrados.");
    }

    public void AtualizarAtributos(Atributos atributos)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("UPDATE atributos SET força = @forca, destreza = @destreza, Constituicao = @constituicao WHERE id = @id", conn))
        {
            cmd.Parameters.AddWithValue("@forca", atributos.Forca);
            cmd.Parameters.AddWithValue("@destreza", atributos.Destreza);
            cmd.Parameters.AddWithValue("@constituicao", atributos.Constituicao);
            cmd.Parameters.AddWithValue("@id", atributos.Id);
            cmd.ExecuteNonQuery();
        }
    }
}