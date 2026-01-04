import React from 'react';
import { useAuth } from '../../../features/auth/hooks/useAuth';
import { Button } from '../ui/Button';
import styles from './Header.module.css';

interface HeaderProps {
  onToggleSidebar: () => void;
  sidebarOpen: boolean;
}

/**
 * Header component with logo, navigation breadcrumb, and user menu.
 */
export const Header: React.FC<HeaderProps> = ({ onToggleSidebar, sidebarOpen }) => {
  const { user, logout } = useAuth();

  return (
    <header className={styles.header}>
      <div className={styles.leftSection}>
        <button 
          className={styles.menuToggle} 
          onClick={onToggleSidebar}
          aria-label={sidebarOpen ? 'Close sidebar' : 'Open sidebar'}
        >
          ☰
        </button>
        <div className={styles.logo}>
          <span className={styles.logoText}>AAI Portfolio</span>
        </div>
      </div>

      <div className={styles.rightSection}>
        {user && (
          <div className={styles.userMenu}>
            <span className={styles.userName}>{user.email}</span>
            <Button 
              variant="ghost" 
              size="sm" 
              onClick={logout}
            >
              Logout
            </Button>
          </div>
        )}
      </div>
    </header>
  );
};
