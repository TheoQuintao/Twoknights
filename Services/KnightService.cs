public class KnightService
{
    private readonly KnightRepository _knightRepository = new KnightRepository();

    public void AdicionarKnight(string nome, int hp, int xp, int nivel, decimal parry, int userId, int armasId, int atributosId)
    {
        var knight = new Knight
        {
            Nome = nome,
            Hp = hp,
            Xp = xp,
            Nivel = nivel,
            Parry = parry,
            UserId = userId,
            ArmasId = armasId,
            AtributosId = atributosId
        };
        _knightRepository.Add(knight);
    }

    public List<Knight> BuscarTodos()
    {
        return _knightRepository.GetAll();
    }

    public void ApagarKnight(int id)
    {
        _knightRepository.Delete(id);
    }

    public Knight? GetById(int id)
    {
        return _knightRepository.GetById(id);
    }

    public void AtualizarKnight(Knight knight)
    {
        _knightRepository.AtualizarKnight(knight);
    }
}