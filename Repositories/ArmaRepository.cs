using MySqlConnector;
using System.Collections.Generic;

public class ArmaRepository
{
    public List<Arma> GetAll()
    {
        var armas = new List<Arma>();
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("SELECT id, nome, dano, velocidade FROM armas", conn))
        using (var reader = cmd.ExecuteReader())
        {
            while (reader.Read())
            {
                armas.Add(new Arma
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Dano = reader.GetInt32("dano"),
                    Velocidade = reader.GetInt32("velocidade")
                });
            }
        }
        return armas;
    }

    public void Add(Arma arma)
    {
        using (var conn = Database.GetConnection())
        using (var cmd = new MySqlCommand("INSERT INTO armas (nome, dano, velocidade) VALUES (@nome, @dano, @velocidade)", conn))
        {
            cmd.Parameters.AddWithValue("@nome", arma.Nome);
            cmd.Parameters.AddWithValue("@dano", arma.Dano);
            cmd.Parameters.AddWithValue("@velocidade", arma.Velocidade);
            cmd.ExecuteNonQuery();
        }
    }
}