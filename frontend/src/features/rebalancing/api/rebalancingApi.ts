import axios from 'axios';
import type { Recommendation, RequestRecommendationsRequest } from '../types/rebalancing';
import { API_ENDPOINTS } from '../../../services/api/config';

export const rebalancingApi = {
  async getRecommendations(): Promise<Recommendation[]> {
    const response = await axios.get<Recommendation[]>(API_ENDPOINTS.rebalancing.getRecommendations);
    return response.data;
  },

  async requestRecommendations(data: RequestRecommendationsRequest): Promise<Recommendation[]> {
    const response = await axios.post<Recommendation[]>(
      API_ENDPOINTS.rebalancing.requestRecommendations,
      data
    );
    return response.data;
  },
};
