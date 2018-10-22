using System;

namespace ATC8.Cpu
{
    public class Register
    {
        public RegisterName Name { get; }
        public Word Value { get; set; }

        public Register(RegisterName name, Word value)
        {
            Name = name;
            Value = value;
        }
    }
}