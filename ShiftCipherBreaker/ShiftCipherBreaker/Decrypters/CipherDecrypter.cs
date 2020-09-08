using System;
using System.Collections.Generic;
using System.Text;

namespace ShiftCipherBreaker
{
    /// <summary>
    /// Defines an abstract class for class that decrypts ciphered text
    /// </summary>
    public abstract class CipherDecrypter
    {
        /// <summary>
        /// Attempts to decrypt cipher. Returns decrypted text if cipher is decrypted. Otherwise returns String.Empty
        /// </summary>
        /// <param name="cipher">ciphered text</param>
        /// <returns></returns>
        public abstract string Decrypt(in string cipher);

        /// <summary>
        /// Sanitize ciphered text before attempting to decrypt
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns></returns>
        protected abstract string Sanitize(in string cipher);
    }
}
