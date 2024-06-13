using Algorithms.GlobalVariables;
using Algorithms.Interfaces;
using Algorithms.MainClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Subclasses
{
    public class PermutationCipher : Cipher, ICipher
    {
        string plaintext;
        string ciphertext;
        string trimmedText;
        string filteredText;
        string blockOfPlaintext;
        string blockOfCipertext;
        string[] keyValues;
        int[] intKeyValues;
        int[] inverseKeyValues;
        char[] plaintextCharacters;
        char[] ciphertextCharacters;
        
        int keySize;
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public string Encrypt(string plaintext, string key)
        {
            if (!KeyIsCorrect(key))
            {
                return "Key is incorrect";
            }

            trimmedText = TrimText(plaintext);
            filteredText = FilterText(trimmedText);
            keySize = keyValues.Length;

            // I might not need this. This just adds characters to the end of the string so that the key and plaintext fit perfectly.
            string paddedFilteredText = PadString(filteredText, keySize);

            // Now I need to center the key at 0 to match indices and make it mod 26
            intKeyValues = new int[keySize];
            for (int i = 0; i < keySize; i++)
            {
                if (!int.TryParse(keyValues[i], out intKeyValues[i]))
                {
                    return "Even though I checked, there was still an error in the key";
                }
                intKeyValues[i] = (intKeyValues[i] - 1) % Globals.modulus;
            }

            ciphertext = string.Empty;
            string[] blocksOfPlaintext = Split(paddedFilteredText, keySize);

            for (int i = 0; i < blocksOfPlaintext.Length; i++)
            {
                blockOfCipertext = SubstituteCharactersEncrypt(blocksOfPlaintext[i], intKeyValues);
                ciphertext += blockOfCipertext;
            }
            return ciphertext;
        }

        private string SubstituteCharactersEncrypt(string blockOfText, int[] key)
        {
            // I need to work on this to find the correct algorithm
            
            //int characterValue = BringASCIINumberToZero(c);
            ciphertextCharacters = new char[blockOfText.Length];
            for (int i = 0; i < blockOfText.Length; i++)
            {
                
                int indexOfChar = GetIndexOfChar(key, i);
                ciphertextCharacters[indexOfChar] = blockOfText[i];
            }

            string result = string.Empty;
            for (int i = 0; i < ciphertextCharacters.Length; i++)
            {
                result += ciphertextCharacters[i];
            }

            return result;
        }

        public string Decrypt(string ciphertext, string key)
        {
            if (!KeyIsCorrect(key))
            {
                return "Key is incorrect";
            }

            trimmedText = TrimText(ciphertext);
            filteredText = FilterText(trimmedText);
            keySize = keyValues.Length;

            // I might not need this. This just adds characters to the end of the string so that the key and plaintext fit perfectly.
            string paddedFilteredText = PadString(filteredText, keySize);

            // Now I need to center the key at 0 to match indices and make it mod 26
            intKeyValues = new int[keySize];
            for (int i = 0; i < keySize; i++)
            {
                if (!int.TryParse(keyValues[i], out intKeyValues[i]))
                {
                    return "Even though I checked, there was still an error in the key";
                }
                intKeyValues[i] = (intKeyValues[i] - 1) % Globals.modulus;
            }

            // Now I need to fiind the inverse of the key to decrypt the ciphertext.
            inverseKeyValues = new int[keySize];


            for (int i = 0; i < keySize; i++)
            {

                inverseKeyValues[i] = GetIndexOfChar(intKeyValues, i);
            }




            //============================================================================================================================================
            plaintext = string.Empty;
            string[] blocksOfCiphertext = Split(paddedFilteredText, keySize);

            for (int i = 0; i < blocksOfCiphertext.Length; i++)
            {
                blockOfPlaintext = SubstituteCharactersEncrypt(blocksOfCiphertext[i], inverseKeyValues);
                plaintext += blockOfPlaintext;
            }
            return plaintext;
        }

        

        public bool KeyIsCorrect(string key)
        {
            key = key.Trim();
            keyValues = key.Split(',');
            keySize = keyValues.Length;

            // First check to make sure every element of keyValues is a number
            for (int i = 0; i < keyValues.Length; i++)
            {
                foreach (char c in keyValues[i])
                {
                    if (!char.IsDigit(c))
                    {
                        return false;
                    }
                }
            }

            // Now I need to center the key at 0 to match indices and make it mod Globals.modulus
            intKeyValues = new int[keySize];
            for (int i = 0; i < keySize; i++)
            {
                if (!int.TryParse(keyValues[i], out intKeyValues[i]))
                {
                    return false;
                }
                intKeyValues[i] = (intKeyValues[i] - 1) % Globals.modulus;
            }

            // Every character in each of the elemenets of blocks is a number
            // now I need to see if it contains sequential numbers starting at 0 and going to the length of the keys
            // This starts at 0 since I centered the keys at 0 by subtracting 1
            for (int i = 0; i < keySize; i++ )
            {
                if (!intKeyValues.Contains(i))
                {
                    // throw exception
                    // The key doesn't contain every number between 1 and keySize, inclusive
                    return false;
                }
            }
            return true;
        }

        public int GetIndexOfChar(int[] key, int indexToFind)
        {
            for (int i = 0; i < key.Length; i++)
            {
                if (key[i] == indexToFind)
                {
                    return i;
                }
            }
            // throw an exception
            Console.WriteLine("GetIndexOfChar didn't match the input value to a key value");
            return -1;
        }
    }
}
