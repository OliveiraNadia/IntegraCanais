namespace IntegraCanais.Application.Interfaces;

public interface IClassificadorService
{
    Task<List<string>>Classificar(string texto);
}