import { ReactNode, HTMLAttributes } from 'react';
import styles from './Card.module.css';

interface CardProps extends HTMLAttributes<HTMLDivElement> {
  children: ReactNode;
  variant?: 'default' | 'outlined' | 'elevated';
  padding?: 'none' | 'small' | 'medium' | 'large';
}

export function Card({
  children,
  variant = 'default',
  padding = 'medium',
  className = '',
  ...props
}: CardProps) {
  const classNames = [
    styles.card,
    styles[variant],
    styles[`padding-${padding}`],
    className,
  ].filter(Boolean).join(' ');

  return (
    <div className={classNames} {...props}>
      {children}
    </div>
  );
}
