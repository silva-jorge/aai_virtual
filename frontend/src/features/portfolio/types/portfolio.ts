// Portfolio types
export interface PortfolioSummary {
  id: string;
  name: string;
  description?: string;
  currency: string;
  totalInvested: number;
  currentValue: number;
  totalGainLoss: number;
  totalGainLossPercent: number;
  positionsCount: number;
  assetsCount: number;
  createdAt: string;
  updatedAt: string;
}

export interface Position {
  id: string;
  assetId: string;
  ticker: string;
  assetName: string;
  assetClass: string;
  quantity: number;
  averageCost: number;
  currentPrice: number;
  totalInvested: number;
  currentValue: number;
  allocationPercent: number;
  gainLoss: number;
  gainLossPercent: number;
}

export interface AssetClassAllocation {
  assetClass: string;
  value: number;
  percent: number;
  positionsCount: number;
}

export interface AllocationBreakdown {
  byAssetClass: AssetClassAllocation[];
  topPositions: Position[];
}

export interface PerformanceHistory {
  date: string;
  value: number;
  returnPercent: number;
}

export interface PerformanceMetrics {
  totalReturn: number;
  totalReturnPercent: number;
  dayChange: number;
  dayChangePercent: number;
  monthReturn: number;
  monthReturnPercent: number;
  yearReturn: number;
  yearReturnPercent: number;
  history: PerformanceHistory[];
}
