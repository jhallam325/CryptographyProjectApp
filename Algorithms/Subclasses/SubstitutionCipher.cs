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
        int intKey;

        public string Encrypt(string plaintext, string key)
        {
            bool keyIsCorrect = KeyIsCorrect(key);
            if (keyIsCorrect)
            {
                // This takes out all of the spaces in the plaintext.
                trimmedText = TrimText(plaintext);

                // This removes all of the symbols from the text and makes every
                // letter a capital letter
                filteredText = FilterText(trimmedText);

                // This takes the filtered text with no spaces, and shifts the ascii
                // values by the key value. I've included numbers so this is all mod
                // 35, 10 numerical digits and 26 letters, giving 36 symbols or 0-35
                // I redefine the values, shift them, and then return them to the
                // standard values so the ciphertext only contains numbers and
                // letters
                // I've since removed letters for the affine cipher
                ciphertext = ShiftASCIIValuesByKey(filteredText, intKey);


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
                // This takes out all of the spaces in the plaintext.
                trimmedText = TrimText(ciphertext);

                // This removes all of the symbols from the text and makes every
                // letter a capital letter
                filteredText = FilterText(trimmedText);

                // This takes the filtered text with no spaces, and shifts the ascii
                // values by the key value. I've included numbers so this is all mod
                // 35, 10 numerical digits and 26 letters, giving 36 symbols or 0-35
                // I redefine the values, shift them, and then return them to the
                // standard values so the ciphertext only contains numbers and
                // letters
                // I've since removed letters for the affine cipher
                plaintext = ShiftASCIIValuesByKey(filteredText, intKey);


                return plaintext;
            }
            else
            {
                // throw an exception
                return "The key you used is invalid.";
            }

            // The Writer (or reader, I forget which), was introducing /r and /n
            // to the file so I need to filter that stuff out.
            trimmedText = TrimText(ciphertext);
            filteredText = FilterText(trimmedText);

            return ShiftASCIIValuesByKey(filteredText, -intKey);
        }

        private string ShiftASCIIValuesByKey(string text, int key)
        {
            int intPlaceHolder;
            char charPlaceHolder;
            string newText = "";
            foreach (char character in text)
            {
                intPlaceHolder = BringASCIINumberToZero(character);
                intPlaceHolder += key;
                intPlaceHolder = intPlaceHolder % Globals.modulus;
                charPlaceHolder = ReturnASCIINumberToOriginal(intPlaceHolder);
                newText += charPlaceHolder;
            }
            return newText;
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
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'B')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'C')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'D')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'E')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'F')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'G')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'H')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'I')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'J')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'K')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'L')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'M')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'N')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'O')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'P')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'Q')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'R')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'S')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'T')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'U')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'V')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'W')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'X')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'Y')
                {
                    counts[i]++;
                }
                else if (upperCaseKey[i] == 'Z')
                {
                    counts[i]++;
                }
            }

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
    }
}