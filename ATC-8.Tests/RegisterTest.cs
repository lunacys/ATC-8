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
            Assert.That(ax.Value.Value == 0b11111010);
            Assert.That(bx.Value.Value == 0b00000000);
            Assert.That(cx.Value.Value == 0b11111111);
            Assert.That(dx.Value.Value == 0b10101010);

            cx.Value = 0xF0; // 0b11110000
            dx.Value = 0x0F; // 0b00001111

            Assert.That(ax.Value == 0b00001111);
            Assert.That(bx.Value == 0b11110000);

            Assert.That(cx.Value + dx.Value == 0b11111111);

            cx.Value += dx.Value;
            Assert.That(cx.Value == 0xFF);
            Assert.Throws<OverflowException>(() => cx.Value += dx.Value);
        }
    }
}
