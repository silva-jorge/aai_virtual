import React, { useState } from 'react';
import { Button } from '../../../shared/components/ui/Button';
import { Input } from '../../../shared/components/ui/Input';
import styles from './PinSetup.module.css';

interface PinSetupProps {
  onPinSetup: (pin: string) => Promise<void>;
  isLoading?: boolean;
}

/**
 * PIN setup component for local storage encryption
 * Allows users to set a PIN for additional security
 */
export const PinSetup: React.FC<PinSetupProps> = ({ onPinSetup, isLoading = false }) => {
  const [pin, setPin] = useState('');
  const [confirmPin, setConfirmPin] = useState('');
  const [error, setError] = useState<string | null>(null);
  const [showPin, setShowPin] = useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    // Validation
    if (!pin) {
      setError('PIN is required');
      return;
    }

    if (pin.length < 4 || pin.length > 6) {
      setError('PIN must be 4-6 digits');
      return;
    }

    if (!/^\d+$/.test(pin)) {
      setError('PIN must contain only digits');
      return;
    }

    if (pin !== confirmPin) {
      setError('PINs do not match');
      return;
    }

    try {
      await onPinSetup(pin);
    } catch (err) {
      setError(err instanceof Error ? err.message : 'Failed to set PIN');
    }
  };

  const handlePinChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.currentTarget.value.replace(/\D/g, '').slice(0, 6);
    setPin(value);
    setError(null);
  };

  const handleConfirmPinChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = e.currentTarget.value.replace(/\D/g, '').slice(0, 6);
    setConfirmPin(value);
  };

  return (
    <form className={styles.pinSetup} onSubmit={handleSubmit}>
      <h2 className={styles.title}>Set up PIN for security</h2>
      <p className={styles.description}>
        Your PIN will be used to encrypt your local data
      </p>

      {error && (
        <div className={styles.errorMessage} role="alert">
          {error}
        </div>
      )}

      <div className={styles.formGroup}>
        <label htmlFor="pin" className={styles.label}>
          PIN (4-6 digits)
        </label>
        <div className={styles.pinInputWrapper}>
          <Input
            id="pin"
            type={showPin ? 'text' : 'password'}
            inputMode="numeric"
            placeholder="••••"
            value={pin}
            onChange={handlePinChange}
            disabled={isLoading}
            maxLength={6}
            required
          />
          <button
            type="button"
            className={styles.toggleButton}
            onClick={() => setShowPin(!showPin)}
            aria-label={showPin ? 'Hide PIN' : 'Show PIN'}
          >
            {showPin ? '👁️' : '👁️‍🗨️'}
          </button>
        </div>
      </div>

      <div className={styles.formGroup}>
        <label htmlFor="confirmPin" className={styles.label}>
          Confirm PIN
        </label>
        <Input
          id="confirmPin"
          type={showPin ? 'text' : 'password'}
          inputMode="numeric"
          placeholder="••••"
          value={confirmPin}
          onChange={handleConfirmPinChange}
          disabled={isLoading}
          maxLength={6}
          required
        />
      </div>

      <div className={styles.info}>
        <p>
          ℹ️ Your PIN is stored locally and never sent to our servers
        </p>
      </div>

      <Button
        type="submit"
        disabled={isLoading}
        className={styles.submitButton}
        fullWidth
      >
        {isLoading ? 'Setting PIN...' : 'Set PIN'}
      </Button>
    </form>
  );
};
