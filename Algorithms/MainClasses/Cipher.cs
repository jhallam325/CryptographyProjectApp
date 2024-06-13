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

        //removed static from Bring...  and Return... to test in Program.cs
        public int BringASCIINumberToZero(char character)
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
            if (Char.IsDigit(character))
            {
                //Console.WriteLine($"Char in filtered string is number: {character}");
                characterValue = characterValue - 48;
                //Console.WriteLine($"It's new index is: {characterValue}");
            }
            else if (Char.IsLetter(character))
            {
                //Console.WriteLine($"Char in filtered string is letter: {character}");
                characterValue = characterValue - 65;
                //Console.WriteLine($"It's new index is: {characterValue}");
            }
            else
            {
                Console.WriteLine("Invalid character in string");
            }
            */
            return characterValue;
        }
        
        public char ReturnASCIINumberToOriginal(int number)
        {
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
            if (number >= 0 && number <= 9)
            {
                number = number + 48;
                //Console.WriteLine($"The number in [0,9] is now: {number}\n");
            }
            else if (number >= 10 && number <= 35)
            {
                number = number + 65;
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

        public string[] Split(string str, int chunkSize)
        {
            int numberOfStrings = (int)MathF.Ceiling(str.Length / chunkSize);
            string[] strings = new string[numberOfStrings];
            for (int i = 0; i < strings.Length; i++)
            {
                for (int j = i * chunkSize; j < chunkSize * (i + 1); j++)
                {
                    strings[i] += str[j];
                }

            }
            return strings;
        }

        public string PadString(string text, int blockSize)
        {
            Random random = new Random();

            // The each letter of the key needs to encrypt a character during it's cycle 
            while (text.Length % blockSize != 0)
            {
                // Choose a random character between A and Z
                int randomChar = random.Next(65, 91);
                text += (char)randomChar;
            }

            return text;
        }

        public int MultiplicativeInverse(int wantedInverse, int modulus)
        {
            // Try to find the inverse of b, b^(-1), of b mod a such that b*b^(-1) mod a = 1
            int a0 = modulus;
            int b0 = wantedInverse;
            int t0 = 0;
            int t = 1;
            int quotient = (int)MathF.Floor((float)a0 / b0);
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
                // We shouldn't reach this in this class since I already checked the GCD of the
                // key and the modulus
                return -1;
            }
        }

        public int PositiveCongruence(int number)
        {
            while (number < 0)
            {
                number += Globals.modulus;
            }
            return number;
        }
    }
}
