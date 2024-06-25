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

        private Size formOriginalSize;

        // Group Boxes
        private Rectangle rectInputGroup;
        private Rectangle rectSelectorGroup;
        private Rectangle rectOutputGroup;
        //inputFileRadioButton

        // Input Group Box
        private Rectangle rectInputFileRadioButton; // Don't Resize
        private Rectangle rectInputFileTextBox; // Resize in X
        private Rectangle rectInputFileBrowseButton; // Don't Resize
        private Rectangle rectInputTextRadioButton; // Don't resize
        private Rectangle rectInputTextBox; // Resize in X and Y;

        // Selector Group Box
        private Rectangle rectEncryptRadioButton; // Don't Resize
        private Rectangle rectDecryptRadioButton; // Don't Resize
        private Rectangle rectMethodLabel; // Don't Resize 
        private Rectangle rectMethodComboBox; // Resize in X
        private Rectangle rectMethodSelectLabel; // Resize in X
        private Rectangle rectKeyLabel; // Don't Resize
        private Rectangle rectKeyTextBox; // Resize in X
        private Rectangle rectKeyCheckBox; // Don't Resize

        // Output Group Box
        private Rectangle rectOutputFileRadioButton; // Don't Resize
        private Rectangle rectOutputFileTextBox; // Resize in X
        private Rectangle rectOutputFileBrowseButton; // Don't Resize
        private Rectangle rectOutputTextRadioButton; // Don't resize
        private Rectangle rectOutputTextBox; // Resize in X and Y;

        // Run Button
        private Rectangle rectRunButton; // Don't Resize




        public Form1()
        {
            InitializeComponent();
            keyCheckBox.Checked = true;


            // Responsiveness
            this.Resize += MyResize;
            formOriginalSize = this.Size;

            // Group Boxes
            rectInputGroup = new Rectangle(inputGroupBox.Location, inputGroupBox.Size);
            rectSelectorGroup = new Rectangle(selectorGroupBox.Location, selectorGroupBox.Size);
            rectOutputGroup = new Rectangle(outputGroupBox.Location, outputGroupBox.Size);

            // Input Group Box
            rectInputFileRadioButton = new Rectangle(inputFileRadioButton.Location, inputFileRadioButton.Size);
            rectInputFileTextBox = new Rectangle(inputFileTextBox.Location, inputFileTextBox.Size);
            rectInputFileBrowseButton = new Rectangle(inputFileBrowseButton.Location, inputFileBrowseButton.Size);
            rectInputTextRadioButton = new Rectangle(inputTextRadioButton.Location, inputTextRadioButton.Size);
            rectInputTextBox = new Rectangle(inputTextBox.Location, inputTextBox.Size);

            // Selector Group
            rectEncryptRadioButton = new Rectangle(encryptRadioButton.Location, encryptRadioButton.Size);
            rectDecryptRadioButton = new Rectangle(decryptRadioButton.Location, decryptRadioButton.Size);
            rectMethodLabel = new Rectangle(methodLabel.Location, methodLabel.Size); 
            rectMethodComboBox = new Rectangle(methodComboBox.Location, methodComboBox.Size); 
            rectMethodSelectLabel = new Rectangle(methodSelectLabel.Location, methodSelectLabel.Size);
            rectKeyLabel = new Rectangle(keyLabel.Location, keyLabel.Size);
            rectKeyTextBox = new Rectangle(keyTextBox.Location, keyTextBox.Size);
            rectKeyCheckBox = new Rectangle(keyCheckBox.Location, keyCheckBox.Size);

            // Output Group Box
            rectOutputFileRadioButton = new Rectangle(outputFileRadioButton.Location, outputFileRadioButton.Size);
            rectOutputFileTextBox = new Rectangle(outputFileTextBox.Location, outputFileTextBox.Size);
            rectOutputFileBrowseButton = new Rectangle(outputFileBrowseButton.Location, outputFileBrowseButton.Size);
            rectOutputTextRadioButton = new Rectangle(outputTextRadioButton.Location, outputTextRadioButton.Size);
            rectOutputTextBox = new Rectangle(outputTextBox.Location, outputTextBox.Size);

            // Run Button
            rectRunButton = new Rectangle(runButton.Location, runButton.Size);
    }

        private void MyResize(object sender, EventArgs e)
        {
            // Group Boxes
            resizeControlXAndY(inputGroupBox, rectInputGroup);
            resizeControlXAndY(selectorGroupBox, rectSelectorGroup);
            resizeControlXAndY(outputGroupBox, rectOutputGroup);

            // Input Group Box
            moveControl(inputFileRadioButton, rectInputFileRadioButton);
            resizeControlX(inputFileTextBox, rectInputFileTextBox);
            moveControl(inputFileBrowseButton, rectInputFileBrowseButton);
            moveControl(inputTextRadioButton, rectInputTextRadioButton);
            resizeControlXAndY(inputTextBox, rectInputTextBox);

            // Selector Group
            moveControl(encryptRadioButton, rectEncryptRadioButton); // Don't Resize
            moveControl(decryptRadioButton, rectDecryptRadioButton); // Don't Resize
            moveControl(methodLabel, rectMethodLabel); // Don't Resize 
            resizeControlX(methodComboBox, rectMethodComboBox); // Resize in X
            resizeControlX(methodSelectLabel, rectMethodSelectLabel); // Resize in X
            moveControl(keyLabel, rectKeyLabel); // Don't Resize
            resizeControlX(keyTextBox, rectKeyTextBox); // Resize in X
            moveControl(keyCheckBox, rectKeyCheckBox); // Don't Resize

            // Output Group Box
            moveControl(outputFileRadioButton, rectOutputFileRadioButton);
            resizeControlX(outputFileTextBox, rectOutputFileTextBox);
            moveControl(outputFileBrowseButton, rectOutputFileBrowseButton);
            moveControl(outputTextRadioButton, rectOutputTextRadioButton);
            resizeControlXAndY(outputTextBox, rectOutputTextBox);

            // Run Button
            moveControl(runButton, rectRunButton);
        }

        private void resizeControlXAndY(Control control, Rectangle rectangle)
        {
            float xRatio = (float)(this.Width) / (float)formOriginalSize.Width;
            float yRatio = (float)(this.Height) / (float)formOriginalSize.Height;
            int newX = (int) (rectangle.X * xRatio);
            int newY = (int)(rectangle.Y * yRatio);
            int newWidth = (int) (rectangle.Width * xRatio);
            int newHeight = (int) (rectangle.Height * yRatio);

            control.Location = new Point(newX, newY);
            control.Size = new Size(newWidth, newHeight);
        }

        private void resizeControlX(Control control, Rectangle rectangle)
        {
            // This will resize the control only in the X direction
            float xRatio = (float)(this.Width) / (float)formOriginalSize.Width;
            float yRatio = (float)(this.Height) / (float)formOriginalSize.Height;
            int newX = (int)(rectangle.X * xRatio);
            int newY = (int)(rectangle.Y * yRatio);
            int newWidth = (int)(rectangle.Width * xRatio);
            //int newHeight = (int)(rectangle.Height * yRatio);

            control.Location = new Point(newX, newY);
            control.Size = new Size(newWidth, rectangle.Height);
        }

        private void moveControl(Control control, Rectangle rectangle)
        {
            // This will move a control without resizing it
            float xRatio = (float)(this.Width) / (float)formOriginalSize.Width;
            float yRatio = (float)(this.Height) / (float)formOriginalSize.Height;
            int newX = (int)(rectangle.X * xRatio);
            int newY = (int)(rectangle.Y * yRatio);
            //int newWidth = (int)(rectangle.Width * xRatio);
            //int newHeight = (int)(rectangle.Height * yRatio);

            control.Location = new Point(newX, newY);
            //control.Size = new Size(newWidth, newHeight);
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
                // Stream Cipher
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
                if (inputTextBox.Text == null || inputTextBox.Text.Length == 0)
                {
                    MessageBox.Show("Um.... we have no text to encrypt/decrypt.");
                    return;
                }
                inputText = inputTextBox.Text;

            }
            else if (inputFileRadioButton.Checked)
            {
                string extension = Path.GetExtension(inputFileTextBox.Text);

                if (inputFileTextBox.Text == null || inputFileTextBox.Text == string.Empty)
                {
                    MessageBox.Show("You need to input a file by typing its location or using the browse button");
                    return;
                }

                if (extension != ".txt")
                {
                    MessageBox.Show("You can only read from a .txt file");
                    return;
                }


                // Open The file to read
                StreamReader reader = null;

                // Write to the file
                using (reader = new StreamReader(inputFileTextBox.Text))
                {
                    inputText = reader.ReadToEnd();
                }


                //try
                //{
                //    // The top line is for debugging practice
                //    //reader = new StreamReader(Globals.PlainTextFullPath);
                //    reader = new StreamReader(inputFileTextBox.Text);

                //    // This line is what will really be in the app.
                //    //reader = new StreamReader(plaintextFullPath);
                //    inputText = reader.ReadToEnd();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
                //finally
                //{
                //    reader.Close();
                //}

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
            try
            {
                if (methodComboBox.SelectedIndex == 0)
                {
                    // Shift Cipher

                    // Can this be a generic method that does this for each cipher?
                    // Maybe I could put a requirement that it must implement ICipher?
                    ShiftCipher shiftCipher = new ShiftCipher();

                    if (!KeyExists())
                    {
                        MessageBox.Show("You forgot to input the key!");
                        return;
                    }

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

                    if (!KeyExists())
                    {
                        MessageBox.Show("You forgot to input the key!");
                        return;
                    }

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

                    if (!KeyExists())
                    {
                        MessageBox.Show("You forgot to input the key!");
                        return;
                    }

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

                    if (!KeyExists())
                    {
                        MessageBox.Show("You forgot to input the key!");
                        return;
                    }

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

                    if (!KeyExists())
                    {
                        MessageBox.Show("You forgot to input the key!");
                        return;
                    }

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

                    if (!KeyExists())
                    {
                        MessageBox.Show("You forgot to input the key!");
                        return;
                    }

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

                    if (!KeyExists())
                    {
                        MessageBox.Show("You forgot to input the key!");
                        return;
                    }

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

                    if (!KeyExists())
                    {
                        MessageBox.Show("You forgot to input the key!");
                        return;
                    }

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

                if (pathOrFile == null || pathOrFile.Length == 0)
                {
                    MessageBox.Show("You need to input an output a file by typing its location or using the browse button\n" +
                        "You can also type in the name of the file you want created and it will automatically\n" +
                        "be created in the directory of the input file.");
                    return;
                }

                // So you can be lazy and type the file name you know or want, without the .txt
                if (extension == string.Empty)
                {
                    pathOrFile += ".txt";
                }

                extension = Path.GetExtension(pathOrFile);
                string fileName = Path.GetFileName(pathOrFile);
                if (inputPathWithoutFile == null || inputPathWithoutFile.Length == 0)
                {
                    // Automatically add into user's documents folder -> C:\users\[User1]\Documents
                    inputPathWithoutFile = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\";
                }

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
                        // Create the file because we have the full path, then close the filestream so we can write to the file.
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
                    // The given path wasn't a full path but we created a full path using the input file's directory
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
                return;
            }

        }

        /*********************************************************************************************
        *                                                                                            * 
        *                                 Make sure a key is unput                                   *
        *                                                                                            *
        *********************************************************************************************/
        private bool CheckForKey()
        {
            if (keyTextBox.Text == null || keyTextBox.Text.Length == 0)
            {
                return false;
            }
            return true;
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

        private bool KeyExists()
        {
            if (keyTextBox.Text == string.Empty || keyTextBox.Text == null)
            {
                return false;
            }
            return true;
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
