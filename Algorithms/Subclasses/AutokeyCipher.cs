using Algorithms.GlobalVariables;
using Algorithms.Interfaces;
using Algorithms.MainClasses;
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
                return "Key is invalid.";
            }

            ciphertext = string.Empty;
            trimmedText = TrimText(plaintext);
            filteredText = FilterText(trimmedText);

            asciiValues = GetASCIIValuesOfString(filteredText);
            keyStream = BuildKeyStream(key[0], asciiValues);
            
            for (int i = 0; i < keyStream.Length; i++)
            {
                int encryptedNumber = (asciiValues[i] + keyStream[i]) % Globals.modulus;
                ciphertext += (char) ReturnASCIINumberToOriginal(encryptedNumber);
            }


            return ciphertext;

        }

        public string Decrypt(string ciphertext, string key)
        {
            throw new NotImplementedException();
        }

        public bool KeyIsCorrect(string key)
        {
            if (key.Length != 1)
            {
                return false;
            }

            char temp = key[0];
            if (char.IsLetter(temp))
            {
                return true;
            }

            return false;
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
            keyStream[0] = (int)(key[0] - 65);
            for (int i = 1; i < keyStream.Length; i++)
            {
                keyStream[i] = asciiValues[i - 1];
            }
            return keyStream;
        }
    }
}
