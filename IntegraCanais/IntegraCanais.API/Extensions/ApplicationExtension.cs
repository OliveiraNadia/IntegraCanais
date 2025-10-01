using IntegraCanais.Application.Interfaces;
using IntegraCanais.Application.Services;

namespace IntegraCanais.API.Extensions;

public static class ApplicationExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IClassificadorService, ClassificadorService>();
    }
}