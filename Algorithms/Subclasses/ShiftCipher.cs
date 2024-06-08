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
            // This is a test to find the correct project in git
            /*
             * For the key, I need to add each letter of the string to a List using a 
             * foreach (char c in key) loop. Then I can see if keyList.Contains(a)
             * || keyList.Contains(b), ... for each letter of the alphabet. 
             * That will tell me that every single letter is in the alphabet and I can 
             * check that the count is = to 26. That will confirm that the key contains
             * every letter, once.
             * 
             * Now I just need to figure out how to map the letters and use the key.
             */

            //if (int.TryParse(key, out intKey))
            //{
            //    intKey = int.Parse(key) % Globals.modulus;
            //}
            //else
            //{
            //    Console.WriteLine("Need to convert letters to numbers");
            //}

            if (KeyIsCorrect(key))
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
                return "The key is invalid. Please choose a whole number as your key.";
            }
            
        }

        public string Decrypt(string ciphertext, string key)
        {
            //if (int.TryParse(key, out intKey))
            //{
            //    intKey = int.Parse(key) % Globals.modulus;
            //}
            //else
            //{
            //    Console.WriteLine("Need to convert letters to numbers");
            //}

            if (KeyIsCorrect(key))
            {
                // The Writer (or reader, I forget which), was introducing /r and /n
                // to the file so I need to filter that stuff out.
                trimmedText = TrimText(ciphertext);
                filteredText = FilterText(trimmedText);

                plaintext = ShiftASCIIValuesByKey(filteredText, -intKey);
                return plaintext;
            }
            else
            {
                return "The key is invalid. Please choose a whole number as your key.";
            }
            
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
            if (int.TryParse((string)key, out intKey))
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
