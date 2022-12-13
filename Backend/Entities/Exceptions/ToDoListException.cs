using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Entities.Exceptions
{
    public class ToDoListException : Exception
    {
        // Constructors
        public ToDoListException(string message) : base(message)
        {
        }
        public ToDoListException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public ToDoListException()
        {
        }
    }
}
