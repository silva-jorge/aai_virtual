/**
 * Validation Utilities
 * Common validation functions for forms and data
 */

/**
 * Email validation
 */
export function isValidEmail(email: string): boolean {
  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  return emailRegex.test(email);
}

/**
 * Password strength validation
 */
export interface PasswordStrength {
  isValid: boolean;
  strength: 'weak' | 'medium' | 'strong';
  errors: string[];
}

export function validatePassword(password: string): PasswordStrength {
  const errors: string[] = [];
  let strength: 'weak' | 'medium' | 'strong' = 'weak';

  // Minimum length
  if (password.length < 8) {
    errors.push('A senha deve ter no mínimo 8 caracteres');
  }

  // Contains uppercase
  if (!/[A-Z]/.test(password)) {
    errors.push('A senha deve conter pelo menos uma letra maiúscula');
  }

  // Contains lowercase
  if (!/[a-z]/.test(password)) {
    errors.push('A senha deve conter pelo menos uma letra minúscula');
  }

  // Contains number
  if (!/\d/.test(password)) {
    errors.push('A senha deve conter pelo menos um número');
  }

  // Contains special character
  if (!/[!@#$%^&*()_+\-=\[\]{};':"\\|,.<>\/?]/.test(password)) {
    errors.push('A senha deve conter pelo menos um caractere especial');
  }

  // Determine strength
  const score = 5 - errors.length;
  if (score >= 4) strength = 'strong';
  else if (score >= 2) strength = 'medium';

  return {
    isValid: errors.length === 0,
    strength,
    errors,
  };
}

/**
 * PIN validation (4-6 digits)
 */
export function isValidPin(pin: string): boolean {
  const pinRegex = /^\d{4,6}$/;
  return pinRegex.test(pin);
}

/**
 * Brazilian CPF validation
 */
export function isValidCPF(cpf: string): boolean {
  // Remove non-digits
  const cleanCPF = cpf.replace(/\D/g, '');

  // Check length
  if (cleanCPF.length !== 11) return false;

  // Check if all digits are the same
  if (/^(\d)\1{10}$/.test(cleanCPF)) return false;

  // Validate check digits
  let sum = 0;
  for (let i = 0; i < 9; i++) {
    sum += parseInt(cleanCPF.charAt(i)) * (10 - i);
  }
  let checkDigit = 11 - (sum % 11);
  if (checkDigit >= 10) checkDigit = 0;
  if (checkDigit !== parseInt(cleanCPF.charAt(9))) return false;

  sum = 0;
  for (let i = 0; i < 10; i++) {
    sum += parseInt(cleanCPF.charAt(i)) * (11 - i);
  }
  checkDigit = 11 - (sum % 11);
  if (checkDigit >= 10) checkDigit = 0;
  if (checkDigit !== parseInt(cleanCPF.charAt(10))) return false;

  return true;
}

/**
 * Brazilian CNPJ validation
 */
export function isValidCNPJ(cnpj: string): boolean {
  // Remove non-digits
  const cleanCNPJ = cnpj.replace(/\D/g, '');

  // Check length
  if (cleanCNPJ.length !== 14) return false;

  // Check if all digits are the same
  if (/^(\d)\1{13}$/.test(cleanCNPJ)) return false;

  // Validate first check digit
  let sum = 0;
  let weight = 5;
  for (let i = 0; i < 12; i++) {
    sum += parseInt(cleanCNPJ.charAt(i)) * weight;
    weight = weight === 2 ? 9 : weight - 1;
  }
  let checkDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);
  if (checkDigit !== parseInt(cleanCNPJ.charAt(12))) return false;

  // Validate second check digit
  sum = 0;
  weight = 6;
  for (let i = 0; i < 13; i++) {
    sum += parseInt(cleanCNPJ.charAt(i)) * weight;
    weight = weight === 2 ? 9 : weight - 1;
  }
  checkDigit = sum % 11 < 2 ? 0 : 11 - (sum % 11);
  if (checkDigit !== parseInt(cleanCNPJ.charAt(13))) return false;

  return true;
}

/**
 * Phone number validation (Brazilian format)
 */
export function isValidPhone(phone: string): boolean {
  const cleanPhone = phone.replace(/\D/g, '');
  // Accept 10 digits (landline) or 11 digits (mobile)
  return /^(\d{10}|\d{11})$/.test(cleanPhone);
}

/**
 * URL validation
 */
export function isValidUrl(url: string): boolean {
  try {
    new URL(url);
    return true;
  } catch {
    return false;
  }
}

/**
 * Number range validation
 */
export function isInRange(value: number, min: number, max: number): boolean {
  return value >= min && value <= max;
}

/**
 * Required field validation
 */
export function isRequired(value: any): boolean {
  if (value === null || value === undefined) return false;
  if (typeof value === 'string') return value.trim().length > 0;
  if (Array.isArray(value)) return value.length > 0;
  return true;
}

/**
 * Minimum length validation
 */
export function hasMinLength(value: string, minLength: number): boolean {
  return value.length >= minLength;
}

/**
 * Maximum length validation
 */
export function hasMaxLength(value: string, maxLength: number): boolean {
  return value.length <= maxLength;
}

/**
 * Numeric validation
 */
export function isNumeric(value: string): boolean {
  return /^\d+$/.test(value);
}

/**
 * Alphanumeric validation
 */
export function isAlphanumeric(value: string): boolean {
  return /^[a-zA-Z0-9]+$/.test(value);
}

/**
 * Date validation (ISO format)
 */
export function isValidDate(dateString: string): boolean {
  const date = new Date(dateString);
  return !isNaN(date.getTime());
}

/**
 * Future date validation
 */
export function isFutureDate(dateString: string): boolean {
  const date = new Date(dateString);
  const now = new Date();
  return date > now;
}

/**
 * Past date validation
 */
export function isPastDate(dateString: string): boolean {
  const date = new Date(dateString);
  const now = new Date();
  return date < now;
}

/**
 * Percentage validation (0-100)
 */
export function isValidPercentage(value: number): boolean {
  return isInRange(value, 0, 100);
}

/**
 * Stock ticker validation (B3 format)
 */
export function isValidB3Ticker(ticker: string): boolean {
  // B3 tickers: 4 letters + up to 2 numbers (e.g., PETR4, VALE3, MGLU3)
  return /^[A-Z]{4}\d{1,2}$/.test(ticker.toUpperCase());
}

/**
 * Money amount validation (positive number with up to 2 decimal places)
 */
export function isValidMoneyAmount(amount: string): boolean {
  return /^\d+(\.\d{1,2})?$/.test(amount) && parseFloat(amount) >= 0;
}

/**
 * Form validation helper
 */
export interface ValidationRule {
  validator: (value: any) => boolean;
  message: string;
}

export interface ValidationResult {
  isValid: boolean;
  errors: string[];
}

export function validateField(value: any, rules: ValidationRule[]): ValidationResult {
  const errors: string[] = [];

  for (const rule of rules) {
    if (!rule.validator(value)) {
      errors.push(rule.message);
    }
  }

  return {
    isValid: errors.length === 0,
    errors,
  };
}

/**
 * Common validation rules
 */
export const ValidationRules = {
  required: (message = 'Campo obrigatório'): ValidationRule => ({
    validator: isRequired,
    message,
  }),

  email: (message = 'E-mail inválido'): ValidationRule => ({
    validator: isValidEmail,
    message,
  }),

  minLength: (min: number, message?: string): ValidationRule => ({
    validator: (value: string) => hasMinLength(value, min),
    message: message || `Mínimo de ${min} caracteres`,
  }),

  maxLength: (max: number, message?: string): ValidationRule => ({
    validator: (value: string) => hasMaxLength(value, max),
    message: message || `Máximo de ${max} caracteres`,
  }),

  numeric: (message = 'Deve conter apenas números'): ValidationRule => ({
    validator: isNumeric,
    message,
  }),

  cpf: (message = 'CPF inválido'): ValidationRule => ({
    validator: isValidCPF,
    message,
  }),

  phone: (message = 'Telefone inválido'): ValidationRule => ({
    validator: isValidPhone,
    message,
  }),

  url: (message = 'URL inválida'): ValidationRule => ({
    validator: isValidUrl,
    message,
  }),
};
