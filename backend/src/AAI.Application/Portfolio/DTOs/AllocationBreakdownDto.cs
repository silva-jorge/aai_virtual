namespace AAI.Application.Portfolio.DTOs;

/// <summary>
/// DTO para breakdown de alocação por classe de ativo
/// </summary>
public class AllocationBreakdownDto
{
    public List<AssetClassAllocationDto> ByAssetClass { get; set; } = new();
    public List<PositionDto> TopPositions { get; set; } = new();
}

/// <summary>
/// DTO para alocação por classe de ativo
/// </summary>
public class AssetClassAllocationDto
{
    public string AssetClass { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public decimal Percent { get; set; }
    public int PositionsCount { get; set; }
}
