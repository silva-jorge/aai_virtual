import { encryptData, decryptData } from '../../shared/utils/encryption';

/**
 * Service for managing encrypted local storage
 * Encrypts sensitive data before storing it in browser's local storage
 */

const ENCRYPTION_KEY = 'aai_encrypted_data_v1';
const PIN_HASH_KEY = 'aai_pin_hash';

interface StorageItem {
  encrypted: string;
  timestamp: number;
}

/**
 * Save encrypted data to local storage
 * @param key - The storage key
 * @param data - The data to save (will be encrypted)
 * @param pin - The PIN/password for encryption
 */
export async function saveEncrypted<T>(key: string, data: T, pin: string): Promise<void> {
  try {
    const encrypted = await encryptData(data, pin);
    const item: StorageItem = {
      encrypted,
      timestamp: Date.now(),
    };
    localStorage.setItem(`${ENCRYPTION_KEY}_${key}`, JSON.stringify(item));
  } catch (error) {
    console.error(`Failed to save encrypted data for key: ${key}`, error);
    throw error;
  }
}

/**
 * Load encrypted data from local storage
 * @param key - The storage key
 * @param pin - The PIN/password for decryption
 * @returns The decrypted data
 */
export async function loadEncrypted<T>(key: string, pin: string): Promise<T | null> {
  try {
    const storageKey = `${ENCRYPTION_KEY}_${key}`;
    const itemJson = localStorage.getItem(storageKey);

    if (!itemJson) {
      return null;
    }

    const item: StorageItem = JSON.parse(itemJson);
    return await decryptData<T>(item.encrypted, pin);
  } catch (error) {
    console.error(`Failed to load encrypted data for key: ${key}`, error);
    throw error;
  }
}

/**
 * Remove encrypted data from local storage
 * @param key - The storage key
 */
export function removeEncrypted(key: string): void {
  try {
    localStorage.removeItem(`${ENCRYPTION_KEY}_${key}`);
  } catch (error) {
    console.error(`Failed to remove data for key: ${key}`, error);
    throw error;
  }
}

/**
 * Clear all encrypted data from local storage
 */
export function clearAllEncrypted(): void {
  try {
    const keys = Object.keys(localStorage);
    keys.forEach((key) => {
      if (key.startsWith(ENCRYPTION_KEY)) {
        localStorage.removeItem(key);
      }
    });
  } catch (error) {
    console.error('Failed to clear encrypted data', error);
    throw error;
  }
}

/**
 * Save PIN hash for verification without storing the PIN itself
 * @param pinHash - The hashed PIN
 */
export function savePinHash(pinHash: string): void {
  try {
    localStorage.setItem(PIN_HASH_KEY, pinHash);
  } catch (error) {
    console.error('Failed to save PIN hash', error);
    throw error;
  }
}

/**
 * Get stored PIN hash
 * @returns The PIN hash or null if not set
 */
export function getPinHash(): string | null {
  try {
    return localStorage.getItem(PIN_HASH_KEY);
  } catch (error) {
    console.error('Failed to get PIN hash', error);
    return null;
  }
}

/**
 * Check if PIN is set
 * @returns True if a PIN hash exists
 */
export function isPinSet(): boolean {
  return getPinHash() !== null;
}

/**
 * Check available storage space
 * @returns Available storage in bytes
 */
export function getAvailableStorage(): number {
  try {
    const test = '__localStorage_test__';
    localStorage.setItem(test, '1');
    localStorage.removeItem(test);
    // Most browsers allow 5-10MB
    return 5 * 1024 * 1024; // 5MB
  } catch {
    return 0;
  }
}

/**
 * Get size of stored encrypted data
 * @returns Total size in bytes
 */
export function getStoredDataSize(): number {
  try {
    let size = 0;
    const keys = Object.keys(localStorage);
    keys.forEach((key) => {
      if (key.startsWith(ENCRYPTION_KEY)) {
        const value = localStorage.getItem(key);
        if (value) {
          size += value.length;
        }
      }
    });
    return size;
  } catch (error) {
    console.error('Failed to calculate storage size', error);
    return 0;
  }
}
