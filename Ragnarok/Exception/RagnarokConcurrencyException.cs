using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarokException
{
    public class RagnarokConcurrencyException : Exception
    {
        public RagnarokConcurrencyException()
        {
        }

        public RagnarokConcurrencyException(string message)
            : base(message)
        {
        }

        public RagnarokConcurrencyException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
