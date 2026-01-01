using AAI.Domain.Common;
using AAI.Domain.Enums;

namespace AAI.Domain.Entities;

/// <summary>
/// Representa as configurações e preferências do usuário investidor.
/// </summary>
public class UserProfile : BaseEntity
{
    /// <summary>
    /// Perfil de risco do investidor.
    /// </summary>
    public RiskProfile RiskProfile { get; set; }

    /// <summary>
    /// Objetivo de investimento do usuário (opcional).
    /// </summary>
    public string? InvestmentGoal { get; set; }

    /// <summary>
    /// Tolerância a volatilidade em percentual (0-100).
    /// </summary>
    public decimal VolatilityTolerance { get; set; }

    /// <summary>
    /// Horizonte temporal de investimento em meses (1-600).
    /// </summary>
    public int TimeHorizonMonths { get; set; }

    /// <summary>
    /// Threshold em percentual para sugerir rebalanceamento (1-50).
    /// </summary>
    public decimal RebalanceThresholdPercent { get; set; }

    /// <summary>
    /// Alocação-alvo por classe de ativo em formato JSON.
    /// Exemplo: {"acao": 60, "renda_fixa": 30, "fii": 10}
    /// </summary>
    public string TargetAllocationJson { get; set; } = "{}";

    /// <summary>
    /// Hash da senha do usuário (Argon2id).
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;

    /// <summary>
    /// Salt da senha (Base64 encoded).
    /// </summary>
    public string PasswordSalt { get; set; } = string.Empty;

    // Navigation properties
    /// <summary>
    /// Portfólio associado ao usuário (relação 1:1).
    /// </summary>
    public Portfolio? Portfolio { get; set; }
}
