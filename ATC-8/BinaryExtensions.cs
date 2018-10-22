using System;

namespace ATC8
{
    public static class BinaryExtensions
    {
        public static string ToBinaryString(this byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        public static byte FromBinaryString(this string value)
        {
            return Convert.ToByte(value, 2);
        }
    }
}