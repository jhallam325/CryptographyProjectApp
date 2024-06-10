using Algorithms.Interfaces;
using Algorithms.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Subclasses
{
    internal class HillCipher : Cipher, ICipher
    {
        public string Encrypt(string plaintext, string key)
        {
            throw new NotImplementedException();
        }
        public string Decrypt(string ciphertext, string key)
        {
            throw new NotImplementedException();
        }

        public bool KeyIsCorrect(string key)
        {
            throw new NotImplementedException();
        }
    }
}
