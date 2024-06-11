using Algorithms.GlobalVariables;
using Algorithms.Interfaces;
using Algorithms.MainClasses;
using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Subclasses
{
    public class HillCipher : Cipher, ICipher
    {
        string plaintext;
        string ciphertext;
        string trimmedText;
        string filteredText;
        int blockSize;
        Matrix<double> keyMatrix;
        Matrix<double> invertedKeyMatrix;

        public string Encrypt(string plaintext, string key)
        {
            if (KeyIsCorrect(key))
            {
                string paddedPlaintext = PadString(plaintext, blockSize);
                ciphertext = string.Empty;

                // Add letters to the plaintext
                trimmedText = TrimText(paddedPlaintext);
                filteredText = FilterText(trimmedText);

                string[] blocks = Split(filteredText, keyMatrix.ColumnCount);
                foreach (string block in blocks)
                {
                    double[] charValue = new double[block.Length];
                    for (int i = 0; i < block.Length; i++)
                    {
                        // get the numeric value of the char and turn it into a double and assign it the the array
                        charValue[i] = BringASCIINumberToZero(block[i]);
                    }

                    // Build the vector with the numerical values of the char array so we can do matrix arithmetic
                    Vector<double> charVector = Vector<double>.Build.DenseOfArray(charValue);

                    // Encrypt the character values mod the modulus
                    Vector<double> ciphertextVector = (charVector * keyMatrix) % Globals.modulus;

                    //for (int i = 0; i < ciphertextVector.Count; i++)
                    //{
                    //    ciphertextVector[i] = ciphertextVector[i] % Globals.modulus;
                    //}

                    for (int i = 0; i < block.Length; i++)
                    {
                        // return the encrypted ascii value back to the text value
                        charValue[i] = ReturnASCIINumberToOriginal((char)(int)ciphertextVector[i]);
                        ciphertext += (char)charValue[i];
                    }

                }
                return ciphertext;
            }
            else
            {
                return "Key is invalid";
            }
        }

        public string Decrypt(string ciphertext, string key)
        {
            if (KeyIsCorrect(key))
            {
                string paddedCiphertext = PadString(ciphertext, blockSize);
                plaintext = string.Empty;
                invertedKeyMatrix = keyMatrix.Inverse();
                return invertedKeyMatrix.Multiply(keyMatrix).ToString();

                // Add letters to the plaintext
                trimmedText = TrimText(paddedCiphertext);
                filteredText = FilterText(trimmedText);

                string[] blocks = Split(filteredText, keyMatrix.ColumnCount);
                foreach (string block in blocks)
                {
                    double[] charValue = new double[block.Length];
                    for (int i = 0; i < block.Length; i++)
                    {
                        // get the numeric value of the char and turn it into a double and assign it the the array
                        charValue[i] = BringASCIINumberToZero(block[i]);
                    }

                    // Build the vector with the numerical values of the char array so we can do matrix arithmetic
                    Vector<double> charVector = Vector<double>.Build.DenseOfArray(charValue);

                    // Encrypt the character values mod the modulus
                    Vector<double> plaintextVector = (charVector * invertedKeyMatrix) % Globals.modulus;

                    //for (int i = 0; i < ciphertextVector.Count; i++)
                    //{
                    //    ciphertextVector[i] = ciphertextVector[i] % Globals.modulus;
                    //}

                    for (int i = 0; i < block.Length; i++)
                    {
                        // return the encrypted ascii value back to the text value
                        charValue[i] = ReturnASCIINumberToOriginal((char)(int)plaintextVector[i]);
                        plaintext += (char)charValue[i];
                    }

                }
                return plaintext;
            }
            else
            {
                return "Key is invalid";
            }
        }

        public bool KeyIsCorrect(string key)
        {
            key = key.Trim(); // Just in case somebody input ', '

            // I need to convert the string to a matrix now

            // This has all of the rows of the matrix
            string[] rows = key.Split(';');
            string[] columns = rows[0].Split(",");

            if (columns.Length != rows.Length)
            {
                //throw an exception
                Console.WriteLine("The matrix must be a square matrix, meaning the number of rows " +
                    "must be equal to the number of columns.");
                return false;
            }

            // I need an integer array to do modular arithmetic but a double array for matrix arithmetic
            //int[,] intArray = new int[rows.Length, columns.Length];
            double[,] doubleArray = new double[rows.Length, columns.Length];

            

            //string[] temp = new string[rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                string[] temp = rows[i].Split(',');
                for (int j = 0; j < temp.Length; j++)
                {
                    // if (!double.TryParse(temp[j], out intArray[i, j]))
                    if (!double.TryParse(temp[j], out doubleArray[i, j]))
                    {
                        // throw an exception
                        Console.WriteLine("Elements of the matrix must be whole numbers");
                        return false;
                    }
                    //doubleArray[i, j] = intArray[i, j];
                }
            }

            // I now have the elements of the matrix and need to build a real matrix.
            keyMatrix = Matrix<double>.Build.DenseOfArray(doubleArray);

            if (keyMatrix.ColumnCount != keyMatrix.RowCount)
            {
                // Throw exception
                Console.WriteLine("The matrix must be a square matrix, meaning the number of rows " +
                    "must be equal to the number of columns.");
                return false;
            }

            blockSize = keyMatrix.RowCount;
            double determinant = keyMatrix.Determinant();
            int intDeterminant = (int)determinant;

            if (GCD(intDeterminant, Globals.modulus) != 1)
            {
                // throw exception
                Console.WriteLine($"The GCD of the determinant of the key and {Globals.modulus} must = 1");
                return false;
            }

            // We passed all the checks and the key is good
            return true;
        }
    }
}
