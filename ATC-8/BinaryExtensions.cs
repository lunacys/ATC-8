using System;
using System.IO;

namespace ATC8
{
    public static class BinaryExtensions
    {
        public static string ToBinaryString(this byte value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        public static string ToBinaryString(this short value)
        {
            return Convert.ToString(value, 2).PadLeft(8, '0');
        }

        public static byte FromBinaryString(this string value)
        {
            return Convert.ToByte(value, 2);
        }

        public static Word[] ToWordArray(this string value)
        {
            Word[] bytecode = new Word[value.Length];

            for (int i = 0; i < value.Length; i++)
            {
                bytecode[i] = new Word((short)value[i]);
            }

            return bytecode;
        }

        public static string FromWordArray(this Word[] value)
        {
            string res = "";
            for (int i = 0; i < value.Length; i++)
            {
                res += (char)value[i];
            }

            return res;
        }

        public static void SaveToFile(this Word[] value, string filepath, int maxBytesOnLine = 0)
        {
            using (StreamWriter sw = new StreamWriter(filepath, false))
            {
                for (int i = 0; i < value.Length; i++)
                {
                    if (maxBytesOnLine != 0 && i != 0 && i % maxBytesOnLine == 0)
                        sw.WriteLine();
                    sw.Write($"{value[i]} ");
                }
            }
        }
    }
}