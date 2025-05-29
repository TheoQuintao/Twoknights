public class AtributosService
{
    private readonly AtributosRepository _atributosRepository = new AtributosRepository();

    public void AdicionarAtributos(int forca, int destreza, int constituicao)
    {
        var atributos = new Atributos
        {
            Forca = forca,
            Destreza = destreza,
            Constituicao = constituicao
        };
        _atributosRepository.Add(atributos);
    }
}