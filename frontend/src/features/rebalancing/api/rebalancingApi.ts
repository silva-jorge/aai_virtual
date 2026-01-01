import axios from 'axios';
import type { Recommendation, RequestRecommendationsRequest } from '../types/rebalancing';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5032/api';

export const rebalancingApi = {
  async getRecommendations(): Promise<Recommendation[]> {
    const response = await axios.get<Recommendation[]>(`${API_BASE_URL}/rebalancing/recommendations`);
    return response.data;
  },

  async requestRecommendations(data: RequestRecommendationsRequest): Promise<Recommendation[]> {
    const response = await axios.post<Recommendation[]>(
      `${API_BASE_URL}/rebalancing/recommendations`,
      data
    );
    return response.data;
  },
};
