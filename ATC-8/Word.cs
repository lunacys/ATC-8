using System;

namespace ATC8
{
    public struct Word
    {
        private readonly byte[] _values;

        public byte Value
        {
            get => Convert.ToByte(string.Concat(Convert.ToString(ValueHigh, 2), Convert.ToString(ValueLow, 2)), 2);
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
            get => Convert.ToByte(Convert.ToString(Value, 2).PadLeft(8, '0').Remove(4, 4), 2);
            set => _values[0] = value;
        }

        public byte ValueLow
        {
            get => Convert.ToByte(Convert.ToString(Value, 2).PadLeft(8, '0').Remove(0, 4), 2);
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
            return $"{{{Value.ToBinaryString()} ({Value}) | {ValueHigh.ToBinaryString()} {ValueLow.ToBinaryString()}}}";
        }

        public static implicit operator Word(byte value)
        {
            return new Word(value);
        }

        public static implicit operator byte(Word value)
        {
            return value.Value;
        }
    }
}