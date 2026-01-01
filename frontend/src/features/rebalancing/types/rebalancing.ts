export interface Recommendation {
  id: string;
  portfolioId: string;
  actionType: string;
  ticker?: string;
  quantity?: number;
  estimatedValue?: number;
  title: string;
  description: string;
  rationale: string;
  impactJson?: string;
  priority: string;
  status: string;
  appliedAt?: string;
  rejectedAt?: string;
  rejectionReason?: string;
  expiresAt?: string;
  createdAt: string;
}

export interface RequestRecommendationsRequest {
  forceRegenerate: boolean;
}
