namespace AAI.Domain.Enums;

/// <summary>
/// Tipo de transação realizada no portfólio
/// </summary>
public enum TransactionType
{
    Compra = 1,
    Venda = 2,
    Dividendo = 3,
    JurosCapitalProprio = 4,
    Aluguel = 5,            // Para FIIs
    Bonificacao = 6,
    Desdobramento = 7,
    Grupamento = 8
}
