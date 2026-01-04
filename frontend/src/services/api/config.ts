/**
 * API Configuration
 * Centralized configuration for all API endpoints
 */

// Base URL from environment or default
export const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5032';

// API version prefix
export const API_PREFIX = '/api';

// Full API base path
export const API_PATH = `${API_BASE_URL}${API_PREFIX}`;

/**
 * API Endpoints
 * All API endpoints are defined here to avoid hardcoding URLs throughout the application
 */
export const API_ENDPOINTS = {
  // Auth endpoints
  auth: {
    login: `${API_PATH}/Auth/login`,
    register: `${API_PATH}/Auth/register`,
    verify: `${API_PATH}/Auth/verify`,
    checkUser: `${API_PATH}/Auth/check-user`,
  },
  
  // Portfolio endpoints
  portfolio: {
    summary: `${API_PATH}/Portfolio/summary`,
    allocation: `${API_PATH}/Portfolio/allocation`,
    performance: `${API_PATH}/Portfolio/performance`,
  },
  
  // Profile endpoints
  profile: {
    get: `${API_PATH}/Profile`,
    updateRiskProfile: `${API_PATH}/Profile/risk-profile`,
    updateThresholds: `${API_PATH}/Profile/thresholds`,
    exportData: `${API_PATH}/Profile/export`,
  },
  
  // Rebalancing endpoints
  rebalancing: {
    getRecommendations: `${API_PATH}/Rebalancing/recommendations`,
    requestRecommendations: `${API_PATH}/Rebalancing/request`,
  },
} as const;

/**
 * Helper function to build API URLs dynamically
 * @param endpoint - The endpoint path (e.g., 'Auth/login')
 * @returns Full API URL
 */
export const buildApiUrl = (endpoint: string): string => {
  return `${API_PATH}/${endpoint}`;
};
