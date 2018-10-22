using System;
using ATC8.Cpu;
using NUnit.Framework;

namespace ATC_8.Tests
{
    [TestFixture]
    public class RegisterTest
    {
        [Test]
        public void Value_HighValue_LowValue_Test()
        {
            var ax = new Register(RegisterName.Ax, 0xFA); // 0b11111010
            var bx = new Register(RegisterName.Bx, 0x00); // 0b00000000
            var cx = new Register(RegisterName.Cx, 0xFF); // 0b11111111
            var dx = new Register(RegisterName.Dx, 0b10101010);

            // Check if the values was written correctly
            Assert.That(ax.Value == 0b11111010);
            Assert.That(bx.Value == 0b00000000);
            Assert.That(cx.Value == 0b11111111);
            Assert.That(dx.Value == 0b10101010);

            // Check if the high & low values was written correctly
            Assert.That(ax.Value.ValueHigh == 0b1111 && ax.Value.ValueLow == 0b1010);
            Assert.That(bx.Value.ValueHigh == 0b0000 && bx.Value.ValueLow == 0b0000);
            Assert.That(cx.Value.ValueHigh == 0b1111 && cx.Value.ValueLow == 0b1111);
            Assert.That(dx.Value.ValueHigh == 0b1010 && dx.Value.ValueLow == 0b1010);

            //ax.Value.ValueHigh = 0;

        }
    }
}
