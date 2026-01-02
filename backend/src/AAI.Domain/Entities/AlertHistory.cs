using AAI.Domain.Common;

namespace AAI.Domain.Entities;

/// <summary>
/// Histórico de alertas disparados.
/// </summary>
public class AlertHistory : BaseEntity
{
    /// <summary>
    /// Referência ao Alert.
    /// </summary>
    public Guid AlertId { get; set; }

    /// <summary>
    /// Título do alerta disparado.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Mensagem detalhada do alerta.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Data/hora em que o alerta foi disparado.
    /// </summary>
    public DateTime TriggeredAt { get; set; }

    /// <summary>
    /// Indica se o alerta foi lido pelo usuário.
    /// </summary>
    public bool IsRead { get; set; } = false;

    /// <summary>
    /// Tipo de entidade relacionada (Asset, MarketEvent, etc).
    /// </summary>
    public string? RelatedEntityType { get; set; }

    /// <summary>
    /// ID da entidade relacionada.
    /// </summary>
    public Guid? RelatedEntityId { get; set; }

    // Navigation properties
    /// <summary>
    /// Alert configuration ao qual este histórico pertence.
    /// </summary>
    public Alert? Alert { get; set; }
}
