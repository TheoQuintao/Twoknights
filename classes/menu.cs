using System;

public class Menu
{
    public static void Exibir()
    {
        User? usuarioLogado = null;

        while (true) // Laço de login/cadastro
        {
            Console.Clear();
            Console.WriteLine("=== MENU DE LOGIN ===");
            Console.WriteLine("1 - Login");
            Console.WriteLine("2 - Cadastro (SignUp)");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine() ?? "";
            Console.WriteLine(); // Linha em branco para melhor visualização
            switch (opcao)
            {
                case "1":
                    usuarioLogado = Utility.Login();
                    if (usuarioLogado != null)
                        MenuUsuario(usuarioLogado); // Só entra no menu principal se login for bem-sucedido
                    break;
                case "2":
                    Utility.Cadastro();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Opção inválida!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private static void MenuUsuario(User usuarioLogado)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"\nBem-vindo, {usuarioLogado.Nome}!");
            Console.WriteLine("1 - Criar Knight");
            Console.WriteLine("2 - Listar Knights");
            Console.WriteLine("3 - Apagar Knight");
            int opcaoInimigo = 4;
            if (usuarioLogado.Nome.ToLower() == "admin")
            {
                Console.WriteLine($"{opcaoInimigo} - Criar Inimigo");
                opcaoInimigo++;
                Console.WriteLine($"{opcaoInimigo} - Listar Inimigos");
                opcaoInimigo++;
                Console.WriteLine($"{opcaoInimigo} - Apagar Inimigo");
                opcaoInimigo++;
            }
            Console.WriteLine($"{opcaoInimigo} - Batalha Knight vs Inimigo");
            opcaoInimigo++;
            Console.WriteLine($"{opcaoInimigo} - Batalha Knight vs Knight"); // NOVA OPÇÃO
            opcaoInimigo++;
            Console.WriteLine($"{opcaoInimigo} - Logout");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine() ?? "";

            if (opcao == "1")
            {
                Utility.CriarKnight(usuarioLogado);
                Console.ReadKey();
            }
            else if (opcao == "2")
            {
                Utility.ListarKnights(usuarioLogado);
                Console.ReadKey();
            }
            else if (opcao == "3")
            {
                Utility.ApagarKnight();
                Console.ReadKey();
            }
            else if (usuarioLogado.Nome.ToLower() == "admin" && opcao == "4")
            {
                Utility.CriarInimigo();
                Console.ReadKey();
            }
            else if (usuarioLogado.Nome.ToLower() == "admin" && opcao == "5")
            {
                Utility.ListarInimigos();
                Console.ReadKey();
            }
            else if (usuarioLogado.Nome.ToLower() == "admin" && opcao == "6")
            {
                Utility.ApagarInimigo();
                Console.ReadKey();
            }
            else if (
                (usuarioLogado.Nome.ToLower() == "admin" && opcao == "7") ||
                (usuarioLogado.Nome.ToLower() != "admin" && opcao == "4")
            )
            {
                Utility.BatalhaKnightVsInimigo(usuarioLogado);
                Console.ReadKey();
            }
            else if (
                (usuarioLogado.Nome.ToLower() == "admin" && opcao == "8") ||
                (usuarioLogado.Nome.ToLower() != "admin" && opcao == "5")
            )
            {
                Utility.BatalhaKnightVsKnight(usuarioLogado);
                Console.ReadKey();
            }
            else if (opcao == opcaoInimigo.ToString())
            {
                Console.WriteLine("Logout realizado!");
                Console.ReadKey();
                break;
            }
            else if (opcao == "0")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Opção inválida!");
                Console.ReadKey();
            }
        }
    }
}