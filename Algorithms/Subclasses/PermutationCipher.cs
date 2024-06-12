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
        string blockOfCipertext;
        //string upperKey;
        //string[] blocks;
        string[] keyValues;
        int[] intKeyValues;
        char[] ciphertextCharacters;
        char[] plaintextCharacters;
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

            /*
             * I don't think that I can input a single character and a single key value
             * I think I need to input a string of characters the size of the the key
             * and the entire key.
             */
            // This makes a character array the size of the plaintext because 
            //ciphertextCharacters = new char[paddedFilteredText.Length];
            //int counter = 0;
            //
            //while (counter < paddedFilteredText.Length)
            //{
            //    for (int i = 0; i < blocksOfPlaintext.Length; i++)
            //    {
            //        blockOfCipertext = SubstituteCharacterEncrypt(blocksOfPlaintext[counter + i], intKeyValues);
            //        ciphertext += blockOfCipertext;
            //    }
            //    counter += intKeyValues.Length;
            //}

            for (int i = 0; i < blocksOfPlaintext.Length; i++)
            {
                blockOfCipertext = SubstituteCharacterEncrypt(blocksOfPlaintext[i], intKeyValues);
                ciphertext += blockOfCipertext;
            }




            return ciphertext;
        }

        private string SubstituteCharacterEncrypt(string blockOfText, int[] key)
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
            throw new NotImplementedException();
        }

        

        public bool KeyIsCorrect(string key)
        {
            key = key.Trim();
            keyValues = key.Split(',');
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
            
            // Every character in each of the elemenets of blocks is a number
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
