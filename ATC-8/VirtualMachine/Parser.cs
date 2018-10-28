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

        public Word[] ParseFile(string filename)
        {
            var bytecode = new List<Word>();

            using (InputStream input = new InputStream(filename))
            {
                _lexer = new LexerBase(input);

                while (true)
                {
                    GetNextToken();
                    bytecode.Add((short) _currentToken.Type);
                    // current token type can be only extension opcode, opcode or label,
                    // and we're moving through the tokens while proceeding those three
                    // main token types, so if we're getting something else besides
                    // those three types, we throw an exception
                    switch (_currentToken.Type)
                    {
                        case TokenType.Eof:
                            return bytecode.ToArray();

                        case TokenType.Opcode:
                            bytecode.AddRange(ProcessOpcode());
                            break;
                        case TokenType.ExtensionOpcode:
                            bytecode.AddRange(ProcessExtensionOpcode());
                            break;
                        case TokenType.Label:
                            bytecode.AddRange(ProcessLabel());
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(_currentToken.Type.ToString(), $"Unsupported token: {_currentToken.Type} with value {_currentToken.Value}");
                    }
                }
            }
        }

        private Word[] ProcessOpcode()
        {
            var result = new List<Word>();

            var opcode = (byte) Enum.Parse<Instructions>((string)_currentToken.Value, true);
            result.Add(opcode);

            GetNextToken();

            if (_currentToken.Type == TokenType.Delimiter)
            {
                if ((char) _currentToken.Value == '[') // memory address
                {
                    GetNextToken();
                    if (_currentToken.Type == TokenType.Integer ||
                        _currentToken.Type == TokenType.Register)
                    {
                        result.Add(new Word((short)_currentToken.Type));
                        result.Add(new Word((short) _currentToken.Value));
                    }
                    else throw new CodeErrorException($"Invalid token: {_currentToken.Type}");
                }
            }
            else if (_currentToken.Type == TokenType.String ||
                     _currentToken.Type == TokenType.Identifier)
            {
                var val = (string)_currentToken.Value;
                result.Add(new Word((short)_currentToken.Type));
                // firstly add string length
                result.Add(val.Length);
                // then add ASCII values from all the characters
                result.AddRange(val.ToWordArray());
            }
            else if (_currentToken.Type == TokenType.Register)
            {
                var val = (byte) _currentToken.Value;
                result.Add(new Word((short)_currentToken.Type));
                result.Add(val);
            }

            GetNextToken();

            if (_currentToken.Type == TokenType.Delimiter && (char) _currentToken.Value == ',')
            {
                GetNextToken();
                if (_currentToken.Type == TokenType.Delimiter)
                {
                    if ((char)_currentToken.Value == '[') // memory address
                    {
                        GetNextToken();
                        if (_currentToken.Type == TokenType.Integer ||
                            _currentToken.Type == TokenType.Register)
                        {
                            result.Add(new Word((short)_currentToken.Type));
                            result.Add(new Word((short)_currentToken.Value));
                        }
                        else throw new CodeErrorException($"Invalid token: {_currentToken.Type}");
                    }
                }
                else if (_currentToken.Type == TokenType.String ||
                         _currentToken.Type == TokenType.Identifier)
                {
                    var val = (string)_currentToken.Value;
                    // firstly add string length
                    result.Add(val.Length);
                    // then add ASCII values from all the characters
                    result.AddRange(val.ToWordArray());
                }
                else if (_currentToken.Type == TokenType.Register)
                {
                    var val = (byte)_currentToken.Value;
                    result.Add(new Word((short)_currentToken.Type));
                    result.Add(val);
                }
            }

            return result.ToArray();
        }

        private Word[] ProcessExtensionOpcode()
        {
            throw new NotImplementedException();
        }

        private Word[] ProcessLabel()
        {
            throw new NotImplementedException();
        }

        private Token GetNextToken()
        {
            return _currentToken = _lexer.GetToken();
        }
    }
}