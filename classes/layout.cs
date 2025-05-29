namespace Layout
{
    class Formatação
    {
        public static void Cor(ConsoleColor Letra, ConsoleColor Fundo)
        {
            Console.ForegroundColor = Letra;  
            Console.BackgroundColor = Fundo;
        }
        public static void ImprimirCabecalho(string txt)
        {
            int posicaoCentralizada = (Console.WindowWidth / 2) - (txt.Length / 2);
            Console.SetCursorPosition(posicaoCentralizada, Console.CursorTop);
            Console.Write("╔═");
            foreach (char i in txt)
            {
                Console.Write("═");
            }
            Console.WriteLine("═╗");
            Console.SetCursorPosition(posicaoCentralizada, Console.CursorTop);
            Console.WriteLine($"║ {txt} ║");
            Console.SetCursorPosition(posicaoCentralizada, Console.CursorTop);
            Console.Write("╚═");
            foreach (char i in txt)
            {
                Console.Write("═");
            }
            Console.WriteLine("═╝");
        }
    }
}