/**
 * DTO for Position data from the API
 */
export interface PositionDTO {
  id: string;
  assetTicker: string;
  assetName: string;
  assetClass: string;
  quantity: number;
  currentPrice: number;
  currentValue: number;
  costBasis: number;
  gainLoss: number;
  priceChangePercent?: number;
  allocationPercent?: number;
  portfolioId: string;
}

/**
 * DTO for Portfolio data from the API
 */
export interface PortfolioDTO {
  id: string;
  userId: string;
  name: string;
  totalValue: number;
  totalCost: number;
  totalGainLoss: number;
  totalGainLossPercent: number;
  positions: PositionDTO[];
}

/**
 * DTO for creating a new position
 */
export interface CreatePositionDTO {
  assetTicker: string;
  assetName: string;
  assetClass: string;
  quantity: number;
  currentPrice: number;
  costBasis: number;
  portfolioId: string;
}

/**
 * DTO for updating a position
 */
export interface UpdatePositionDTO {
  assetTicker?: string;
  assetName?: string;
  assetClass?: string;
  quantity?: number;
  currentPrice?: number;
  costBasis?: number;
}
