using System.Collections.Generic;

public class InimigoService
{
    private readonly InimigoRepository _inimigoRepository = new InimigoRepository();

    public void AdicionarInimigo(string nome, int hp, int nivel, decimal parry, int armasId)
    {
        var inimigo = new Inimigo
        {
            Nome = nome,
            Hp = hp,
            Nivel = nivel,
            Parry = parry,
            ArmasId = armasId
        };
        _inimigoRepository.Add(inimigo);
    }

    public List<Inimigo> BuscarTodos()
    {
        return _inimigoRepository.GetAll();
    }

    public void ApagarInimigo(int id)
    {
        _inimigoRepository.Delete(id);
    }
}