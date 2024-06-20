using Algorithms.Interfaces;
using Algorithms.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Subclasses
{
    public class StreamCipher : Cipher, ICipher
    {
        List<int> listOfBinaryDigits;
        string plaintext;
        string ciphertext;
        string trimmedText;
        string filteredText;
        string[] binaryOfASCII;
        string[] plaintextBinary;
        string[] ciphertextBinary;
        int[] plaintextInts;
        int[] ciphertextInts;
        int[] randomBits;
        int[] arrayOfBinaryDigits;
        int[] encrypedBits;
        int[] keyArray;
        int intKey;
        int asciiBinarySize = 7;
        int sixBits = 6;
        int lengthOfKey = 8;

        // ASCII ; to z are the available ciphertext characters, 59 to 122
        // That gives 63 characters, which is the highest 6 bit number 111111
        // Since ; is the smallest character at 59, every 6 bit number will have to add 
        // 59 to it as we encrypt to a character.
        int characterShift = 122 - 63; 

        public string Encrypt(string plaintext, string key)
        {
            if (!KeyIsCorrect(key))
            {
                return "Key is invalid";
            }

            ciphertext = String.Empty;
            trimmedText = plaintext.Trim();
            filteredText = FilterText(trimmedText);

            // Each element of binaryOfASCII is the ASCII byte of each character as a string
            binaryOfASCII = ConvertStringToBinary(filteredText);


            // Build the key that will generate a stream of random bits
            // the keyArray will be the initial vector for the LSFR to find random bits that will be used to encrypt the plaintext
            keyArray = BuildKeyArray(intKey, lengthOfKey);

            // These random bits will be used to encrypt the plaintext and decrypt the ciphertext
            randomBits = LinearFeedbackShiftRegister(keyArray);

            // muiltipled by 7 since each element of binaryOfASCII is 7 bits
            arrayOfBinaryDigits = new int[binaryOfASCII.Length * asciiBinarySize];
            int index = 0;
            for (int i = 0; i < binaryOfASCII.Length; i++)
            {
                foreach (char c in binaryOfASCII[i])
                {
                    arrayOfBinaryDigits[index] = c - 48;
                    Console.Write(arrayOfBinaryDigits[index]);
                    index++;
                }
            }

            // We have the plaintext as binary, and we have the random bit stream. We now add them together to get the ciphertext
            encrypedBits = new int[arrayOfBinaryDigits.Length];
            for (int i = 0; i < encrypedBits.Length; i++)
            {
                // The randomBits are added periodically by index mod randomBits.Length
                encrypedBits[i] = (arrayOfBinaryDigits[i] + randomBits[i % randomBits.Length]) % 2;
            }


            ciphertextBinary = new string[(int)MathF.Ceiling((float) encrypedBits.Length / sixBits)];
            int count = 0;
            for (int i = 0; i < ciphertextBinary.Length; i++)
            {
                for (int j = 0 + count; j < sixBits + count && j < encrypedBits.Length; j++)
                {
                    ciphertextBinary[i] += encrypedBits[j];
                }
                Console.WriteLine(ciphertextBinary[i]);
                count += sixBits;
            }

            ciphertextInts = new int[ciphertextBinary.Length];
            for (int i = 0; i < ciphertextBinary.Length; i++)
            {
                ciphertextInts[i] = Convert.ToInt32(ciphertextBinary[i], 2);

                // 122 is the highest ascii value for a letter and 63 is the decimal number for 111111 on binary, the largest 6 bit number.
                // so the character values can range from 59 to 122 -> which is 122-63, to have 63, 6 bit characters available.
                ciphertext += (char)(ciphertextInts[i] + characterShift);
            }

            return ciphertext;
        }

        public string Decrypt(string ciphertext, string key)
        {
            if(!KeyIsCorrect(key))
            {
                return "Key is invalid";
            }

            plaintext = String.Empty;

            // The ciphertext has been encoded with 6 bits
            ciphertextInts = new int[ciphertext.Length];
            int index = 0;
            foreach (char c in ciphertext)
            {
                ciphertextInts[index++] = (int) (c - characterShift);
            }

            // Each element of binaryOfASCII is the ASCII byte of each character as a string
            binaryOfASCII = ConvertIntsToBinary(ciphertextInts);

            // This adds leading 0's to numbers so they have 6 characters. The last digit is the size where the whole
            // string of bits will be divisible by 7
            binaryOfASCII = BuildSixBitNumbers(binaryOfASCII);

            // Build the key that will generate a stream of random bits
            // the keyArray will be the initial vector for the LSFR to find random bits that will be used to encrypt the plaintext
            keyArray = BuildKeyArray(intKey, lengthOfKey);

            // These random bits will be used to encrypt the plaintext and decrypt the ciphertext
            randomBits = LinearFeedbackShiftRegister(keyArray);
            
            listOfBinaryDigits = new List<int>();
            for (int i = 0; i < binaryOfASCII.Length; i++)
            {
                foreach (char c in binaryOfASCII[i])
                {
                    listOfBinaryDigits.Add(c - 48);
                }
            }

            // We have the ciphertext as binary, and we have the random bit stream. We now add them together to get the plaintext
            encrypedBits = new int[listOfBinaryDigits.Count];
            for (int i = 0; i < encrypedBits.Length; i++)
            {
                // The randomBits are added periodically by index mod randomBits.Length
                encrypedBits[i] = (listOfBinaryDigits[i] + randomBits[i % randomBits.Length]) % 2;
            }

            plaintextBinary = new string[(int)MathF.Ceiling(encrypedBits.Length / asciiBinarySize)];
            int count = 0;
            for (int i = 0; i < plaintextBinary.Length; i++)
            {
                for (int j = 0 + count; j < asciiBinarySize + count; j++)
                {
                    plaintextBinary[i] += encrypedBits[j];
                }
                Console.WriteLine(plaintextBinary[i]);
                count += asciiBinarySize;
            }

            plaintextInts = new int[plaintextBinary.Length];

            for (int i = 0; i < plaintextBinary.Length; i++)
            {
                plaintextInts[i] = Convert.ToInt32(plaintextBinary[i], 2);
                plaintext += (char)(plaintextInts[i]);
            }

            return plaintext;
        }

        public bool KeyIsCorrect(string key)
        {
            // The key is the seed for the random number generator so it just has to be an int.
            if (int.TryParse(key, out intKey))
            {
                return true;
            }
            else
            {
                // throw exception
                return false;
            }
        }

        private string[] ConvertStringToBinary(string input)
        {
            string[] binaryOfASCIICharacters = new string[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                binaryOfASCIICharacters[i] = Convert.ToString(input[i], 2);
            }
            return binaryOfASCIICharacters;
        }

        private int[] LinearFeedbackShiftRegister(int[] initialVector)
        {
            // The period of my LFSR is 217 from https://www.omnicalculator.com/math/linear-feedback-shift-register
            int randomBitStringLength = 217;
            int[,] streamArray = new int[initialVector.Length, randomBitStringLength];

            int[] randomBitstream = new int[streamArray.GetLength(1)];

            for (int j = 0; j < streamArray.GetLength(1); j++)
            {
                for (int i = 0; i < initialVector.Length; i++)
                {
                    streamArray[i, j] = initialVector[i];
                }

                // This stores the first position so it can be shifted later
                int fallOffDigit = initialVector[0];

                // Assign position 0 by XORing these numbers, before they get shifted
                initialVector[0] = (initialVector[2] + initialVector[3] + initialVector[6] + initialVector[7]) % 2;

                for (int i = initialVector.Length - 1; i > 0; i--)
                {
                    // assign last position to 2nd position, just shifting each value
                    if (i > 1)
                    {
                        initialVector[i] = initialVector[i - 1];
                    }
                    // Assign position 1 as position 0 before we recalculated 0
                    else
                    {
                        initialVector[i] = fallOffDigit;
                    }

                }

                // The last position of the shift at each iteration is the random bit that we want to encrypt the message
                randomBitstream[j] = streamArray[initialVector.Length - 1, j];
            }
            return randomBitstream;
        }

        private int[] BuildKeyArray(int intKey, int lengthOfKey)
        {
            Random random = new Random(intKey);

            // the keyArray will be the initial vector for the LSFR to find random bits that will be used to encrypt the plaintext
            int[] keyArray = new int[lengthOfKey];
            int countOf0 = lengthOfKey;

            while(countOf0 == lengthOfKey)
            {
                countOf0 = 0;

                for (int i = 0; i < keyArray.Length; i++)
                {

                    keyArray[i] = random.Next(0, 2);
                    if (keyArray[i] == 0)
                    {
                        countOf0++;
                    }
                }

                // If the seed (input by the user) gives a keyArray of all zero's the plaintext won't be encrypted;
                // We'll just increase the key by 1 and the same will happen during decryption so the key's will still
                // match.
                if (countOf0 == lengthOfKey)
                {
                    random = new Random(intKey + 1);
                }
            }

            return keyArray;
        }

        private string[] BuildSixBitNumbers(string[] binaryOfASCII)
        {
            // Every number except the last one should be a full six digits long
            // The last number needs to make the whole string of numbers divisible by 7
            for (int i = 0; i < binaryOfASCII.Length - 1; i++)
            {

                while (binaryOfASCII[i].Length < 6)
                {
                    binaryOfASCII[i] = "0" + binaryOfASCII[i];
                }
            }

            // 6 * number of 6 bit numbers + the length of the last number need to be divisible by 7
            while ((binaryOfASCII[binaryOfASCII.Length - 1].Length + 6 * (binaryOfASCII.Length - 1)) % 7 != 0)
            {
                binaryOfASCII[binaryOfASCII.Length - 1] = "0" + binaryOfASCII[binaryOfASCII.Length - 1];
            }

            return binaryOfASCII;
        }

        private string[] ConvertIntsToBinary(int[] ciphertextInts)
        {
            string[] binaryOfASCIICharacters = new string[ciphertextInts.Length];
            for (int i = 0; i < ciphertextInts.Length; i++)
            {
                binaryOfASCIICharacters[i] = Convert.ToString(ciphertextInts[i], 2);
            }
            return binaryOfASCIICharacters;
        }
    }
}
