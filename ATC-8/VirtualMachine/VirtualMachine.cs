using System;
using System.Collections.Generic;
using ATC8.Cpu;
using ATC8.Logging;
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

        private CpuBase _cpu;
        private RamBase _ram;

        private LabelStorage _labelStorage;

        private List<int> _debugPoints;

        private OpcodeHandler _opcodeHandler;

        private LoggerBase _logger => LoggerFactory.Get("VirtualMachine");

        public VirtualMachine()
        {
            _stack = new Stack<Word>(MaxStackSize);

            _cpu = new CpuBase(null);
            _ram = new RamBase(262144); // 32KB

            _labelStorage = new LabelStorage();
            _opcodeHandler = new OpcodeHandler(_cpu, _labelStorage);

            _debugPoints = new List<int>();

            Console.WriteLine($"CPU: \n{_cpu}");
        }

        public void Interpret(Word[] bytecode)
        {
            _bytecode = bytecode;

            _logger.Debug($"Interpreting bytecode (size: {_bytecode.Length}):");

            for (_currentPosition = 0; _currentPosition < _bytecode.Length; _currentPosition++)
            {
                TokenType tt = ReadCurrentTokenType();

                if (tt == TokenType.Opcode)
                {
                    var opcode = (Instructions) _bytecode[_currentPosition].Value;

                    _logger.Debug(" Got an opcode: " + opcode);

                    HandleOpcode(opcode);
                }
                else if (tt == TokenType.ExtensionOpcode)
                {
                    string resultStr = ReadString();

                    _logger.Debug($" Got an extension opcode (size: {resultStr.Length}): " + resultStr);

                    HandleExtensionOpcode(resultStr);
                }
                else if (tt == TokenType.Label)
                {
                    string resultStr = ReadString();

                    _logger.Debug($" Got a label (size: {resultStr.Length}): " + resultStr);

                    HandleLabel(resultStr);
                }
                else if (tt == TokenType.DebugPoint)
                {
                    _logger.Debug($" Got a debug point at position " + _currentPosition);

                    HandleDebugPoint();
                }
                else
                {
                    throw new InvalidVmOperationException($"Invalid token type '{tt}' at position {_currentPosition}.");
                }
            }
        }

        private void HandleOpcode(Instructions opcode)
        {
            var queue = ReadLine(new[] {new Word((short)opcode)});

            _opcodeHandler.Handle(queue);
        }

        private void HandleExtensionOpcode(string opcode)
        {
            var queue = ReadLine(opcode.ToWordArray());

            _opcodeHandler.HandleExtension(queue);
        }

        private void HandleLabel(string label)
        {
            _labelStorage[label] = _currentPosition;
        }

        private void HandleDebugPoint()
        {
            _debugPoints.Add(_currentPosition);

            throw new NotImplementedException();
        }

        private TokenType ReadCurrentTokenType()
        {
            return (TokenType)_bytecode[_currentPosition++].Value;
        }

        private string ReadString()
        {
            var size = _bytecode[++_currentPosition].Value;
            string resultStr = "";

            for (int i = 0; i < size; i++)
            {
                char ch = (char)_bytecode[++_currentPosition].Value;
                resultStr += ch;
            }

            return resultStr;
        }

        private Queue<Word> ReadLine(Word[] beginData)
        {
            Queue<Word> temp = new Queue<Word>();
            temp.EnqueueWordArray(beginData);

            while (true)
            {
                var currentWord = _bytecode[++_currentPosition];

                if ((TokenType)currentWord.Value == TokenType.Delimiter)
                {
                    // Skip the delimiter as we don't need it anyways
                    var delimiter = _bytecode[_currentPosition + 1];

                    if (delimiter == ',') // Skip commas, pass the rest
                    {
                        _currentPosition++;
                        continue;
                    }
                }
                else if ((TokenType)currentWord.Value == TokenType.Identifier)
                {
                    // TODO: Find memory address value and pass it as identifier
                }
                else if ((TokenType)currentWord.Value == TokenType.NewLine)
                {
                    // New line is the end of the current instruction
                    break;
                }

                temp.Enqueue(currentWord);
                var value = _bytecode[++_currentPosition];

                _logger.Debug($"Pushing: type: {(TokenType)currentWord.Value}, value: {value}");

                temp.Enqueue(value);
            }

            return temp;
        }
    }
}