using System;

namespace Wegister.Exceptions
{
    public class UnknownWeekException : Exception
    {
        public int ErrorCode { get; }
        public UnknownWeekException(string message, int errorCode)
        {
            ErrorCode = errorCode;
        }
        public UnknownWeekException(string message)
        {
            ErrorCode = 0;
        }
    }
}
