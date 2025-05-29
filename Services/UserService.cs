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
}