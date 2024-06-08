using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Algorithms.GlobalVariables;
using Algorithms.Interfaces;
using Algorithms.MainClasses;

namespace Algorithms.Subclasses
{
    public class AffineCipher : Cipher, ICipher
    {
        string plaintext;
        string ciphertext;
        string trimmedText;
        string filteredText;
        string[] keys;
        int firstKey;
        int secondKey;
        
        public string Encrypt(string plaintext, string key)
        {
            // Here I need to split key = 3,10 into a = 3 and b = 10
            keys = key.Split(',', 2);

            // Check the GCD of teh two numbers in the key
            if (KeyIsCorrect(keys[0]) && int.TryParse(keys[1], out secondKey))
            {
                // This ensures the second key is between 0-25
                secondKey = secondKey % Globals.modulus;

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
                ciphertext = ShiftASCIIValuesByKeysEncrypt(filteredText, firstKey, secondKey);
                return ciphertext;
            }
            else
            {
               return $"Invalid key. GCD of 1st key and {Globals.modulus} must be 1.\n" +
                        "Allowed keys are: 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, & 25\n" +
                        "The second key must be a number between 0 and 25\n" +
                        "Please enter the keys with only a comma between them: 3,10";
            }
        }

        public string Decrypt(string ciphertext, string key)
        {
            // Here I need to split key = 3,10 into a = 3 and b = 10
            keys = key.Split(',', 2);

            // Check the GCD of teh two numbers in the key
            if (KeyIsCorrect(keys[0]) && int.TryParse(keys[1], out secondKey))
            {
                // This ensures the second key is between 0-25
                secondKey = secondKey % Globals.modulus;

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
                plaintext = ShiftASCIIValuesByKeysDecrypt(filteredText, firstKey, secondKey);
                return plaintext;
            }
            else
            {
                return $"Invalid key. GCD of 1st key and {Globals.modulus} must be 1.\n" +
                         "Allowed keys are: 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, & 25\n" +
                         "The second key must be a number between 0 and 25\n" +
                         "Please enter the keys with only a comma between them: 3,10";
            }
        }

        private string ShiftASCIIValuesByKeysEncrypt(string text, int firstkey, int secondKey)
        {
            int intPlaceHolder;
            char charPlaceHolder;
            string newText = string.Empty;
            foreach (char character in text)
            {
                intPlaceHolder = BringASCIINumberToZero(character);
                intPlaceHolder *= firstkey;
                intPlaceHolder += secondKey;
                intPlaceHolder = intPlaceHolder % Globals.modulus;
                charPlaceHolder = ReturnASCIINumberToOriginal(intPlaceHolder);
                newText += charPlaceHolder;
            }
            return newText;
        }

        private string ShiftASCIIValuesByKeysDecrypt(string text, int firstkey, int secondKey)
        {
            int intPlaceHolder;
            char charPlaceHolder;
            string newText = string.Empty;
            foreach (char character in text)
            {
                intPlaceHolder = BringASCIINumberToZero(character);
                intPlaceHolder -= secondKey;
                intPlaceHolder *= MultiplicativeInverse(Globals.modulus, firstkey);
                intPlaceHolder = intPlaceHolder % Globals.modulus;
                charPlaceHolder = ReturnASCIINumberToOriginal(intPlaceHolder);
                newText += charPlaceHolder;
            }
            return newText;
        }

        public bool KeyIsCorrect(string key)
        {
            if (int.TryParse((string)key, out firstKey))
            {
                firstKey = int.Parse(key) % Globals.modulus;
                if (GCD(firstKey, Globals.modulus) == 1)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Invalid key. GCD of key and {Globals.modulus} must be 1.\n" +
                        $"Allowed keys are: 1, 3, 5, 7, 9, 11, 15, 17, 19, 21, 23, & 25");
                    return false;
                }
                
            }
            else
            {
                Console.WriteLine("Invalid key.");
                return false;
            }
        }

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

        private int MultiplicativeInverse(int modulus, int wantedInverse)
        {
            int a0 = modulus;
            int b0 = wantedInverse;
            int t0 = 0;
            int t = 1;
            int quotient = (int) MathF.Floor((float) a0 / b0);
            int remainder = a0 - quotient * b0;
            while (remainder > 0)
            {
                int temp = (t0 - quotient * t) % modulus;
                t0 = t;
                t = temp;
                a0 = b0;
                b0 = remainder;
                quotient = (int)MathF.Floor((float)a0 / b0);
                remainder = a0 - quotient * b0;
            }
            if (b0 == 1)
            {
                // b^(-1) mod a = t
                return t;
            }
            else
            {
                // There is no b^(-1) mod a
                return -1;
            }
        }
    }
}
