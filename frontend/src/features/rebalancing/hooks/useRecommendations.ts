import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { rebalancingApi } from '../api/rebalancingApi';
import type { RequestRecommendationsRequest } from '../types/rebalancing';

export const useRecommendations = () => {
  const queryClient = useQueryClient();

  const recommendationsQuery = useQuery({
    queryKey: ['recommendations'],
    queryFn: rebalancingApi.getRecommendations,
  });

  const requestRecommendationsMutation = useMutation({
    mutationFn: (data: RequestRecommendationsRequest) => rebalancingApi.requestRecommendations(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['recommendations'] });
    },
  });

  return {
    recommendations: recommendationsQuery.data || [],
    isLoading: recommendationsQuery.isLoading,
    error: recommendationsQuery.error,
    requestRecommendations: requestRecommendationsMutation.mutate,
    isRequesting: requestRecommendationsMutation.isPending,
    refetch: recommendationsQuery.refetch,
  };
};
