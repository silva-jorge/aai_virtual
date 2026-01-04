import React, { useMemo } from 'react';
import {
  PieChart as RechartsPieChart,
  Pie,
  Cell,
  Legend,
  Tooltip,
  ResponsiveContainer,
  PieProps,
} from 'recharts';

interface PieChartData {
  name: string;
  value: number;
  [key: string]: any;
}

interface PieChartProps {
  data: PieChartData[];
  dataKey?: string;
  nameKey?: string;
  colors?: string[];
  title?: string;
  height?: number;
  showLegend?: boolean;
  showTooltip?: boolean;
  onClick?: (entry: any) => void;
}

/**
 * Reusable PieChart component wrapper around Recharts
 * Displays data distribution with customizable colors and legend
 */
export const PieChart: React.FC<PieChartProps> = ({
  data,
  dataKey = 'value',
  nameKey = 'name',
  colors = [
    '#3b82f6',
    '#10b981',
    '#f59e0b',
    '#ef4444',
    '#8b5cf6',
    '#ec4899',
    '#14b8a6',
  ],
  title,
  height = 300,
  showLegend = true,
  showTooltip = true,
  onClick,
}) => {
  // Calculate total for percentage display
  const total = useMemo(() => {
    return data.reduce((sum, item) => sum + (item[dataKey] || 0), 0);
  }, [data, dataKey]);

  const handleCellClick = (entry: any) => {
    onClick?.(entry);
  };

  return (
    <div>
      {title && <h3 style={{ margin: '0 0 1rem 0', fontSize: '1.125rem' }}>{title}</h3>}
      <ResponsiveContainer width="100%" height={height}>
        <RechartsPieChart>
          <Pie
            data={data}
            dataKey={dataKey}
            nameKey={nameKey}
            cx="50%"
            cy="50%"
            outerRadius={80}
            label={({ name, percent }) =>
              `${name} ${(percent * 100).toFixed(0)}%`
            }
            onClick={handleCellClick}
          >
            {data.map((entry, index) => (
              <Cell
                key={`cell-${index}`}
                fill={colors[index % colors.length]}
                cursor={onClick ? 'pointer' : 'default'}
              />
            ))}
          </Pie>
          {showTooltip && (
            <Tooltip
              formatter={(value: number) => [
                `${(value / total * 100).toFixed(2)}%`,
                'Percentage',
              ]}
            />
          )}
          {showLegend && <Legend />}
        </RechartsPieChart>
      </ResponsiveContainer>
    </div>
  );
};
