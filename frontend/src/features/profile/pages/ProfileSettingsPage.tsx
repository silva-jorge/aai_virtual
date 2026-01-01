import { useState, useEffect } from 'react';
import { useProfile } from '../hooks/useProfile';
import styles from './ProfileSettingsPage.module.css';

export const ProfileSettingsPage = () => {
  const {
    profile,
    isLoading,
    error,
    updateRiskProfile,
    updateThresholds,
    isUpdatingRiskProfile,
    isUpdatingThresholds,
    updateRiskProfileError,
    updateThresholdsError,
  } = useProfile();

  const [riskProfile, setRiskProfile] = useState('moderado');
  const [investmentGoal, setInvestmentGoal] = useState('');
  const [volatilityTolerance, setVolatilityTolerance] = useState(50);
  const [timeHorizonMonths, setTimeHorizonMonths] = useState(60);
  const [rebalanceThreshold, setRebalanceThreshold] = useState(5);
  const [successMessage, setSuccessMessage] = useState('');

  useEffect(() => {
    if (profile) {
      setRiskProfile(profile.riskProfile.toLowerCase());
      setInvestmentGoal(profile.investmentGoal || '');
      setVolatilityTolerance(profile.volatilityTolerance);
      setTimeHorizonMonths(profile.timeHorizonMonths);
      setRebalanceThreshold(profile.rebalanceThresholdPercent);
    }
  }, [profile]);

  const handleSaveRiskProfile = () => {
    setSuccessMessage('');
    updateRiskProfile(
      {
        riskProfile,
        investmentGoal: investmentGoal || undefined,
        volatilityTolerance,
        timeHorizonMonths,
      },
      {
        onSuccess: () => {
          setSuccessMessage('Perfil de risco atualizado com sucesso!');
          setTimeout(() => setSuccessMessage(''), 3000);
        },
      }
    );
  };

  const handleSaveThresholds = () => {
    setSuccessMessage('');
    updateThresholds(
      {
        rebalanceThresholdPercent: rebalanceThreshold,
        targetAllocationJson: profile?.targetAllocationJson || '{}',
      },
      {
        onSuccess: () => {
          setSuccessMessage('Thresholds atualizados com sucesso!');
          setTimeout(() => setSuccessMessage(''), 3000);
        },
      }
    );
  };

  if (isLoading) {
    return <div className={styles.loading}>Carregando perfil...</div>;
  }

  if (error) {
    return (
      <div className={styles.container}>
        <div className={styles.error}>Erro ao carregar perfil: {(error as Error).message}</div>
      </div>
    );
  }

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <h1 className={styles.title}>Configurações de Perfil</h1>
        <p className={styles.subtitle}>Personalize seu perfil de investimento</p>
      </div>

      {successMessage && <div className={styles.success}>{successMessage}</div>}

      {/* Risk Profile Section */}
      <div className={styles.section}>
        <h2 className={styles.sectionTitle}>
          <span className={styles.sectionIcon}>📊</span>
          Perfil de Risco
        </h2>

        <div className={styles.formGroup}>
          <label className={styles.label}>Selecione seu perfil</label>
          <div className={styles.riskProfiles}>
            <label className={`${styles.riskProfile} ${riskProfile === 'conservador' ? styles.selected : ''}`}>
              <input
                type="radio"
                name="riskProfile"
                value="conservador"
                checked={riskProfile === 'conservador'}
                onChange={(e) => setRiskProfile(e.target.value)}
              />
              <div className={styles.riskProfileTitle}>🛡️ Conservador</div>
              <div className={styles.riskProfileDescription}>
                Prioriza segurança e estabilidade
              </div>
            </label>
            <label className={`${styles.riskProfile} ${riskProfile === 'moderado' ? styles.selected : ''}`}>
              <input
                type="radio"
                name="riskProfile"
                value="moderado"
                checked={riskProfile === 'moderado'}
                onChange={(e) => setRiskProfile(e.target.value)}
              />
              <div className={styles.riskProfileTitle}>⚖️ Moderado</div>
              <div className={styles.riskProfileDescription}>
                Equilíbrio entre risco e retorno
              </div>
            </label>
            <label className={`${styles.riskProfile} ${riskProfile === 'agressivo' ? styles.selected : ''}`}>
              <input
                type="radio"
                name="riskProfile"
                value="agressivo"
                checked={riskProfile === 'agressivo'}
                onChange={(e) => setRiskProfile(e.target.value)}
              />
              <div className={styles.riskProfileTitle}>🚀 Agressivo</div>
              <div className={styles.riskProfileDescription}>
                Busca maior retorno com mais risco
              </div>
            </label>
          </div>
        </div>

        <div className={styles.formGroup}>
          <label htmlFor="investmentGoal" className={styles.label}>
            Objetivo de Investimento
          </label>
          <textarea
            id="investmentGoal"
            className={styles.textarea}
            value={investmentGoal}
            onChange={(e) => setInvestmentGoal(e.target.value)}
            placeholder="Ex: Aposentadoria, compra de imóvel, viagem..."
            maxLength={500}
          />
          <div className={styles.description}>Descreva seus objetivos financeiros (opcional)</div>
        </div>

        <div className={styles.formGroup}>
          <label htmlFor="volatilityTolerance" className={styles.label}>
            Tolerância à Volatilidade
            <span className={styles.rangeValue}>{volatilityTolerance}%</span>
          </label>
          <input
            type="range"
            id="volatilityTolerance"
            className={styles.rangeInput}
            min="0"
            max="100"
            value={volatilityTolerance}
            onChange={(e) => setVolatilityTolerance(Number(e.target.value))}
          />
          <div className={styles.description}>Quanto de oscilação você aceita em seus investimentos?</div>
        </div>

        <div className={styles.formGroup}>
          <label htmlFor="timeHorizon" className={styles.label}>
            Horizonte de Tempo
          </label>
          <select
            id="timeHorizon"
            className={styles.select}
            value={timeHorizonMonths}
            onChange={(e) => setTimeHorizonMonths(Number(e.target.value))}
          >
            <option value="12">1 ano</option>
            <option value="24">2 anos</option>
            <option value="36">3 anos</option>
            <option value="60">5 anos</option>
            <option value="120">10 anos</option>
            <option value="240">20 anos</option>
          </select>
          <div className={styles.description}>Por quanto tempo pretende manter seus investimentos?</div>
        </div>

        {updateRiskProfileError && (
          <div className={styles.error}>Erro: {(updateRiskProfileError as Error).message}</div>
        )}

        <div className={styles.actions}>
          <button
            className={`${styles.button} ${styles.buttonPrimary}`}
            onClick={handleSaveRiskProfile}
            disabled={isUpdatingRiskProfile}
          >
            {isUpdatingRiskProfile ? 'Salvando...' : 'Salvar Perfil de Risco'}
          </button>
        </div>
      </div>

      {/* Rebalance Settings */}
      <div className={styles.section}>
        <h2 className={styles.sectionTitle}>
          <span className={styles.sectionIcon}>⚙️</span>
          Configurações de Rebalanceamento
        </h2>

        <div className={styles.formGroup}>
          <label htmlFor="rebalanceThreshold" className={styles.label}>
            Threshold de Rebalanceamento
            <span className={styles.rangeValue}>{rebalanceThreshold}%</span>
          </label>
          <input
            type="range"
            id="rebalanceThreshold"
            className={styles.rangeInput}
            min="1"
            max="20"
            step="0.5"
            value={rebalanceThreshold}
            onChange={(e) => setRebalanceThreshold(Number(e.target.value))}
          />
          <div className={styles.description}>
            Quando seu portfólio desviar mais de {rebalanceThreshold}% da alocação alvo, você será notificado
          </div>
        </div>

        {updateThresholdsError && (
          <div className={styles.error}>Erro: {(updateThresholdsError as Error).message}</div>
        )}

        <div className={styles.actions}>
          <button
            className={`${styles.button} ${styles.buttonPrimary}`}
            onClick={handleSaveThresholds}
            disabled={isUpdatingThresholds}
          >
            {isUpdatingThresholds ? 'Salvando...' : 'Salvar Configurações'}
          </button>
        </div>
      </div>
    </div>
  );
};
