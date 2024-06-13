using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.GlobalVariables;
using Algorithms.Interfaces;
using Algorithms.MainClasses;

namespace Algorithms.Subclasses
{
    public class SubstitutionCipher : Cipher, ICipher
    {
        string plaintext;
        string ciphertext;
        string trimmedText;
        string filteredText;
        string upperKey;
        char[] ciphertextCharacters;
        char[] plaintextCharacters;
        string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";


        public string Encrypt(string plaintext, string key)
        {
            bool keyIsCorrect = KeyIsCorrect(key);
            if (keyIsCorrect)
            {
                upperKey = key.ToUpper();

                // This takes out all of the spaces in the plaintext.
                trimmedText = TrimText(plaintext);

                // This removes all of the symbols from the text and makes every
                // letter a capital letter
                filteredText = FilterText(trimmedText);

                // This makes a character array the size of the plaintext because 
                ciphertextCharacters = new char[filteredText.Length];

                for (int i = 0; i < filteredText.Length; i++)
                {
                    ciphertextCharacters[i] = SubstituteCharacterEncrypt(filteredText[i]);
                    ciphertext += ciphertextCharacters[i];
                }

                return ciphertext;
            }
            else
            {
                // throw an exception
                return "The key you used is invalid.";
            }
        }

        public string Decrypt(string ciphertext, string key)
        {
            bool keyIsCorrect = KeyIsCorrect(key);
            if (keyIsCorrect)
            {
                upperKey = key.ToUpper();

                // This takes out all of the spaces in the plaintext.
                trimmedText = TrimText(ciphertext);

                // This removes all of the symbols from the text and makes every
                // letter a capital letter
                filteredText = FilterText(trimmedText);
                
                // This makes a character array the size of the plaintext because 
                plaintextCharacters = new char[filteredText.Length];

                for (int i = 0; i < filteredText.Length; i++)
                {
                    plaintextCharacters[i] = SubstituteCharacterDecrypt(filteredText[i]);
                    plaintext += plaintextCharacters[i];
                }

                return plaintext;
            }
            else
            {
                // throw an exception
                return "The key you used is invalid.";
            }
        }

        public bool KeyIsCorrect(string key)
        {
            // The key here has to chack for letters A-Z
            if (key.Length != 26)
            {
                // Throw an exception
                return false;
            }

            // Check for symbols and numbers in the key
            for (int i = 0; i < key.Length; i++)
            {
                if (Char.IsDigit(key[i]) || Char.IsSymbol(key[i]) || Char.IsWhiteSpace(key[i]))
                {
                    // Throw an exception
                    return false;
                }
            }

            // Check for each letter in the alphabet is here only once.
            // Create an array of 0's that will count each letter in the alphabet
            int[] counts = new int[key.Length];
            string upperCaseKey = key.ToUpper();

            for (int i = 0; i < upperCaseKey.Length; i++)
            {
                if (upperCaseKey[i] == 'A')
                {
                    counts[0]++;
                }
                else if (upperCaseKey[i] == 'B')
                {
                    counts[1]++;
                }
                else if (upperCaseKey[i] == 'C')
                {
                    counts[2]++;
                }
                else if (upperCaseKey[i] == 'D')
                {
                    counts[3]++;
                }
                else if (upperCaseKey[i] == 'E')
                {
                    counts[4]++;
                }
                else if (upperCaseKey[i] == 'F')
                {
                    counts[5]++;
                }
                else if (upperCaseKey[i] == 'G')
                {
                    counts[6]++;
                }
                else if (upperCaseKey[i] == 'H')
                {
                    counts[7]++;
                }
                else if (upperCaseKey[i] == 'I')
                {
                    counts[8]++;
                }
                else if (upperCaseKey[i] == 'J')
                {
                    counts[9]++;
                }
                else if (upperCaseKey[i] == 'K')
                {
                    counts[10]++;
                }
                else if (upperCaseKey[i] == 'L')
                {
                    counts[11]++;
                }
                else if (upperCaseKey[i] == 'M')
                {
                    counts[12]++;
                }
                else if (upperCaseKey[i] == 'N')
                {
                    counts[13]++;
                }
                else if (upperCaseKey[i] == 'O')
                {
                    counts[14]++;
                }
                else if (upperCaseKey[i] == 'P')
                {
                    counts[15]++;
                }
                else if (upperCaseKey[i] == 'Q')
                {
                    counts[16]++;
                }
                else if (upperCaseKey[i] == 'R')
                {
                    counts[17]++;
                }
                else if (upperCaseKey[i] == 'S')
                {
                    counts[18]++;
                }
                else if (upperCaseKey[i] == 'T')
                {
                    counts[19]++;
                }
                else if (upperCaseKey[i] == 'U')
                {
                    counts[20]++;
                }
                else if (upperCaseKey[i] == 'V')
                {
                    counts[21]++;
                }
                else if (upperCaseKey[i] == 'W')
                {
                    counts[22]++;
                }
                else if (upperCaseKey[i] == 'X')
                {
                    counts[23]++;
                }
                else if (upperCaseKey[i] == 'Y')
                {
                    counts[24]++;
                }
                else if (upperCaseKey[i] == 'Z')
                {
                    counts[25]++;
                }
            }

            // Check for duplicates of the same character
            for (int i = 0; i < counts.Length; i++)
            {
                if (counts[i] != 1)
                {
                    // throw exception
                    return false;
                }
            }
            return true;
        }

        private char SubstituteCharacterEncrypt(char c)
        {
            int indexOfChar = alphabet.IndexOf(c);
            return upperKey[indexOfChar];
        }

        private char SubstituteCharacterDecrypt(char c)
        {
            int indexOfChar = upperKey.IndexOf(c);
            return alphabet[indexOfChar];
        }
    }
}