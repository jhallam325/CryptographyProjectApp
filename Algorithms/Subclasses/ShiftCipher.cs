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
    public class ShiftCipher : Cipher, ICipher
    {
        string plaintext;
        string ciphertext;
        string trimmedText;
        string filteredText;
        int intKey;

        public string Encrypt(string plaintext, string key)
        {
            if (!KeyIsCorrect(key))
            {
                return "The key is invalid. Please choose a whole number as your key.";
            }

            // This takes out all of the spaces in the plaintext.
            trimmedText = TrimText(plaintext);

            // This removes all of the symbols from the text and makes every
            // letter a capital letter
            filteredText = FilterText(trimmedText);

            // This takes the filtered text with no spaces, and shifts the ascii
            // values by the key value.
            ciphertext = ShiftASCIIValuesByKey(filteredText, intKey);

            return ciphertext;

        }

        public string Decrypt(string ciphertext, string key)
        {
            if (KeyIsCorrect(key))
            {
                return "The key is invalid. Please choose a whole number as your key.";
            }

            trimmedText = TrimText(ciphertext);
            filteredText = FilterText(trimmedText);

            plaintext = ShiftASCIIValuesByKey(filteredText, -intKey);
            return plaintext;

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
            if (int.TryParse(key, out intKey))
            {
                intKey = int.Parse(key) % Globals.modulus;
                return true;
            }
            else
            {
                Console.WriteLine("Need to convert letters to numbers");
                return false;
            }
        }
    }
}
