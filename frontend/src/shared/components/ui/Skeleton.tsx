/**
 * Skeleton Component
 * Loading placeholder with shimmer animation
 */

import React from 'react';
import styles from './Skeleton.module.css';

export interface SkeletonProps {
  variant?: 'text' | 'circular' | 'rectangular';
  width?: string | number;
  height?: string | number;
  animation?: 'pulse' | 'wave' | 'none';
  className?: string;
}

export const Skeleton: React.FC<SkeletonProps> = ({
  variant = 'text',
  width,
  height,
  animation = 'pulse',
  className = '',
}) => {
  const style: React.CSSProperties = {
    width: typeof width === 'number' ? `${width}px` : width,
    height: typeof height === 'number' ? `${height}px` : height,
  };

  return (
    <div
      className={`${styles.skeleton} ${styles[`skeleton--${variant}`]} ${
        animation !== 'none' ? styles[`skeleton--${animation}`] : ''
      } ${className}`}
      style={style}
      aria-busy="true"
      aria-live="polite"
    />
  );
};

/**
 * Skeleton Text Lines
 * Multiple text skeleton lines
 */
export interface SkeletonTextProps {
  lines?: number;
  lastLineWidth?: string | number;
  className?: string;
}

export const SkeletonText: React.FC<SkeletonTextProps> = ({
  lines = 3,
  lastLineWidth = '80%',
  className = '',
}) => {
  return (
    <div className={`${styles.skeletonText} ${className}`}>
      {Array.from({ length: lines }).map((_, index) => (
        <Skeleton
          key={index}
          variant="text"
          width={index === lines - 1 ? lastLineWidth : '100%'}
        />
      ))}
    </div>
  );
};

/**
 * Skeleton Card
 * Card skeleton with avatar and text
 */
export interface SkeletonCardProps {
  hasAvatar?: boolean;
  lines?: number;
  className?: string;
}

export const SkeletonCard: React.FC<SkeletonCardProps> = ({
  hasAvatar = false,
  lines = 3,
  className = '',
}) => {
  return (
    <div className={`${styles.skeletonCard} ${className}`}>
      {hasAvatar && (
        <div className={styles.skeletonCardHeader}>
          <Skeleton variant="circular" width={40} height={40} />
          <div className={styles.skeletonCardHeaderText}>
            <Skeleton variant="text" width="60%" />
            <Skeleton variant="text" width="40%" />
          </div>
        </div>
      )}
      <SkeletonText lines={lines} />
    </div>
  );
};

/**
 * Skeleton Table
 * Table skeleton with rows
 */
export interface SkeletonTableProps {
  rows?: number;
  columns?: number;
  className?: string;
}

export const SkeletonTable: React.FC<SkeletonTableProps> = ({
  rows = 5,
  columns = 4,
  className = '',
}) => {
  return (
    <div className={`${styles.skeletonTable} ${className}`}>
      {/* Header */}
      <div className={styles.skeletonTableHeader}>
        {Array.from({ length: columns }).map((_, index) => (
          <Skeleton key={`header-${index}`} variant="text" width="80%" />
        ))}
      </div>
      {/* Rows */}
      {Array.from({ length: rows }).map((_, rowIndex) => (
        <div key={`row-${rowIndex}`} className={styles.skeletonTableRow}>
          {Array.from({ length: columns }).map((_, colIndex) => (
            <Skeleton key={`cell-${rowIndex}-${colIndex}`} variant="text" width="90%" />
          ))}
        </div>
      ))}
    </div>
  );
};
