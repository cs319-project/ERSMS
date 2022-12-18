namespace Backend.Entities.Exceptions
{
    /// <summary>Exception thrown when an error occurs in the To-Do List operations.</summary>
    public class ToDoListException : Exception
    {
        /// <summary>Initializes a new instance of the <see cref="ToDoListException"/> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        public ToDoListException(string message) : base(message)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ToDoListException"/> class.</summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ToDoListException(string message, Exception innerException) : base(message, innerException)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="ToDoListException"/> class.</summary>
        public ToDoListException()
        {
        }
    }
}
