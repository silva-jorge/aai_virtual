export interface UserProfile {
  id: string;
  riskProfile: 'conservador' | 'moderado' | 'agressivo';
  investmentGoal?: string;
  volatilityTolerance: number;
  timeHorizonMonths: number;
  rebalanceThresholdPercent: number;
  targetAllocationJson: string;
  createdAt: string;
  updatedAt: string;
}

export interface UpdateRiskProfileRequest {
  riskProfile: string;
  investmentGoal?: string;
  volatilityTolerance: number;
  timeHorizonMonths: number;
}

export interface UpdateThresholdsRequest {
  rebalanceThresholdPercent: number;
  targetAllocationJson: string;
}
