using System;

namespace ATC8.Cpu
{
    public class Register
    {
        public RegisterName Name { get; }
        public Word Value { get; set; }

        public byte ValueHigh
        {
            get => Value.ValueHigh;
            set => Value = new Word(value, Value.ValueLow);
        }

        public byte ValueLow
        {
            get => Value.ValueLow;
            set => Value = new Word(Value.ValueHigh, value);
        }

        public Register(RegisterName name, Word value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}