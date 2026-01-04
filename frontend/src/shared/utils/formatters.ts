/**
 * Utility functions for formatting values in the UI
 */

/**
 * Format a number as Brazilian currency (BRL)
 * @param value - The numeric value to format
 * @returns Formatted currency string (e.g., "R$ 1.234,56")
 */
export const formatCurrency = (value: number): string => {
  return new Intl.NumberFormat('pt-BR', {
    style: 'currency',
    currency: 'BRL',
  }).format(value);
};

/**
 * Format a number as percentage
 * @param value - The numeric value (0-1 or 0-100 depending on input)
 * @param decimals - Number of decimal places (default: 2)
 * @returns Formatted percentage string (e.g., "12,34%")
 */
export const formatPercentage = (value: number, decimals: number = 2): string => {
  // Assume value is between 0-1, multiply by 100
  const percentage = value * 100;
  return new Intl.NumberFormat('pt-BR', {
    style: 'percent',
    minimumFractionDigits: decimals,
    maximumFractionDigits: decimals,
  }).format(value);
};

/**
 * Format a number with thousands separator
 * @param value - The numeric value to format
 * @param decimals - Number of decimal places (default: 2)
 * @returns Formatted number string (e.g., "1.234,56")
 */
export const formatNumber = (value: number, decimals: number = 2): string => {
  return new Intl.NumberFormat('pt-BR', {
    minimumFractionDigits: decimals,
    maximumFractionDigits: decimals,
  }).format(value);
};

/**
 * Format a date in Brazilian format
 * @param date - The date to format
 * @param includeTime - Whether to include time (default: false)
 * @returns Formatted date string (e.g., "01/01/2026" or "01/01/2026 14:30")
 */
export const formatDate = (date: string | Date, includeTime: boolean = false): string => {
  const dateObj = typeof date === 'string' ? new Date(date) : date;
  
  const options: Intl.DateTimeFormatOptions = {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
  };

  if (includeTime) {
    options.hour = '2-digit';
    options.minute = '2-digit';
  }

  return new Intl.DateTimeFormat('pt-BR', options).format(dateObj);
};

/**
 * Format a relative time difference
 * @param date - The date to compare
 * @returns Relative time string (e.g., "2 horas atrás", "em 3 dias")
 */
export const formatRelativeTime = (date: string | Date): string => {
  const dateObj = typeof date === 'string' ? new Date(date) : date;
  const now = new Date();
  const diffMs = now.getTime() - dateObj.getTime();
  const diffSecs = Math.floor(diffMs / 1000);
  const diffMins = Math.floor(diffSecs / 60);
  const diffHours = Math.floor(diffMins / 60);
  const diffDays = Math.floor(diffHours / 24);

  if (diffSecs < 60) return 'agora';
  if (diffMins < 60) return `${diffMins} min atrás`;
  if (diffHours < 24) return `${diffHours}h atrás`;
  if (diffDays < 7) return `${diffDays}d atrás`;
  if (diffDays < 30) return `${Math.floor(diffDays / 7)}s atrás`;
  
  return formatDate(dateObj);
};

/**
 * Format a percentage change with color indicator
 * @param value - The percentage change (-1 to 1 or -100 to 100)
 * @param asPercent - Whether the value is already a percentage (default: true)
 * @returns Object with formatted string and color class
 */
export const formatPriceChange = (
  value: number,
  asPercent: boolean = true
): { formatted: string; isPositive: boolean; isNeutral: boolean } => {
  const actualValue = asPercent ? value : value * 100;
  const formatted = `${actualValue > 0 ? '+' : ''}${formatNumber(actualValue, 2)}%`;
  
  return {
    formatted,
    isPositive: actualValue > 0,
    isNeutral: actualValue === 0,
  };
};

/**
 * Format a ticker symbol (uppercase)
 * @param ticker - The ticker symbol
 * @returns Formatted ticker (e.g., "PETR4")
 */
export const formatTicker = (ticker: string): string => {
  return ticker.toUpperCase().trim();
};

/**
 * Truncate text with ellipsis
 * @param text - The text to truncate
 * @param maxLength - Maximum length
 * @returns Truncated text with ellipsis if needed
 */
export const truncateText = (text: string, maxLength: number = 50): string => {
  if (text.length <= maxLength) return text;
  return `${text.substring(0, maxLength)}...`;
};
