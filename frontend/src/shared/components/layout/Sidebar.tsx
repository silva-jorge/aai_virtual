import React from 'react';
import { Link, useLocation } from 'react-router-dom';
import styles from './Sidebar.module.css';

interface SidebarProps {
  isOpen: boolean;
  onClose: () => void;
}

interface NavItem {
  label: string;
  path: string;
  icon: string;
}

/**
 * Sidebar navigation component with menu items for all features.
 */
export const Sidebar: React.FC<SidebarProps> = ({ isOpen, onClose }) => {
  const location = useLocation();

  const navItems: NavItem[] = [
    { label: 'Dashboard', path: '/', icon: '📊' },
    { label: 'Portfolio', path: '/portfolio', icon: '💼' },
    { label: 'Rebalancing', path: '/rebalancing', icon: '⚖️' },
    { label: 'News', path: '/news', icon: '📰' },
    { label: 'Analytics', path: '/analytics', icon: '📈' },
    { label: 'Alerts', path: '/alerts', icon: '🔔' },
    { label: 'Profile', path: '/profile', icon: '👤' },
  ];

  const isActive = (path: string) => location.pathname === path;

  return (
    <>
      {/* Overlay for mobile */}
      {isOpen && (
        <div 
          className={styles.overlay} 
          onClick={onClose}
          aria-hidden="true"
        />
      )}

      <aside 
        className={`${styles.sidebar} ${isOpen ? styles.open : ''}`}
        role="navigation"
        aria-label="Main navigation"
      >
        <nav className={styles.nav}>
          {navItems.map((item) => (
            <Link
              key={item.path}
              to={item.path}
              className={`${styles.navItem} ${isActive(item.path) ? styles.active : ''}`}
              onClick={onClose}
            >
              <span className={styles.icon}>{item.icon}</span>
              <span className={styles.label}>{item.label}</span>
            </Link>
          ))}
        </nav>
      </aside>
    </>
  );
};
