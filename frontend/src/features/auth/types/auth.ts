export interface LoginRequest {
  pin: string;
}

export interface RegisterRequest {
  pin: string;
  riskProfile?: string;
  investmentGoal?: string;
  volatilityTolerance?: number;
  timeHorizonMonths?: number;
  rebalanceThresholdPercent?: number;
  targetAllocationJson?: string;
}

export interface AuthResponse {
  userId: string;
  token: string;
  success: boolean;
  message: string;
  userProfile?: UserProfile;
}

export interface UserProfile {
  id: string;
  riskProfile: string;
  investmentGoal?: string;
  volatilityTolerance: number;
  timeHorizonMonths: number;
  rebalanceThresholdPercent: number;
  targetAllocationJson: string;
}
