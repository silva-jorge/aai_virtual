using AAI.Domain.Common;
using AAI.Domain.Enums;

namespace AAI.Domain.Entities;

/// <summary>
/// Configuração de alertas do usuário.
/// </summary>
public class Alert : BaseEntity
{
    /// <summary>
    /// Referência ao UserProfile.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Referência ao Asset (opcional - null = todos os ativos).
    /// </summary>
    public Guid? AssetId { get; set; }

    /// <summary>
    /// Tipo de alerta.
    /// </summary>
    public AlertType AlertType { get; set; }

    /// <summary>
    /// Condição do alerta em formato JSON.
    /// Exemplo: {"threshold": 5, "direction": "up"} para price_variation
    /// </summary>
    public string Condition { get; set; } = "{}";

    /// <summary>
    /// Indica se o alerta está ativo.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Data/hora do último disparo.
    /// </summary>
    public DateTime? LastTriggered { get; set; }

    // Navigation properties
    /// <summary>
    /// Usuário dono do alerta.
    /// </summary>
    public UserProfile? User { get; set; }

    /// <summary>
    /// Ativo relacionado ao alerta (opcional).
    /// </summary>
    public Asset? Asset { get; set; }

    /// <summary>
    /// Histórico de disparos deste alerta.
    /// </summary>
    public ICollection<AlertHistory> History { get; set; } = new List<AlertHistory>();
}
