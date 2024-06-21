using Algorithms.GlobalVariables;
using Algorithms.Subclasses;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CryptographyProject
{
    public partial class Form1 : Form
    {
        string plaintextFullPath = string.Empty;
        string ciphertextFullPath = string.Empty;
        // Add a modulus variable than can be changed from 26 to 127-special characters
        // That would require a huge rewrite in the stream cipher. maybe others?
        public Form1()
        {
            InitializeComponent();
            keyCheckBox.Checked = true;
        }

        private void inputFileBrowseButton_Click(object sender, EventArgs e)
        {
            /*
            // Show the dialog that allows user to select a file, the 
            // call will result a value from the DialogResult enum
            // when the dialog is dismissed.
            DialogResult result = this.openFileDialog.ShowDialog();
            // if a file is selected
            if (result == DialogResult.OK)
            {
                // Set the selected file URL to the textbox
                this.fileURLTextBox.Text = this.openFileDialog.FileName;
            }
            */
            inputFileRadioButton.Checked = true;
            int size = -1;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text|*.txt|All|*.*";
            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                plaintextFullPath = openFileDialog.FileName;
                try
                {
                    string text = File.ReadAllText(plaintextFullPath);
                    size = text.Length;
                    inputFileTextBox.Text = plaintextFullPath;
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void outputFileBrowseButton_Click(object sender, EventArgs e)
        {
            outputFileRadioButton.Checked = true;
            int size = -1;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text|*.txt|All|*.*";
            DialogResult result = openFileDialog.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                ciphertextFullPath = openFileDialog.FileName;
                try
                {
                    string text = File.ReadAllText(ciphertextFullPath);
                    size = text.Length;
                    outputFileTextBox.Text = ciphertextFullPath;
                }
                catch (IOException)
                {
                }
            }
            Console.WriteLine(size); // <-- Shows file size in debugging mode.
            Console.WriteLine(result); // <-- For debugging use.
        }

        private void inputTextBox_TextChanged(object sender, EventArgs e)
        {
            inputTextRadioButton.Checked = true;
            outputTextRadioButton.Checked = true;
        }

        private void methodComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (methodComboBox.SelectedIndex == 0)
            {
                // Shift Cipher
                methodSelectLabel.Text = "Key Information: Choose an integer, usually between 0 and 25";
            }
            else if (methodComboBox.SelectedIndex == 1)
            {
                // Substitution Cipher
                methodSelectLabel.Text = "Key Information: Choose each letter in the alphabet, only once, and A will be substituted with the " +
                    "first letter, B with the second letter, ...";
            }
            else if (methodComboBox.SelectedIndex == 2)
            {
                // Affine Cipher
                methodSelectLabel.Text = $"Key Information: Choose 2 integers \"a\" and \"b\" such that GCD(a, {Globals.modulus}) = 1 and a and b are between 0-25 inclusive.\n" +
                    "\t\tEnter the numbers as a,b like 3,10";
            }
            else if (methodComboBox.SelectedIndex == 3)
            {
                // Vignere Cipher
                methodSelectLabel.Text = "Key Information: Choose an string of letters, possibly a word";
            }
            else if (methodComboBox.SelectedIndex == 4)
            {
                // Hill Cipher
                methodSelectLabel.Text = "Key Information: Enter a square Matrix in the form: 1,2,3;4,5,6;7,8,9 where individual " +
                "elements are seperated by commas and rows \n" +
                "are seperated by semi-colons";
            }
            else if (methodComboBox.SelectedIndex == 5)
            {
                // Permutation Cipher
                methodSelectLabel.Text = "Key Information: Enter a list of numbers, seperated by a comma. If you chose 5 numbers, they need to be the numbers 1-5 but \n" +
                    "rearranged however you like. ex: 3,2,5,4,1";
            }
            else if (methodComboBox.SelectedIndex == 6)
            {
                // Synchronous Stream Cipher
                methodSelectLabel.Text = "Key Information: Enter a whole number";
            }
            else if (methodComboBox.SelectedIndex == 7)
            {
                // Autokey Cipher
                methodSelectLabel.Text = "Key Information: Enter a letter";
            }
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            /*********************************************************************************************
            *                                                                                            * 
            *                           Find input method whether file or text                           *
            *                                                                                            *
            *********************************************************************************************/
            string inputText = string.Empty;
            string outputText = string.Empty;
            string key = keyTextBox.Text;

            if (inputTextRadioButton.Checked)
            {
                inputText = inputTextBox.Text;
            }
            else if (inputFileRadioButton.Checked)
            {
                string extension = Path.GetExtension(inputFileTextBox.Text);
                if (extension != ".txt")
                {
                    MessageBox.Show("You can only read from a .txt file");
                    return;
                }

                // Open The file to read
                StreamReader reader = null;

                try
                {
                    // The top line is for debugging practice
                    //reader = new StreamReader(Globals.PlainTextFullPath);
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    reader.Close();
                }

            }
            else
            {
                MessageBox.Show("Hey, you need to choose an input method!");
                return;
            }

            /*********************************************************************************************
            *                                                                                            * 
            *                                       Check for key                                        *
            *                                                                                            *
            *********************************************************************************************/
            if (keyTextBox.Text == string.Empty || keyTextBox.Text == null)
            {
                MessageBox.Show("You forgot to input the key!");
                return;
            }


            /*********************************************************************************************
            *                                                                                            * 
            *                           Find encryption algorithm to use                                 *
            *                                                                                            *
            *********************************************************************************************/
            try
            {
                if (methodComboBox.SelectedIndex == 0)
                {
                    // Shift Cipher

                    // Can this be a generic method that does this for each cipher?
                    // Maybe I could put a requirement that it must implement ICipher?
                    ShiftCipher shiftCipher = new ShiftCipher();

                    if (encryptRadioButton.Checked)
                    {
                        // run Encrypt method
                        outputText = shiftCipher.Encrypt(inputText, key);

                    }
                    else if (decryptRadioButton.Checked)
                    {
                        // run Decrypt method
                        outputText = shiftCipher.Decrypt(inputText, key);
                    }
                    else
                    {
                        MessageBox.Show("Hey, you need to choose whether you want to encrypt or decrypt your message!");
                    }
                }
                else if (methodComboBox.SelectedIndex == 1)
                {
                    // Substitution Cipher

                    SubstitutionCipher substitutionCipher = new SubstitutionCipher();

                    if (encryptRadioButton.Checked)
                    {
                        outputText = substitutionCipher.Encrypt(inputText, key);

                    }
                    else if (decryptRadioButton.Checked)
                    {
                        outputText = substitutionCipher.Decrypt(inputText, key);
                    }
                    else
                    {
                        MessageBox.Show("Hey, you need to choose whether you want to encrypt or decrypt your message!");
                    }
                }
                else if (methodComboBox.SelectedIndex == 2)
                {
                    // Affine Cipher
                    AffineCipher affineCipher = new AffineCipher();

                    if (encryptRadioButton.Checked)
                    {
                        outputText = affineCipher.Encrypt(inputText, key);

                    }
                    else if (decryptRadioButton.Checked)
                    {
                        outputText = affineCipher.Decrypt(inputText, key);
                    }
                    else
                    {
                        MessageBox.Show("Hey, you need to choose whether you want to encrypt or decrypt your message!");
                    }
                }
                else if (methodComboBox.SelectedIndex == 3)
                {
                    // Vigenere Cipher
                    VigenereCipher vigenereCipher = new VigenereCipher();

                    if (encryptRadioButton.Checked)
                    {
                        outputText = vigenereCipher.Encrypt(inputText, key);

                    }
                    else if (decryptRadioButton.Checked)
                    {
                        outputText = vigenereCipher.Decrypt(inputText, key);
                    }
                    else
                    {
                        MessageBox.Show("Hey, you need to choose whether you want to encrypt or decrypt your message!");
                    }
                }
                else if (methodComboBox.SelectedIndex == 4)
                {
                    // Hill Cipher
                    HillCipher hillCipher = new HillCipher();

                    if (encryptRadioButton.Checked)
                    {
                        outputText = hillCipher.Encrypt(inputText, key);

                    }
                    else if (decryptRadioButton.Checked)
                    {
                        outputText = hillCipher.Decrypt(inputText, key);
                    }
                    else
                    {
                        MessageBox.Show("Hey, you need to choose whether you want to encrypt or decrypt your message!");
                    }
                }
                else if (methodComboBox.SelectedIndex == 5)
                {
                    // Permutation Cipher
                    PermutationCipher permutationCipher = new PermutationCipher();


                    if (encryptRadioButton.Checked)
                    {
                        outputText = permutationCipher.Encrypt(inputText, key);

                    }
                    else if (decryptRadioButton.Checked)
                    {
                        outputText = permutationCipher.Decrypt(inputText, key);
                    }
                    else
                    {
                        MessageBox.Show("Hey, you need to choose whether you want to encrypt or decrypt your message!");
                    }

                }
                else if (methodComboBox.SelectedIndex == 6)
                {
                    // Stream Cipher
                    StreamCipher streamCipher = new StreamCipher();

                    if (encryptRadioButton.Checked)
                    {
                        outputText = streamCipher.Encrypt(inputText, key);

                    }
                    else if (decryptRadioButton.Checked)
                    {
                        outputText = streamCipher.Decrypt(inputText, key);
                    }
                    else
                    {
                        MessageBox.Show("Hey, you need to choose whether you want to encrypt or decrypt your message!");
                    }
                }
                else if (methodComboBox.SelectedIndex == 7)
                {
                    // Autokey Cipher
                    AutokeyCipher autokeyCipher = new AutokeyCipher();

                    if (encryptRadioButton.Checked)
                    {
                        outputText = autokeyCipher.Encrypt(inputText, key);

                    }
                    else if (decryptRadioButton.Checked)
                    {
                        outputText = autokeyCipher.Decrypt(inputText, key);
                    }
                    else
                    {
                        MessageBox.Show("Hey, you need to choose whether you want to encrypt or decrypt your message!");
                    }
                }
                else
                {
                    MessageBox.Show("Don't forget to choose an encryption/decryption algorithm!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if (methodComboBox.SelectedIndex == 0)
                {
                    // Shift Cipher
                    methodSelectLabel.Text = "Key Information: Choose an integer, usually between 0 and 25\n" +
                        "Try using 3";

                }
                else if (methodComboBox.SelectedIndex == 1)
                {
                    // Substitution Cipher
                    methodSelectLabel.Text = "Key Information: Choose each letter in the alphabet, only once, and A will be substituted with the\n" +
                    "first letter, B with the second letter, ...\n" +
                    "Try using: QWERTYUIOPASDFGHJKLZXCVBNM";
                }
                else if (methodComboBox.SelectedIndex == 2)
                {
                    // Affine Cipher
                    methodSelectLabel.Text = $"Key Information: Choose 2 integers \"a\" and \"b\" such that GCD(a, {Globals.modulus}) = 1 and a and b are between 0-25 inclusive.\n" +
                   "\t\tEnter the numbers as a,b like 2,8\n" +
                   "Try using 3,10";


                }
                else if (methodComboBox.SelectedIndex == 3)
                {
                    // Vigenere Cipher
                    methodSelectLabel.Text = "Key Information: Choose an string of letters, possibly a word\n" +
                        "Try using WORD";
                }
                else if (methodComboBox.SelectedIndex == 4)
                {
                    // Hill Cipher
                    methodSelectLabel.Text = "Key Information: Enter a square Matrix in the form: 1,2,3;4,5,6;7,8,9 where individual " +
                "elements are seperated by commas and rows \n" +
                "are seperated by semi-colons\n" +
                "Finding an invertible matrix is tough.\n" +
                "Try using 11,8;3,7";
                }
                else if (methodComboBox.SelectedIndex == 5)
                {
                    // Permutation Cipher
                    methodSelectLabel.Text = "Key Information: Enter a list of numbers, seperated by a comma. If you chose 5 numbers, they need to be the numbers 1-5 but \n" +
                    "rearranged however you like. ex: 3,2,5,4,1\n" +
                    "Try using 3,2,5,4,1";
                }
                else if (methodComboBox.SelectedIndex == 6)
                {
                    // Stream Cipher
                    methodSelectLabel.Text = "Key Information: Enter a whole number\n" +
                        "Try using 18";


                }
                else if (methodComboBox.SelectedIndex == 7)
                {
                    // Autokey Cipher
                    methodSelectLabel.Text = "Key Information: Enter a letter\n" +
                        "Try using C";


                }
                return;
            }

            /*********************************************************************************************
            *                                                                                            * 
            *                           Output text to the screen or a file                              *
            *                                                                                            *
            *********************************************************************************************/
            if (outputTextRadioButton.Checked)
            {
                outputTextBox.Text = outputText;
            }
            else if (outputFileRadioButton.Checked)
            {
                string inputPath = inputFileTextBox.Text;
                string inputPathWithoutFile = inputPath.Substring(0, inputPath.Length - Path.GetFileName(inputPath).Length);
                string pathOrFile = outputFileTextBox.Text;
                string extension = Path.GetExtension(pathOrFile);

                // So you can be lazy and type the file name you know or want, without the .txt
                if (extension == string.Empty)
                {
                    pathOrFile += ".txt";
                }

                extension = Path.GetExtension(pathOrFile);
                string fileName = Path.GetFileName(pathOrFile);
                string outputFullPath = inputPathWithoutFile + fileName;
                StreamWriter writer = null;

                // First, make sure it's a text file.
                if (extension != ".txt")
                {
                    MessageBox.Show("You can only save to a .txt file");
                    return;
                }

                // The path given is a full path and we can continue
                if (Path.IsPathRooted(pathOrFile) && !Path.GetPathRoot(pathOrFile).Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                {
                    if (!File.Exists(pathOrFile))
                    {
                        // Create the file because we have the full path.
                        using (File.Create(pathOrFile))
                        {

                        }
                    }

                    // Write to the file
                    using (writer = new StreamWriter(pathOrFile))
                    {
                        writer.WriteLine(outputText);
                    }
                }
                else if (!File.Exists(outputFullPath))
                {
                    using (File.Create(outputFullPath))
                    {

                    }
                    using (writer = new StreamWriter(outputFullPath))
                    {
                        writer.WriteLine(outputText);
                    }
                }
                else
                {
                    using (writer = new StreamWriter(outputFullPath))
                    {
                        writer.WriteLine(outputText);
                    }
                }
                MessageBox.Show($"Your file {outputFullPath} was successfully saved.");
            }
            else
            {
                MessageBox.Show("Hey, you need to choose an output method!");
            }

        }

        private void inputFileTextBox_TextChanged(object sender, EventArgs e)
        {
            inputFileRadioButton.Checked = true;
        }

        private void outputFileTextBox_TextChanged(object sender, EventArgs e)
        {
            outputFileRadioButton.Checked = true;
        }

        private void keyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (keyCheckBox.Checked)
            {
                keyTextBox.PasswordChar = ' ';
            }

            if (!keyCheckBox.Checked)
            {
                keyTextBox.PasswordChar = '\0';
            }
        }



        // This came from stackoverflow https://stackoverflow.com/questions/3730968/how-to-disable-cursor-in-textbox
        [DllImport("user32.dll")]
        static extern bool HideCaret(IntPtr hWnd);

        private void keyTextBox_TextChanged(object sender, EventArgs e)
        {
            if (keyCheckBox.Checked)
            {
                HideCaret(keyTextBox.Handle);
            }
        }
    }
}
