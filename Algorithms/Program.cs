using Algorithms.Subclasses;
using Algorithms.GlobalVariables;

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

            string plaintext;
            //string trimmed;
            //string filtered = null;
            string ciphertext = null;
            char charPlaceHolder;
            int intPlaceHolder = 0;
            //int key = 6;

            StreamReader reader = null;
            StreamWriter writer = null;

            ShiftCipher shiftCipher = new ShiftCipher();
            
            try
            {
                // This is the key and should be an number of how many letters to shift.
                Console.Write("Input a number: ");
                string key = Console.ReadLine();

                if (!Directory.Exists(Globals.FilePath))
                {
                    Directory.CreateDirectory(Globals.FilePath);
                }


                
                //-------------------Encrypt--------------------------//
                // Opens text files where we will read and write.
                reader = new StreamReader(Globals.PlainTextFullPath);
                writer = new StreamWriter(Globals.CipherTextFullPath);

                // Reads every character in the reader (plaintext here) file
                plaintext = reader.ReadToEnd();
                //ciphertext = reader.ReadToEnd();

                // Use the Shift Cipher to Encrypy the plaintext.
                ciphertext = shiftCipher.Encrypt(plaintext, key);
                //plaintext = shiftCipher.Decrypt(ciphertext, key);

                // Write the ciphertext to the ciphertext file.
                writer.WriteLine(ciphertext);
                //writer.WriteLine(plaintext);
                

                /*
                //--------------------Decrypt----------------------//
                // Opens text files where we will read and write.
                reader = new StreamReader(Globals.WriteFullPath);
                writer = new StreamWriter(Globals.ReadFullPath);

                // Reads every character in the reader (plaintext here) file
                //plaintext = reader.ReadToEnd();
                ciphertext = reader.ReadToEnd();

                // Use the Shift Cipher to Encrypy the plaintext.
                //ciphertext = shiftCipher.Encrypt(plaintext, key);
                plaintext = shiftCipher.Decrypt(ciphertext, key);

                // Write the ciphertext to the ciphertext file.
                //writer.WriteLine(ciphertext);
                writer.WriteLine(plaintext);
                */
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                reader.Close();
                writer.Close();
            }
            

            // Decrypt

            try
            {
                // This is the key and should be an number of how many letters to shift.
                Console.Write("Input a number: ");
                string key = Console.ReadLine();

                if (!Directory.Exists(Globals.FilePath))
                {
                    Directory.CreateDirectory(Globals.FilePath);
                }


                //--------------------Decrypt----------------------//
                // Opens text files where we will read and write.
                reader = new StreamReader(Globals.CipherTextFullPath);
                writer = new StreamWriter(Globals.PlainTextFullPath);

                // Reads every character in the reader (plaintext here) file
                //plaintext = reader.ReadToEnd();
                ciphertext = reader.ReadToEnd();

                // Use the Shift Cipher to Encrypy the plaintext.
                //ciphertext = shiftCipher.Encrypt(plaintext, key);
                plaintext = shiftCipher.Decrypt(ciphertext, key);

                // Write the ciphertext to the ciphertext file.
                //writer.WriteLine(ciphertext);
                writer.WriteLine(plaintext);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                reader.Close();
                writer.Close();
            }
        }
    }
}
