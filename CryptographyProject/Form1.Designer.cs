namespace CryptographyProject
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            inputGroupBox = new GroupBox();
            inputTextBox = new TextBox();
            inputTextRadioButton = new RadioButton();
            inputFileBrowseButton = new Button();
            inputFileTextBox = new TextBox();
            inputFileRadioButton = new RadioButton();
            selectorGroupBox = new GroupBox();
            methodSelectLabel = new Label();
            keyCheckBox = new CheckBox();
            keyTextBox = new TextBox();
            keyLabel = new Label();
            methodComboBox = new ComboBox();
            methodLabel = new Label();
            decryptRadioButton = new RadioButton();
            encryptRadioButton = new RadioButton();
            outputGroupBox = new GroupBox();
            outputTextBox = new TextBox();
            outputTextRadioButton = new RadioButton();
            outputFileBrowseButton = new Button();
            outputFileTextBox = new TextBox();
            outputFileRadioButton = new RadioButton();
            runButton = new Button();
            inputGroupBox.SuspendLayout();
            selectorGroupBox.SuspendLayout();
            outputGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // inputGroupBox
            // 
            inputGroupBox.Anchor = AnchorStyles.None;
            inputGroupBox.Controls.Add(inputTextBox);
            inputGroupBox.Controls.Add(inputTextRadioButton);
            inputGroupBox.Controls.Add(inputFileBrowseButton);
            inputGroupBox.Controls.Add(inputFileTextBox);
            inputGroupBox.Controls.Add(inputFileRadioButton);
            inputGroupBox.Location = new Point(12, 12);
            inputGroupBox.Name = "inputGroupBox";
            inputGroupBox.Size = new Size(731, 203);
            inputGroupBox.TabIndex = 1;
            inputGroupBox.TabStop = false;
            inputGroupBox.Text = "Input";
            // 
            // inputTextBox
            // 
            inputTextBox.BorderStyle = BorderStyle.FixedSingle;
            inputTextBox.Location = new Point(6, 75);
            inputTextBox.Multiline = true;
            inputTextBox.Name = "inputTextBox";
            inputTextBox.Size = new Size(719, 107);
            inputTextBox.TabIndex = 4;
            inputTextBox.TextChanged += inputTextBox_TextChanged;
            // 
            // inputTextRadioButton
            // 
            inputTextRadioButton.AutoSize = true;
            inputTextRadioButton.Location = new Point(6, 50);
            inputTextRadioButton.Name = "inputTextRadioButton";
            inputTextRadioButton.Size = new Size(46, 19);
            inputTextRadioButton.TabIndex = 3;
            inputTextRadioButton.TabStop = true;
            inputTextRadioButton.Text = "Text";
            inputTextRadioButton.UseVisualStyleBackColor = true;
            // 
            // inputFileBrowseButton
            // 
            inputFileBrowseButton.Location = new Point(650, 21);
            inputFileBrowseButton.Name = "inputFileBrowseButton";
            inputFileBrowseButton.Size = new Size(75, 23);
            inputFileBrowseButton.TabIndex = 2;
            inputFileBrowseButton.Text = "Browse";
            inputFileBrowseButton.UseVisualStyleBackColor = true;
            inputFileBrowseButton.Click += inputFileBrowseButton_Click;
            // 
            // inputFileTextBox
            // 
            inputFileTextBox.BorderStyle = BorderStyle.FixedSingle;
            inputFileTextBox.Location = new Point(65, 21);
            inputFileTextBox.Name = "inputFileTextBox";
            inputFileTextBox.Size = new Size(566, 23);
            inputFileTextBox.TabIndex = 1;
            // 
            // inputFileRadioButton
            // 
            inputFileRadioButton.AutoSize = true;
            inputFileRadioButton.Location = new Point(6, 22);
            inputFileRadioButton.Name = "inputFileRadioButton";
            inputFileRadioButton.Size = new Size(43, 19);
            inputFileRadioButton.TabIndex = 0;
            inputFileRadioButton.TabStop = true;
            inputFileRadioButton.Text = "File";
            inputFileRadioButton.UseVisualStyleBackColor = true;
            // 
            // selectorGroupBox
            // 
            selectorGroupBox.Anchor = AnchorStyles.None;
            selectorGroupBox.Controls.Add(methodSelectLabel);
            selectorGroupBox.Controls.Add(keyCheckBox);
            selectorGroupBox.Controls.Add(keyTextBox);
            selectorGroupBox.Controls.Add(keyLabel);
            selectorGroupBox.Controls.Add(methodComboBox);
            selectorGroupBox.Controls.Add(methodLabel);
            selectorGroupBox.Controls.Add(decryptRadioButton);
            selectorGroupBox.Controls.Add(encryptRadioButton);
            selectorGroupBox.Location = new Point(12, 222);
            selectorGroupBox.Name = "selectorGroupBox";
            selectorGroupBox.Size = new Size(731, 232);
            selectorGroupBox.TabIndex = 2;
            selectorGroupBox.TabStop = false;
            selectorGroupBox.Text = "Select a function:";
            // 
            // methodSelectLabel
            // 
            methodSelectLabel.AutoSize = true;
            methodSelectLabel.Location = new Point(6, 119);
            methodSelectLabel.Name = "methodSelectLabel";
            methodSelectLabel.Size = new Size(95, 15);
            methodSelectLabel.TabIndex = 6;
            methodSelectLabel.Text = "Key Information:";
            // 
            // keyCheckBox
            // 
            keyCheckBox.AutoSize = true;
            keyCheckBox.Location = new Point(8, 198);
            keyCheckBox.Name = "keyCheckBox";
            keyCheckBox.Size = new Size(110, 19);
            keyCheckBox.TabIndex = 3;
            keyCheckBox.Text = "Hide Characters";
            keyCheckBox.UseVisualStyleBackColor = true;
            // 
            // keyTextBox
            // 
            keyTextBox.BorderStyle = BorderStyle.FixedSingle;
            keyTextBox.Location = new Point(74, 169);
            keyTextBox.Name = "keyTextBox";
            keyTextBox.Size = new Size(645, 23);
            keyTextBox.TabIndex = 5;
            // 
            // keyLabel
            // 
            keyLabel.AutoSize = true;
            keyLabel.Location = new Point(8, 171);
            keyLabel.Name = "keyLabel";
            keyLabel.Size = new Size(26, 15);
            keyLabel.TabIndex = 4;
            keyLabel.Text = "Key";
            // 
            // methodComboBox
            // 
            methodComboBox.FormattingEnabled = true;
            methodComboBox.Items.AddRange(new object[] { "Shift Cipher", "Substitution Cipher", "Affine Cipher", "Vigenere Cipher", "Hill Cipher", "Permutation Cipher", "Stream Cipher", "Autokey Cipher" });
            methodComboBox.Location = new Point(74, 82);
            methodComboBox.Name = "methodComboBox";
            methodComboBox.Size = new Size(645, 23);
            methodComboBox.TabIndex = 3;
            methodComboBox.SelectedIndexChanged += methodComboBox_SelectedIndexChanged;
            // 
            // methodLabel
            // 
            methodLabel.AutoSize = true;
            methodLabel.Location = new Point(8, 82);
            methodLabel.Name = "methodLabel";
            methodLabel.Size = new Size(49, 15);
            methodLabel.TabIndex = 2;
            methodLabel.Text = "Method";
            // 
            // decryptRadioButton
            // 
            decryptRadioButton.AutoSize = true;
            decryptRadioButton.Location = new Point(6, 47);
            decryptRadioButton.Name = "decryptRadioButton";
            decryptRadioButton.Size = new Size(66, 19);
            decryptRadioButton.TabIndex = 1;
            decryptRadioButton.TabStop = true;
            decryptRadioButton.Text = "Decrypt";
            decryptRadioButton.UseVisualStyleBackColor = true;
            // 
            // encryptRadioButton
            // 
            encryptRadioButton.AutoSize = true;
            encryptRadioButton.Location = new Point(6, 22);
            encryptRadioButton.Name = "encryptRadioButton";
            encryptRadioButton.Size = new Size(65, 19);
            encryptRadioButton.TabIndex = 0;
            encryptRadioButton.TabStop = true;
            encryptRadioButton.Text = "Encrypt";
            encryptRadioButton.UseVisualStyleBackColor = true;
            // 
            // outputGroupBox
            // 
            outputGroupBox.Anchor = AnchorStyles.None;
            outputGroupBox.Controls.Add(outputTextBox);
            outputGroupBox.Controls.Add(outputTextRadioButton);
            outputGroupBox.Controls.Add(outputFileBrowseButton);
            outputGroupBox.Controls.Add(outputFileTextBox);
            outputGroupBox.Controls.Add(outputFileRadioButton);
            outputGroupBox.Location = new Point(12, 460);
            outputGroupBox.Name = "outputGroupBox";
            outputGroupBox.Size = new Size(731, 203);
            outputGroupBox.TabIndex = 3;
            outputGroupBox.TabStop = false;
            outputGroupBox.Text = "Output";
            // 
            // outputTextBox
            // 
            outputTextBox.BorderStyle = BorderStyle.FixedSingle;
            outputTextBox.Location = new Point(6, 75);
            outputTextBox.Multiline = true;
            outputTextBox.Name = "outputTextBox";
            outputTextBox.Size = new Size(713, 107);
            outputTextBox.TabIndex = 4;
            // 
            // outputTextRadioButton
            // 
            outputTextRadioButton.AutoSize = true;
            outputTextRadioButton.Location = new Point(6, 50);
            outputTextRadioButton.Name = "outputTextRadioButton";
            outputTextRadioButton.Size = new Size(46, 19);
            outputTextRadioButton.TabIndex = 3;
            outputTextRadioButton.TabStop = true;
            outputTextRadioButton.Text = "Text";
            outputTextRadioButton.UseVisualStyleBackColor = true;
            // 
            // outputFileBrowseButton
            // 
            outputFileBrowseButton.Location = new Point(650, 21);
            outputFileBrowseButton.Name = "outputFileBrowseButton";
            outputFileBrowseButton.Size = new Size(75, 23);
            outputFileBrowseButton.TabIndex = 2;
            outputFileBrowseButton.Text = "Browse";
            outputFileBrowseButton.UseVisualStyleBackColor = true;
            outputFileBrowseButton.Click += outputFileBrowseButton_Click;
            // 
            // outputFileTextBox
            // 
            outputFileTextBox.BorderStyle = BorderStyle.FixedSingle;
            outputFileTextBox.Location = new Point(65, 21);
            outputFileTextBox.Name = "outputFileTextBox";
            outputFileTextBox.Size = new Size(566, 23);
            outputFileTextBox.TabIndex = 1;
            // 
            // outputFileRadioButton
            // 
            outputFileRadioButton.AutoSize = true;
            outputFileRadioButton.Location = new Point(6, 22);
            outputFileRadioButton.Name = "outputFileRadioButton";
            outputFileRadioButton.Size = new Size(43, 19);
            outputFileRadioButton.TabIndex = 0;
            outputFileRadioButton.TabStop = true;
            outputFileRadioButton.Text = "File";
            outputFileRadioButton.UseVisualStyleBackColor = true;
            // 
            // runButton
            // 
            runButton.Anchor = AnchorStyles.None;
            runButton.Location = new Point(347, 669);
            runButton.Name = "runButton";
            runButton.Size = new Size(75, 23);
            runButton.TabIndex = 4;
            runButton.Text = "Run";
            runButton.UseVisualStyleBackColor = true;
            runButton.Click += runButton_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(760, 701);
            Controls.Add(runButton);
            Controls.Add(outputGroupBox);
            Controls.Add(selectorGroupBox);
            Controls.Add(inputGroupBox);
            Name = "Form1";
            Text = "Cryptography Project";
            inputGroupBox.ResumeLayout(false);
            inputGroupBox.PerformLayout();
            selectorGroupBox.ResumeLayout(false);
            selectorGroupBox.PerformLayout();
            outputGroupBox.ResumeLayout(false);
            outputGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox inputGroupBox;
        private RadioButton inputFileRadioButton;
        private TextBox inputTextBox;
        private RadioButton inputTextRadioButton;
        private Button inputFileBrowseButton;
        private TextBox inputFileTextBox;
        private GroupBox selectorGroupBox;
        private CheckBox keyCheckBox;
        private TextBox keyTextBox;
        private Label keyLabel;
        private ComboBox methodComboBox;
        private Label methodLabel;
        private RadioButton decryptRadioButton;
        private RadioButton encryptRadioButton;
        private GroupBox outputGroupBox;
        private TextBox outputTextBox;
        private RadioButton outputTextRadioButton;
        private Button outputFileBrowseButton;
        private TextBox outputFileTextBox;
        private RadioButton outputFileRadioButton;
        private Button runButton;
        private Label methodSelectLabel;
    }
}
