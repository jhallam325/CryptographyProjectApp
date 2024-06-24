using Algorithms.GlobalVariables;
using Algorithms.Interfaces;
using Algorithms.MainClasses;
using Algorithms.Exceptions;
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
        int numRows;
        int numCols;
        Matrix<double> keyMatrix;
        Matrix<double> invertedKeyMatrix;

        // invertiblae 4x4 matrix: "1,0,0,1;0,1,0,1;0,0,1,1;1,1,1,0"
        // invertiblae 3x3 matrix: "1,2,4;3,5,7;2,4,5"
        // invertiblae 2x2 matrix: "11,8;3,7"

        public string Encrypt(string plaintext, string key)
        {
            if (!KeyIsCorrect(key))
            {
                throw new IncorrectKeyException("The matrix key is invalid.");
            }

            ciphertext = string.Empty;

            trimmedText = TrimText(plaintext);
            filteredText = FilterText(trimmedText);

            // Add letters to the plaintext so the vector and matrix will always be the correct size
            string paddedFilteredText = PadString(filteredText, blockSize);

            string[] blocks = Split(paddedFilteredText, keyMatrix.ColumnCount);
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

                for (int i = 0; i < block.Length; i++)
                {
                    // return the encrypted ascii value back to the text value
                    charValue[i] = ReturnASCIINumberToOriginal((char)(int)ciphertextVector[i]);
                    ciphertext += (char)charValue[i];
                }

            }
            return ciphertext;
        }

        public string Decrypt(string ciphertext, string key)
        {
            if (!KeyIsCorrect(key))
            {
                throw new IncorrectKeyException("The matrix key is invalid.");
            }

            plaintext = string.Empty;
            trimmedText = TrimText(ciphertext);
            filteredText = FilterText(trimmedText);
            string paddedFilteredText = PadString(filteredText, blockSize);

            invertedKeyMatrix = InvertMatrixModN(keyMatrix, Globals.modulus);

            string[] blocks = Split(paddedFilteredText, keyMatrix.ColumnCount);
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

                // Decrypt the character values mod the modulus
                Vector<double> plaintextVector = (charVector * invertedKeyMatrix) % Globals.modulus;

                for (int i = 0; i < block.Length; i++)
                {
                    // return the decrypted ascii value back to the text value
                    charValue[i] = ReturnASCIINumberToOriginal((char)(int)plaintextVector[i]);
                    plaintext += (char)charValue[i];
                }

            }
            return plaintext;
        }

        public bool KeyIsCorrect(string key)
        {
            key = key.Trim(); // Just in case somebody input ', '

            // I need to convert the string to a matrix now

            // This has all of the rows of the matrix
            string[] rows = key.Split(';');
            string[] columns = rows[0].Split(",");

            // Check to make sure each row is the same size
            for (int i = 1; i < rows.Length; i++)
            {
                if (columns.Length != rows[i].Split(",").Length)
                {
                    throw new IncorrectKeyException("There is a row in your matrix that is not the same size as the rest.\n" +
                        "Meaning, you input something like: 1,2,3;1,2;1,2,3 where the 2nd row is missing a 3rd element");
                }
            }
            

            if (columns.Length != rows.Length)
            {
                throw new IncorrectKeyException("The matrix must be a square matrix, meaning the number of rows must be equal to the number of columns.");
                //Console.WriteLine("The matrix must be a square matrix, meaning the number of rows " +
                //    "must be equal to the number of columns.");
                //return false;
            }

            // I need an array to build the matrix
            double[,] doubleArray = new double[rows.Length, columns.Length];
            
            for (int i = 0; i < rows.Length; i++)
            {
                string[] temp = rows[i].Split(',');
                for (int j = 0; j < temp.Length; j++)
                {
                    if (!double.TryParse(temp[j], out doubleArray[i, j]))
                    {
                        throw new IncorrectKeyException("Elements of the matrix must be whole numbers");
                        //Console.WriteLine("Elements of the matrix must be whole numbers");
                        //return false;
                    }
                }
            }

            // I now have the elements of the matrix assigned to an array and need to build a real matrix.
            keyMatrix = Matrix<double>.Build.DenseOfArray(doubleArray);
            numRows = keyMatrix.RowCount;
            numCols = keyMatrix.ColumnCount;

            if (numRows != numCols)
            {
                throw new IncorrectKeyException("The matrix must be a square matrix, meaning the number of rows " +
                    "must be equal to the number of columns.");
                //Console.WriteLine("The matrix must be a square matrix, meaning the number of rows " +
                //    "must be equal to the number of columns.");
                //return false;
            }

            blockSize = numRows;
            double determinant = keyMatrix.Determinant();
            int intDeterminant = (int)determinant;

            if (GCD(intDeterminant, Globals.modulus) != 1)
            {
                throw new IncorrectKeyException($"The GCD of the determinant of the key and {Globals.modulus} must = 1");
                //Console.WriteLine($"The GCD of the determinant of the key and {Globals.modulus} must = 1");
                //return false;
            }

            // We passed all the checks and the key is good
            return true;
        }

        public Matrix<double> InvertMatrixModN(Matrix<double> A, int modulus)
        {
            // A is the key matrix that we want to invert.
            int numRows = A.RowCount;
            int numCols = A.ColumnCount;

            if (numRows != numCols)
            {
                throw new InvertibleMatrixException("The input matrix must be a square matrix or you can't take an inverse");
                //Console.WriteLine("The input matrix must be a square matrix or you can't take an inverse");
                //return A;
            }

            int determinant = (int)A.Determinant();
            determinant = determinant % modulus;
            determinant = PositiveCongruence(determinant);

            if (GCD(determinant, modulus) != 1)
            {
                throw new InvertibleMatrixException("GCD(A, modulus) must not equal 1 or the inverse doesn't exist\n" +
                    "This means the determinant and the modulus must be coprime");
                //Console.WriteLine("GCD(A, modulus) must not equal 1 or the inverse doesn't exist\n" +
                //    "This means the determinant and the modulus must be coprime");
                //return A;
            }

            int inverseOfDeterminant = MultiplicativeInverse(determinant, modulus);
            inverseOfDeterminant = PositiveCongruence(inverseOfDeterminant);

            var adjoint = AdjointMatrixModN(A, modulus);
            var inverse = inverseOfDeterminant * adjoint;

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    inverse[i, j] = inverse[i, j] % modulus;
                    inverse[i, j] = PositiveCongruence((int) Math.Round(inverse[i, j]));
                }
            }

            //Console.WriteLine($"Determinate = {determinant}");
            //Console.WriteLine($"Inverse det = {inverseOfDeterminant}");
            //Console.WriteLine("Inverse Matrix");
            //Console.WriteLine(inverse.ToString());

            return inverse;
            /*
            // I'm creating the Identity matrix to test A*A^(-1) = I
            Matrix<double> I = CreateMatrix.DenseIdentity<double>(numRows);

            // Now I will make Matrix B where the elements Bx = Columns of I stacked into a vector
            // and the vector x will be the columns of A^(-1) stacked in a vector

            // Thry and buil B to debog:
            Matrix<double> B = CreateMatrix.Dense<double>(numRows*numRows, numCols*numCols);
            // I will assign the numbers a=1 and try to make B look like the one on my paper
            int rowIndex = 0;
            int columnIndex = 0;
            double[] valuesOfA = new double[numRows * numCols];
            int arrayIndex = 0;

            for (int i  = 0; i < numRows; i++)
            {
                for (int j = 0;j< numCols; j++)
                {
                    valuesOfA[arrayIndex] = A[i, j];
                    arrayIndex++;
                }
            }

            arrayIndex = 0;

            // This will go from 0 to the last row
            while (rowIndex < numRows * numRows)
            {
                // This will go from 0 to the last column
                while (columnIndex < numCols * numCols)
                {
                    // This loops through rows, by the number of rows of A at a time
                    for (int i = 0 + rowIndex; i < numRows + rowIndex; i++)
                    {
                        // This loops through the columns, by the number of columns of A at a time
                        for (int j = 0 + columnIndex; j < numCols + columnIndex; j++)
                        {
                            // This builds diagonals in the chunks the size of the no. rows of A x no. of cols of A
                            if (i == j % numCols || i % numRows == j || i == j || i + numRows == j || i == j + numCols)
                            {
                                B[i, j] = valuesOfA[arrayIndex];
                            }

                        } 
                    }
                    // We jump ahead in the column indices, but the rows stay the same
                    columnIndex += numCols;
                    arrayIndex++;
                }
                // We jump ahead in the row indices and reset the columns back to 0 as a new line, almost
                columnIndex = 0;
                rowIndex += numRows;
            }
            Console.WriteLine(B.ToString());

            // This will take the Identity matrix and pivot each row to a column and put all those column into a vector
            // to solve Bx = I
            // x will contain the elements of the inverseKeyMatrix and we will have to take that vector and put it into
            // a square matrix

            double[] identityMatrixAsArray = new double[numRows * numCols];
            int congruence = 0;
            int startIndex = 0;
            for (int i = 0; i < identityMatrixAsArray.Length; i++)
            {
                if (i % numRows == congruence && i >= startIndex)
                {
                    identityMatrixAsArray[i] = 1;
                    congruence += 1;
                    startIndex += numRows;
                }
                Console.WriteLine($"{i}: {identityMatrixAsArray[i]}");
            }

            Vector<double> identityMatrixAsVector = Vector<double>.Build.DenseOfArray(identityMatrixAsArray);

            // Solve for x
            Vector<double> x = B.Solve(identityMatrixAsVector);
            Console.WriteLine(x.ToString());

            var C = A.Solve(I);
            Console.WriteLine(C.ToString());
            
            return B;
            */
        }

        public Matrix<double> MinorMatrixModN(Matrix<double> A, int modulus)
        {
            // A is the key matrix that we want to invert.
            int numRows = A.RowCount;
            int numCols = A.ColumnCount;

            if (numRows != numCols)
            {
                throw new InvertibleMatrixException("The input matrix must be a square matrix or you can't find a minor matrix");
                //Console.WriteLine("The input matrix must be a square matrix or you can't find a Minor Matrix");
                //return A;
            }

            Matrix<double> M = CreateMatrix.Dense<double>(numRows, numCols);
            Matrix<double> tempMatrix = CreateMatrix.Dense<double>(numRows - 1, numCols - 1);

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numCols; j++)
                {
                    // The determinant of the tempMatrix is the value of the minor matrix
                    tempMatrix = A.RemoveRow(i).RemoveColumn(j);
                    M[i, j] = (int) Math.Round(tempMatrix.Determinant()) % modulus;
                    M[i, j] = PositiveCongruence((int)M[i, j]);
                }
            }

            return M;
        }

        public Matrix<double> CofactorMatrixModN(Matrix<double> M, int modulus)
        {
            // M is the minor matrix of the key matrix that we want to invert.
            int numRows = M.RowCount;
            int numCols = M.ColumnCount;

            if (numRows != numCols)
            {
                throw new InvertibleMatrixException("The input matrix must be a square matrix or you can't find a cofactor matrix");
                //Console.WriteLine("The input matrix must be a square matrix or you can't find a Cofactor Matrix");
                //return M;
            }

            Matrix<double> C = CreateMatrix.Dense<double>(numRows, numCols);
            
            for (int i = 0; i < numRows; i++) 
            {
                for (int j = 0;j < numCols; j++)
                {
                    C[i,j] = (int)( (Math.Pow(-1, i+j)) * M[i,j] ) % modulus;
                    C[i,j] = PositiveCongruence((int)C[i, j]);
                }
            }

            return C;
        }

        public Matrix<double> AdjointMatrixModN( Matrix<double> A, int modulus )
        {
            // A is the key matrix that we want to invert.
            int numRows = A.RowCount;
            int numCols = A.ColumnCount;

            if (numRows != numCols)
            {
                throw new InvertibleMatrixException("The input matrix must be a square matrix or you can't find an adjoint matrix");
                //Console.WriteLine("The input matrix must be a square matrix or you can't find an Adjoint Matrix");
                //return A;
            }

            // Make the minor matrix
            Matrix<double> temp = MinorMatrixModN(A, modulus);

            // Make the Cofactor Matrix
            temp = CofactorMatrixModN(temp, modulus);

            temp = temp.Transpose();

            return temp;
        }
    }
}
