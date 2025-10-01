using AutoFixture;
using IntegraCanais.Application.Interfaces;
using IntegraCanais.Application.Services;
using NSubstitute;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using IntegraCanais.Application.Services;

namespace IntegraCanais.Services.Tests;

public class ClassificadorServiceTest
{
    private readonly ClassificadorService _service;

    public ClassificadorServiceTest()
    {
        _service = new ClassificadorService();
    }
    
    [Theory]
    [InlineData("Quero crédito imobiliário para comprar uma casa", new[] { "imobiliário" })]
    [InlineData("Preciso de socorro e resgate urgente", new[] { "seguros" })]
    [InlineData("Estou recebendo cobrança indevida", new[] { "cobrança" })]
    [InlineData("Não consigo acessar o app, está com erro", new[] { "acesso", "aplicativo" })]
    [InlineData("Recebi uma notificação que não reconheço, pode ser fraude", new[] { "fraude" })]
    [InlineData("Texto irrelevante sem nenhuma palavra-chave", new string[0])]
    public async Task DeveClassificarTextoCorretamente(string texto, string[] categoriasEsperadas)
    {
        // Act
        var resultado = await _service.Classificar(texto);

        // Assert
        Assert.NotNull(resultado);
        Assert.Equal(
            new HashSet<string>(categoriasEsperadas),
            new HashSet<string>(resultado)
        );
    }

    [Fact]
    public async Task DeveRetornarListaVaziaParaTextoVazio()
    {
        var resultado = await _service.Classificar("");
        Assert.NotNull(resultado);
        Assert.Empty(resultado);
    }

    [Fact]
    public async Task DeveIgnorarMaiusculasEMinusculas()
    {
        var resultado = await _service.Classificar("FATURA indeVIDO");
        Assert.Contains("cobrança", resultado);
    }

    [Fact]
    public async Task DeveRetornarTodasCategoriasPresentesNoTexto()
    {
        var texto = "Quero acessar o aplicativo para ver minha fatura com valor indevido";
        var resultado = await _service.Classificar(texto);

        Assert.Contains("acesso", resultado);
        Assert.Contains("aplicativo", resultado);
        Assert.Contains("cobrança", resultado);
        Assert.Contains("fraude", resultado);
        Assert.Equal(4, resultado.Count);
    }
}