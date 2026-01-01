namespace AAI.Domain.Enums;

/// <summary>
/// Classe de ativo para categorização de investimentos
/// </summary>
public enum AssetClass
{
    AcoesBrasil = 1,        // Ações B3
    ETF = 2,                // Exchange Traded Funds
    FII = 3,                // Fundos Imobiliários
    RendaFixa = 4,          // Títulos de renda fixa
    AcoesInternacional = 5, // Ações internacionais
    Criptomoedas = 6,       // Criptomoedas
    Outros = 99             // Outros ativos
}
