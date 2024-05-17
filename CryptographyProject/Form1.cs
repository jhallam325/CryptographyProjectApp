using Algorithms.GlobalVariables;
using Algorithms.Subclasses;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CryptographyProject
{
    public partial class Form1 : Form
    {
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
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            //**********************************************************************************************
            // Find input method whether file or text
            string inputText = "";
            string outputText = "";
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
                    reader = new StreamReader(Globals.PlainTextFullPath);
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
                ShiftCipher shiftCipher = new ShiftCipher();
                // Shift Cipher
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
            }
            else if (methodComboBox.SelectedIndex == 2)
            {
                // Affine Cipher
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
                    writer = new StreamWriter(Globals.CipherTextFullPath);
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
                methodSelectLabel.Text = "Key Information: Choose an integer, usually between 0 and 25";
            }
            else if (methodComboBox.SelectedIndex == 1)
            {
                methodSelectLabel.Text = "Key Information: Choose each letter in the alphabet, only once, and A will be substituted with the " +
                    "first letter, B with the second letter, ...";
            }
            else if (methodComboBox.SelectedIndex == 2)
            {
                methodSelectLabel.Text = "Key Information: Choose an integer such that GCD(key, 26) = 1";
            }


        }
    }
}
