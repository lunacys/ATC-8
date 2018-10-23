using System;
using System.Collections.Generic;

namespace ATC8.Cpu
{
    public class RegisterContainer
    {
        private readonly Dictionary<RegisterName, Register> _registers;

        public Register this[string name]
        {
            get => Get(name);
            set => Set(name, value);
        }

        public Register this[RegisterName name]
        {
            get => Get(name);
            set => Set(name, value);
        }

        public RegisterContainer()
        {
            _registers = new Dictionary<RegisterName, Register>();
        }

        public RegisterContainer(IEnumerable<Register> registers)
        {
            _registers = new Dictionary<RegisterName, Register>();
            foreach (var register in registers)
            {
                Set(register.Name, register);
            }
        }

        public void Set(RegisterName name, Register register)
        {
            _registers[name] = register;
        }

        public void Set(string registerName, Register register)
        {
            var res = Enum.TryParse(registerName, true, out RegisterName regName);

            if (!res)
                throw new Exception("Cannot parse the register name");

            Set(regName, register);
        }

        public Register Get(RegisterName name)
        {
            if (!_registers.ContainsKey(name))
                throw new Exception("No such register");
            return _registers[name];
        }

        public Register Get(string registerName)
        {
            var res = Enum.TryParse(registerName, true, out RegisterName regName);

            if (!res)
                throw new Exception("Cannot parse the register name");

            return Get(regName);
        }
    }
}