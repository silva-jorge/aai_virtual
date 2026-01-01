namespace AAI.Domain.Enums;

/// <summary>
/// Tipo de alerta de mercado
/// </summary>
public enum AlertType
{
    VariacaoPreco = 1,          // Variação significativa no preço
    FatoRelevante = 2,          // Fato relevante da empresa
    DivulgacaoBalanco = 3,      // Divulgação de balanço/DRE
    IndicadorMacro = 4,         // Indicador macroeconômico
    NoticiaMercado = 5,         // Notícia relevante
    ThresholdAlocacao = 6       // Threshold de alocação atingido
}
