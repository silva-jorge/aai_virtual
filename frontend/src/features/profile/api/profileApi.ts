import axios from 'axios';
import type { UserProfile, UpdateRiskProfileRequest, UpdateThresholdsRequest } from '../types/profile';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5032/api';

export const profileApi = {
  async getProfile(): Promise<UserProfile> {
    const response = await axios.get<UserProfile>(`${API_BASE_URL}/profile`);
    return response.data;
  },

  async updateRiskProfile(data: UpdateRiskProfileRequest): Promise<UserProfile> {
    const response = await axios.put<UserProfile>(
      `${API_BASE_URL}/profile/risk-profile`,
      data
    );
    return response.data;
  },

  async updateThresholds(data: UpdateThresholdsRequest): Promise<UserProfile> {
    const response = await axios.put<UserProfile>(
      `${API_BASE_URL}/profile/thresholds`,
      data
    );
    return response.data;
  },
};
