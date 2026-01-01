import { Link } from 'react-router-dom';
import { authApi } from '../../auth/api/authApi';
import styles from './DashboardPage.module.css';

export const DashboardPage = () => {
  const userProfile = authApi.getUserProfile();

  const handleLogout = () => {
    authApi.logout();
    window.location.href = '/login';
  };

  return (
    <div className={styles.container}>
      <div className={styles.header}>
        <h1 className={styles.title}>Dashboard - AAI Portfolio Manager</h1>
        <p className={styles.subtitle}>
          Bem-vindo, {userProfile?.name || 'Investidor'}! 👋
        </p>
      </div>

      <div className={styles.grid}>
        <div className={styles.card}>
          <div className={styles.cardIcon}>📊</div>
          <h2 className={styles.cardTitle}>Seu Portfólio</h2>
          <p className={styles.cardDescription}>
            Visualize e gerencie seus investimentos
          </p>
          <div className={styles.cardValue}>R$ 0,00</div>
        </div>

        <div className={styles.card}>
          <div className={styles.cardIcon}>📈</div>
          <h2 className={styles.cardTitle}>Performance</h2>
          <p className={styles.cardDescription}>
            Acompanhe o desempenho dos seus ativos
          </p>
          <div className={styles.cardValue}>+0%</div>
        </div>

        <div className={styles.card}>
          <div className={styles.cardIcon}>🎯</div>
          <h2 className={styles.cardTitle}>Objetivos</h2>
          <p className={styles.cardDescription}>
            Defina e monitore suas metas financeiras
          </p>
          <div className={styles.cardValue}>0/0</div>
        </div>

        <div className={styles.card}>
          <div className={styles.cardIcon}>🤖</div>
          <h2 className={styles.cardTitle}>Recomendações IA</h2>
          <p className={styles.cardDescription}>
            Sugestões inteligentes para otimizar seu portfólio
          </p>
          <div className={styles.cardValue}>0 novas</div>
        </div>
      </div>

      <div className={styles.actions}>
        <Link to="/portfolio" className={`${styles.button} ${styles.buttonPrimary}`}>
          📊 Ver Portfólio Completo
        </Link>
        <Link to="/recommendations" className={`${styles.button} ${styles.buttonPrimary}`}>
          🤖 Ver Recomendações IA
        </Link>
        <Link to="/settings" className={`${styles.button} ${styles.buttonPrimary}`}>
          ⚙️ Configurações de Perfil
        </Link>
        <button onClick={handleLogout} className={`${styles.button} ${styles.buttonDanger}`}>
          🚪 Sair
        </button>
      </div>
    </div>
  );
};
