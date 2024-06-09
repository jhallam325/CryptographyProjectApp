﻿using Algorithms.GlobalVariables;
using Algorithms.Interfaces;
using Algorithms.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Algorithms.Subclasses
{
    public class VigenereCipher : Cipher, ICipher
    {
        string plaintext;
        string ciphertext;
        string trimmedText;
        string filteredText;

        public string Encrypt(string plaintext, string key)
        {
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
                ciphertext = ShiftASCIIValuesByKeyEncrypt(filteredText, key);

                return ciphertext;
            }
            else
            {
                return "Your key is invalid";
            }
        }

        

        public string Decrypt(string ciphertext, string key)
        {
            if (KeyIsCorrect(key))
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
                plaintext = ShiftASCIIValuesByKeyDecrypt(filteredText, key);

                return plaintext;
            }
            else
            {
                return "Your key is invalid";
            }
        }
        
        public bool KeyIsCorrect(string key)
        {
            foreach (char c in key)
            {
                if (Char.IsDigit(c) || Char.IsWhiteSpace(c) || Char.IsPunctuation(c) || Char.IsSymbol(c))
                {
                    return false;
                }
            }

            // No character is a number, space, or punctuation, or symbol.
            return true;
        }

        private string ShiftASCIIValuesByKeyEncrypt(string text, string key)
        {
            Random random = new Random();
            int intPlaceHolder;
            char charPlaceHolder;
            string newText = string.Empty;
            int count = 0;

            // The each letter of the key needs to encrypt a character during it's cycle 
            //while (text.Length % key.Length != 0)
            //{
            //    // Choose a random character between A and Z
            //    int randomChar = random.Next(65, 91);
            //    text += (char)randomChar;
            //}

            while (count < text.Length)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if (count == text.Length)
                    {
                        // Once count equals the text length, we have encrypted the whole text,
                        // so we can stop the while loop.
                        break;
                    }
                    // This takes the text ASCII character and shifts it to the origin.
                    intPlaceHolder = BringASCIINumberToZero(text[count]);

                    // This takes the shifted text character and add the shifted key character.
                    intPlaceHolder += BringASCIINumberToZero(key[i]);

                    // Now that we are at the origin, we can take the mod
                    intPlaceHolder = intPlaceHolder % Globals.modulus;

                    // Now the encrypted character needs to be brought back to the letter ASCII values.
                    charPlaceHolder = ReturnASCIINumberToOriginal(intPlaceHolder);

                    // And the newly encrypted character is added to the newText
                    newText += charPlaceHolder;

                    // Increase the count so that the for loop loops to cycle through the key
                    // over and over until the text runs out of characters
                    count++;
                }
            }
            
            return newText;
        }

        private string ShiftASCIIValuesByKeyDecrypt(string text, string key)
        {
            Random random = new Random();
            int intPlaceHolder;
            char charPlaceHolder;
            string newText = string.Empty;
            int count = 0;

            // The each letter of the key needs to encrypt a character during it's cycle 
            //while (text.Length % key.Length != 0)
            //{
            //    // Choose a random character between A and Z
            //    int randomChar = random.Next(65, 91);
            //    text += (char)randomChar;
            //}

            while (count < text.Length)
            {
                for (int i = 0; i < key.Length; i++)
                {
                    if (count == text.Length)
                    {
                        // Once count equals the text length, we have encrypted the whole text,
                        // so we can stop the while loop.
                        break;
                    }
                    // This takes the text ASCII character and shifts it to the origin.
                    intPlaceHolder = BringASCIINumberToZero(text[count]);

                    // This takes the shifted text character and add the shifted key character.
                    intPlaceHolder -= BringASCIINumberToZero(key[i]);

                    // Now that we are at the origin, we can take the mod
                    intPlaceHolder = intPlaceHolder % Globals.modulus;

                    // Now the encrypted character needs to be brought back to the letter ASCII values.
                    charPlaceHolder = ReturnASCIINumberToOriginal(intPlaceHolder);

                    // And the newly encrypted character is added to the newText
                    newText += charPlaceHolder;

                    // Increase the count so that the for loop loops to cycle through the key
                    // over and over until the text runs out of characters
                    count++;
                }
            }

            return newText;
        }
    }
}