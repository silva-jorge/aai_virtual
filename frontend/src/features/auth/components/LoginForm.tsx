import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Button } from '../../../shared/components/ui/Button';
import { Input } from '../../../shared/components/ui/Input';
import { useAuth } from '../hooks/useAuth';
import styles from './LoginForm.module.css';

/**
 * Login form component for user authentication
 * Handles email/password authentication
 */
export const LoginForm: React.FC = () => {
  const navigate = useNavigate();
  const { login, isLoading, error } = useAuth();

  const [formData, setFormData] = useState({
    email: '',
    password: '',
  });

  const [localError, setLocalError] = useState<string | null>(null);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.currentTarget;
    setFormData((prev) => ({
      ...prev,
      [name]: value,
    }));
    setLocalError(null);
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLocalError(null);

    // Validation
    if (!formData.email) {
      setLocalError('Email is required');
      return;
    }

    if (!formData.password) {
      setLocalError('Password is required');
      return;
    }

    try {
      await login(formData.email, formData.password);
      navigate('/');
    } catch (err) {
      setLocalError(error || 'Login failed. Please try again.');
    }
  };

  const displayError = localError || error;

  return (
    <form className={styles.loginForm} onSubmit={handleSubmit}>
      <h1 className={styles.title}>AAI Portfolio</h1>
      <p className={styles.subtitle}>Sign in to your account</p>

      {displayError && (
        <div className={styles.errorMessage} role="alert">
          {displayError}
        </div>
      )}

      <div className={styles.formGroup}>
        <label htmlFor="email" className={styles.label}>
          Email
        </label>
        <Input
          id="email"
          name="email"
          type="email"
          placeholder="your@email.com"
          value={formData.email}
          onChange={handleChange}
          disabled={isLoading}
          required
        />
      </div>

      <div className={styles.formGroup}>
        <label htmlFor="password" className={styles.label}>
          Password
        </label>
        <Input
          id="password"
          name="password"
          type="password"
          placeholder="••••••••"
          value={formData.password}
          onChange={handleChange}
          disabled={isLoading}
          required
        />
      </div>

      <Button
        type="submit"
        disabled={isLoading}
        className={styles.submitButton}
        fullWidth
      >
        {isLoading ? 'Signing in...' : 'Sign In'}
      </Button>

      <div className={styles.footer}>
        <p>First time here?</p>
        <a href="/setup" className={styles.link}>
          Set up your account
        </a>
      </div>
    </form>
  );
};
