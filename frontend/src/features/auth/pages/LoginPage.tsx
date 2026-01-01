import { useState } from 'react';
import { useAuth } from '../hooks/useAuth';
import styles from './LoginPage.module.css';

export const LoginPage = () => {
  const [pin, setPin] = useState('');
  const { login, isLoggingIn } = useAuth();
  const [error, setError] = useState('');

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (pin.length < 4 || pin.length > 6) {
      setError('PIN deve ter entre 4 e 6 dígitos');
      return;
    }

    login({ pin }, {
      onSuccess: (data) => {
        if (!data.success) {
          setError(data.message || 'Erro ao fazer login');
        }
      },
      onError: (err: any) => {
        console.error('Login error:', err);
        setError(err?.response?.data?.message || err?.message || 'Erro ao fazer login. Verifique se o backend está rodando.');
      }
    });
  };

  const handlePinChange = (value: string) => {
    // Only allow numbers
    const numericValue = value.replace(/\D/g, '');
    if (numericValue.length <= 6) {
      setPin(numericValue);
    }
  };

  return (
    <div className={styles.container}>
      <div className={styles.card}>
        <h1 className={styles.title}>AAI Portfolio Manager</h1>
        <p className={styles.subtitle}>Entre com seu PIN</p>

        <form onSubmit={handleSubmit} className={styles.form}>
          <div className={styles.inputGroup}>
            <label htmlFor="pin" className={styles.label}>
              PIN
            </label>
            <input
              id="pin"
              type="password"
              value={pin}
              onChange={(e) => handlePinChange(e.target.value)}
              placeholder="Digite seu PIN (4-6 dígitos)"
              className={styles.input}
              autoFocus
              disabled={isLoggingIn}
              inputMode="numeric"
              pattern="[0-9]*"
            />
          </div>

          {error && <div className={styles.error}>{error}</div>}

          <button
            type="submit"
            disabled={isLoggingIn || pin.length < 4}
            className={styles.button}
          >
            {isLoggingIn ? 'Entrando...' : 'Entrar'}
          </button>
        </form>

        <div className={styles.footer}>
          <p>Primeira vez aqui?</p>
          <a href="/register" className={styles.link}>
            Criar conta
          </a>
        </div>
      </div>
    </div>
  );
};
