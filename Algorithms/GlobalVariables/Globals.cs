using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.GlobalVariables
{
    public static class Globals
    {
        public static readonly string FilePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
            Path.DirectorySeparatorChar + "Cryptography";

        public static readonly string PlainTextFileName = Path.DirectorySeparatorChar + "PlainText.txt";

        public static readonly string PlainTextFullPath = FilePath + PlainTextFileName;

        public static readonly string CipherTextFileName = Path.DirectorySeparatorChar + "CipherText.txt";

        public static readonly string CipherTextFullPath = FilePath + CipherTextFileName;

        public static readonly int modulus = 26;
    }

}
