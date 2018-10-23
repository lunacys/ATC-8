using System.Collections.Generic;
using ATC8.IO;

using RAR = ATC8.Cpu.RegisterAccessRights;

namespace ATC8.Cpu
{
    public class CpuBase : ConsoleComponent
    {
        private readonly RegisterContainer _registers;

        public CpuBase(Bus bus)
            : base(bus)
        {
            _registers = new RegisterContainer(new List<Register>
            {
                // Public registers
                new Register(RegisterName.Ax, 0x00), // Accumulator 
                new Register(RegisterName.Bx, 0x00), // Base
                new Register(RegisterName.Cx, 0x00), // Counter
                new Register(RegisterName.Dx, 0x00), // Data
                // Indexes
                new Register(RegisterName.Si, 0x00), // Source index
                new Register(RegisterName.Di, 0x00), // Destination index
                // Pointers
                new Register(RegisterName.Sp, 0x00), // Stack pointer
                new Register(RegisterName.Bp, 0x00), // Base pointer
                // Buttons
                new Register(RegisterName.Kp, 0x00, RAR.Read | RAR.Write,
                    RAR.Read), // Keys pressed
                new Register(RegisterName.Ku, 0xFF, RAR.Read | RAR.Write,
                    RAR.Read), // Keys unpressed
            });
        }
    }
}