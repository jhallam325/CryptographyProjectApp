using Algorithms.GlobalVariables;
using Algorithms.Subclasses;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CryptographyProject
{
    public partial class Form1 : Form
    {
        string plaintextFullPath = string.Empty;
        string ciphertextFullPath = string.Empty;
        public Form1()
        {
            InitializeComponent();
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
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                plaintextFullPath = openFileDialog1.FileName;
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
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                ciphertextFullPath = openFileDialog1.FileName;
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
                // Open The file to read
                StreamReader reader = null;

                try
                {
                    // The top line is for debugging practice
                    //reader = new StreamReader(Globals.PlainTextFullPath);
                    reader = new StreamReader(inputFileTextBox.Text);

                    // This line is what will really be in the app.
                    //reader = new StreamReader(plaintextFullPath);
                    inputText = reader.ReadToEnd();
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
            *                           Find encryption algorithm to use                                 *
            *                                                                                            *
            *********************************************************************************************/
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

                string inputDirectory = Path.GetDirectoryName(inputFileTextBox.Text);
                string outputDirectory = Path.GetDirectoryName(outputFileTextBox.Text);
                string pathOrFile = outputFileTextBox.Text;
                string fileName = Path.GetFileName(pathOrFile);
                bool temp = File.Exists(pathOrFile);
                // The path given is a full path and we can continue
                if (Path.IsPathRooted(pathOrFile) && !Path.GetPathRoot(pathOrFile).Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal))
                {
                    // We can override the file
                    // Open The file to read
                    StreamWriter writer = null;

                    try
                    {
                        // The top line is for debugging practice
                        //writer = new StreamWriter(Globals.CipherTextFullPath);
                        writer = new StreamWriter(outputFileTextBox.Text);

                        // This line is what will really be in the app.
                        // Can I get open or create permission to create a new file if a new file is input?
                        //writer = new StreamReader(ciphertextFullPath);
                        writer.WriteLine(outputText);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        writer.Close();
                        MessageBox.Show($"{outputFileTextBox.Text} saved correctly");
                    }
                }
                else if (File.Exists(pathOrFile))
                {
                    File.Create(pathOrFile);
                }

                if (false)
                {
                    //try
                    //{
                    //    string text = File.ReadAllText(ciphertextFullPath);
                    //    size = text.Length;
                    //    outputFileTextBox.Text = ciphertextFullPath;

                    //}
                    //catch (IOException)
                    //{
                    //}
                }
                else
                {
                    outputFileTextBox.Text = $"{inputDirectory} adds {fileName}";
                }


                /* Real code - break for debugging
                // Open The file to read
                StreamWriter writer = null;

                try
                {
                    // The top line is for debugging practice
                    //writer = new StreamWriter(Globals.CipherTextFullPath);
                    writer = new StreamWriter(outputFileTextBox.Text);

                    // This line is what will really be in the app.
                    // Can I get open or create permission to create a new file if a new file is input?
                    //writer = new StreamReader(ciphertextFullPath);
                    writer.WriteLine(outputText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    writer.Close();
                    MessageBox.Show($"{outputFileTextBox.Text} saved correctly");
                }
                */

            }
            else
            {
                MessageBox.Show("Hey, you need to choose an output method!");
            }

        }
    }
}
