public class Utility
{
    // Cadastro de usuário
    public static void Cadastro()
    {
        Console.WriteLine("Digite seu nome de usuário:");
        string nome = Console.ReadLine()?? string.Empty;
        Console.WriteLine("Digite sua senha:");
        string senha = Console.ReadLine()?? string.Empty;

        UserService userService = new UserService();
        if (userService.SignUp(nome, senha))
        {
            Console.WriteLine("Usuário cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Usuário já existe. Tente novamente.");
        }
    }
    // Login de usuário
    public static void Login()
    {
        Console.WriteLine("Digite seu nome de usuário:");
        string nome = Console.ReadLine()?? string.Empty;
        Console.WriteLine("Digite sua senha:");
        string senha = Console.ReadLine()?? string.Empty;

        UserService userService = new UserService();
        if (userService.Login(nome, senha))
        {
            Console.WriteLine("Login bem-sucedido!");
        }
        else
        {
            Console.WriteLine("Nome de usuário ou senha incorretos. Tente novamente.");
        }
    }
}
