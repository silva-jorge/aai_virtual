import { usePortfolio } from '../hooks/usePortfolio';
import styles from './PortfolioDashboardPage.module.css';

export const PortfolioDashboardPage = () => {
  const { summary, allocation, performance, isLoading, error } = usePortfolio();

  if (isLoading) {
    return <div className={styles.loading}>Carregando portfólio...</div>;
  }

  if (error) {
    return (
      <div className={styles.container}>
        <div className={styles.error}>
          Erro ao carregar portfólio: {(error as Error).message}
        </div>
      </div>
    );
  }

  if (!summary) {
    return (
      <div className={styles.container}>
        <div className={styles.emptyState}>
          <div className={styles.emptyStateIcon}>📊</div>
          <div className={styles.emptyStateText}>Nenhum portfólio encontrado</div>
          <div className={styles.emptyStateSubtext}>
            Adicione seus primeiros ativos para começar
          </div>
        </div>
      </div>
    );
  }

  const formatCurrency = (value: number) => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: summary.currency,
    }).format(value);
  };

  const formatPercent = (value: number) => {
    return `${value >= 0 ? '+' : ''}${value.toFixed(2)}%`;
  };

  const getChangeClass = (value: number) => {
    if (value > 0) return styles.positive;
    if (value < 0) return styles.negative;
    return styles.neutral;
  };

  const assetClassColors: Record<string, string> = {
    Acao: '#3b82f6',
    FII: '#10b981',
    RendaFixa: '#f59e0b',
    Cripto: '#8b5cf6',
    Internacional: '#ec4899',
  };

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <h1 className={styles.title}>{summary.name}</h1>
        {summary.description && (
          <p className={styles.subtitle}>{summary.description}</p>
        )}
      </div>

      {/* Summary Cards */}
      <div className={styles.summaryCards}>
        <div className={styles.card}>
          <div className={styles.cardTitle}>Valor Investido</div>
          <div className={styles.cardValue}>{formatCurrency(summary.totalInvested)}</div>
          <div className={styles.cardChange + ' ' + styles.neutral}>
            {summary.assetsCount} ativos • {summary.positionsCount} posições
          </div>
        </div>

        <div className={styles.card}>
          <div className={styles.cardTitle}>Valor Atual</div>
          <div className={styles.cardValue}>{formatCurrency(summary.currentValue)}</div>
          <div className={styles.cardChange + ' ' + getChangeClass(summary.totalGainLoss)}>
            {formatCurrency(summary.totalGainLoss)}
          </div>
        </div>

        <div className={styles.card}>
          <div className={styles.cardTitle}>Rentabilidade</div>
          <div className={`${styles.cardValue} ${getChangeClass(summary.totalGainLossPercent)}`}>
            {formatPercent(summary.totalGainLossPercent)}
          </div>
          {performance && (
            <div className={styles.cardChange + ' ' + styles.neutral}>
              Ano: {formatPercent(performance.yearReturnPercent)}
            </div>
          )}
        </div>
      </div>

      {/* Allocation Section */}
      {allocation && allocation.byAssetClass.length > 0 && (
        <div className={styles.card}>
          <h2 className={styles.sectionTitle}>
            <span>📊</span>
            Alocação por Classe de Ativo
          </h2>
          <div className={styles.allocationGrid}>
            <div>
              <ul className={styles.allocationList}>
                {allocation.byAssetClass.map((item) => (
                  <li key={item.assetClass} className={styles.allocationItem}>
                    <div className={styles.allocationLabel}>
                      <div
                        className={styles.allocationColor}
                        style={{ backgroundColor: assetClassColors[item.assetClass] || '#gray' }}
                      />
                      <span>{item.assetClass}</span>
                    </div>
                    <div>
                      <div className={styles.allocationValue}>
                        {item.percent.toFixed(1)}%
                      </div>
                      <div className={styles.cardChange + ' ' + styles.neutral}>
                        {formatCurrency(item.value)}
                      </div>
                    </div>
                  </li>
                ))}
              </ul>
            </div>
          </div>
        </div>
      )}

      {/* Top Positions */}
      {allocation && allocation.topPositions.length > 0 && (
        <div className={styles.card}>
          <h2 className={styles.sectionTitle}>
            <span>🏆</span>
            Principais Posições
          </h2>
          <ul className={styles.positionsList}>
            {allocation.topPositions.map((position) => (
              <li key={position.id} className={styles.positionItem}>
                <div>
                  <div className={styles.positionTicker}>{position.ticker}</div>
                  <div className={styles.positionName}>{position.assetName}</div>
                </div>
                <div className={styles.positionValue}>
                  {formatCurrency(position.currentValue)}
                </div>
                <div className={styles.positionValue}>
                  {position.allocationPercent.toFixed(1)}%
                </div>
                <div className={`${styles.positionValue} ${getChangeClass(position.gainLossPercent)}`}>
                  {formatPercent(position.gainLossPercent)}
                </div>
              </li>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
};
