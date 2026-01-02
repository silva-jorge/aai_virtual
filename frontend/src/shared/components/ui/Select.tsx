/**
 * Select Component
 * Custom select dropdown with label, error, and helper text
 */

import React, { forwardRef } from 'react';
import styles from './Select.module.css';

export interface SelectOption {
  value: string;
  label: string;
  disabled?: boolean;
}

export interface SelectProps extends Omit<React.SelectHTMLAttributes<HTMLSelectElement>, 'size'> {
  label?: string;
  error?: string;
  helperText?: string;
  options: SelectOption[];
  placeholder?: string;
  size?: 'sm' | 'md' | 'lg';
  fullWidth?: boolean;
}

export const Select = forwardRef<HTMLSelectElement, SelectProps>(
  (
    {
      label,
      error,
      helperText,
      options,
      placeholder = 'Selecione...',
      size = 'md',
      fullWidth = false,
      className = '',
      disabled,
      required,
      ...props
    },
    ref
  ) => {
    const hasError = Boolean(error);
    const showHelper = Boolean(helperText && !error);

    return (
      <div className={`${styles.selectWrapper} ${fullWidth ? styles['selectWrapper--fullWidth'] : ''}`}>
        {label && (
          <label className={styles.label} htmlFor={props.id || props.name}>
            {label}
            {required && <span className={styles.required}>*</span>}
          </label>
        )}

        <div className={styles.selectContainer}>
          <select
            ref={ref}
            className={`${styles.select} ${styles[`select--${size}`]} ${
              hasError ? styles['select--error'] : ''
            } ${disabled ? styles['select--disabled'] : ''} ${className}`}
            disabled={disabled}
            required={required}
            aria-invalid={hasError}
            aria-describedby={
              hasError
                ? `${props.id || props.name}-error`
                : showHelper
                ? `${props.id || props.name}-helper`
                : undefined
            }
            {...props}
          >
            {placeholder && (
              <option value="" disabled>
                {placeholder}
              </option>
            )}
            {options.map((option) => (
              <option key={option.value} value={option.value} disabled={option.disabled}>
                {option.label}
              </option>
            ))}
          </select>

          <div className={styles.selectIcon}>
            <svg
              width="16"
              height="16"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              strokeWidth="2"
              strokeLinecap="round"
              strokeLinejoin="round"
            >
              <polyline points="6 9 12 15 18 9" />
            </svg>
          </div>
        </div>

        {hasError && (
          <span
            className={styles.errorText}
            id={`${props.id || props.name}-error`}
            role="alert"
          >
            {error}
          </span>
        )}

        {showHelper && (
          <span className={styles.helperText} id={`${props.id || props.name}-helper`}>
            {helperText}
          </span>
        )}
      </div>
    );
  }
);

Select.displayName = 'Select';
