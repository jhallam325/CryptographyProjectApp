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

        private void runButton_Click(object sender, EventArgs e)
        {
            //**********************************************************************************************
            // Find input method whether file or text
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
                    reader = new StreamReader(Globals.PlainTextFullPath);

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
            }

            //******************************************************************************
            // Find encryption algorithm to use
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
                    //run decrypy method
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
                // Can this be a generic method that does this for each cipher?
                // Maybe I could put a requirement that it must implement ICipher?
                SubstitutionCipher substitutionCipher = new SubstitutionCipher();

                if (encryptRadioButton.Checked)
                {
                    // run Encrypt method
                    outputText = substitutionCipher.Encrypt(inputText, key);

                }
                else if (decryptRadioButton.Checked)
                {
                    //run decrypy method
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
                    // run Encrypt method
                    outputText = affineCipher.Encrypt(inputText, key);

                }
                else if (decryptRadioButton.Checked)
                {
                    //run decrypy method
                    outputText = affineCipher.Decrypt(inputText, key);
                }
                else
                {
                    MessageBox.Show("Hey, you need to choose whether you want to encrypt or decrypt your message!");
                }
            }
            else if (methodComboBox.SelectedIndex == 3)
            {
                // Vignere Cipher
            }
            else if (methodComboBox.SelectedIndex == 4)
            {
                // Hill Cipher
            }
            else if (methodComboBox.SelectedIndex == 5)
            {
                // Permutation Cipher
            }
            else if (methodComboBox.SelectedIndex == 6)
            {
                // Synchronous Stream Cipher
            }
            else if (methodComboBox.SelectedIndex == 7)
            {
                // Periodic Stream Cipher
            }
            else if (methodComboBox.SelectedIndex == 8)
            {
                // Autokey Cipher
            }

            //**************************************************************************************************************
            // Output the text to the screen or file
            if (outputTextRadioButton.Checked)
            {
                outputTextBox.Text = outputText;
            }
            else if (inputFileRadioButton.Checked)
            {

                // Open The file to read
                StreamWriter writer = null;

                try
                {
                    // The top line is for debugging practice
                    writer = new StreamWriter(Globals.CipherTextFullPath);

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
                }

            }
            else
            {
                MessageBox.Show("Hey, you need to choose an output method!");
            }

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
                methodSelectLabel.Text = "Key Information: Hill Cipher";
            }
            else if (methodComboBox.SelectedIndex == 5)
            {
                // Permutation Cipher
                methodSelectLabel.Text = "Key Information: Permutation Cipher";
            }
            else if (methodComboBox.SelectedIndex == 6)
            {
                // Synchronous Stream Cipher
                methodSelectLabel.Text = "Key Information: Synchronous Stream Cipher";
            }
            else if (methodComboBox.SelectedIndex == 7)
            {
                // Periodic Stream Cipher
                methodSelectLabel.Text = "Key Information: Periodic Stream Cipher";
            }
            else if (methodComboBox.SelectedIndex == 8)
            {
                // Autokey Cipher
                methodSelectLabel.Text = "Key Information: Autokey Cipher";
            }


        }

        private void outputFileBrowseButton_Click(object sender, EventArgs e)
        {
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
        }
    }
}
