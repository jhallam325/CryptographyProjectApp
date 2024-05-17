using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithms.GlobalVariables;
using Algorithms.MainClasses;

namespace Algorithms.Subclasses
{
    public class AffineCipher : Cipher
    {
        string plaintext;
        string ciphertext;
        string trimmedText;
        string filteredText;
        int intKey;
        //char charPlaceHolder;
        //int intPlaceHolder = 0;

        // The very first thing I have to do is check the gcd of 2 characters of the string.



        private int GCD(int number1, int number2)
        {
            int remainder;

            while (number2 != 0)
            {
                remainder = number1 % number2;
                number1 = number2;
                number2 = remainder;
            }

            return number1;
        }

        public string Encrypt(string plaintext, string key)
        {
            // Check the GCD of teh two numbers in the key
            if (int.TryParse(key, out intKey))
            {
                intKey = int.Parse(key) % Globals.modulus;
            }
            else
            {
                Console.WriteLine("Need to convert letters to numbers");
            }

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
            ciphertext = ShiftASCIIValuesByKey(filteredText, intKey);
            /*
            foreach (char character in filteredText)
            {
                intPlaceHolder = BringASCIINumberToZero(character);
                intPlaceHolder += key;
                intPlaceHolder = intPlaceHolder % 35;
                charPlaceHolder = ReturnASCIINumberToOriginal(intPlaceHolder);
                ciphertext += charPlaceHolder;
            }
            */

            return ciphertext;
        }

        public string Decrypt(string ciphertext, string key)
        {
            //***************************************************************************************************************
            if (int.TryParse(key, out intKey))
            {
                intKey = int.Parse(key) % 36;
            }
            else
            {
                Console.WriteLine("Need to convert letters to numbers");
            }
            return ShiftASCIIValuesByKey(ciphertext, -intKey);
            //So, right now, as a letter gets mapped to a number, I think the shifting is breaking that.
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
                intPlaceHolder = intPlaceHolder % 36;
                charPlaceHolder = ReturnASCIINumberToOriginal(intPlaceHolder);
                newText += charPlaceHolder;
            }
            return newText;
        }
    }
}
