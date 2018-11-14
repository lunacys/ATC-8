using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using ATC8.Cpu;
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

        public VirtualMachine()
        {
            //_stackSize = 0;
            //_stack = new int[MaxStackSize];
            _stack = new Stack<Word>(MaxStackSize);
        }

        public void Interpret(Word[] bytecode)
        {
            _bytecode = bytecode;
            for (int i = 0; i < _bytecode.Length; i++)
            {
                TokenType tokenType = (TokenType)(sbyte)_bytecode[i];
                switch (tokenType)
                {
                    case TokenType.Eof:
                        return;
                    case TokenType.Opcode:
                        var opcode = (Instructions) (byte) _bytecode[++i];
                        HandleOpcode(opcode, ref i);
                        break;
                    case TokenType.Identifier:
                        var identSize = _bytecode[++i];
                        var identStr = new Word[identSize];
                        for (int j = 0; j < identSize; j++)
                            identStr[j] = _bytecode[++i];
                        HandleIdentifier(identStr.FromWordArray());
                        break;
                    case TokenType.Integer:
                        HandleInteger(_bytecode[++i]);
                        break;
                    case TokenType.String:
                        var stringSize = _bytecode[++i];
                        var stringStr = new Word[stringSize];
                        for (int j = 0; j < stringSize; j++)
                            stringStr[j] = _bytecode[++i];
                        HandleString(stringStr.FromWordArray());
                        break;
                    case TokenType.ExtensionOpcode:
                        var extOpcodeSize = _bytecode[++i];
                        var extOpcodeStr = new Word[extOpcodeSize];
                        for (int j = 0; j < extOpcodeSize; j++)
                            extOpcodeStr[j] = _bytecode[++i];
                        HandleExtOpcode(extOpcodeStr.FromWordArray());
                        break;
                    case TokenType.Delimiter:
                        HandleDelimiter((char)_bytecode[++i]);
                        break;
                    case TokenType.Label:
                        var labelSize = _bytecode[++i];
                        var labelStr = new Word[labelSize];
                        for (int j = 0; j < labelSize; j++)
                            labelStr[j] = _bytecode[++i];
                        HandleLabel(labelStr.FromWordArray(), _currentPosition);
                        break;
                    case TokenType.Operator:
                        HandleOperator((char)_bytecode[++i]);
                        break;
                    case TokenType.Register:
                        var register = (RegisterName) (short) _bytecode[++i];
                        HandleRegister(register);
                        break;
                    case TokenType.DebugPoint:
                        HandleDebugPoint(_bytecode[++i]);
                        break;
                    case TokenType.NewLine:
                        Console.WriteLine("New Line");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _currentPosition = i;
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