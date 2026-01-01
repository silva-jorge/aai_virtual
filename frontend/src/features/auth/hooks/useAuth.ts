import { useMutation, useQueryClient } from '@tanstack/react-query';
import { useNavigate } from 'react-router-dom';
import { authApi } from '../api/authApi';
import type { LoginRequest, RegisterRequest } from '../types/auth';

export const useAuth = () => {
  const navigate = useNavigate();
  const queryClient = useQueryClient();

  const loginMutation = useMutation({
    mutationFn: (request: LoginRequest) => authApi.login(request),
    onSuccess: (data) => {
      if (data.success) {
        queryClient.invalidateQueries();
        navigate('/dashboard');
      }
    }
  });

  const registerMutation = useMutation({
    mutationFn: (request: RegisterRequest) => authApi.register(request),
    onSuccess: (data) => {
      if (data.success) {
        queryClient.invalidateQueries();
        navigate('/dashboard');
      }
    }
  });

  const logout = () => {
    authApi.logout();
    queryClient.clear();
    navigate('/login');
  };

  return {
    login: loginMutation.mutate,
    register: registerMutation.mutate,
    logout,
    isLoggingIn: loginMutation.isPending,
    isRegistering: registerMutation.isPending,
    loginError: loginMutation.error,
    registerError: registerMutation.error,
    isAuthenticated: authApi.isAuthenticated(),
    userProfile: authApi.getUserProfile()
  };
};
