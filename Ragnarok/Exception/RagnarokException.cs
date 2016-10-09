using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RagnarokException
{

    public class AmazonWebException : Exception
    {
        public AmazonWebException()
        {
        }

        public AmazonWebException(string message)
            : base(message)
        {
        }

        public AmazonWebException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
