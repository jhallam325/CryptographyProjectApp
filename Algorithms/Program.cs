using Algorithms.Subclasses;
using Algorithms.GlobalVariables;
using System.ComponentModel;
using MathNet.Numerics.LinearAlgebra;
using Algorithms.MainClasses;
using MathNet.Numerics.Optimization;
using System.Reflection.Metadata.Ecma335;
using System.Linq.Expressions;
using System.Transactions;

namespace Algorithms
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            /*
             * args[0] = cipher name
             * args[1] = key
             * args[2] = file path to read
             * args[3] = encrypt or decrypt
             * args[4] = file path to write
             */

            /*
             * RegEx Cheat Sheet
             * ^ - Starts with
             * $ - Ends with
             * [] - Range
             * () - Group
             * . - Single character once
             * + - one or more characters in a row
             * ? - optional preceding character match
             * \ - escape character
             * \n - New line
             * \d - Digit
             * \D - Non-digit
             * \s - White space
             * \S - non-white space
             * \w - alphanumeric/underscore character (word chars)
             * \W - non-word characters
             * {x,y} - Repeat low (x) to high (y) (no "y" means at least x, no ",y" means that many)
             * (x|y) - Alternative - x or y
             * 
             * [^x] - Anything but x (where x is whatever character you want)
             */


            string plaintext;
            string ciphertext;
            string trimmedText;
            string filteredText;
            string[] binaryOfASCII;
            int[] intOfASCII;
            int intKey;

            Cipher cipher = new Cipher();
            plaintext = "This is the end my friend";
            trimmedText = plaintext.Trim();
            filteredText = cipher.FilterText(trimmedText);



            // I can convert back and forth from binary to decimal
            binaryOfASCII = new string[filteredText.Length];
            intOfASCII = new int[filteredText.Length];
            for (int i = 0; i < filteredText.Length; i++)
            {
                binaryOfASCII[i] = Convert.ToString(filteredText[i], 2);
                Console.WriteLine(binaryOfASCII[i]);
            }
            Console.WriteLine("==============================================================================");
            for (int i = 0; i < binaryOfASCII.Length; i++)
            {
                intOfASCII[i] = Convert.ToInt32(binaryOfASCII[i], 2);
                Console.WriteLine(i + " " + intOfASCII[i]);
            }
            Console.WriteLine("==============================================================================");


            // Key Generator algorithm
            Console.Write("Enter a whole number as your key: ");
            string key = Console.ReadLine();
            if (int.TryParse(key, out intKey))
            {
                // Do key validation here

            }
            else
            {
                Console.WriteLine("Invalid key");
                return;
            }

            // the intKey is the seed for the random numbers so it can be decrypted
            Random random = new Random(intKey);
            int lengthOfKey = 8;
            int countOf0 = 0;
            int[] keyArray = new int[lengthOfKey];

            for (int i = 0; i < keyArray.Length; i++)
            {

                keyArray[i] = random.Next(0, 2);
                Console.WriteLine(keyArray[i]);
                if (keyArray[i] == 0)
                {
                    countOf0++;
                }
            }
            

            if (countOf0 == keyArray.Length)
            {
                // The key generated was all 0's and we need to regenerate the key
                //But how?
            }

            Console.WriteLine("=========================New Practice =============================");
            int[] init = { 0, 0, 1, 0 };
            int randomBitStringLength = 1015;

            //int[,] streamArray = new int[init.Length, randomBitStringLength];
            int[,] streamArray = new int[keyArray.Length, randomBitStringLength];

            int[] randomBitstream = new int[streamArray.GetLength(1)];

            for (int j = 0; j < streamArray.GetLength(1); j++)
            {
                //for (int i = 0; i < init.Length; i++)
                //{
                //    streamArray[i, j] = init[i];
                //    Console.Write(streamArray[i, j] + ", ");
                //}
                //Console.WriteLine();
                //int fallOffDigit = init[0];
                //init[0] = (init[2] + init[3]) % 2;
                //for (int i = init.Length - 1; i > 0; i--)
                //{
                //    if (i > 1)
                //    {
                //        init[i] = init[i - 1];
                //    }
                //    else
                //    {
                //        init[i] = fallOffDigit;
                //    }

                //}
                //randomBitstream[j] = streamArray[init.Length - 1, j];



                for (int i = 0; i < keyArray.Length; i++)
                {
                    streamArray[i, j] = keyArray[i];
                    Console.Write(streamArray[i, j] + ", ");
                }
                Console.WriteLine();
                int fallOffDigit = keyArray[0];
                keyArray[0] = (keyArray[2] + keyArray[3] + keyArray[6] + keyArray[7]) % 2;
                for (int i = keyArray.Length - 1; i > 0; i--)
                {
                    if (i > 1)
                    {
                        keyArray[i] = keyArray[i - 1];
                    }
                    else
                    {
                        keyArray[i] = fallOffDigit;
                    }

                }
                randomBitstream[j] = streamArray[keyArray.Length - 1, j];



            }
            // matched https://www.youtube.com/watch?v=Y0DlCM4iKeA&list=PLE6ty64ouo1M7Xz6Qj5bgXZOoEE0qilX6&index=25

            for (int i = 0;i < randomBitstream.Length; i++)
            {
                Console.Write(randomBitstream[i] + ", ");
            }

            // Now I take the random bit stream and encode the plaintext
            //string[] blocks = cipher.Split(binaryOfASCII, intKey);

            //// XOR operator
            //Console.WriteLine(true ^ true);    // output: False
            //Console.WriteLine(true ^ false);   // output: True
            //Console.WriteLine(false ^ true);   // output: True
            //Console.WriteLine(false ^ false);  // output: False




            //* Hill Cipher debugging
            //Cipher cipher = new Cipher();
            //HillCipher hill = new HillCipher();
            //int size = 3;
            //double[,] array = new double[size, size];
            //array[0, 0] = 11;
            //array[0, 1] = 8;
            //array[0, 2] = 7;
            //array[1, 0] = 3;
            //array[1, 1] = 7;
            //array[1, 2] = 5;
            //array[2, 0] = 13;
            //array[2, 1] = 9;
            //array[2, 2] = 25;
            //int number = 1;
            //for (int i = 0; i < size; i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        array[i,j] = number;
            //        number++;
            //    }
            //}
            //Console.WriteLine(array.ToString());
            //Matrix<double> A = Matrix<double>.Build.DenseOfArray(array);
            //Console.WriteLine(A.ToString());
            //var M = hill.MinorMatrixModN(A, Globals.modulus);
            //Console.WriteLine(M.ToString());
            //var C = hill.CofactorMatrixModN(M, Globals.modulus);
            //Console.WriteLine(C.ToString());


            

            //Console.WriteLine(hill.Decrypt("this is a test", "1,0,0,1;0,1,0,1;0,0,1,1;1,1,1,0"));


            /*
            Console.WriteLine($"Determinant = {A.Determinant() % Globals.modulus}");
            Console.WriteLine($"GCD({A.Determinant() % Globals.modulus},{Globals.modulus}) = {cipher.GCD((int)A.Determinant(), Globals.modulus)}");
            Console.WriteLine();
            // A is the key matrix that we want to invert.
            int numRows = A.RowCount;
            int numCols = A.ColumnCount;
            //hill.InvertMatrixModN(A, Globals.modulus);


            if (numRows != numCols)
            {
                // throw exception
                Console.WriteLine("The input matrix must be a square matrix or you can't find a Minor Matrix");
            }

            Matrix<double> M = CreateMatrix.Dense<double>(numRows, numCols);
            Matrix<double> tempMatrix = CreateMatrix.Dense<double>(numRows - 1, numCols - 1);

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    // Can I build TempMatrix here?
                    tempMatrix = A.RemoveRow(i).RemoveColumn(j);
                    Console.WriteLine(tempMatrix.ToString());

                    Console.WriteLine($"Determinant = {tempMatrix.Determinant()}");
                    M[i, j] = (int)tempMatrix.Determinant() % Globals.modulus;
                }
            }
            Console.WriteLine(M.ToString());
            */
            /*

            // I'm creating the Identity matrix to test A*A^(-1) = I
            Matrix<double> I = CreateMatrix.DenseIdentity<double>(numRows);

            // Now I will make Matrix B where the elements Bx = Columns of I stacked into a vector
            // and the vector x will be the columns of A^(-1) stacked in a vector

            // Thry and buil B to debog:
            Matrix<double> B = CreateMatrix.Dense<double>(numRows * numRows, numCols * numCols);
            // I will assign the numbers a=1 and try to make B look like the one on my paper
            int rowIndex = 0;
            int columnIndex = 0;
            while (rowIndex < numRows)
            {

            }
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    if (i == j)
                    {
                        B[i, j] = 1;
                    }

                }
            }
            */




            //Console.WriteLine("Enter a square Matrix in the form: 1,2,3;4,5,6;7,8,9 where individual " +
            //    "elements are seperated by commas and rows are seperated by semi-colons");
            //string input = Console.ReadLine();
            //input = input.Trim(); // Just in case somebody input ', '

            //// I need to convert the string to a matrix now

            //// This has all of the rows of the matrix
            //string[] rows = input.Split(';');
            //string[] columns = rows[0].Split(",");

            //// I need an integer array to do modular arithmetic but a double array for matrix arithmetic
            //int[,] intArray = new int[rows.Length, columns.Length];
            //double[,] doubleArray = new double[rows.Length, columns.Length];

            ////string[] temp = new string[rows.Length];
            //for (int i = 0; i < rows.Length; i++)
            //{
            //    string[] temp = rows[i].Split(',');
            //    for (int j = 0; j < temp.Length; j++)
            //    {
            //        if (!int.TryParse(temp[j], out intArray[i,j]))
            //        {
            //            // throw an exception
            //            Console.WriteLine("Elements of the matrix must be whole numbers");
            //            Environment.Exit(0);
            //        }
            //        doubleArray[i,j] = intArray[i,j];
            //    }
            //}

            //// I now have the elements of the matrix and need to build a real matrix.
            //Matrix<double> keyMatrix = Matrix<double>.Build.DenseOfArray(doubleArray);

            //if (keyMatrix.ColumnCount != keyMatrix.RowCount)
            //{
            //    // Throw exception
            //    Console.WriteLine("The matrix must be a square matrix, meaning the number of rows " +
            //        "must be equal to the number of columns.");
            //    Environment.Exit(0);
            //}

            //double determinant = keyMatrix.Determinant();
            //int intDeterminant = (int)determinant;

            //Cipher cipher = new Cipher();
            //if (cipher.GCD(intDeterminant, Globals.modulus) != 1)
            //{
            //    // throw exception
            //    Console.WriteLine($"The GCD of the determinant of the key and {Globals.modulus} must = 1");
            //    Environment.Exit(0);
            //}
            ////==================================================================================================

            //// The key check should be done and I can begin to encrypt the message
            //Console.Write("\nEnter a messagge to encrypt: ");

            //string plaintext = Console.ReadLine();
            //plaintext = plaintext.ToUpper();
            //// Here I need to add extra characters to the message so that the 

            //string ciphertext = string.Empty;
            //string[] blocks = Split(plaintext, keyMatrix.ColumnCount);
            //foreach (string block in blocks)
            //{
            //    double[] charValue = new double[block.Length];
            //    for (int i = 0; i < block.Length; i++)
            //    {
            //        // get the numeric value of the char and turn it into a double and assign it the the array
            //        charValue[i] = cipher.BringASCIINumberToZero(block[i]);
            //    }

            //    // Build the vector with the numerical values of the char array so we can do matrix arithmetic
            //    Vector<double> charVector = Vector<double>.Build.DenseOfArray(charValue);

            //    // Encrypt the character values mod the modulus
            //    Vector<double> ciphertextVector = (charVector * keyMatrix) % Globals.modulus;

            //    //for (int i = 0; i < ciphertextVector.Count; i++)
            //    //{
            //    //    ciphertextVector[i] = ciphertextVector[i] % Globals.modulus;
            //    //}

            //    for (int i = 0; i < block.Length; i++)
            //    {
            //        // return the encrypted ascii value back to the text value
            //        charValue[i] = cipher.ReturnASCIINumberToOriginal((char)(int)ciphertextVector[i]);
            //        ciphertext += (char) charValue[i];
            //    }

            //}
            //Console.WriteLine($"Ciphertext = {ciphertext}");


            ////=========================================================================================================
            //// Decrypt
            //Matrix<double> inverseKeyMatrix = keyMatrix.Inverse();








            //string plaintext;
            ////string trimmed;
            ////string filtered = null;
            //string ciphertext = null;
            //char charPlaceHolder;
            //int intPlaceHolder = 0;
            ////int key = 6;

            //StreamReader reader = null;
            //StreamWriter writer = null;

            //ShiftCipher shiftCipher = new ShiftCipher();
            //SubstitutionCipher substitutionCipher = new SubstitutionCipher();

            //try
            //{
            //    // This is the key and should be an number of how many letters to shift.
            //    Console.Write("Input a substitution ciper key: ");
            //    string key = Console.ReadLine();

            //    if (!Directory.Exists(Globals.FilePath))
            //    {
            //        Directory.CreateDirectory(Globals.FilePath);
            //    }



            //    //-------------------Encrypt--------------------------//
            //    // Opens text files where we will read and write.
            //    reader = new StreamReader(Globals.PlainTextFullPath);
            //    writer = new StreamWriter(Globals.CipherTextFullPath);

            //    // Reads every character in the reader (plaintext here) file
            //    plaintext = reader.ReadToEnd();
            //    //ciphertext = reader.ReadToEnd();

            //    // Use the Shift Cipher to Encrypy the plaintext.
            //    ciphertext = substitutionCipher.Encrypt(plaintext, key);
            //    //plaintext = shiftCipher.Decrypt(ciphertext, key);

            //    // Write the ciphertext to the ciphertext file.
            //    writer.WriteLine(ciphertext);
            //    //writer.WriteLine(plaintext);


            //    /*
            //    //--------------------Decrypt----------------------//
            //    // Opens text files where we will read and write.
            //    reader = new StreamReader(Globals.WriteFullPath);
            //    writer = new StreamWriter(Globals.ReadFullPath);

            //    // Reads every character in the reader (plaintext here) file
            //    //plaintext = reader.ReadToEnd();
            //    ciphertext = reader.ReadToEnd();

            //    // Use the Shift Cipher to Encrypy the plaintext.
            //    //ciphertext = shiftCipher.Encrypt(plaintext, key);
            //    plaintext = shiftCipher.Decrypt(ciphertext, key);

            //    // Write the ciphertext to the ciphertext file.
            //    //writer.WriteLine(ciphertext);
            //    writer.WriteLine(plaintext);
            //    */
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //finally
            //{
            //    reader.Close();
            //    writer.Close();
            //}


            //// Decrypt

            //try
            //{
            //    // This is the key and should be an number of how many letters to shift.
            //    Console.Write("Input a number: ");
            //    string key = Console.ReadLine();

            //    if (!Directory.Exists(Globals.FilePath))
            //    {
            //        Directory.CreateDirectory(Globals.FilePath);
            //    }


            //    //--------------------Decrypt----------------------//
            //    // Opens text files where we will read and write.
            //    reader = new StreamReader(Globals.CipherTextFullPath);
            //    writer = new StreamWriter(Globals.PlainTextFullPath);

            //    // Reads every character in the reader (plaintext here) file
            //    //plaintext = reader.ReadToEnd();
            //    ciphertext = reader.ReadToEnd();

            //    // Use the Shift Cipher to Encrypy the plaintext.
            //    //ciphertext = shiftCipher.Encrypt(plaintext, key);
            //    plaintext = shiftCipher.Decrypt(ciphertext, key);

            //    // Write the ciphertext to the ciphertext file.
            //    //writer.WriteLine(ciphertext);
            //    writer.WriteLine(plaintext);

            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //finally
            //{
            //    reader.Close();
            //    writer.Close();
            //}
            //
        }
    }
}
