import React from 'react';
import { Header } from './Header';
import { Sidebar } from './Sidebar';
import { PageContainer } from './PageContainer';
import styles from './MainLayout.module.css';

interface MainLayoutProps {
  children: React.ReactNode;
}

/**
 * Main layout component that wraps the entire application.
 * Provides header, sidebar navigation, and main content area.
 */
export const MainLayout: React.FC<MainLayoutProps> = ({ children }) => {
  const [sidebarOpen, setSidebarOpen] = React.useState(true);

  const toggleSidebar = () => {
    setSidebarOpen(!sidebarOpen);
  };

  return (
    <div className={styles.mainLayout}>
      <Header onToggleSidebar={toggleSidebar} sidebarOpen={sidebarOpen} />
      <div className={styles.container}>
        <Sidebar isOpen={sidebarOpen} onClose={() => setSidebarOpen(false)} />
        <main className={styles.main}>
          <PageContainer>{children}</PageContainer>
        </main>
      </div>
    </div>
  );
};
