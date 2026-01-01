import { createBrowserRouter, Navigate } from 'react-router-dom';
import { LoginPage } from '../features/auth/pages/LoginPage';
import { RegisterPage } from '../features/auth/pages/RegisterPage';
import { DashboardPage } from '../features/dashboard/pages/DashboardPage';
import { ProfileSettingsPage } from '../features/profile/pages/ProfileSettingsPage';
import { PortfolioDashboardPage } from '../features/portfolio/pages/PortfolioDashboardPage';
import { RecommendationsPage } from '../features/rebalancing/pages/RecommendationsPage';
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
  },
  {
    path: '/portfolio',
    element: (
      <ProtectedRoute>
        <Layout>
          <PortfolioDashboardPage />
        </Layout>
      </ProtectedRoute>
    )
  },
  {
    path: '/recommendations',
    element: (
      <ProtectedRoute>
        <Layout>
          <RecommendationsPage />
        </Layout>
      </ProtectedRoute>
    )
  },
  {
    path: '/settings',
    element: (
      <ProtectedRoute>
        <Layout>
          <ProfileSettingsPage />
        </Layout>
      </ProtectedRoute>
    )
  }
]);
