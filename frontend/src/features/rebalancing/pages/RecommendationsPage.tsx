import { useRecommendations } from '../hooks/useRecommendations';
import styles from './RecommendationsPage.module.css';

export const RecommendationsPage = () => {
  const { recommendations, isLoading, error, requestRecommendations, isRequesting, refetch } = useRecommendations();

  const handleRequestNew = () => {
    requestRecommendations({ forceRegenerate: true });
  };

  const getPriorityClass = (priority: string) => {
    switch (priority.toLowerCase()) {
      case 'alta':
        return styles.priorityAlta;
      case 'critica':
        return styles.priorityCritica;
      case 'media':
        return styles.priorityMedia;
      case 'baixa':
        return styles.priorityBaixa;
      default:
        return styles.priorityMedia;
    }
  };

  const formatDate = (dateStr?: string) => {
    if (!dateStr) return '-';
    return new Intl.DateTimeFormat('pt-BR', {
      day: '2-digit',
      month: '2-digit',
      year: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    }).format(new Date(dateStr));
  };

  if (isLoading) {
    return <div className={styles.loading}>Carregando recomendações...</div>;
  }

  if (error) {
    return (
      <div className={styles.container}>
        <div className={styles.error}>
          Erro ao carregar recomendações: {(error as Error).message}
        </div>
      </div>
    );
  }

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <h1 className={styles.title}>🤖 Recomendações de IA</h1>
        <p className={styles.subtitle}>Sugestões inteligentes para otimizar seu portfólio</p>
      </div>

      <div className={styles.actions}>
        <button
          className={`${styles.button} ${styles.buttonPrimary}`}
          onClick={handleRequestNew}
          disabled={isRequesting}
        >
          {isRequesting ? 'Gerando...' : '🔄 Gerar Novas Recomendações'}
        </button>
        <button
          className={`${styles.button} ${styles.buttonPrimary}`}
          onClick={() => refetch()}
          disabled={isLoading}
        >
          🔃 Atualizar
        </button>
      </div>

      {recommendations.length === 0 ? (
        <div className={styles.emptyState}>
          <div className={styles.emptyStateIcon}>🤖</div>
          <div className={styles.emptyStateText}>Nenhuma recomendação disponível</div>
          <div className={styles.emptyStateSubtext}>
            Clique em "Gerar Novas Recomendações" para receber sugestões da IA
          </div>
        </div>
      ) : (
        <div className={styles.recommendationsList}>
          {recommendations.map((rec) => (
            <div key={rec.id} className={styles.recommendationCard}>
              <div className={styles.cardHeader}>
                <h3 className={styles.cardTitle}>
                  {rec.actionType === 'Rebalancear' && '⚖️'}
                  {rec.actionType === 'Comprar' && '📈'}
                  {rec.actionType === 'Vender' && '📉'}
                  {rec.title}
                </h3>
                <span className={`${styles.priorityBadge} ${getPriorityClass(rec.priority)}`}>
                  {rec.priority}
                </span>
              </div>

              <p className={styles.cardDescription}>{rec.description}</p>

              <div className={styles.cardRationale}>
                <div className={styles.rationaleTitle}>💡 Justificativa da IA</div>
                <div className={styles.rationaleText}>{rec.rationale}</div>
              </div>

              <div className={styles.cardDetails}>
                {rec.ticker && (
                  <div className={styles.detailItem}>
                    <span className={styles.detailLabel}>Ativo</span>
                    <span className={styles.detailValue}>{rec.ticker}</span>
                  </div>
                )}
                {rec.quantity && (
                  <div className={styles.detailItem}>
                    <span className={styles.detailLabel}>Quantidade</span>
                    <span className={styles.detailValue}>{rec.quantity}</span>
                  </div>
                )}
                {rec.estimatedValue && (
                  <div className={styles.detailItem}>
                    <span className={styles.detailLabel}>Valor Estimado</span>
                    <span className={styles.detailValue}>
                      {new Intl.NumberFormat('pt-BR', {
                        style: 'currency',
                        currency: 'BRL',
                      }).format(rec.estimatedValue)}
                    </span>
                  </div>
                )}
                <div className={styles.detailItem}>
                  <span className={styles.detailLabel}>Status</span>
                  <span className={styles.detailValue}>{rec.status}</span>
                </div>
                {rec.expiresAt && (
                  <div className={styles.detailItem}>
                    <span className={styles.detailLabel}>Expira em</span>
                    <span className={styles.detailValue}>{formatDate(rec.expiresAt)}</span>
                  </div>
                )}
              </div>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};
