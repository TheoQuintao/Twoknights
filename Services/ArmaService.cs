public class ArmaService
{
    private readonly ArmaRepository _armaRepository = new ArmaRepository();

    public void AdicionarArma(string nome, int dano, int velocidade)
    {
        var arma = new Arma
        {
            Nome = nome,
            Dano = dano,
            Velocidade = velocidade
        };
        _armaRepository.Add(arma);
    }
}