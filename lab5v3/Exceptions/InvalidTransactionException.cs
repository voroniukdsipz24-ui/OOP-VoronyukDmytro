using System;

namespace Exceptions
{
    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException(string message)
            : base(message)
        {
        }
    }
}