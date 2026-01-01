import axios, { AxiosError } from 'axios';
import type { LoginRequest, RegisterRequest, AuthResponse } from '../types/auth';

const API_BASE_URL = import.meta.env.VITE_API_URL || 'http://localhost:5032/api';

const TOKEN_KEY = 'aai_auth_token';
const USER_KEY = 'aai_user_profile';

export const authApi = {
  async login(request: LoginRequest): Promise<AuthResponse> {
    try {
      const response = await axios.post<AuthResponse>(
        `${API_BASE_URL}/auth/login`,
        request
      );
      
      if (response.data.success && response.data.token) {
        // Store token and user profile
        localStorage.setItem(TOKEN_KEY, response.data.token);
        if (response.data.userProfile) {
          localStorage.setItem(USER_KEY, JSON.stringify(response.data.userProfile));
        }
      }
      
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        const axiosError = error as AxiosError<{ message: string }>;
        return {
          userId: '',
          token: '',
          success: false,
          message: axiosError.response?.data?.message || 'Erro ao fazer login'
        };
      }
      return {
        userId: '',
        token: '',
        success: false,
        message: 'Erro inesperado ao fazer login'
      };
    }
  },

  async register(request: RegisterRequest): Promise<AuthResponse> {
    try {
      const response = await axios.post<AuthResponse>(
        `${API_BASE_URL}/auth/register`,
        request
      );
      
      if (response.data.success && response.data.token) {
        // Store token
        localStorage.setItem(TOKEN_KEY, response.data.token);
        if (response.data.userProfile) {
          localStorage.setItem(USER_KEY, JSON.stringify(response.data.userProfile));
        }
      }
      
      return response.data;
    } catch (error) {
      if (axios.isAxiosError(error)) {
        const axiosError = error as AxiosError<{ message: string }>;
        return {
          userId: '',
          token: '',
          success: false,
          message: axiosError.response?.data?.message || 'Erro ao cadastrar'
        };
      }
      return {
        userId: '',
        token: '',
        success: false,
        message: 'Erro inesperado ao cadastrar'
      };
    }
  },

  async verifyToken(): Promise<boolean> {
    const token = this.getToken();
    if (!token) return false;

    try {
      await axios.get(`${API_BASE_URL}/auth/verify`, {
        headers: { Authorization: `Bearer ${token}` }
      });
      return true;
    } catch {
      this.logout();
      return false;
    }
  },

  logout() {
    localStorage.removeItem(TOKEN_KEY);
    localStorage.removeItem(USER_KEY);
  },

  getToken(): string | null {
    return localStorage.getItem(TOKEN_KEY);
  },

  getUserProfile(): any {
    const userStr = localStorage.getItem(USER_KEY);
    return userStr ? JSON.parse(userStr) : null;
  },

  isAuthenticated(): boolean {
    return !!this.getToken();
  }
};

// Setup axios interceptor to add token to requests
axios.interceptors.request.use((config) => {
  const token = authApi.getToken();
  if (token && config.headers) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

// Setup axios interceptor to handle 401 responses
axios.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      authApi.logout();
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);
