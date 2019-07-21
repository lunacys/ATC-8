using System;
using ATC8;
using NUnit.Framework;

namespace ATC_8.Tests
{
    [TestFixture]
    public class BinaryExtensionsTest
    {
        [Test]
        public void BinaryExtensionsMethodsTests()
        {
            byte a1 = 0b11110000;
            byte a2 = 0b00000000;
            byte a3 = 0b11111111;

            Assert.That(a1.ToBinaryString() == "11110000");
            Assert.That(a2.ToBinaryString() == "00000000");
            Assert.That(a3.ToBinaryString() == "11111111");

            string b1 = "10101010";
            string b2 = "00000000";
            string b3 = "11111111";

            Assert.That(b1.FromBinaryString() == 0b10101010);
            Assert.That(b2.FromBinaryString() == 0b00000000);
            Assert.That(b3.FromBinaryString() == 0b11111111);

            byte c1 = 0;
            byte c2 = 255;
            byte c3 = 0xFA;

            Assert.That(c1.ToBinaryString() == "00000000");
            Assert.That(c2.ToBinaryString() == "11111111");
            Assert.That(c3.ToBinaryString() == "11111010");

            Assert.That(c1.ToBinaryString().FromBinaryString() == 0b00000000);
            Assert.That(c2.ToBinaryString().FromBinaryString() == 0b11111111);
            Assert.That(c3.ToBinaryString().FromBinaryString() == 0b11111010);

            string str1 = "test string";
            var str1Arr = str1.ToWordArray();

            var size = str1Arr[0];

            for (int i = 1; i < size; i++)
            {
                Assert.That((short)str1[i - 1] == str1Arr[i]);
            }

        }
    }
}