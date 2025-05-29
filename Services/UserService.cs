using MySqlConnector;

public class UserService
{
    private readonly UserRepository _userRepository = new UserRepository();

    public void AdicionarUsuario(string nome, string senha)
    {
        var user = new User
        {
            Nome = nome,
            Senha = senha
        };
        _userRepository.Add(user);
    }

    public bool Login(string nome, string senha)
    {
        var user = _userRepository.GetByNomeSenha(nome, senha);
        return user != null;
    }

    public bool SignUp(string nome, string senha)
    {
        // Verifica se já existe usuário com esse nome
        var existente = _userRepository.GetByNome(nome);
        if (existente.Count > 0)
            return false; // Usuário já existe

        AdicionarUsuario(nome, senha);
        return true;
    }

    public User? GetUser(string nome, string senha)
    {
        return _userRepository.GetByNomeSenha(nome, senha);
    }

    public User? GetById(int id)
    {
        using (var conn = Database.GetConnection())
        {
            // conn.Open(); // Remova ou comente esta linha!
            using (var cmd = new MySqlCommand("SELECT id, nome, senha FROM user WHERE id = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
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
        }
        return null;
    }
}