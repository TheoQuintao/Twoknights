public class AtributosService
{
    private readonly AtributosRepository _atributosRepository = new AtributosRepository();

    public int AdicionarAtributosERetornarId(int forca, int destreza, int constituicao)
    {
        var atributos = new Atributos
        {
            Forca = forca,
            Destreza = destreza,
            Constituicao = constituicao
        };
        return _atributosRepository.AddAndReturnId(atributos);
    }
}