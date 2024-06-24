using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Exceptions
{
    public class InvertibleMatrixException : Exception
    {
        public InvertibleMatrixException() : base("The matrix is not invertible.")
        {
        }

        public InvertibleMatrixException(string message) : base(message)
        {
        }
    }
}
