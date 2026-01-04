import React from 'react';
import { Card } from '../../../shared/components/ui/Card';
import styles from './RiskProfileSelector.module.css';

interface RiskProfileSelectorProps {
  selectedProfile: string;
  onSelect: (profile: string) => void;
}

interface RiskProfile {
  id: string;
  label: string;
  description: string;
  allocation: {
    stocks: number;
    bonds: number;
    alternatives: number;
  };
  expectedReturn: string;
  volatility: string;
}

/**
 * Component to select investment risk profile
 * Shows different portfolio allocation strategies
 */
export const RiskProfileSelector: React.FC<RiskProfileSelectorProps> = ({
  selectedProfile,
  onSelect,
}) => {
  const riskProfiles: RiskProfile[] = [
    {
      id: 'conservative',
      label: 'Conservative',
      description: 'Low risk, stable returns',
      allocation: { stocks: 20, bonds: 70, alternatives: 10 },
      expectedReturn: '4-6%',
      volatility: 'Low',
    },
    {
      id: 'moderate',
      label: 'Moderate',
      description: 'Balanced risk and returns',
      allocation: { stocks: 50, bonds: 40, alternatives: 10 },
      expectedReturn: '7-10%',
      volatility: 'Medium',
    },
    {
      id: 'aggressive',
      label: 'Aggressive',
      description: 'High growth potential',
      allocation: { stocks: 75, bonds: 15, alternatives: 10 },
      expectedReturn: '11-15%',
      volatility: 'High',
    },
  ];

  return (
    <div className={styles.container}>
      {riskProfiles.map((profile) => (
        <div
          key={profile.id}
          className={`${styles.profileOption} ${
            selectedProfile === profile.id ? styles.selected : ''
          }`}
          onClick={() => onSelect(profile.id)}
          role="radio"
          aria-checked={selectedProfile === profile.id}
          tabIndex={0}
          onKeyPress={(e) => {
            if (e.key === 'Enter' || e.key === ' ') {
              onSelect(profile.id);
            }
          }}
        >
          <div className={styles.header}>
            <h3 className={styles.label}>{profile.label}</h3>
            <div className={styles.checkmark}>
              {selectedProfile === profile.id && '✓'}
            </div>
          </div>

          <p className={styles.description}>{profile.description}</p>

          <div className={styles.allocation}>
            <div className={styles.bar}>
              <div
                className={styles.segment}
                style={{ width: `${profile.allocation.stocks}%` }}
                title={`Stocks: ${profile.allocation.stocks}%`}
              />
              <div
                className={styles.segment}
                style={{ width: `${profile.allocation.bonds}%` }}
                title={`Bonds: ${profile.allocation.bonds}%`}
              />
              <div
                className={styles.segment}
                style={{ width: `${profile.allocation.alternatives}%` }}
                title={`Alternatives: ${profile.allocation.alternatives}%`}
              />
            </div>
            <div className={styles.legend}>
              <span>
                <span className={styles.legendColor} style={{ backgroundColor: '#3b82f6' }} />
                Stocks: {profile.allocation.stocks}%
              </span>
              <span>
                <span className={styles.legendColor} style={{ backgroundColor: '#10b981' }} />
                Bonds: {profile.allocation.bonds}%
              </span>
              <span>
                <span className={styles.legendColor} style={{ backgroundColor: '#f59e0b' }} />
                Alt: {profile.allocation.alternatives}%
              </span>
            </div>
          </div>

          <div className={styles.metrics}>
            <div className={styles.metric}>
              <span className={styles.metricLabel}>Expected Return:</span>
              <span className={styles.metricValue}>{profile.expectedReturn}</span>
            </div>
            <div className={styles.metric}>
              <span className={styles.metricLabel}>Volatility:</span>
              <span className={styles.metricValue}>{profile.volatility}</span>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
};
