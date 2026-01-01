import { createBrowserRouter, Navigate } from 'react-router-dom';
import { LoginPage } from '../features/auth/pages/LoginPage';
import { RegisterPage } from '../features/auth/pages/RegisterPage';
import { authApi } from '../features/auth/api/authApi';

// Simple layout component
const Layout = ({ children }: { children: React.ReactNode }) => (
  <div>{children}</div>
);

// Protected route wrapper
const ProtectedRoute = ({ children }: { children: React.ReactNode }) => {
  const isAuthenticated = authApi.isAuthenticated();
  
  if (!isAuthenticated) {
    return <Navigate to="/login" replace />;
  }

  return <>{children}</>;
};

// Dashboard placeholder
const DashboardPage = () => (
  <div style={{ padding: '2rem' }}>
    <h1>Dashboard - AAI Portfolio Manager</h1>
    <p>Bem-vindo ao sistema de gerenciamento inteligente de portfólio!</p>
    <button 
      onClick={() => {
        authApi.logout();
        window.location.href = '/login';
      }}
      style={{
        marginTop: '1rem',
        padding: '0.5rem 1rem',
        backgroundColor: '#dc3545',
        color: 'white',
        border: 'none',
        borderRadius: '4px',
        cursor: 'pointer'
      }}
    >
      Sair
    </button>
  </div>
);

export const router = createBrowserRouter([
  {
    path: '/',
    element: <Navigate to="/login" replace />
  },
  {
    path: '/login',
    element: (
      <Layout>
        <LoginPage />
      </Layout>
    )
  },
  {
    path: '/register',
    element: (
      <Layout>
        <RegisterPage />
      </Layout>
    )
  },
  {
    path: '/dashboard',
    element: (
      <ProtectedRoute>
        <Layout>
          <DashboardPage />
        </Layout>
      </ProtectedRoute>
    )
  }
]);
