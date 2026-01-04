import React, { useState, useEffect } from 'react';
import { Button } from '../../../shared/components/ui/Button';
import { Card } from '../../../shared/components/ui/Card';
import { RiskProfileSelector } from './RiskProfileSelector';
import { ThresholdConfig } from './ThresholdConfig';
import { useProfile } from '../hooks/useProfile';
import styles from './ProfileSettings.module.css';

/**
 * Profile settings component that manages user configuration
 * Allows users to set risk profile and rebalancing thresholds
 */
export const ProfileSettings: React.FC = () => {
  const { profile, isLoading, error, updateRiskProfile, updateThresholds, isUpdatingRiskProfile, isUpdatingThresholds } = useProfile();

  const [riskProfile, setRiskProfile] = useState<string>('moderate');
  const [thresholds, setThresholds] = useState({
    rebalancingThreshold: 10,
    alertThreshold: 5,
  });

  const [isSaving, setIsSaving] = useState(false);
  const [saveSuccess, setSaveSuccess] = useState(false);

  useEffect(() => {
    if (profile) {
      setRiskProfile(profile.riskProfile);
      setThresholds({
        rebalancingThreshold: profile.rebalancingThreshold || 10,
        alertThreshold: profile.alertThreshold || 5,
      });
    }
  }, [profile]);

  const handleSave = async () => {
    setIsSaving(true);
    setSaveSuccess(false);

    try {
      await updateRiskProfile({ riskProfile });
      await updateThresholds({ ...thresholds });
      setSaveSuccess(true);
      setTimeout(() => setSaveSuccess(false), 3000);
    } catch (err) {
      console.error('Failed to save profile:', err);
    } finally {
      setIsSaving(false);
    }
  };

  if (isLoading) {
    return (
      <div className={styles.container}>
        <p>Loading profile settings...</p>
      </div>
    );
  }

  return (
    <div className={styles.container}>
      <h1 className={styles.title}>Profile Settings</h1>

      {error && (
        <div className={styles.errorMessage} role="alert">
          {error}
        </div>
      )}

      {saveSuccess && (
        <div className={styles.successMessage} role="status">
          ✓ Profile settings saved successfully!
        </div>
      )}

      <Card className={styles.card}>
        <div className={styles.section}>
          <h2 className={styles.sectionTitle}>Risk Profile</h2>
          <p className={styles.sectionDescription}>
            Select your investment risk profile to customize recommendations
          </p>
          <RiskProfileSelector
            selectedProfile={riskProfile}
            onSelect={setRiskProfile}
          />
        </div>
      </Card>

      <Card className={styles.card}>
        <div className={styles.section}>
          <h2 className={styles.sectionTitle}>Rebalancing Configuration</h2>
          <p className={styles.sectionDescription}>
            Set thresholds for automatic rebalancing alerts
          </p>
          <ThresholdConfig
            thresholds={thresholds}
            onChange={setThresholds}
          />
        </div>
      </Card>

      <div className={styles.actions}>
        <Button
          variant="primary"
          size="lg"
          onClick={handleSave}
          disabled={isSaving || isUpdatingRiskProfile || isUpdatingThresholds}
        >
          {isSaving || isUpdatingRiskProfile || isUpdatingThresholds ? 'Saving...' : 'Save Settings'}
        </Button>
      </div>
    </div>
  );
};
