import axios from 'axios';
import type { UserProfile, UpdateRiskProfileRequest, UpdateThresholdsRequest } from '../types/profile';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5032/api';

export const profileApi = {
  async getProfile(): Promise<UserProfile> {
    console.log('[ProfileAPI] GET /profile');
    const response = await axios.get<UserProfile>(`${API_BASE_URL}/profile`);
    console.log('[ProfileAPI] GET /profile response:', response.data);
    return response.data;
  },

  async updateRiskProfile(data: UpdateRiskProfileRequest): Promise<UserProfile> {
    console.log('[ProfileAPI] PUT /profile/risk-profile', data);
    const response = await axios.put<UserProfile>(
      `${API_BASE_URL}/profile/risk-profile`,
      data
    );
    console.log('[ProfileAPI] PUT /profile/risk-profile response:', response.data);
    return response.data;
  },

  async updateThresholds(data: UpdateThresholdsRequest): Promise<UserProfile> {
    console.log('[ProfileAPI] PUT /profile/thresholds', data);
    const response = await axios.put<UserProfile>(
      `${API_BASE_URL}/profile/thresholds`,
      data
    );
    console.log('[ProfileAPI] PUT /profile/thresholds response:', response.data);
    return response.data;
  },
};
