import React from 'react';
import styles from './ThresholdConfig.module.css';

interface ThresholdConfigProps {
  thresholds: {
    rebalancingThreshold: number;
    alertThreshold: number;
  };
  onChange: (thresholds: { rebalancingThreshold: number; alertThreshold: number }) => void;
}

/**
 * Component to configure rebalancing and alert thresholds
 */
export const ThresholdConfig: React.FC<ThresholdConfigProps> = ({
  thresholds,
  onChange,
}) => {
  const handleRebalancingChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = parseFloat(e.target.value);
    onChange({
      ...thresholds,
      rebalancingThreshold: value,
    });
  };

  const handleAlertChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const value = parseFloat(e.target.value);
    onChange({
      ...thresholds,
      alertThreshold: value,
    });
  };

  return (
    <div className={styles.container}>
      <div className={styles.threshold}>
        <div className={styles.header}>
          <label htmlFor="rebalancingThreshold" className={styles.label}>
            Rebalancing Threshold
          </label>
          <span className={styles.value}>{thresholds.rebalancingThreshold}%</span>
        </div>

        <input
          id="rebalancingThreshold"
          type="range"
          min="1"
          max="50"
          step="1"
          value={thresholds.rebalancingThreshold}
          onChange={handleRebalancingChange}
          className={styles.slider}
        />

        <p className={styles.description}>
          System will suggest rebalancing when your allocation deviates more than{' '}
          <strong>{thresholds.rebalancingThreshold}%</strong> from target
        </p>

        <div className={styles.examples}>
          <p className={styles.exampleTitle}>Examples:</p>
          <ul>
            <li>5%: Very sensitive - rebalance frequently</li>
            <li>10%: Moderate - balanced approach (recommended)</li>
            <li>20%: Conservative - less frequent changes</li>
          </ul>
        </div>
      </div>

      <div className={styles.divider} />

      <div className={styles.threshold}>
        <div className={styles.header}>
          <label htmlFor="alertThreshold" className={styles.label}>
            Price Alert Threshold
          </label>
          <span className={styles.value}>{thresholds.alertThreshold}%</span>
        </div>

        <input
          id="alertThreshold"
          type="range"
          min="1"
          max="30"
          step="1"
          value={thresholds.alertThreshold}
          onChange={handleAlertChange}
          className={styles.slider}
        />

        <p className={styles.description}>
          You'll receive alerts when an asset moves more than{' '}
          <strong>{thresholds.alertThreshold}%</strong> from current price
        </p>

        <div className={styles.examples}>
          <p className={styles.exampleTitle}>Examples:</p>
          <ul>
            <li>3%: High sensitivity - frequent notifications</li>
            <li>5%: Normal - balanced notifications (recommended)</li>
            <li>10%: Low sensitivity - only major moves</li>
          </ul>
        </div>
      </div>
    </div>
  );
};
