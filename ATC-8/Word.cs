using System;

namespace ATC8
{
    public struct Word
    {
        public short Value { get; set; }

        public Word(short value)
        {
            Value = value;
        }

        public static implicit operator Word(short value)
        {
            return new Word(value);
        }

        public static implicit operator short(Word value)
        {
            return value.Value;
        }

        public static implicit operator int(Word value)
        {
            return Convert.ToInt16(value.Value);
        }

        public static implicit operator Word(int value)
        {
            return new Word(Convert.ToInt16(value));
        }

        public static Word operator +(Word a, Word b)
        {
            return new Word(Convert.ToInt16(a.Value + b.Value));
        }

        public static Word operator -(Word a, Word b)
        {
            return new Word(Convert.ToInt16(a.Value - b.Value));
        }

        public static Word operator *(Word a, Word b)
        {
            return new Word(Convert.ToInt16(a.Value * b.Value));
        }
        public static Word operator /(Word a, Word b)
        {
            return new Word(Convert.ToInt16(a.Value / b.Value));
        }

        public static Word Parse(string value)
        {
            return new Word(short.Parse(value));
        }
    }
}