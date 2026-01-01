import { useQuery } from '@tanstack/react-query';
import { portfolioApi } from '../api/portfolioApi';

export const usePortfolio = () => {
  const summaryQuery = useQuery({
    queryKey: ['portfolio', 'summary'],
    queryFn: portfolioApi.getSummary,
  });

  const allocationQuery = useQuery({
    queryKey: ['portfolio', 'allocation'],
    queryFn: portfolioApi.getAllocation,
  });

  const performanceQuery = useQuery({
    queryKey: ['portfolio', 'performance'],
    queryFn: portfolioApi.getPerformance,
  });

  return {
    summary: summaryQuery.data,
    allocation: allocationQuery.data,
    performance: performanceQuery.data,
    isLoading: summaryQuery.isLoading || allocationQuery.isLoading || performanceQuery.isLoading,
    error: summaryQuery.error || allocationQuery.error || performanceQuery.error,
    refetch: () => {
      summaryQuery.refetch();
      allocationQuery.refetch();
      performanceQuery.refetch();
    },
  };
};
