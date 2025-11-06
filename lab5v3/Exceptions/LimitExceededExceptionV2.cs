using System;

namespace Exceptions
{
    public class LimitExceededExceptionV2 : Exception
    {
        public LimitExceededExceptionV2(string message)
            : base(message)
        {
        }
    }
}
