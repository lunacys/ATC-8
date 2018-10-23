using System;

namespace ATC8
{
    public struct Word
    {
        private readonly byte[] _values;

        public byte Value
        {
            get => Convert.ToByte(string.Concat(Convert.ToString(ValueHigh, 2).PadLeft(4, '0'), Convert.ToString(ValueLow, 2).PadLeft(4, '0')), 2);
            set
            {
                var high = Convert.ToByte(Convert.ToString(value, 2).PadLeft(8, '0').Remove(4, 4), 2);
                var low = Convert.ToByte(Convert.ToString(value, 2).PadLeft(8, '0').Remove(0, 4), 2);
                ValueHigh = high;
                ValueLow = low;
            }
        }

        public byte ValueHigh
        {
            get => _values[0];
            set => _values[0] = value;
        }

        public byte ValueLow
        {
            get => _values[1];
            set => _values[1] = value;
        }

        public Word(byte value)
        {
            _values = new byte[2];
            Value = value;
        }

        public Word(byte high, byte low)
        {
            _values = new byte[2];
            ValueHigh = high;
            ValueLow = low;
        }

        public override string ToString()
        {
            return $"{{{Value.ToBinaryString()} ({Value})}}";
        }

        public static implicit operator Word(byte value)
        {
            return new Word(value);
        }

        public static implicit operator byte(Word value)
        {
            return value.Value;
        }

        public static implicit operator int(Word value)
        {
            return Convert.ToByte(value.Value);
        }

        public static implicit operator Word(int value)
        {
            return new Word(Convert.ToByte(value));
        }

        public static Word operator +(Word a, Word b)
        {
            return new Word(Convert.ToByte(a.Value + b.Value));
        }

        public static Word operator -(Word a, Word b)
        {
            return new Word(Convert.ToByte(a.Value - b.Value));
        }
    }
}