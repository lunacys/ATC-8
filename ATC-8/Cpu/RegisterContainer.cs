using System;
using System.Collections;
using System.Collections.Generic;

namespace ATC8.Cpu
{
    public class RegisterContainer : IEnumerable<KeyValuePair<RegisterName, Register>>
    {
        private readonly Dictionary<RegisterName, Register> _registers;

        public Register this[string name]
        {
            get => Get(name);
            set => Add(name, value);
        }

        public Register this[RegisterName name]
        {
            get => Get(name);
            set => Add(name, value);
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
                Add(register.Name, register);
            }
        }

        public void Add(RegisterName name, Register register)
        {
            _registers[name] = register;
        }

        public void Add(string registerName, Register register)
        {
            var res = Enum.TryParse(registerName, true, out RegisterName regName);

            if (!res)
                throw new ArgumentException("Cannot parse the register name", nameof(registerName));

            Add(regName, register);
        }

        public Register Get(RegisterName name)
        {
            if (!_registers.ContainsKey(name))
                throw new ArgumentOutOfRangeException(nameof(name), "No such register");
            return _registers[name];
        }

        public Register Get(string registerName)
        {
            var res = Enum.TryParse(registerName, true, out RegisterName regName);

            if (!res)
                throw new ArgumentException("Cannot parse the register name", nameof(registerName));

            return Get(regName);
        }

        public IEnumerator<KeyValuePair<RegisterName, Register>> GetEnumerator()
        {
            return _registers.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}