import React from 'react';
import {
  LineChart as RechartsLineChart,
  Line,
  XAxis,
  YAxis,
  CartesianGrid,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from 'recharts';

interface LineChartData {
  [key: string]: any;
}

interface LineChartProps {
  data: LineChartData[];
  lines: Array<{
    dataKey: string;
    stroke?: string;
    name?: string;
    strokeWidth?: number;
  }>;
  xAxisKey?: string;
  title?: string;
  height?: number;
  showLegend?: boolean;
  showTooltip?: boolean;
  showGrid?: boolean;
  margin?: { top: number; right: number; left: number; bottom: number };
}

/**
 * Reusable LineChart component wrapper around Recharts
 * Displays time-series or multi-line data with customization options
 */
export const LineChart: React.FC<LineChartProps> = ({
  data,
  lines,
  xAxisKey = 'name',
  title,
  height = 300,
  showLegend = true,
  showTooltip = true,
  showGrid = true,
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
        <RechartsLineChart data={data} margin={margin}>
          {showGrid && <CartesianGrid strokeDasharray="3 3" />}
          <XAxis dataKey={xAxisKey} />
          <YAxis />
          {showTooltip && <Tooltip />}
          {showLegend && <Legend />}
          {lines.map((line, index) => (
            <Line
              key={`line-${index}`}
              type="monotone"
              dataKey={line.dataKey}
              stroke={line.stroke || colors[index % colors.length]}
              name={line.name || line.dataKey}
              strokeWidth={line.strokeWidth || 2}
              dot={false}
              activeDot={{ r: 5 }}
            />
          ))}
        </RechartsLineChart>
      </ResponsiveContainer>
    </div>
  );
};
