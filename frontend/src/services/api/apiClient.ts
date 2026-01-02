/**
 * API Client
 * Axios instance with interceptors for authentication, error handling, and logging
 */

import axios, { AxiosError, AxiosInstance, InternalAxiosRequestConfig, AxiosResponse } from 'axios';

const API_URL = import.meta.env.VITE_API_URL || 'http://localhost:5032';
const API_TIMEOUT = parseInt(import.meta.env.VITE_API_TIMEOUT || '30000', 10);

/**
 * Create axios instance with default configuration
 */
const apiClient: AxiosInstance = axios.create({
  baseURL: API_URL,
  timeout: API_TIMEOUT,
  headers: {
    'Content-Type': 'application/json',
  },
});

/**
 * Request interceptor
 * Adds authentication token to requests
 */
apiClient.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => {
    // Get token from localStorage
    const token = localStorage.getItem('aai_auth_token');
    
    if (token && config.headers) {
      config.headers.Authorization = `Bearer ${token}`;
    }

    // Log request in development
    if (import.meta.env.DEV) {
      console.log(`[API Request] ${config.method?.toUpperCase()} ${config.url}`, {
        params: config.params,
        data: config.data,
      });
    }

    return config;
  },
  (error: AxiosError) => {
    console.error('[API Request Error]', error);
    return Promise.reject(error);
  }
);

/**
 * Response interceptor
 * Handles token refresh and error responses
 */
apiClient.interceptors.response.use(
  (response: AxiosResponse) => {
    // Log response in development
    if (import.meta.env.DEV) {
      console.log(`[API Response] ${response.config.method?.toUpperCase()} ${response.config.url}`, {
        status: response.status,
        data: response.data,
      });
    }

    return response;
  },
  async (error: AxiosError) => {
    const originalRequest = error.config as InternalAxiosRequestConfig & { _retry?: boolean };

    // Handle 401 Unauthorized - Token expired
    if (error.response?.status === 401 && !originalRequest._retry) {
      originalRequest._retry = true;

      try {
        // Try to refresh token
        const refreshToken = localStorage.getItem('aai_refresh_token');
        
        if (refreshToken) {
          const response = await axios.post(`${API_URL}/api/v1/auth/refresh-token`, {
            refreshToken,
          });

          const { token, refreshToken: newRefreshToken } = response.data;

          // Update tokens in localStorage
          localStorage.setItem('aai_auth_token', token);
          localStorage.setItem('aai_refresh_token', newRefreshToken);

          // Retry original request with new token
          if (originalRequest.headers) {
            originalRequest.headers.Authorization = `Bearer ${token}`;
          }

          return apiClient(originalRequest);
        }
      } catch (refreshError) {
        // Refresh failed - clear tokens and redirect to login
        localStorage.removeItem('aai_auth_token');
        localStorage.removeItem('aai_refresh_token');
        localStorage.removeItem('aai_user');
        
        // Dispatch custom event for auth error
        window.dispatchEvent(new CustomEvent('auth:error', { detail: 'Session expired' }));
        
        return Promise.reject(refreshError);
      }
    }

    // Handle other errors
    const errorMessage = getErrorMessage(error);
    
    console.error('[API Response Error]', {
      url: error.config?.url,
      method: error.config?.method,
      status: error.response?.status,
      message: errorMessage,
      data: error.response?.data,
    });

    // Dispatch custom event for API errors
    window.dispatchEvent(new CustomEvent('api:error', { 
      detail: { 
        message: errorMessage,
        status: error.response?.status,
        url: error.config?.url,
      } 
    }));

    return Promise.reject(error);
  }
);

/**
 * Extract user-friendly error message from error response
 */
function getErrorMessage(error: AxiosError): string {
  if (error.response?.data) {
    const data = error.response.data as any;
    
    // API returned error message
    if (data.message) {
      return data.message;
    }
    
    // Validation errors
    if (data.errors) {
      const errors = Object.values(data.errors).flat();
      return errors.join(', ');
    }
    
    // Generic error with title
    if (data.title) {
      return data.title;
    }
  }

  // Network errors
  if (error.code === 'ECONNABORTED') {
    return 'A requisição excedeu o tempo limite. Tente novamente.';
  }
  
  if (error.code === 'ERR_NETWORK') {
    return 'Erro de conexão. Verifique sua internet e tente novamente.';
  }

  // Default error message
  return error.message || 'Ocorreu um erro inesperado. Tente novamente.';
}

/**
 * Export apiClient instance
 */
export default apiClient;

/**
 * Export helper functions for common HTTP methods
 */
export const api = {
  get: <T = any>(url: string, config = {}) => 
    apiClient.get<T>(url, config).then(res => res.data),
  
  post: <T = any>(url: string, data?: any, config = {}) => 
    apiClient.post<T>(url, data, config).then(res => res.data),
  
  put: <T = any>(url: string, data?: any, config = {}) => 
    apiClient.put<T>(url, data, config).then(res => res.data),
  
  patch: <T = any>(url: string, data?: any, config = {}) => 
    apiClient.patch<T>(url, data, config).then(res => res.data),
  
  delete: <T = any>(url: string, config = {}) => 
    apiClient.delete<T>(url, config).then(res => res.data),
};
