using Algorithms.GlobalVariables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.MainClasses
{
    public class Cipher
    {
        // This method removes all white spaces from the input text
        public string TrimText(string text)
        {
            return String.Concat(text.Where(c => !Char.IsWhiteSpace(c)));
        }

        // This method removes special characters and numbers from the input text
        public string FilterText(string text)
        {
            string filteredText = "";
            foreach (char character in text)
            {

                if (Char.IsLetter(character))
                {
                    filteredText += Char.ToUpper(character);
                }
                /*
                 * This was going to retain the numbers, but I think it will be too 
                 * hard to figure out how to do it. And, the Affine Cipher key has 
                 * to have a GCD of 0 with 26 and I don't know how changing it to 36
                 * will affect it
                if (Char.IsDigit(character))
                {
                    filteredText += character;
                }
                else if (Char.IsLetter(character))
                {
                    filteredText += Char.ToUpper(character);
                }
                */
            }
            return filteredText;
        }

        public static int BringASCIINumberToZero(char character)
        {
            int characterValue = (int)character;

            if (Char.IsLetter(character))
            {
                //Console.WriteLine($"Char in filtered string is letter: {character}");
                characterValue = characterValue - 65;
                //Console.WriteLine($"It's new index is: {characterValue}");
            }
            else
            {
                Console.WriteLine("Invalid character in string");
            }
            /*
             * Trying to use numbers seems too hard with the affine cipher and
             * something is broken anyways so I'll try and narrow it down by only
             * allowing letters
            if (Char.IsDigit(character))
            {
                //Console.WriteLine($"Char in filtered string is number: {character}");
                characterValue = characterValue - 48;
                //Console.WriteLine($"It's new index is: {characterValue}");
            }
            else if (Char.IsLetter(character))
            {
                //Console.WriteLine($"Char in filtered string is letter: {character}");
                characterValue = characterValue - 55;
                //Console.WriteLine($"It's new index is: {characterValue}");
            }
            else
            {
                Console.WriteLine("Invalid character in string");
            }
            */
            return characterValue;
        }
        
        public static char ReturnASCIINumberToOriginal(int number)
        {
            // C# does not perform modular arithmetic correctly and will return a negative number
            // if the number is negative. This will force number to be an element in [0,35]
            while (number < 0)
            {
                number += Globals.modulus;
            }
            if (number >=0 && number < Globals.modulus)
            {
                number = number + 65;
            }
            

            /*
             * Trying to use numbers seems too hard with the affine cipher and
             * something is broken anyways so I'll try and narrow it down by only
             * allowing letters
            //Console.WriteLine("The Return input is : " + number);
            if (number >= 0 && number <= 9)
            {
                number = number + 48;
                //Console.WriteLine($"The number in [0,9] is now: {number}\n");
            }
            else if (number >= 10 && number <= 35)
            {
                number = number + 55;
                // Console.WriteLine($"The number in [10,35] is now: {number}\n");
            }
            else
            {
                Console.WriteLine("index out of bounds\n");
            }
            */
            return (char)number;
        }

        public int GCD(int number1, int number2)
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
    }
}
