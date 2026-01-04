import React from 'react';
import { Card } from '../../../shared/components/ui/Card';
import { Button } from '../../../shared/components/ui/Button';
import { formatCurrency, formatNumber, formatPriceChange } from '../../../shared/utils/formatters';
import { PositionDTO } from '../types/portfolio';
import styles from './PositionCard.module.css';

interface PositionCardProps {
  position: PositionDTO;
  onEdit?: (position: PositionDTO) => void;
  onDelete?: (id: string) => void;
  showActions?: boolean;
}

/**
 * Card component for displaying a single position
 * Shows asset info, quantity, value, and change
 */
export const PositionCard: React.FC<PositionCardProps> = ({
  position,
  onEdit,
  onDelete,
  showActions = true,
}) => {
  const priceChange = formatPriceChange(position.priceChangePercent || 0);
  const isPositive = priceChange.isPositive;

  return (
    <Card className={styles.card}>
      <div className={styles.header}>
        <div className={styles.assetInfo}>
          <h3 className={styles.ticker}>{position.assetTicker}</h3>
          <p className={styles.assetName}>{position.assetName}</p>
        </div>
        {showActions && (
          <div className={styles.actions}>
            {onEdit && (
              <Button
                variant="ghost"
                size="sm"
                onClick={() => onEdit(position)}
                title="Edit position"
              >
                ✎
              </Button>
            )}
            {onDelete && (
              <Button
                variant="ghost"
                size="sm"
                onClick={() => onDelete(position.id)}
                title="Delete position"
              >
                ✕
              </Button>
            )}
          </div>
        )}
      </div>

      <div className={styles.content}>
        <div className={styles.metric}>
          <span className={styles.label}>Quantity</span>
          <span className={styles.value}>
            {formatNumber(position.quantity, 2)}
          </span>
        </div>

        <div className={styles.metric}>
          <span className={styles.label}>Current Price</span>
          <span className={styles.value}>
            {formatCurrency(position.currentPrice)}
          </span>
        </div>

        <div className={styles.metric}>
          <span className={styles.label}>Total Value</span>
          <span className={styles.value}>
            {formatCurrency(position.currentValue)}
          </span>
        </div>

        <div className={styles.metric}>
          <span className={styles.label}>Cost Basis</span>
          <span className={styles.value}>
            {formatCurrency(position.costBasis)}
          </span>
        </div>
      </div>

      <div className={styles.footer}>
        <div className={styles.gain}>
          <span className={styles.label}>Gain/Loss</span>
          <span className={`${styles.value} ${isPositive ? styles.positive : styles.negative}`}>
            {formatCurrency(position.gainLoss)}
          </span>
        </div>

        <div className={styles.changePercent}>
          <span className={styles.label}>Change %</span>
          <span className={`${styles.value} ${isPositive ? styles.positive : styles.negative}`}>
            {priceChange.formatted}
          </span>
        </div>

        <div className={styles.allocation}>
          <span className={styles.label}>Allocation</span>
          <span className={styles.value}>
            {formatNumber(position.allocationPercent || 0, 1)}%
          </span>
        </div>
      </div>
    </Card>
  );
};
