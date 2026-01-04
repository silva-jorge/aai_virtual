import React, { useState } from 'react';
import { PositionCard } from './PositionCard';
import { PositionDTO } from '../types/portfolio';
import styles from './PositionList.module.css';

interface PositionListProps {
  positions: PositionDTO[];
  isLoading?: boolean;
  onEdit?: (position: PositionDTO) => void;
  onDelete?: (id: string) => void;
  showActions?: boolean;
}

/**
 * List component for displaying multiple positions
 * Supports sorting and filtering
 */
export const PositionList: React.FC<PositionListProps> = ({
  positions,
  isLoading = false,
  onEdit,
  onDelete,
  showActions = true,
}) => {
  const [sortBy, setSortBy] = useState<'value' | 'gain' | 'change'>('value');
  const [filterAssetClass, setFilterAssetClass] = useState<string>('all');

  // Get unique asset classes from positions
  const assetClasses = ['all', ...new Set(positions.map((p) => p.assetClass))];

  // Sort positions
  const sortedPositions = [...positions].sort((a, b) => {
    switch (sortBy) {
      case 'value':
        return b.currentValue - a.currentValue;
      case 'gain':
        return b.gainLoss - a.gainLoss;
      case 'change':
        return (b.priceChangePercent || 0) - (a.priceChangePercent || 0);
      default:
        return 0;
    }
  });

  // Filter by asset class
  const filteredPositions =
    filterAssetClass === 'all'
      ? sortedPositions
      : sortedPositions.filter((p) => p.assetClass === filterAssetClass);

  if (isLoading) {
    return (
      <div className={styles.container}>
        <p>Loading positions...</p>
      </div>
    );
  }

  if (positions.length === 0) {
    return (
      <div className={styles.empty}>
        <p>No positions yet. Add your first position to get started!</p>
      </div>
    );
  }

  return (
    <div className={styles.container}>
      <div className={styles.controls}>
        <div className={styles.filterGroup}>
          <label htmlFor="assetClassFilter" className={styles.label}>
            Asset Class:
          </label>
          <select
            id="assetClassFilter"
            value={filterAssetClass}
            onChange={(e) => setFilterAssetClass(e.target.value)}
            className={styles.select}
          >
            {assetClasses.map((ac) => (
              <option key={ac} value={ac}>
                {ac === 'all' ? 'All Assets' : ac}
              </option>
            ))}
          </select>
        </div>

        <div className={styles.sortGroup}>
          <label htmlFor="sortBy" className={styles.label}>
            Sort by:
          </label>
          <select
            id="sortBy"
            value={sortBy}
            onChange={(e) => setSortBy(e.target.value as typeof sortBy)}
            className={styles.select}
          >
            <option value="value">Total Value</option>
            <option value="gain">Gain/Loss</option>
            <option value="change">% Change</option>
          </select>
        </div>
      </div>

      <div className={styles.count}>
        Showing {filteredPositions.length} of {positions.length} positions
      </div>

      <div className={styles.grid}>
        {filteredPositions.map((position) => (
          <PositionCard
            key={position.id}
            position={position}
            onEdit={onEdit}
            onDelete={onDelete}
            showActions={showActions}
          />
        ))}
      </div>
    </div>
  );
};
