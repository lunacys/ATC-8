using System;

namespace ATC8.VirtualMachine
{
    public class InvalidVmOperationException : Exception
    {
        public InvalidVmOperationException()
            : base()
        { }

        public InvalidVmOperationException(string message)
            : base(message)
        { }

        public InvalidVmOperationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}