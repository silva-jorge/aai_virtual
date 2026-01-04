import React from 'react';
import { Card } from '../../../shared/components/ui/Card';
import { PieChart } from '../../../shared/components/charts/PieChart';
import { LineChart } from '../../../shared/components/charts/LineChart';
import { PositionList } from './PositionList';
import { usePortfolio } from '../hooks/usePortfolio';
import { usePositions } from '../hooks/usePositions';
import { formatCurrency, formatPercentage } from '../../../shared/utils/formatters';
import styles from './PortfolioDashboard.module.css';

/**
 * Main portfolio dashboard component
 * Displays portfolio summary, allocation breakdown, and position list
 */
export const PortfolioDashboard: React.FC = () => {
  const { portfolio, summary, isLoading: portfolioLoading, error } = usePortfolio();
  const { positions, isLoading: positionsLoading } = usePositions(portfolio?.id);

  const isLoading = portfolioLoading || positionsLoading;

  if (isLoading) {
    return (
      <div className={styles.container}>
        <p>Loading portfolio...</p>
      </div>
    );
  }

  if (error) {
    return (
      <div className={styles.container}>
        <div className={styles.error} role="alert">
          {error}
        </div>
      </div>
    );
  }

  if (!summary) {
    return (
      <div className={styles.container}>
        <div className={styles.empty}>
          <p>No portfolio data available. Please create a portfolio first.</p>
        </div>
      </div>
    );
  }

  // Prepare data for allocation chart
  const allocationData = summary.allocation?.map((item: any) => ({
    name: item.assetClass,
    value: item.percentage,
  })) || [];

  // Prepare data for performance chart (mock data)
  const performanceData = [
    { month: 'Jan', value: 10000 },
    { month: 'Feb', value: 10500 },
    { month: 'Mar', value: 11000 },
    { month: 'Apr', value: 10800 },
    { month: 'May', value: 11500 },
    { month: 'Jun', value: summary.totalValue },
  ];

  return (
    <div className={styles.container}>
      <h1 className={styles.title}>Portfolio Dashboard</h1>

      {/* Summary Cards */}
      <div className={styles.summaryGrid}>
        <Card className={styles.summaryCard}>
          <div className={styles.summaryContent}>
            <p className={styles.summaryLabel}>Total Value</p>
            <p className={styles.summaryValue}>
              {formatCurrency(summary.totalValue)}
            </p>
          </div>
        </Card>

        <Card className={styles.summaryCard}>
          <div className={styles.summaryContent}>
            <p className={styles.summaryLabel}>Total Invested</p>
            <p className={styles.summaryValue}>
              {formatCurrency(summary.totalCost)}
            </p>
          </div>
        </Card>

        <Card className={styles.summaryCard}>
          <div className={styles.summaryContent}>
            <p className={styles.summaryLabel}>Total Gain/Loss</p>
            <p className={`${styles.summaryValue} ${summary.totalGain >= 0 ? styles.positive : styles.negative}`}>
              {formatCurrency(summary.totalGain)}
            </p>
          </div>
        </Card>

        <Card className={styles.summaryCard}>
          <div className={styles.summaryContent}>
            <p className={styles.summaryLabel}>Return %</p>
            <p className={`${styles.summaryValue} ${summary.returnPercent >= 0 ? styles.positive : styles.negative}`}>
              {formatPercentage(summary.returnPercent / 100)}
            </p>
          </div>
        </Card>
      </div>

      {/* Charts Section */}
      <div className={styles.chartsGrid}>
        <Card className={styles.chartCard}>
          {allocationData.length > 0 ? (
            <PieChart
              data={allocationData}
              title="Asset Allocation"
              dataKey="value"
              nameKey="name"
              height={300}
            />
          ) : (
            <p>No allocation data available</p>
          )}
        </Card>

        <Card className={styles.chartCard}>
          <LineChart
            data={performanceData}
            title="Portfolio Performance"
            lines={[
              {
                dataKey: 'value',
                stroke: '#3b82f6',
                name: 'Portfolio Value',
              },
            ]}
            xAxisKey="month"
            height={300}
          />
        </Card>
      </div>

      {/* Positions Section */}
      <Card className={styles.positionsCard}>
        <h2 className={styles.sectionTitle}>Your Positions</h2>
        <PositionList
          positions={positions}
          isLoading={positionsLoading}
          showActions={true}
        />
      </Card>
    </div>
  );
};
