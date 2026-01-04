/**
 * Common types shared across the application
 */

/**
 * HTTP API Response wrapper
 */
export interface ApiResponse<T = unknown> {
  success: boolean;
  data?: T;
  error?: string;
  message?: string;
}

/**
 * Paginated API response
 */
export interface PaginatedResponse<T> {
  items: T[];
  total: number;
  page: number;
  pageSize: number;
  totalPages: number;
}

/**
 * Error response from API
 */
export interface ApiError {
  code: string;
  message: string;
  details?: Record<string, string>;
  timestamp: string;
}

/**
 * Request parameters for listing
 */
export interface ListParams {
  page?: number;
  pageSize?: number;
  sortBy?: string;
  sortOrder?: 'asc' | 'desc';
  search?: string;
  filters?: Record<string, unknown>;
}

/**
 * Request state for async operations
 */
export interface AsyncState<T> {
  isLoading: boolean;
  error: ApiError | null;
  data: T | null;
}

/**
 * Async operation result
 */
export type AsyncResult<T> = 
  | { success: true; data: T }
  | { success: false; error: ApiError };

/**
 * Select option for dropdown components
 */
export interface SelectOption<T = string> {
  label: string;
  value: T;
  disabled?: boolean;
}

/**
 * Tab configuration
 */
export interface Tab {
  id: string;
  label: string;
  content: React.ReactNode;
  disabled?: boolean;
}

/**
 * Notification message
 */
export interface Notification {
  id: string;
  type: 'success' | 'error' | 'warning' | 'info';
  message: string;
  duration?: number;
  action?: {
    label: string;
    onClick: () => void;
  };
}

/**
 * Table column configuration
 */
export interface TableColumn<T> {
  key: keyof T;
  label: string;
  sortable?: boolean;
  filterable?: boolean;
  render?: (value: T[keyof T], row: T) => React.ReactNode;
  width?: string | number;
}

/**
 * Modal configuration
 */
export interface ModalConfig {
  isOpen: boolean;
  title: string;
  message: string;
  onConfirm: () => void;
  onCancel?: () => void;
  confirmText?: string;
  cancelText?: string;
  isDangerous?: boolean;
}

/**
 * Auth user information
 */
export interface AuthUser {
  id: string;
  email: string;
  name?: string;
  riskProfile?: string;
  createdAt: string;
}

/**
 * Authentication tokens
 */
export interface AuthTokens {
  accessToken: string;
  refreshToken: string;
  expiresIn: number;
}

/**
 * Generic container for app settings
 */
export interface AppSettings {
  theme: 'light' | 'dark';
  language: 'pt-BR' | 'en-US';
  notifications: boolean;
  autoRefresh: boolean;
}
