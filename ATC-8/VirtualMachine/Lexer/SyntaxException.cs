using System;

namespace ATC8.VirtualMachine.Lexer
{
    public class SyntaxException : Exception
    {
        public SyntaxException(string message)
            : base(message)
        {
            
        }
    }
}