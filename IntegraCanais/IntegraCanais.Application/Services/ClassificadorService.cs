using IntegraCanais.Application.Interfaces;

namespace IntegraCanais.Application.Services;

public class ClassificadorService: IClassificadorService
{
    public async Task<List<string>> Classificar(string texto)
    {
        var textoLower = texto.ToLower();
        var categoriasDetectadas = new HashSet<string>();

        foreach (var categoria in Categorias)
        {
            foreach (var palavraChave in categoria.Value)
            {
                if (textoLower.Contains(palavraChave.ToLower()))
                {
                    categoriasDetectadas.Add(categoria.Key);
                }
            }
        }

        return categoriasDetectadas.ToList();
    }
    private static readonly Dictionary<string, List<string>> Categorias = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
    {
        { "imobiliário", new List<string> { "credito imobiliario", "casa", "apartamento" } },
        { "seguros", new List<string> { "resgate", "capitalizacao", "socorro" } },
        { "cobrança", new List<string> { "fatura", "cobrança", "valor", "indevido" } },
        { "acesso", new List<string> { "acessar", "login", "senha" } },
        { "aplicativo", new List<string> { "app", "aplicativo", "travando", "erro" } },
        { "fraude", new List<string> { "fatura", "nao reconhece divida", "fraude" } }
    };
}