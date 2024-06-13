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
                // This ensures the second key is between 0-Global.modulus
                secondKey = secondKey % Globals.modulus;

                // This takes out all of the spaces in the plaintext.
                trimmedText = TrimText(plaintext);

                // This removes all of the symbols from the text and makes every
                // letter a capital letter
                filteredText = FilterText(trimmedText);

                // This takes the filtered text with no spaces, and shifts the ascii
                // values by the key value.
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

            // Check the GCD of the two numbers in the key
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
                // values by the key value.
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
                intPlaceHolder *= MultiplicativeInverse(firstkey, Globals.modulus);
                intPlaceHolder = intPlaceHolder % Globals.modulus;
                charPlaceHolder = ReturnASCIINumberToOriginal(intPlaceHolder);
                newText += charPlaceHolder;
            }
            return newText;
        }

        public bool KeyIsCorrect(string key)
        {
            if (int.TryParse(key, out firstKey))
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
    }
}
