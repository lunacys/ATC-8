using System;
using System.Collections.Generic;
using ATC8.Cpu;
using ATC8.VirtualMachine.Lexer;
using ATC8.VirtualMachine.Lexer.Tokens;

namespace ATC8.VirtualMachine
{
    public class Parser
    {
        private Token _currentToken;
        private LexerBase _lexer;
        private List<Word> _bytecode;
        private int _tokenPosition;
        private int _parserPosition;

        private void AddTokenType() =>
            _bytecode.Add(new Word((short)_currentToken.Type));

        private void AddTokenValue() =>
            _bytecode.Add(new Word(Convert.ToInt16(_currentToken.Value)));

        private void GetNextToken()
        {
            _currentToken = _lexer.GetToken();
            _tokenPosition++;
            if (_currentToken.Type != TokenType.Eof)
                AddTokenType();
        }

        public Word[] ParseFile(string filename)
        {
            _bytecode = new List<Word>();

            using (InputStream input = new InputStream(filename))
            {
                _lexer = new LexerBase(input);

                while (true)
                {
                    GetNextToken();

                    // current token type can be only extension opcode, opcode or label,
                    // and we're moving through the tokens while proceeding those three
                    // main token types, so if we're getting something else besides
                    // those three types, we throw an exception
                    switch (_currentToken.Type)
                    {
                        case TokenType.Eof:
                            //_bytecode.Add((short)TokenType.Eof);
                            return _bytecode.ToArray();
                        case TokenType.Opcode:
                            HandleOpcode();
                            break;
                        case TokenType.ExtensionOpcode:
                            HandleExtensionOpcode();
                            break;
                        case TokenType.Label:
                            HandleLabel();
                            break;
                        case TokenType.DebugPoint:
                            HandleDebugPoint();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(_currentToken.Type.ToString(),
                                $"Unsupported token: {_currentToken.Type} with value {_currentToken.Value}  " +
                                $"({input.Line}, {input.Column})\n" +
                                $"TP: {_tokenPosition}, PP: {_parserPosition} ");
                    }

                    if (_currentToken.Type != TokenType.DebugPoint)
                        _parserPosition++;
                }
            }
        }

        private void HandleDebugPoint()
        {
            //Console.WriteLine($"Debug point on: \n" +
            //                  $"tokenPosition: {_tokenPosition} | parserPosition: {_parserPosition}");
            //_bytecode.Add(_parserPosition);
        }

        private void HandleOpcode()
        {
            var opcode = (short) Enum.Parse<Instructions>((string)_currentToken.Value, true);
            _bytecode.Add(opcode);

            GetNextToken();

            ParseOperand();
        }

        private void HandleExtensionOpcode()
        {
            var opcode = (string)_currentToken.Value;
            var wa = opcode.ToWordArray();
            _bytecode.Add(wa.Length);
            _bytecode.AddRange(wa);

            GetNextToken();

            ParseOperand();
        }

        private void HandleLabel()
        {
            ParseStringOrIdentifierOrLabel();
        }

        private void ParseOperand()
        {
            if (_currentToken.Type == TokenType.Identifier ||
                 _currentToken.Type == TokenType.String ||
                 _currentToken.Type == TokenType.Label)
                ParseStringOrIdentifierOrLabel();
            else if (_currentToken.Type == TokenType.Register)
                ParseRegister();
            else if (_currentToken.Type == TokenType.Integer)
                ParseInteger();
            else if (_currentToken.Type == TokenType.Delimiter)
                ParseDelimiter();

            GetNextToken();
            
            if (_currentToken.Type == TokenType.Delimiter)
                ParseDelimiter(); 
        }

        private void ParseDelimiter()
        {
            if ((char)_currentToken.Value == '[')
            {
                _bytecode.Add(new Word((short)(char)_currentToken.Value));
                GetNextToken();
                ParseMemoryAddress();
            }
            else if ((char)_currentToken.Value == ',')
            {
                _bytecode.Add(new Word((short)(char)_currentToken.Value));
                GetNextToken();
                ParseOperand();
            }
            else if ((char)_currentToken.Value == ']')
            {
                _bytecode.Add(new Word((short)(char)_currentToken.Value));
            }
            else throw new CodeErrorException($"Unsupported delimiter: {_currentToken.Value}");
        }

        private void ParseMemoryAddress()
        {
            if (_currentToken.Type == TokenType.Integer ||
                _currentToken.Type == TokenType.Register)
            {
                AddTokenValue(); // TODO: Ignore the ']'
            }
            else throw new CodeErrorException($"Invalid token: {_currentToken.Type}");
        }

        private void ParseStringOrIdentifierOrLabel()
        {
            var val = (string)_currentToken.Value;
            // firstly add string length
            _bytecode.Add(val.Length);
            // then add ASCII values from all the characters
            _bytecode.AddRange(val.ToWordArray());
        }

        private void ParseInteger()
        {
            _bytecode.Add(new Word((short)_currentToken.Value));
        }

        private void ParseRegister()
        {
            var register = (RegisterName)_currentToken.Value;
            _bytecode.Add(new Word((short)register));
        }
    }
}