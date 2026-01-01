using AAI.Domain.Common;

namespace AAI.Domain.Entities;

/// <summary>
/// Representa o portfólio de investimentos do usuário.
/// </summary>
public class Portfolio : BaseEntity
{
    /// <summary>
    /// Referência ao UserProfile (relação 1:1).
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Nome do portfólio.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Descrição do portfólio (opcional).
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Moeda base do portfólio (ISO 4217: BRL, USD, etc).
    /// </summary>
    public string Currency { get; set; } = "BRL";

    // Navigation properties
    /// <summary>
    /// Usuário dono do portfólio.
    /// </summary>
    public UserProfile? User { get; set; }

    /// <summary>
    /// Posições no portfólio.
    /// </summary>
    public ICollection<Position> Positions { get; set; } = new List<Position>();
}
