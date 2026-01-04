import axios from 'axios';
import type { UserProfile, UpdateRiskProfileRequest, UpdateThresholdsRequest } from '../types/profile';
import { API_ENDPOINTS } from '../../../services/api/config';

export const profileApi = {
  async getProfile(): Promise<UserProfile> {
    const response = await axios.get<UserProfile>(API_ENDPOINTS.profile.get);
    return response.data;
  },

  async updateRiskProfile(data: UpdateRiskProfileRequest): Promise<UserProfile> {
    const response = await axios.put<UserProfile>(
      API_ENDPOINTS.profile.updateRiskProfile,
      data
    );
    return response.data;
  },

  async updateThresholds(data: UpdateThresholdsRequest): Promise<UserProfile> {
    const response = await axios.put<UserProfile>(
      API_ENDPOINTS.profile.updateThresholds,
      data
    );
    return response.data;
  },
};
