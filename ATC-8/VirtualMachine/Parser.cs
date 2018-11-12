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

        private void AddTokenType() =>
            _bytecode.Add(new Word((short)_currentToken.Type));
        private void AddTokenValue() =>
            _bytecode.Add(new Word((short)_currentToken.Value));
        private Token GetNextToken() =>
            _currentToken = _lexer.GetToken();

        public Word[] ParseFile(string filename)
        {
            //var bytecode = new List<Word>();
            _bytecode = new List<Word>();

            using (InputStream input = new InputStream(filename))
            {
                _lexer = new LexerBase(input);

                while (true)
                {
                    GetNextToken();
                    _bytecode.Add((short) _currentToken.Type);
                    // current token type can be only extension opcode, opcode or label,
                    // and we're moving through the tokens while proceeding those three
                    // main token types, so if we're getting something else besides
                    // those three types, we throw an exception
                    switch (_currentToken.Type)
                    {
                        case TokenType.Eof:
                            return _bytecode.ToArray();
                        case TokenType.Opcode:
                            ProcessOpcode();
                            break;
                        case TokenType.ExtensionOpcode:
                            ProcessExtensionOpcode();
                            break;
                        case TokenType.Label:
                            ProcessLabel();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(_currentToken.Type.ToString(), $"Unsupported token: {_currentToken.Type} with value {_currentToken.Value}");
                    }
                }
            }
        }

        private void ProcessOpcode()
        {
            var result = new List<Word>();

            var opcode = (byte) Enum.Parse<Instructions>((string)_currentToken.Value, true);
            result.Add(opcode);

            
        }

        private void ProcessExtensionOpcode()
        {
            throw new NotImplementedException();
        }

        private void ProcessLabel()
        {
            ParseLabel();
        }

        private void ParseOperand()
        {
            GetNextToken();

            AddTokenType();

            if (_currentToken.Type == TokenType.Delimiter && (char) _currentToken.Value == '[')
                ParseMemoryAddress();
            else if (_currentToken.Type == TokenType.Identifier ||
                     _currentToken.Type == TokenType.String)
                ParseStringOrIdentifier();
            else if (_currentToken.Type == TokenType.Label)
                ParseLabel();
            else if (_currentToken.Type == TokenType.Register)
                ParseRegister();
            else if (_currentToken.Type == TokenType.Integer)
                ParseInteger();
            else if (_currentToken.Type == TokenType.Delimiter && (char)_currentToken.Value == ',')
                ParseOperand();
            else 
                throw new CodeErrorException($"Invalid token as Operand: {_currentToken.Value}");
        }

        private void ParseMemoryAddress()
        {
            GetNextToken(); // get next token as current token is '['

            if (_currentToken.Type == TokenType.Integer ||
                _currentToken.Type == TokenType.Register)
            {
                AddTokenValue();
            }
            else throw new CodeErrorException($"Invalid token: {_currentToken.Type}");
        }

        private void ParseStringOrIdentifier()
        {
            var val = (string)_currentToken.Value;
            // firstly add string length
            _bytecode.Add(val.Length);
            // then add ASCII values from all the characters
            _bytecode.AddRange(val.ToWordArray());
        }
        
        private void ParseLabel()
        {
            AddTokenValue();
        }

        private void ParseInteger()
        {
            AddTokenValue();
        }

        private void ParseRegister()
        {
            var register = Enum.Parse<RegisterName>((string) _currentToken.Value, true);
            _bytecode.Add(new Word((short)register));
        }
    }
}