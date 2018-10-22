using System;

namespace ATC8.VirtualMachine
{
    [Serializable]
    public class CodeErrorException : Exception
    {
        public CodeErrorException()
        { }

        public CodeErrorException(string message) : base(message)
        { }

        public CodeErrorException(string message, Exception inner) : base(message, inner)
        { }
    }
}