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
                        i++;
                        var opcode = (Instructions) (byte) _bytecode[i];
                        HandleOpcode(opcode);
                        break;
                    case TokenType.Identifier:
                        HandleIdentifier(_bytecode[i].ToString());
                        break;
                    case TokenType.Integer:
                        HandleInteger(_bytecode[i]);
                        break;
                    case TokenType.String:
                        HandleString(_bytecode[i].ToString());
                        break;
                    case TokenType.ExtensionOpcode:
                        HandleExtOpcode(_bytecode[i].ToString());
                        break;
                    case TokenType.Delimiter:
                        HandleDelimiter((char)_bytecode[i]);
                        break;
                    case TokenType.Label:
                        HandleLabel(_bytecode[i].ToString(), _bytecode[i]);
                        break;
                    case TokenType.Operator:
                        HandleOperator((char)_bytecode[i]);
                        break;
                    case TokenType.Register:
                        HandleRegister(RegisterName.Ax);
                        break;
                    case TokenType.DebugPoint:
                        HandleDebugPoint(_bytecode[i]);
                        break;
                    case TokenType.NewLine:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                i = _currentPosition;
            }
        }

        private void HandleOpcode(Instructions opcode)
        {
            throw new NotImplementedException();
        }

        private void HandleIdentifier(string identifier)
        {
            throw new NotImplementedException();
        }

        private void HandleInteger(Word integer)
        {
            _stack.Push(integer);
        }

        private void HandleString(string str)
        {
            throw new NotImplementedException();
        }

        private void HandleExtOpcode(string opcode)
        {
            throw new NotImplementedException();
        }

        private void HandleDelimiter(char delimiter)
        {
            throw new NotImplementedException();
        }

        private void HandleLabel(string label, Word position)
        {
            throw new NotImplementedException();
        }

        private void HandleOperator(char op)
        {
            throw new NotImplementedException();
        }

        private void HandleRegister(RegisterName register)
        {
            throw new NotImplementedException();
        }

        private void HandleDebugPoint(Word position)
        {
            throw new NotImplementedException();
        }
    }
}