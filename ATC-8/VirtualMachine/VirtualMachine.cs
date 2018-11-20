using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ATC8.Cpu;
using ATC8.Ram;
using ATC8.VirtualMachine.Lexer.Tokens;

namespace ATC8.VirtualMachine
{
    public class VirtualMachine
    {
        private const int MaxStackSize = 0xFF; // 255

        private int _currentPosition = 0;
        private int StackSize => _bytecode.Length;
        private Stack<Word> _stack;
        private Word[] _bytecode;
        private Instructions _nextInstruction;

        private CpuBase _cpu;
        private RamBase _ram;

        public VirtualMachine()
        {
            //_stackSize = 0;
            //_stack = new int[MaxStackSize];
            _stack = new Stack<Word>(MaxStackSize);

            _cpu = new CpuBase(null);
            _ram = new RamBase(262144); // 32KB

            Console.WriteLine($"CPU: \n{_cpu}");
        }

        public void Interpret(Word[] bytecode)
        {
            _bytecode = bytecode;

            for (_currentPosition = 0; _currentPosition < _bytecode.Length; _currentPosition++)
            {
                
            }
        }

        private void Process()
        {
            Console.WriteLine("Processing Line");

            if (_nextInstruction == Instructions.Dvr)
            {
                

            }
        }

        private string FromWordString(Word size, Word[] bytes)
        { 
            var str = new Word[size];
            for (int i = 0; i < size; i++)
                str[i] = bytes[i];
            return str.FromWordArray();
        }

        private void HandleOpcode(Instructions opcode, ref int i)
        {
            Console.WriteLine($"Got an opcode: {opcode}");
            _nextInstruction = opcode;
            if (opcode == Instructions.Jmp)
            {
                Console.WriteLine("Jumping at 119");
                //i = 119;
            }
        }

        private void HandleIdentifier(string identifier)
        {
            Console.WriteLine($"Got an identifier: {identifier}");
            //throw new NotImplementedException();
        }

        private void HandleInteger(Word integer)
        {
            Console.WriteLine($"Got an integer: {integer}");
            _stack.Push(integer);
        }

        private void HandleString(string str)
        {
            Console.WriteLine($"Got a string: {str}");
            //throw new NotImplementedException();
        }

        private void HandleExtOpcode(string opcode)
        {
            Console.WriteLine($"Got an extension opcode: {opcode}");
            if (opcode == "dbug")
                Console.WriteLine("Debug string");
            //throw new NotImplementedException();
        }

        private void HandleDelimiter(char delimiter)
        {
            Console.WriteLine($"Got a delimiter: {delimiter}");
            //throw new NotImplementedException();
        }

        private void HandleLabel(string label, Word position)
        {
            Console.WriteLine($"Got a label: {label} at position {position}");
            //throw new NotImplementedException();
        }

        private void HandleOperator(char op)
        {
            Console.WriteLine($"Got an operator: {op}");
            //throw new NotImplementedException();
        }

        private void HandleRegister(RegisterName register)
        {
            Console.WriteLine($"Got a register: {register}");
            //throw new NotImplementedException();
        }

        private void HandleDebugPoint(Word position)
        {
            throw new NotImplementedException();
        }
    }
}