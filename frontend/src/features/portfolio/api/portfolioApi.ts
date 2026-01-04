import axios from 'axios';
import type { PortfolioSummary, AllocationBreakdown, PerformanceMetrics, PositionDTO, CreatePositionDTO, UpdatePositionDTO } from '../types/portfolio';
import { API_ENDPOINTS, buildApiUrl } from '../../../services/api/config';

export const portfolioApi = {
  async getSummary(): Promise<PortfolioSummary> {
    const response = await axios.get<PortfolioSummary>(API_ENDPOINTS.portfolio.summary);
    return response.data;
  },

  async getAllocation(): Promise<AllocationBreakdown> {
    const response = await axios.get<AllocationBreakdown>(API_ENDPOINTS.portfolio.allocation);
    return response.data;
  },

  async getPerformance(): Promise<PerformanceMetrics> {
    const response = await axios.get<PerformanceMetrics>(API_ENDPOINTS.portfolio.performance);
    return response.data;
  },

  async getPositions(portfolioId: string): Promise<PositionDTO[]> {
    const response = await axios.get<PositionDTO[]>(buildApiUrl(`Portfolio/${portfolioId}/positions`));
    return response.data;
  },

  async createPosition(position: CreatePositionDTO): Promise<PositionDTO> {
    const response = await axios.post<PositionDTO>(buildApiUrl('Portfolio/positions'), position);
    return response.data;
  },

  async updatePosition(id: string, position: UpdatePositionDTO): Promise<PositionDTO> {
    const response = await axios.put<PositionDTO>(buildApiUrl(`Portfolio/positions/${id}`), position);
    return response.data;
  },

  async deletePosition(id: string): Promise<void> {
    await axios.delete(buildApiUrl(`Portfolio/positions/${id}`));
  },

  async getPosition(id: string): Promise<PositionDTO> {
    const response = await axios.get<PositionDTO>(buildApiUrl(`Portfolio/positions/${id}`));
    return response.data;
  },
};
