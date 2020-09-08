using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ShiftCipherBreaker
{
    public class ShiftCipherDecrypter : CipherDecrypter
    {
        //List holding all patterns to match for
        public List<string> patternList = new List<string>();

        public ShiftCipherDecrypter(string patternFilePath)
        {
            patternList = File.ReadAllLines(patternFilePath).ToList();
        }

        /// <summary>
        /// Attempts to decrypt shift cipher using pattern matching
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns></returns>
        public override string Decrypt(in string cipher)
        {
            string cipherToDecrypt = Sanitize(cipher);
            //Contains number of matches (ie times a pattern is found in cipher) for particular shift value at index
            List<int> numberOfMatches = new List<int>();

            for (int shift = 0; shift < 26; shift++)
            {
                //Add number of matches found at shifted cipher at shift
                numberOfMatches.Add(PatternMatch(ShiftCipher(cipherToDecrypt, shift), patternList));
            }

            return ShiftCipher(cipherToDecrypt, numberOfMatches.IndexOf(numberOfMatches.Max()));
        }

        protected override string Sanitize(in string cipher)
        {
            string sanitized = new string(cipher);
            sanitized = sanitized.Replace(" ", "");
            sanitized = sanitized.Replace("\r", "");
            sanitized = sanitized.Replace("\n", "");
            sanitized = sanitized.ToLower();

            return sanitized;
        }

        /// <summary>
        /// Shifts characters of cipher right by shift value
        /// Note: In UTF-16, a is 97, z is 122
        /// </summary>
        /// <param name="shift">number of characters to shift cipher by</param>
        private string ShiftCipher(string cipher, int shift) {
            char[] text = cipher.ToCharArray();

            for (int index = 0; index < text.Length; index++)
            {
                if (text[index] + shift > 122)
                {
                    text[index] = (char)(text[index] - 122 + 96 + shift);
                }
                else
                {
                    text[index] = (char)(text[index] + shift);
                }
            }

            return new string(text);
        }

        /// <summary>
        /// Returns number of times a pattern is matched in cipher
        /// </summary>
        /// <param name="cipher">ciphered text</param>
        /// <param name="patternList">list of bigrams and trigrams to match for</param>
        /// <returns></returns>
        private int PatternMatch(string cipher, List<string> patternList)
        {
            int matches = 0;
            foreach (var pattern in patternList)
            {
                matches += Regex.Matches(cipher, pattern).Count;
            }
            
            return matches;
        }
    }
}
