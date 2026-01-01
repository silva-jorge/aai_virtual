import axios from 'axios';
import type { PortfolioSummary, AllocationBreakdown, PerformanceMetrics } from '../types/portfolio';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5032/api';

export const portfolioApi = {
  async getSummary(): Promise<PortfolioSummary> {
    const response = await axios.get<PortfolioSummary>(`${API_BASE_URL}/portfolio/summary`);
    return response.data;
  },

  async getAllocation(): Promise<AllocationBreakdown> {
    const response = await axios.get<AllocationBreakdown>(`${API_BASE_URL}/portfolio/allocation`);
    return response.data;
  },

  async getPerformance(): Promise<PerformanceMetrics> {
    const response = await axios.get<PerformanceMetrics>(`${API_BASE_URL}/portfolio/performance`);
    return response.data;
  },
};
