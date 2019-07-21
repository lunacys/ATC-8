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

        private Instructions _lastOpcode;

        private Dictionary<string, int> _labelDictionary;

        private OpcodeHandler _opcodeHandler;

        public VirtualMachine()
        {
            //_stackSize = 0;
            //_stack = new int[MaxStackSize];
            _stack = new Stack<Word>(MaxStackSize);

            _cpu = new CpuBase(null);
            _ram = new RamBase(262144); // 32KB
            _opcodeHandler = new OpcodeHandler(_cpu);
            _labelDictionary = new Dictionary<string, int>();

            Console.WriteLine($"CPU: \n{_cpu}");
        }

        public void Interpret(Word[] bytecode)
        {
            _bytecode = bytecode;

            for (_currentPosition = 0; _currentPosition < _bytecode.Length; _currentPosition++)
            {
                TokenType tt = (TokenType)_bytecode[_currentPosition++].Value;

                if (tt == TokenType.Opcode)
                {
                    var opcode = (Instructions) _bytecode[_currentPosition].Value;
                    Console.WriteLine(" Got an opcode: " + opcode);

                    HandleOpcode(opcode);
                    //_lastOpcode = opcode;
                }
                else if (tt == TokenType.ExtensionOpcode)
                {
                    var size = _bytecode[_currentPosition].Value;
                    string resultStr = "";

                    for (int i = 0; i < size; i++)
                    {
                        char ch = (char)_bytecode[++_currentPosition].Value;
                        resultStr += ch;
                    }

                    Console.WriteLine($" Got an extension opcode (size: {size}): " + resultStr);
                }
                else if (tt == TokenType.Label)
                {
                    var size = _bytecode[_currentPosition].Value;
                    string resultStr = "";

                    for (int i = 0; i < size; i++)
                    {
                        char ch = (char) _bytecode[++_currentPosition].Value;
                        resultStr += ch;
                    }

                    _labelDictionary[resultStr] = _currentPosition;

                    Console.WriteLine($" Got a label (size: {size}): " + resultStr);
                }
                else if (tt == TokenType.DebugPoint)
                {

                }
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

        private void HandleOpcode(Instructions opcode)
        {
            Stack<Word> temp = new Stack<Word>();

            while (true)//((TokenType) _bytecode[++_currentPosition].Value != TokenType.NewLine)
            {
                var currentWord = _bytecode[++_currentPosition];

                if ((TokenType) currentWord.Value == TokenType.String)
                {

                }
                else if ((TokenType)currentWord.Value == TokenType.Label)
                {

                }
                else if ((TokenType)currentWord.Value == TokenType.Identifier)
                {

                }
                else if ((TokenType) currentWord.Value == TokenType.NewLine)
                {
                    break;
                }
                else if ((TokenType) currentWord.Value == TokenType.Delimiter)
                {
                    // Skip the delimiter as we don't need it anyways
                    _currentPosition++; // 'Eat' delimiter value
                }
                else
                {
                    var value = _bytecode[++_currentPosition];
                    Console.WriteLine($"Pushing: type: {(TokenType)currentWord.Value}, value: {value}");
                    temp.Push(value);
                }
            }

            _opcodeHandler.Handle(temp);
            /*int expectedParamsCount = 0;

            if (opcode == Instructions.Add)
            {
                expectedParamsCount = 2;
                Console.WriteLine($"char 44: " + (char)44);
                TokenType firstParamType = (TokenType) _bytecode[++_currentPosition].Value;
                RegisterName regName;

                if (firstParamType == TokenType.Register)
                {
                    regName = (RegisterName)_bytecode[++_currentPosition].Value;
                    //Register reg = _cpu[regName];
                    Console.WriteLine("RegName is " + regName);
                }
                else
                {
                    Console.WriteLine("Invalid first param type: " + firstParamType);
                    return;
                }

                if ((TokenType) _bytecode[++_currentPosition].Value != TokenType.Delimiter &&
                    (char) _bytecode[++_currentPosition].Value != ',')
                {
                    Console.WriteLine("Incorrect syntax");
                    return;
                }

                if ((char) _bytecode[++_currentPosition].Value != ',')
                {
                    return;
                }

                TokenType secondParamType = (TokenType) _bytecode[++_currentPosition].Value;

                if (secondParamType == TokenType.Integer)
                {
                    var value = _bytecode[++_currentPosition].Value;
                    Console.WriteLine("Second param is " + value);
                    _cpu[regName] += value;
                }

                Console.WriteLine($"CPU:");
                Console.WriteLine($"{_cpu}");
            }*/
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