using System.Threading;

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

    public static User? Login()
    {
        Console.WriteLine("Digite seu nome de usuário:");
        string nome = Console.ReadLine() ?? string.Empty;
        Console.WriteLine("Digite sua senha:");
        string senha = Console.ReadLine() ?? string.Empty;

        UserService userService = new UserService();
        var user = userService.GetUser(nome, senha);

        if (user != null)
        {
            Console.WriteLine("Login bem-sucedido!");
            return user;
        }
        else
        {
            Console.WriteLine("Nome de usuário ou senha incorretos. Tente novamente.");
            Console.ReadKey();
            return null;
        }
    }
    // Distribuição de atributos
    public static int DistribuirAtributos()
    {
        while (true)
        {
            int forca = 1, destreza = 1, constituicao = 1;
            int pontosRestantes = 5;

            while (pontosRestantes > 0)
            {
                Console.WriteLine("Distribua seus pontos entre os atributos:");
                Console.WriteLine($"\nPontos restantes: {pontosRestantes}");
                Console.WriteLine($"Força: {forca}, Destreza: {destreza}, Constituição: {constituicao}");
                Console.WriteLine("Digite o atributo para adicionar um ponto (forca, destreza, constituicao):");
                string escolha = Console.ReadLine()?.Trim().ToLower() ?? "";

                switch (escolha)
                {
                    case "forca":
                        forca++;
                        pontosRestantes--;
                        break;
                    case "destreza":
                        destreza++;
                        pontosRestantes--;
                        break;
                    case "constituicao":
                        constituicao++;
                        pontosRestantes--;
                        break;
                    default:
                        Console.WriteLine("Atributo inválido. Tente novamente.");
                        break;
                }
                Console.Clear();
            }

            Console.WriteLine($"\nDistribuição final: Força={forca}, Destreza={destreza}, Constituição={constituicao}");
            Console.WriteLine("Confirma esta distribuição? (s/n)");
            string confirmacao = Console.ReadLine()?.Trim().ToLower() ?? "";
            if (confirmacao == "s" || confirmacao == "sim")
            {
                // Salva no banco e retorna o id
                var atributosService = new AtributosService();
                int idAtributos = atributosService.AdicionarAtributosERetornarId(forca, destreza, constituicao);
                return idAtributos;
            }
            else
            {
                Console.WriteLine("Vamos reiniciar a distribuição dos pontos.");
            }
        }
    }
    public static int SelecionarArma()
    {
        var armaService = new ArmaService();
        var armas = armaService.BuscarTodas(); // Implemente esse método no ArmaService se ainda não existir

        Console.WriteLine("\nArmas disponíveis:\n");
        foreach (var arma in armas)
        {
            Console.WriteLine($"ID: {arma.Id} | Nome: {arma.Nome} | Dano: {arma.Dano} | Velocidade: {arma.Velocidade}");
        }

        while (true)
        {
            Console.Write("Digite o ID da arma desejada: ");
            string entrada = Console.ReadLine() ?? "";
            if (int.TryParse(entrada, out int idEscolhido) && armas.Any(a => a.Id == idEscolhido))
            {
                return idEscolhido;
            }
            else
            {
                Console.WriteLine("ID inválido. Tente novamente.");
            }
        }
    }
    public static void CriarKnight(User usuario)
    {
        Console.WriteLine("Digite o nome do seu cavaleiro:");
        string nomeKnight = Console.ReadLine() ?? string.Empty;

        // Distribuição de atributos e obtenção do ID
        int atributosId = DistribuirAtributos();

        // Recupera os valores dos atributos para cálculo
        var atributosRepo = new AtributosRepository();
        var atributos = atributosRepo.GetById(atributosId);

        // Seleção de arma
        int armasId = SelecionarArma();

        // Cálculo de HP e Parry
        int hp = 50 + atributos.Constituicao * 2;
        decimal parry = atributos.Destreza * 2.0m;

        // Valores padrão
        int xp = 0;
        int nivel = 1;

        // Criação do Knight
        var knightService = new KnightService();
        knightService.AdicionarKnight(
            nomeKnight,
            hp,
            xp,
            nivel,
            parry,
            usuario.Id,   // ID do usuário logado
            armasId,
            atributosId
        );

        Console.WriteLine("Cavaleiro criado com sucesso!");
    }
    public static void CriarInimigo()
    {
        Console.WriteLine("Digite o nome do inimigo:");
        string nome = Console.ReadLine() ?? string.Empty;

        Console.WriteLine("Digite o nível do inimigo:");
        int nivel = int.TryParse(Console.ReadLine(), out int n) ? n : 1;

        Console.WriteLine("Digite o HP do inimigo:");
        int hp = int.TryParse(Console.ReadLine(), out int h) ? h : 50;

        Console.WriteLine("Digite o Parry do inimigo (em %):");
        decimal parry = decimal.TryParse(Console.ReadLine(), out decimal p) ? p : 0.0m;

        // Seleção de arma
        int armasId = SelecionarArma();

        // Criação do inimigo
        var inimigoService = new InimigoService();
        inimigoService.AdicionarInimigo(
            nome,
            hp,
            nivel,
            parry,
            armasId
        );

        Console.WriteLine("Inimigo criado com sucesso!");
    }
    public static void ListarInimigos()
    {
        var inimigoService = new InimigoService();
        var inimigos = inimigoService.BuscarTodos();
        var armaService = new ArmaService();

        Console.WriteLine("\n--- Inimigos Criados ---");
        for (int i = 0; i < inimigos.Count; i++)
        {
            var inimigo = inimigos[i];
            var arma = armaService.BuscarTodas().FirstOrDefault(a => a.Id == inimigo.ArmasId);
            string armaInfo = arma != null
                ? $"Arma: {arma.Nome} | Dano: {arma.Dano} | Velocidade: {arma.Velocidade}"
                : "Arma: (não encontrada)";
            Console.WriteLine($"{i + 1} - Nome: {inimigo.Nome} | HP: {inimigo.Hp} | Nível: {inimigo.Nivel} | {armaInfo}");
        }
    }

    public static void ApagarInimigo()
    {
        var inimigoService = new InimigoService();
        var inimigos = inimigoService.BuscarTodos();
        var armaService = new ArmaService();

        Console.WriteLine("\n--- Inimigos Criados ---");
        for (int i = 0; i < inimigos.Count; i++)
        {
            var inimigo = inimigos[i];
            var arma = armaService.BuscarTodas().FirstOrDefault(a => a.Id == inimigo.ArmasId);
            string armaInfo = arma != null
                ? $"Arma: {arma.Nome} | Dano: {arma.Dano} | Velocidade: {arma.Velocidade}"
                : "Arma: (não encontrada)";
            Console.WriteLine($"{i + 1} - Nome: {inimigo.Nome} | HP: {inimigo.Hp} | Nível: {inimigo.Nivel} | {armaInfo}");
        }
        Console.Write("\nDigite o número do Inimigo que deseja apagar: ");
        string entrada = Console.ReadLine() ?? "";
        if (int.TryParse(entrada, out int idx) && idx > 0 && idx <= inimigos.Count)
        {
            var inimigo = inimigos[idx - 1];
            inimigoService.ApagarInimigo(inimigo.Id);
            Console.WriteLine("Inimigo apagado (se existir).");
        }
        else
        {
            Console.WriteLine("Número inválido.");
        }
    }

    public static void ListarKnights(User usuarioLogado)
    {
        var knightService = new KnightService();
        var knights = knightService.BuscarTodos();
        var armaService = new ArmaService();
        var userService = new UserService();

        // Só mostra todos se for admin
        if (usuarioLogado.Nome.ToLower() != "admin")
            knights = knights.Where(k => k.UserId == usuarioLogado.Id).ToList();

        Console.WriteLine("\n--- Knights Criados ---");
        for (int i = 0; i < knights.Count; i++)
        {
            var k = knights[i];
            var arma = armaService.BuscarTodas().FirstOrDefault(a => a.Id == k.ArmasId);
            var usuario = userService.GetById(k.UserId);
            string nomeUsuario = usuario != null ? usuario.Nome : "Desconhecido";
            string armaInfo = arma != null
                ? $"Arma: {arma.Nome} | Dano: {arma.Dano} | Velocidade: {arma.Velocidade}"
                : "Arma: (não encontrada)";
            Console.WriteLine($"{i + 1} - Nome: {k.Nome} | HP: {k.Hp} | Nível: {k.Nivel} | User: {nomeUsuario} | {armaInfo}");
        }
    }

    public static void ApagarKnight()
    {
        var knightService = new KnightService();
        var knights = knightService.BuscarTodos();
        var armaService = new ArmaService();

        Console.WriteLine("\n--- Knights Criados ---");
        for (int i = 0; i < knights.Count; i++)
        {
            var k = knights[i];
            var arma = armaService.BuscarTodas().FirstOrDefault(a => a.Id == k.ArmasId);
            string armaInfo = arma != null
                ? $"Arma: {arma.Nome} | Dano: {arma.Dano} | Velocidade: {arma.Velocidade}"
                : "Arma: (não encontrada)";
            Console.WriteLine($"{i + 1} - Nome: {k.Nome} | HP: {k.Hp} | Nível: {k.Nivel} | {armaInfo}");
        }
        Console.Write("\nDigite o número do Knight que deseja apagar: ");
        string entrada = Console.ReadLine() ?? "";
        if (int.TryParse(entrada, out int idx) && idx > 0 && idx <= knights.Count)
        {
            var knight = knights[idx - 1];
            knightService.ApagarKnight(knight.Id);
            Console.WriteLine("Knight apagado (se existir).");
        }
        else
        {
            Console.WriteLine("Número inválido.");
        }
    }

    public static void BatalhaKnightVsInimigo(User usuarioLogado)
    {
        var knightService = new KnightService();
        var knights = knightService.BuscarTodos();
        var armaService = new ArmaService();
        var userService = new UserService();

        // Só mostra todos se for admin
        if (usuarioLogado.Nome.ToLower() != "admin")
            knights = knights.Where(k => k.UserId == usuarioLogado.Id).ToList();

        Console.WriteLine("\n--- Knights Criados ---");
        for (int i = 0; i < knights.Count; i++)
        {
            var k = knights[i];
            var arma = armaService.BuscarTodas().FirstOrDefault(a => a.Id == k.ArmasId);
            var usuario = userService.GetById(k.UserId);
            string nomeUsuario = usuario != null ? usuario.Nome : "Desconhecido";
            string armaInfo = arma != null
                ? $"Arma: {arma.Nome} | Dano: {arma.Dano} | Velocidade: {arma.Velocidade}"
                : "Arma: (não encontrada)";
            Console.WriteLine($"{i + 1} - Nome: {k.Nome} | HP: {k.Hp} | Nível: {k.Nivel} | User: {nomeUsuario} | {armaInfo}");
        }
        Console.Write("\nDigite o número do Knight para a batalha: ");
        int knightIndex = int.TryParse(Console.ReadLine(), out int kIdx) ? kIdx - 1 : -1;
        if (knightIndex < 0 || knightIndex >= knights.Count)
        {
            Console.WriteLine("Knight não encontrado.");
            return;
        }
        var knight = knights[knightIndex];

        // Buscar inimigos aleatórios do nível adequado
        var inimigosPossiveis = BuscarInimigosAleatoriosParaKnight(knight.Nivel);

        if (inimigosPossiveis.Count == 0)
        {
            Console.WriteLine("Não há inimigos disponíveis nesse nível para a batalha.");
            return;
        }

        Console.WriteLine("\nEscolha um inimigo para a batalha:");
        for (int i = 0; i < inimigosPossiveis.Count; i++)
        {
            var inimigoPossivel = inimigosPossiveis[i];
            var arma = armaService.BuscarTodas().FirstOrDefault(a => a.Id == inimigoPossivel.ArmasId);
            string armaInfo = arma != null
                ? $"Arma: {arma.Nome} | Dano: {arma.Dano} | Velocidade: {arma.Velocidade}"
                : "Arma: (não encontrada)";
            Console.WriteLine($"{i + 1} - Nome: {inimigoPossivel.Nome} | HP: {inimigoPossivel.Hp} | Nível: {inimigoPossivel.Nivel} | {armaInfo}");
        }
        Console.Write("\nDigite o número do Inimigo para a batalha: ");
        int inimigoIndex = int.TryParse(Console.ReadLine(), out int iIdx) ? iIdx - 1 : -1;
        if (inimigoIndex < 0 || inimigoIndex >= inimigosPossiveis.Count)
        {
            Console.WriteLine("Inimigo não encontrado.");
            return;
        }
        var inimigo = inimigosPossiveis[inimigoIndex];

        // Busca atributos e arma do Knight
        var atributosRepo = new AtributosRepository();
        var atributosKnight = atributosRepo.GetById(knight.AtributosId);
        var armaRepo = new ArmaRepository();
        var armaKnight = armaRepo.GetAll().FirstOrDefault(a => a.Id == knight.ArmasId);

        // Busca arma do Inimigo
        var armaInimigo = armaRepo.GetAll().FirstOrDefault(a => a.Id == inimigo.ArmasId);

        // Inicializa HPs
        int hpKnight = knight.Hp;
        int hpInimigo = inimigo.Hp;

        // Cálculo de dano e velocidade ajustados
        int danoKnight = (armaKnight?.Dano ?? 0) + (atributosKnight?.Forca ?? 0);
        int danoInimigo = armaInimigo?.Dano ?? 0;

        // Velocidade ajustada: base + destreza do usuário
        int velKnight = (armaKnight?.Velocidade ?? 1) + (atributosKnight?.Destreza ?? 0);
        int velInimigo = armaInimigo?.Velocidade ?? 1;

        // Parry
        decimal parryKnight = knight.Parry;
        decimal parryInimigo = inimigo.Parry;

        int barraKnight = 0;
        int barraInimigo = 0;
        int turno = 1;
        Random rnd = new Random();

        Console.Clear();
        Console.WriteLine("===================================");
        Console.WriteLine("         INÍCIO DA BATALHA         ");
        Console.WriteLine("===================================");
        Console.WriteLine($"\n{knight.Nome} (HP: {hpKnight})");
        Console.WriteLine($"  Arma: {armaKnight.Nome} | Dano: {danoKnight} | Velocidade: {velKnight}");
        Console.WriteLine($"  Parry: {parryKnight}%");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"{inimigo.Nome} (HP: {hpInimigo})");
        Console.WriteLine($"  Arma: {armaInimigo.Nome} | Dano: {danoInimigo} | Velocidade: {velInimigo}");
        Console.WriteLine($"  Parry: {parryInimigo}%");
        Console.WriteLine("===================================\n");

        while (hpKnight > 0 && hpInimigo > 0)
        {
            barraKnight += velKnight;
            barraInimigo += velInimigo;

            bool knightAtaca = false, inimigoAtaca = false;

            if (barraKnight >= 100)
            {
                knightAtaca = true;
                barraKnight -= 100;
            }
            if (barraInimigo >= 100)
            {
                inimigoAtaca = true;
                barraInimigo -= 100;
            }

            if (knightAtaca)
            {
                bool parry = rnd.Next(0, 100) < (int)parryInimigo;
                if (parry)
                {
                    Console.WriteLine($"{knight.Nome} ataca, mas {inimigo.Nome} DEFENDEU o golpe!");
                }
                else
                {
                    hpInimigo -= danoKnight;
                    Console.WriteLine($"{knight.Nome} ataca com {armaKnight.Nome} e causa {danoKnight} de dano em {inimigo.Nome}!");
                }
                Thread.Sleep(1000);
                if (hpInimigo <= 0)
                {
                    Console.WriteLine($"\n{inimigo.Nome} foi derrotado!");
                    DarXpPorInimigoDerrotado(knight.Id, inimigo.Nivel);
                    UparNivelKnight(usuarioLogado);
                    break;
                }
            }

            if (inimigoAtaca)
            {
                bool parry = rnd.Next(0, 100) < (int)parryKnight;
                if (parry)
                {
                    Console.WriteLine($"{inimigo.Nome} ataca, mas {knight.Nome} DEFENDEU o golpe!");
                }
                else
                {
                    hpKnight -= danoInimigo;
                    Console.WriteLine($"{inimigo.Nome} ataca com {armaInimigo.Nome} e causa {danoInimigo} de dano em {knight.Nome}!");
                }
                Thread.Sleep(1000);
                if (hpKnight <= 0)
                {
                    Console.WriteLine($"\n{knight.Nome} foi derrotado!");
                    break;
                }
            }

            // Exibe status e rodada apenas se alguém atacou
            if (knightAtaca || inimigoAtaca)
            {
                Console.WriteLine($"\n--- Rodada {turno} ---");
                Console.WriteLine("\n-------------------------------");
                Console.WriteLine($"Status após a rodada {turno}:");
                Console.WriteLine($"{knight.Nome} - HP: {Math.Max(hpKnight, 0)}");
                Console.WriteLine($"{inimigo.Nome} - HP: {Math.Max(hpInimigo, 0)}");
                Console.WriteLine("-------------------------------");
                turno++;
                Console.WriteLine("Pressione ENTER para continuar...");
                Console.ReadLine();
            }
        }

        Console.WriteLine("\n===================================");
        Console.WriteLine("           FIM DA BATALHA          ");
        Console.WriteLine("===================================");
    }

    public static void BatalhaKnightVsKnight(User usuarioLogado)
    {
        var knightService = new KnightService();
        var knights = knightService.BuscarTodos();
        var armaService = new ArmaService();
        var userService = new UserService();

        // Só mostra todos se for admin
        if (usuarioLogado.Nome.ToLower() != "admin")
            knights = knights.Where(k => k.UserId == usuarioLogado.Id).ToList();

        // Removido o bloqueio de pelo menos 2 knights

        // Seleção do primeiro knight (apenas do usuário logado, exceto admin)
        var knightsUsuario = knights;
        if (usuarioLogado.Nome.ToLower() != "admin")
            knightsUsuario = knights.Where(k => k.UserId == usuarioLogado.Id).ToList();

        if (knightsUsuario.Count < 1)
        {
            Console.WriteLine("Você não possui knights para batalhar.");
            return;
        }

        Console.WriteLine("\n--- Seus Knights ---");
        for (int i = 0; i < knightsUsuario.Count; i++)
        {
            var k = knightsUsuario[i];
            var arma = armaService.BuscarTodas().FirstOrDefault(a => a.Id == k.ArmasId);
            var usuario = userService.GetById(k.UserId);
            string nomeUsuario = usuario != null ? usuario.Nome : "Desconhecido";
            string armaInfo = arma != null
                ? $"Arma: {arma.Nome} | Dano: {arma.Dano} | Velocidade: {arma.Velocidade}"
                : "Arma: (não encontrada)";
            Console.WriteLine($"{i + 1} - Nome: {k.Nome} | HP: {k.Hp} | Nível: {k.Nivel} | User: {nomeUsuario} | {armaInfo}");
        }
        Console.Write("\nDigite o número do seu Knight: ");
        int idx1 = int.TryParse(Console.ReadLine(), out int k1) ? k1 - 1 : -1;
        if (idx1 < 0 || idx1 >= knightsUsuario.Count)
        {
            Console.WriteLine("Knight inválido.");
            return;
        }
        var knight1 = knightsUsuario[idx1];

        // Agora busque todos os knights do banco (exceto o escolhido)
        var todosKnights = new KnightService().BuscarTodos()
            .Where(k => k.Id != knight1.Id)
            .ToList();

        // Filtra knights de nível no máximo 1 acima ou abaixo
        var oponentesElegiveis = todosKnights
            .Where(k => k.Nivel >= knight1.Nivel - 1 && k.Nivel <= knight1.Nivel + 1)
            .OrderBy(x => Guid.NewGuid()) // embaralha
            .Take(5)
            .ToList();

        if (oponentesElegiveis.Count == 0)
        {
            Console.WriteLine("Nenhum oponente elegível encontrado para esse knight.");
            return;
        }

        Console.WriteLine("\n--- Oponentes Elegíveis ---");
        for (int i = 0; i < oponentesElegiveis.Count; i++)
        {
            var k = oponentesElegiveis[i];
            var arma = armaService.BuscarTodas().FirstOrDefault(a => a.Id == k.ArmasId);
            var usuario = userService.GetById(k.UserId);
            string nomeUsuario = usuario != null ? usuario.Nome : "Desconhecido";
            string armaInfo = arma != null
                ? $"Arma: {arma.Nome} | Dano: {arma.Dano} | Velocidade: {arma.Velocidade}"
                : "Arma: (não encontrada)";
            Console.WriteLine($"{i + 1} - Nome: {k.Nome} | HP: {k.Hp} | Nível: {k.Nivel} | User: {nomeUsuario} | {armaInfo}");
        }
        Console.Write("\nDigite o número do oponente: ");
        int idx2 = int.TryParse(Console.ReadLine(), out int k2) ? k2 - 1 : -1;
        if (idx2 < 0 || idx2 >= oponentesElegiveis.Count)
        {
            Console.WriteLine("Oponente inválido.");
            return;
        }
        var knight2 = oponentesElegiveis[idx2];

        // Busca atributos e armas
        var atributosRepo = new AtributosRepository();
        var atributos1 = atributosRepo.GetById(knight1.AtributosId);
        var atributos2 = atributosRepo.GetById(knight2.AtributosId);
        var armaRepo = new ArmaRepository();
        var arma1 = armaRepo.GetAll().FirstOrDefault(a => a.Id == knight1.ArmasId);
        var arma2 = armaRepo.GetAll().FirstOrDefault(a => a.Id == knight2.ArmasId);

        // Inicializa HPs
        int hp1 = knight1.Hp;
        int hp2 = knight2.Hp;

        // Cálculo de dano e velocidade ajustados
        int dano1 = (arma1?.Dano ?? 0) + (atributos1?.Forca ?? 0);
        int dano2 = (arma2?.Dano ?? 0) + (atributos2?.Forca ?? 0);

        int vel1 = (arma1?.Velocidade ?? 1) + (atributos1?.Destreza ?? 0);
        int vel2 = (arma2?.Velocidade ?? 1) + (atributos2?.Destreza ?? 0);

        decimal parry1 = knight1.Parry;
        decimal parry2 = knight2.Parry;

        int barra1 = 0;
        int barra2 = 0;
        int turno = 1;
        Random rnd = new Random();

        Console.Clear();
        Console.WriteLine("===================================");
        Console.WriteLine("      BATALHA ENTRE KNIGHTS        ");
        Console.WriteLine("===================================");
        Console.WriteLine($"\n{knight1.Nome} (HP: {hp1})");
        Console.WriteLine($"  Arma: {arma1?.Nome ?? "Nenhuma"} | Dano: {dano1} | Velocidade: {vel1}");
        Console.WriteLine($"  Parry: {parry1}%");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"{knight2.Nome} (HP: {hp2})");
        Console.WriteLine($"  Arma: {arma2?.Nome ?? "Nenhuma"} | Dano: {dano2} | Velocidade: {vel2}");
        Console.WriteLine($"  Parry: {parry2}%");
        Console.WriteLine("===================================\n");

        while (hp1 > 0 && hp2 > 0)
        {
            barra1 += vel1;
            barra2 += vel2;

            bool k1Ataca = false, k2Ataca = false;

            if (barra1 >= 100)
            {
                k1Ataca = true;
                barra1 -= 100;
            }
            if (barra2 >= 100)
            {
                k2Ataca = true;
                barra2 -= 100;
            }

            if (k1Ataca)
            {
                bool parry = rnd.Next(0, 100) < (int)parry2;
                if (parry)
                {
                    Console.WriteLine($"{knight1.Nome} ataca, mas {knight2.Nome} DEFENDEU o golpe!");
                }
                else
                {
                    hp2 -= dano1;
                    Console.WriteLine($"{knight1.Nome} ataca com {arma1?.Nome ?? "Nenhuma"} e causa {dano1} de dano em {knight2.Nome}!");
                }
                Thread.Sleep(1000);
                if (hp2 <= 0)
                {
                    Console.WriteLine($"\n{knight2.Nome} foi derrotado!");
                    DarXpPorInimigoDerrotado(knight1.Id, knight2.Nivel);
                    UparNivelKnight(usuarioLogado);
                    break;
                }
            }

            if (k2Ataca)
            {
                bool parry = rnd.Next(0, 100) < (int)parry1;
                if (parry)
                {
                    Console.WriteLine($"{knight2.Nome} ataca, mas {knight1.Nome} DEFENDEU o golpe!");
                }
                else
                {
                    hp1 -= dano2;
                    Console.WriteLine($"{knight2.Nome} ataca com {arma2?.Nome ?? "Nenhuma"} e causa {dano2} de dano em {knight1.Nome}!");
                }
                Thread.Sleep(1000);
                if (hp1 <= 0)
                {
                    Console.WriteLine($"\n{knight1.Nome} foi derrotado!");
                    DarXpPorInimigoDerrotado(knight2.Id, knight1.Nivel);
                    UparNivelKnight(usuarioLogado);
                    break;
                }
            }

            if (k1Ataca || k2Ataca)
            {
                Console.WriteLine($"\n--- Rodada {turno} ---");
                Console.WriteLine("\n-------------------------------");
                Console.WriteLine($"Status após a rodada {turno}:");
                Console.WriteLine($"{knight1.Nome} - HP: {Math.Max(hp1, 0)}");
                Console.WriteLine($"{knight2.Nome} - HP: {Math.Max(hp2, 0)}");
                Console.WriteLine("-------------------------------");
                turno++;
                Console.WriteLine("Pressione ENTER para continuar...");
                Console.ReadLine();
            }
        }

        Console.WriteLine("\n===================================");
        Console.WriteLine("           FIM DA BATALHA          ");
        Console.WriteLine("===================================");
    }

    // Dá XP ao knight vencedor por derrotar um inimigo ou knight
    public static void DarXpPorInimigoDerrotado(int knightId, int nivelInimigoDerrotado)
    {
        var knightService = new KnightService();
        var knight = knightService.GetById(knightId);
        if (knight == null)
        {
            Console.WriteLine("Knight não encontrado para adicionar XP.");
            return;
        }
        int xpGanho = 5 * nivelInimigoDerrotado;
        knight.Xp += xpGanho;
        knightService.AtualizarKnight(knight);
        Console.WriteLine($"{knight.Nome} ganhou {xpGanho} XP por derrotar um inimigo de nível {nivelInimigoDerrotado}!");
    }

    public static List<Inimigo> BuscarInimigosAleatoriosParaKnight(int knightNivel)
    {
        var inimigoService = new InimigoService();
        var todosInimigos = inimigoService.BuscarTodos();

        // Filtra inimigos com nível entre (knightNivel - 1) e (knightNivel + 1)
        var elegiveis = todosInimigos
            .Where(i => i.Nivel >= knightNivel - 1 && i.Nivel <= knightNivel + 1)
            .ToList();

        // Embaralha e pega até 5
        var rnd = new Random();
        var selecionados = elegiveis.OrderBy(x => rnd.Next()).Take(5).ToList();

        return selecionados;
    }

    public static void UparNivelKnight(User usuarioLogado)
    {
        var knightService = new KnightService();
        var knights = knightService.BuscarTodos();

        // Só permite upar nível se houver pelo menos 2 knights
        if (usuarioLogado.Nome.ToLower() != "admin" && knights.Count(k => k.UserId == usuarioLogado.Id) < 2)
        {
            Console.WriteLine("É necessário ter pelo menos 2 knights para upar de nível.");
            return;
        }

        Console.WriteLine("\n--- Knights Disponíveis para Upar de Nível ---");
        for (int i = 0; i < knights.Count; i++)
        {
            var k = knights[i];
            if (k.UserId == usuarioLogado.Id || usuarioLogado.Nome.ToLower() == "admin")
            {
                Console.WriteLine($"{i + 1} - Nome: {k.Nome} | Nível: {k.Nivel} | HP: {k.Hp}");
            }
        }
        Console.Write("\nDigite o número do Knight que deseja upar de nível: ");
        string entrada = Console.ReadLine() ?? "";
        if (int.TryParse(entrada, out int idx) && idx > 0 && idx <= knights.Count)
        {
            var knight = knights[idx - 1];

            // Aumento de nível
            knight.Nivel++;
            knight.Xp = 0; // Zera a experiência ao upar de nível

            // Permite distribuir 5 pontos de atributo ao upar
            var atributosRepo = new AtributosRepository();
            var atributos = atributosRepo.GetById(knight.AtributosId);
            if (atributos != null)
            {
                int pontosRestantes = 5;
                while (pontosRestantes > 0)
                {
                    Console.WriteLine($"\nVocê ganhou {pontosRestantes} ponto(s) de atributo! Escolha onde deseja adicionar:");
                    Console.WriteLine($"Força atual: {atributos.Forca}, Destreza atual: {atributos.Destreza}, Constituição atual: {atributos.Constituicao}");
                    Console.WriteLine("Digite o atributo para adicionar um ponto (forca, destreza, constituicao):");
                    string atributoEscolhido = Console.ReadLine()?.Trim().ToLower() ?? "";
                    switch (atributoEscolhido)
                    {
                        case "forca":
                            atributos.Forca++;
                            pontosRestantes--;
                            break;
                        case "destreza":
                            atributos.Destreza++;
                            pontosRestantes--;
                            break;
                        case "constituicao":
                            atributos.Constituicao++;
                            pontosRestantes--;
                            break;
                        default:
                            Console.WriteLine("Atributo inválido. Nenhum ponto distribuído.");
                            break;
                    }
                }
                atributosRepo.AtualizarAtributos(atributos);
                Console.WriteLine($"Novos atributos: Força={atributos.Forca}, Destreza={atributos.Destreza}, Constituição={atributos.Constituicao}");
            }

            knightService.AtualizarKnight(knight);
            Console.WriteLine($"Knight {knight.Nome} upado para o nível {knight.Nivel} com sucesso!");
        }
        else
        {
            Console.WriteLine("Número inválido.");
        }
    }
}
