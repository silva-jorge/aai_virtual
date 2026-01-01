import { useState } from 'react';
import { useAuth } from '../hooks/useAuth';
import styles from './RegisterPage.module.css';

export const RegisterPage = () => {
  const [step, setStep] = useState(1);
  const [pin, setPin] = useState('');
  const [confirmPin, setConfirmPin] = useState('');
  const [riskProfile, setRiskProfile] = useState('moderado');
  const [investmentGoal, setInvestmentGoal] = useState('');
  const { register, isRegistering } = useAuth();
  const [error, setError] = useState('');

  const handlePinChange = (value: string, setter: (val: string) => void) => {
    const numericValue = value.replace(/\D/g, '');
    if (numericValue.length <= 6) {
      setter(numericValue);
    }
  };

  const handleNextStep = () => {
    setError('');

    if (pin.length < 4 || pin.length > 6) {
      setError('PIN deve ter entre 4 e 6 dígitos');
      return;
    }

    if (pin !== confirmPin) {
      setError('PINs não coincidem');
      return;
    }

    setStep(2);
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    register({
      pin,
      riskProfile,
      investmentGoal: investmentGoal || undefined,
      volatilityTolerance: riskProfile === 'conservador' ? 30 : riskProfile === 'agressivo' ? 70 : 50,
      timeHorizonMonths: 60,
      rebalanceThresholdPercent: 5,
      targetAllocationJson: '{}'
    }, {
      onSuccess: (data) => {
        if (!data.success) {
          setError(data.message || 'Erro ao cadastrar');
        }
      },
      onError: (err: any) => {
        console.error('Register error:', err);
        setError(err?.response?.data?.message || err?.message || 'Erro ao cadastrar. Verifique se o backend está rodando.');
      }
    });
  };

  return (
    <div className={styles.container}>
      <div className={styles.card}>
        <h1 className={styles.title}>Criar Conta</h1>
        <p className={styles.subtitle}>
          {step === 1 ? 'Configure seu PIN' : 'Configure seu perfil'}
        </p>

        {step === 1 ? (
          <div className={styles.form}>
            <div className={styles.inputGroup}>
              <label htmlFor="pin" className={styles.label}>
                Criar PIN
              </label>
              <input
                id="pin"
                type="password"
                value={pin}
                onChange={(e) => handlePinChange(e.target.value, setPin)}
                placeholder="4-6 dígitos"
                className={styles.input}
                autoFocus
                inputMode="numeric"
                pattern="[0-9]*"
              />
            </div>

            <div className={styles.inputGroup}>
              <label htmlFor="confirmPin" className={styles.label}>
                Confirmar PIN
              </label>
              <input
                id="confirmPin"
                type="password"
                value={confirmPin}
                onChange={(e) => handlePinChange(e.target.value, setConfirmPin)}
                placeholder="4-6 dígitos"
                className={styles.input}
                inputMode="numeric"
                pattern="[0-9]*"
              />
            </div>

            {error && <div className={styles.error}>{error}</div>}

            <button
              type="button"
              onClick={handleNextStep}
              disabled={pin.length < 4 || confirmPin.length < 4}
              className={styles.button}
            >
              Próximo
            </button>
          </div>
        ) : (
          <form onSubmit={handleSubmit} className={styles.form}>
            <div className={styles.inputGroup}>
              <label htmlFor="riskProfile" className={styles.label}>
                Perfil de Risco
              </label>
              <select
                id="riskProfile"
                value={riskProfile}
                onChange={(e) => setRiskProfile(e.target.value)}
                className={styles.select}
                disabled={isRegistering}
              >
                <option value="conservador">Conservador</option>
                <option value="moderado">Moderado</option>
                <option value="agressivo">Agressivo</option>
              </select>
            </div>

            <div className={styles.inputGroup}>
              <label htmlFor="investmentGoal" className={styles.label}>
                Objetivo de Investimento (Opcional)
              </label>
              <textarea
                id="investmentGoal"
                value={investmentGoal}
                onChange={(e) => setInvestmentGoal(e.target.value)}
                placeholder="Ex: Aposentadoria, Compra de imóvel..."
                className={styles.textarea}
                disabled={isRegistering}
                maxLength={500}
                rows={3}
              />
            </div>

            {error && <div className={styles.error}>{error}</div>}

            <div className={styles.buttonGroup}>
              <button
                type="button"
                onClick={() => setStep(1)}
                disabled={isRegistering}
                className={styles.buttonSecondary}
              >
                Voltar
              </button>
              <button
                type="submit"
                disabled={isRegistering}
                className={styles.button}
              >
                {isRegistering ? 'Criando...' : 'Criar Conta'}
              </button>
            </div>
          </form>
        )}

        <div className={styles.footer}>
          <p>Já tem uma conta?</p>
          <a href="/login" className={styles.link}>
            Fazer login
          </a>
        </div>
      </div>
    </div>
  );
};
