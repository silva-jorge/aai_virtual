import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { profileApi } from '../api/profileApi';
import type { UpdateRiskProfileRequest, UpdateThresholdsRequest } from '../types/profile';

export const useProfile = () => {
  const queryClient = useQueryClient();

  const profileQuery = useQuery({
    queryKey: ['profile'],
    queryFn: profileApi.getProfile,
  });

  const updateRiskProfileMutation = useMutation({
    mutationFn: (data: UpdateRiskProfileRequest) => profileApi.updateRiskProfile(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['profile'] });
    },
  });

  const updateThresholdsMutation = useMutation({
    mutationFn: (data: UpdateThresholdsRequest) => profileApi.updateThresholds(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['profile'] });
    },
  });

  return {
    profile: profileQuery.data,
    isLoading: profileQuery.isLoading,
    error: profileQuery.error,
    updateRiskProfile: updateRiskProfileMutation.mutate,
    updateThresholds: updateThresholdsMutation.mutate,
    isUpdatingRiskProfile: updateRiskProfileMutation.isPending,
    isUpdatingThresholds: updateThresholdsMutation.isPending,
    updateRiskProfileError: updateRiskProfileMutation.error,
    updateThresholdsError: updateThresholdsMutation.error,
  };
};
