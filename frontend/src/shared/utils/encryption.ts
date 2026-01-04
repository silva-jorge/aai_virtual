/**
 * Encryption utilities for secure local storage of sensitive data
 * Uses Web Crypto API for AES-256-GCM encryption
 */

/**
 * Derive a key from a PIN/password using PBKDF2
 * @param password - The PIN or password
 * @param salt - The salt for key derivation (must be 16 bytes)
 * @returns The derived encryption key
 */
async function deriveKey(password: string, salt: Uint8Array): Promise<CryptoKey> {
  const encoder = new TextEncoder();
  const passwordBuffer = encoder.encode(password);

  const baseKey = await window.crypto.subtle.importKey(
    'raw',
    passwordBuffer,
    'PBKDF2',
    false,
    ['deriveKey']
  );

  return window.crypto.subtle.deriveKey(
    {
      name: 'PBKDF2',
      salt: salt,
      iterations: 100000,
      hash: 'SHA-256',
    },
    baseKey,
    { name: 'AES-GCM', length: 256 },
    false,
    ['encrypt', 'decrypt']
  );
}

/**
 * Encrypt data using AES-256-GCM
 * @param data - The data to encrypt (as JSON string)
 * @param password - The PIN or password for encryption
 * @returns Base64 encoded encrypted data with IV and salt
 */
export async function encryptData(data: unknown, password: string): Promise<string> {
  try {
    const encoder = new TextEncoder();
    const decoder = new TextDecoder();

    // Convert data to JSON string
    const jsonString = JSON.stringify(data);
    const plaintext = encoder.encode(jsonString);

    // Generate random salt and IV
    const salt = window.crypto.getRandomValues(new Uint8Array(16));
    const iv = window.crypto.getRandomValues(new Uint8Array(12));

    // Derive key from password
    const key = await deriveKey(password, salt);

    // Encrypt the data
    const ciphertext = await window.crypto.subtle.encrypt(
      { name: 'AES-GCM', iv: iv },
      key,
      plaintext
    );

    // Combine salt + iv + ciphertext and encode to base64
    const combined = new Uint8Array(salt.length + iv.length + ciphertext.byteLength);
    combined.set(salt, 0);
    combined.set(iv, salt.length);
    combined.set(new Uint8Array(ciphertext), salt.length + iv.length);

    // Convert to base64 for storage
    const binaryString = String.fromCharCode.apply(null, Array.from(combined));
    return btoa(binaryString);
  } catch (error) {
    console.error('Encryption failed:', error);
    throw new Error('Failed to encrypt data');
  }
}

/**
 * Decrypt data encrypted with encryptData
 * @param encryptedData - Base64 encoded encrypted data
 * @param password - The PIN or password for decryption
 * @returns The decrypted data (parsed from JSON)
 */
export async function decryptData<T = unknown>(
  encryptedData: string,
  password: string
): Promise<T> {
  try {
    const decoder = new TextDecoder();

    // Decode from base64
    const binaryString = atob(encryptedData);
    const bytes = new Uint8Array(binaryString.length);
    for (let i = 0; i < binaryString.length; i++) {
      bytes[i] = binaryString.charCodeAt(i);
    }

    // Extract salt, IV, and ciphertext
    const salt = bytes.slice(0, 16);
    const iv = bytes.slice(16, 28);
    const ciphertext = bytes.slice(28);

    // Derive key from password
    const key = await deriveKey(password, salt);

    // Decrypt the data
    const plaintext = await window.crypto.subtle.decrypt(
      { name: 'AES-GCM', iv: iv },
      key,
      ciphertext
    );

    // Decode and parse JSON
    const jsonString = decoder.decode(plaintext);
    return JSON.parse(jsonString) as T;
  } catch (error) {
    console.error('Decryption failed:', error);
    throw new Error('Failed to decrypt data - wrong password or corrupted data');
  }
}

/**
 * Hash a PIN using SHA-256 (for comparison without storing original PIN)
 * @param pin - The PIN to hash
 * @returns Hex-encoded SHA-256 hash
 */
export async function hashPin(pin: string): Promise<string> {
  const encoder = new TextEncoder();
  const data = encoder.encode(pin);
  const hashBuffer = await window.crypto.subtle.digest('SHA-256', data);

  // Convert to hex string
  const hashArray = Array.from(new Uint8Array(hashBuffer));
  return hashArray.map((b) => b.toString(16).padStart(2, '0')).join('');
}

/**
 * Verify a PIN against its hash
 * @param pin - The PIN to verify
 * @param hash - The hash to verify against
 * @returns True if PIN matches hash
 */
export async function verifyPin(pin: string, hash: string): Promise<boolean> {
  const pinHash = await hashPin(pin);
  return pinHash === hash;
}
