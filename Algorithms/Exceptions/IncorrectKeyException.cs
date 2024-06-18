using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Exceptions
{
    public class IncorrectKeyException : Exception
    {
        public IncorrectKeyException() : base("Incorrect Key") 
        {
        }

        public IncorrectKeyException(string message) : base(message)
        {
        }
    }
}
