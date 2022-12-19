using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities.Exceptions
{
    public class AlreadyLoggedException : Exception
    {
        public AlreadyLoggedException(string message) : base(message)
        {
        }

        public AlreadyLoggedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public AlreadyLoggedException()
        {
        }
    }
}
