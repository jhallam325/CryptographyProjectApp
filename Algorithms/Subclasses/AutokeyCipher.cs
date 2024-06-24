using Algorithms.GlobalVariables;
using Algorithms.Interfaces;
using Algorithms.MainClasses;
using Algorithms.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Subclasses
{
    public class AutokeyCipher : Cipher, ICipher
    {
        string plaintext;
        string ciphertext;
        string trimmedText;
        string filteredText;
        int[] asciiValues;
        int[] keyStream;

        public string Encrypt(string plaintext, string key)
        {
            if(!KeyIsCorrect(key))
            {
                throw new IncorrectKeyException();
            }

            key = key.ToUpper();

            ciphertext = string.Empty;
            trimmedText = TrimText(plaintext);
            filteredText = FilterText(trimmedText);

            asciiValues = GetASCIIValuesOfString(filteredText);
            keyStream = BuildKeyStream(key[0], asciiValues);
            
            for (int i = 0; i < keyStream.Length; i++)
            {
                int encryptedNumber = (asciiValues[i] + keyStream[i]) % Globals.modulus;
                ciphertext += ReturnASCIINumberToOriginal(encryptedNumber);
            }

            return ciphertext;
        }

        public string Decrypt(string ciphertext, string key)
        {
            if (!KeyIsCorrect(key))
            {
                throw new IncorrectKeyException();
            }

            key = key.ToUpper();

            plaintext = string.Empty;
            trimmedText = TrimText(ciphertext);
            filteredText = FilterText(trimmedText);

            asciiValues = GetASCIIValuesOfString(filteredText);

            keyStream = new int[asciiValues.Length];
            keyStream[0] = BringASCIINumberToZero(key[0]);

            for (int i = 0; i < keyStream.Length; i++)
            {
                int decryptedNumber = (asciiValues[i] - keyStream[i]) % Globals.modulus;
                plaintext += ReturnASCIINumberToOriginal(decryptedNumber);

                // We use the plaintext character as the next character in the key stream
                if (i < keyStream.Length - 1)
                {
                    keyStream[i + 1] = decryptedNumber;
                }
            }


            return plaintext;
        }

        public bool KeyIsCorrect(string key)
        {
            if (key.Length != 1)
            {
                throw new IncorrectKeyException("The key must be a single character only");
            }

            char temp = key[0];
            if (!char.IsLetter(temp))
            {
                throw new IncorrectKeyException("The key must be a single letter only");
            }

            return true;
        }

        private int[] GetASCIIValuesOfString(string text)
        {
            int[] asciiValues = new int[text.Length];
            int index = 0;
            foreach(char c in text)
            {
                asciiValues[index++] = BringASCIINumberToZero(c);
            }
            return asciiValues;
        }

        private int[] BuildKeyStream(char key, int[] asciiValues)
        {
            int[] keyStream = new int[asciiValues.Length];
            keyStream[0] = BringASCIINumberToZero(key);
            for (int i = 1; i < keyStream.Length; i++)
            {
                keyStream[i] = asciiValues[i - 1];
            }
            return keyStream;
        }
    }
}
