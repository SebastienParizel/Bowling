using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bowling
{
    public class BowlingException : Exception
    {
        public BowlingException(string message) : base (message)
        {
        }
    }
}
