using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Interfaces
{
    public interface ICipher
    {
        abstract string Encrypt(string  plaintext, string key);
        abstract string Decrypt(string ciphertext, string key);
        abstract bool KeyIsCorrect(string key);
    }
}
