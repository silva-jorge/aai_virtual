import React from 'react';
import styles from './PageContainer.module.css';

interface PageContainerProps {
  children: React.ReactNode;
  className?: string;
}

/**
 * Container component for page content with consistent padding and constraints.
 * Ensures responsive layout and proper spacing across all pages.
 */
export const PageContainer: React.FC<PageContainerProps> = ({ children, className }) => {
  return (
    <div className={`${styles.pageContainer} ${className || ''}`}>
      {children}
    </div>
  );
};
