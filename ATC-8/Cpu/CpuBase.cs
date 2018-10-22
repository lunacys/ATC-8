using System.Collections.Generic;

namespace ATC8.Cpu
{
    public class CpuBase
    {
        private List<Register> _registers;

        public byte this[RegisterName name]
        {
            get { return _registers.Find(r => r.Name == name).Value; }
            set { _registers.Find(r => r.Name == name).Value = value; }
        }

        public CpuBase()
        {
            _registers = new List<Register>()
            {
                new Register(RegisterName.Ax, 0x00),
                new Register(RegisterName.Bx, 0x00),
                new Register(RegisterName.Cx, 0x00),
                new Register(RegisterName.Dx, 0x00),
                new Register(RegisterName.Si, 0x00),
                new Register(RegisterName.Di, 0x00),
                new Register(RegisterName.Sp, 0x00),
                new Register(RegisterName.Bp, 0x00),
            };
        }

        private byte GetRegisterValue(RegisterName name)
        {
            return _registers.Find(r => r.Name == name).Value;
        }
    }
}