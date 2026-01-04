import React from 'react';
import {
  BarChart as RechartsBarChart,
  Bar,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from 'recharts';

interface BarChartData {
  [key: string]: any;
}

interface BarChartProps {
  data: BarChartData[];
  bars: Array<{
    dataKey: string;
    fill?: string;
    name?: string;
  }>;
  xAxisKey?: string;
  title?: string;
  height?: number;
  showLegend?: boolean;
  showTooltip?: boolean;
  showGrid?: boolean;
  layout?: 'vertical' | 'horizontal';
  margin?: { top: number; right: number; left: number; bottom: number };
}

/**
 * Reusable BarChart component wrapper around Recharts
 * Displays categorical data with bars, supports horizontal and vertical layouts
 */
export const BarChart: React.FC<BarChartProps> = ({
  data,
  bars,
  xAxisKey = 'name',
  title,
  height = 300,
  showLegend = true,
  showTooltip = true,
  showGrid = true,
  layout = 'vertical',
  margin = { top: 5, right: 30, left: 0, bottom: 5 },
}) => {
  const colors = [
    '#3b82f6',
    '#10b981',
    '#f59e0b',
    '#ef4444',
    '#8b5cf6',
    '#ec4899',
  ];

  return (
    <div>
      {title && <h3 style={{ margin: '0 0 1rem 0', fontSize: '1.125rem' }}>{title}</h3>}
      <ResponsiveContainer width="100%" height={height}>
        <RechartsBarChart data={data} margin={margin} layout={layout}>
          {showGrid && <CartesianGrid strokeDasharray="3 3" />}
          {layout === 'vertical' ? (
            <>
              <XAxis type="number" />
              <YAxis type="category" dataKey={xAxisKey} width={80} />
            </>
          ) : (
            <>
              <XAxis dataKey={xAxisKey} />
              <YAxis type="number" />
            </>
          )}
          {showTooltip && <Tooltip />}
          {showLegend && <Legend />}
          {bars.map((bar, index) => (
            <Bar
              key={`bar-${index}`}
              dataKey={bar.dataKey}
              fill={bar.fill || colors[index % colors.length]}
              name={bar.name || bar.dataKey}
            />
          ))}
        </RechartsBarChart>
      </ResponsiveContainer>
    </div>
  );
};
