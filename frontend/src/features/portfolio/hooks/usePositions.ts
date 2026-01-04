import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { portfolioApi } from '../api/portfolioApi';
import { PositionDTO } from '../types/portfolio';

/**
 * Hook for managing position data and operations
 */
export const usePositions = (portfolioId?: string) => {
  const queryClient = useQueryClient();

  const {
    data: positions = [],
    isLoading,
    error,
    refetch,
  } = useQuery({
    queryKey: ['positions', portfolioId],
    queryFn: () => portfolioApi.getPositions(portfolioId || ''),
    enabled: !!portfolioId,
    staleTime: 5 * 60 * 1000, // 5 minutes
  });

  const createPositionMutation = useMutation({
    mutationFn: (position: Omit<PositionDTO, 'id'>) =>
      portfolioApi.createPosition(position),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['positions'] });
      queryClient.invalidateQueries({ queryKey: ['portfolio'] });
    },
  });

  const updatePositionMutation = useMutation({
    mutationFn: ({ id, ...position }: PositionDTO) =>
      portfolioApi.updatePosition(id, position),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['positions'] });
      queryClient.invalidateQueries({ queryKey: ['portfolio'] });
    },
  });

  const deletePositionMutation = useMutation({
    mutationFn: (id: string) => portfolioApi.deletePosition(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['positions'] });
      queryClient.invalidateQueries({ queryKey: ['portfolio'] });
    },
  });

  return {
    positions,
    isLoading,
    error: error?.message || null,
    refetch,
    createPosition: createPositionMutation.mutate,
    updatePosition: updatePositionMutation.mutate,
    deletePosition: deletePositionMutation.mutate,
    isCreating: createPositionMutation.isPending,
    isUpdating: updatePositionMutation.isPending,
    isDeleting: deletePositionMutation.isPending,
  };
};
