using System;
using System.Collections.Generic;
using ATC8.IO;

using RAR = ATC8.Cpu.RegisterAccessRights;

namespace ATC8.Cpu
{
    public class CpuBase : ConsoleComponent
    {
        private readonly RegisterContainer _registers;

        public Word this[RegisterName reg]
        {
            get { return _registers[reg].Value; }
            set { _registers[reg].Value = value; }
        }

        public CpuBase(Bus bus)
            : base(bus)
        {
            _registers = new RegisterContainer(new List<Register>
            {
                // Common registers
                new Register(RegisterName.Ax, 0x00), 
                new Register(RegisterName.Bx, 0x00), 
                new Register(RegisterName.Cx, 0x00), 
                new Register(RegisterName.Dx, 0x00),
                new Register(RegisterName.Acx, 0x00),
                new Register(RegisterName.Bcx, 0x00), 
                // Other
                new Register(RegisterName.Jd, 0x00), // jump data
                new Register(RegisterName.Sp, 0x00), // stack pointer
                new Register(RegisterName.Bp, 0x00), // base pointer
                new Register(RegisterName.Ex, 0x00), // extra/excess
                new Register(RegisterName.Ia, 0x00), // interrupt address
                new Register(RegisterName.Pc, 0x00), // program counter
            });
        }

        public override string ToString()
        {
            var result = "";

            foreach (var reg in _registers)
                result += reg.Value.ToString() + "\n";

            return result;
        }
    }
}