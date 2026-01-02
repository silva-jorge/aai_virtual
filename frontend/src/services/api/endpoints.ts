/**
 * API Endpoints
 * Centralized API endpoint definitions
 */

const API_VERSION = 'v1';

/**
 * Authentication Endpoints
 */
export const AUTH_ENDPOINTS = {
  LOGIN: `/api/${API_VERSION}/auth/login`,
  SETUP_PASSWORD: `/api/${API_VERSION}/auth/setup-password`,
  REFRESH_TOKEN: `/api/${API_VERSION}/auth/refresh-token`,
  CHANGE_PASSWORD: `/api/${API_VERSION}/auth/change-password`,
  LOGOUT: `/api/${API_VERSION}/auth/logout`,
} as const;

/**
 * User Profile Endpoints
 */
export const PROFILE_ENDPOINTS = {
  GET_PROFILE: `/api/${API_VERSION}/profile`,
  UPDATE_PROFILE: `/api/${API_VERSION}/profile`,
  UPDATE_RISK_PROFILE: `/api/${API_VERSION}/profile/risk-profile`,
  UPDATE_THRESHOLDS: `/api/${API_VERSION}/profile/thresholds`,
  EXPORT_DATA: `/api/${API_VERSION}/profile/export`,
  IMPORT_DATA: `/api/${API_VERSION}/profile/import`,
  DELETE_ALL_DATA: `/api/${API_VERSION}/profile/delete-all`,
} as const;

/**
 * Portfolio Endpoints
 */
export const PORTFOLIO_ENDPOINTS = {
  GET_SUMMARY: `/api/${API_VERSION}/portfolio/summary`,
  GET_ALLOCATION: `/api/${API_VERSION}/portfolio/allocation`,
  GET_PERFORMANCE: `/api/${API_VERSION}/portfolio/performance`,
  GET_POSITIONS: `/api/${API_VERSION}/positions`,
  GET_POSITION: (id: string) => `/api/${API_VERSION}/positions/${id}`,
  CREATE_POSITION: `/api/${API_VERSION}/positions`,
  UPDATE_POSITION: (id: string) => `/api/${API_VERSION}/positions/${id}`,
  DELETE_POSITION: (id: string) => `/api/${API_VERSION}/positions/${id}`,
  IMPORT_TRANSACTIONS: `/api/${API_VERSION}/positions/import`,
} as const;

/**
 * Asset Endpoints
 */
export const ASSET_ENDPOINTS = {
  GET_ASSETS: `/api/${API_VERSION}/assets`,
  GET_ASSET: (id: string) => `/api/${API_VERSION}/assets/${id}`,
  SEARCH_ASSETS: `/api/${API_VERSION}/assets/search`,
  GET_ASSET_PRICE: (symbol: string) => `/api/${API_VERSION}/assets/${symbol}/price`,
  GET_ASSET_HISTORY: (symbol: string) => `/api/${API_VERSION}/assets/${symbol}/history`,
} as const;

/**
 * Transaction Endpoints
 */
export const TRANSACTION_ENDPOINTS = {
  GET_TRANSACTIONS: `/api/${API_VERSION}/transactions`,
  GET_TRANSACTION: (id: string) => `/api/${API_VERSION}/transactions/${id}`,
  GET_TRANSACTION_HISTORY: `/api/${API_VERSION}/transactions/history`,
} as const;

/**
 * Rebalancing Endpoints
 */
export const REBALANCING_ENDPOINTS = {
  GET_RECOMMENDATIONS: `/api/${API_VERSION}/rebalancing/recommendations`,
  REQUEST_RECOMMENDATIONS: `/api/${API_VERSION}/rebalancing/request`,
  APPLY_RECOMMENDATION: (id: string) => `/api/${API_VERSION}/rebalancing/recommendations/${id}/apply`,
  REJECT_RECOMMENDATION: (id: string) => `/api/${API_VERSION}/rebalancing/recommendations/${id}/reject`,
  SIMULATE_REBALANCING: `/api/${API_VERSION}/rebalancing/simulate`,
} as const;

/**
 * News Endpoints
 */
export const NEWS_ENDPOINTS = {
  GET_FEED: `/api/${API_VERSION}/news/feed`,
  GET_NEWS_FOR_ASSET: (symbol: string) => `/api/${API_VERSION}/news/asset/${symbol}`,
  MARK_AS_READ: (id: string) => `/api/${API_VERSION}/news/${id}/read`,
} as const;

/**
 * Alert Endpoints
 */
export const ALERT_ENDPOINTS = {
  GET_ALERTS: `/api/${API_VERSION}/alerts`,
  GET_ALERT_HISTORY: `/api/${API_VERSION}/alerts/history`,
  CREATE_ALERT: `/api/${API_VERSION}/alerts`,
  UPDATE_ALERT: (id: string) => `/api/${API_VERSION}/alerts/${id}`,
  DELETE_ALERT: (id: string) => `/api/${API_VERSION}/alerts/${id}`,
} as const;

/**
 * Analytics Endpoints
 */
export const ANALYTICS_ENDPOINTS = {
  GET_HISTORICAL_PERFORMANCE: `/api/${API_VERSION}/analytics/performance`,
  GET_RISK_METRICS: `/api/${API_VERSION}/analytics/risk-metrics`,
  GET_BENCHMARK_COMPARISON: `/api/${API_VERSION}/analytics/benchmark-comparison`,
} as const;

/**
 * Health Check Endpoint
 */
export const HEALTH_ENDPOINT = '/health';

/**
 * All endpoints combined
 */
export const ENDPOINTS = {
  AUTH: AUTH_ENDPOINTS,
  PROFILE: PROFILE_ENDPOINTS,
  PORTFOLIO: PORTFOLIO_ENDPOINTS,
  ASSET: ASSET_ENDPOINTS,
  TRANSACTION: TRANSACTION_ENDPOINTS,
  REBALANCING: REBALANCING_ENDPOINTS,
  NEWS: NEWS_ENDPOINTS,
  ALERT: ALERT_ENDPOINTS,
  ANALYTICS: ANALYTICS_ENDPOINTS,
  HEALTH: HEALTH_ENDPOINT,
} as const;
