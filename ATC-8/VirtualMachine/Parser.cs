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
        private int _tokenPosition = 0;
        private int _parserPosition = 0;

        private void AddTokenType() =>
            _bytecode.Add(new Word((short)_currentToken.Type));
        private void AddTokenValue() =>
            _bytecode.Add(new Word(Convert.ToInt16(_currentToken.Value)));

        private Token GetNextToken()
        {
            _currentToken = _lexer.GetToken();
            _tokenPosition++;
            if (_currentToken.Type != TokenType.Eof)
                AddTokenType();
            return _currentToken;
        }
            

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
                    //_bytecode.Add((short) _currentToken.Type);
                    //AddTokenType();
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
                            ProcessOpcode();
                            break;
                        case TokenType.ExtensionOpcode:
                            ProcessExtensionOpcode();
                            break;
                        case TokenType.Label:
                            ProcessLabel();
                            break;
                        case TokenType.DebugPoint:
                            Console.WriteLine($"Debug point on: \ntokenPosition: {_tokenPosition} | parserPosition: {_parserPosition}");
                            _bytecode.Add(_parserPosition);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(_currentToken.Type.ToString(),
                                $"Unsupported token: {_currentToken.Type} with value {_currentToken.Value}  " +
                                $"({input.Line}, {input.Column})\n" +
                                $"TP: {_tokenPosition}, PP: {_parserPosition} ");
                    }

                    _parserPosition++;
                }
            }
        }

        private void ParseDelimiter()
        {
            if ((char) _currentToken.Value == '[')
            {
                _bytecode.Add(new Word((short)(char)_currentToken.Value));
                GetNextToken();
                ParseMemoryAddress();
            }
            else if ((char) _currentToken.Value == ',')
            {
                _bytecode.Add(new Word((short)(char)_currentToken.Value));
                GetNextToken();
                ParseOperand();
            }
            else if ((char) _currentToken.Value == ']')
            {
                _bytecode.Add(new Word((short)(char)_currentToken.Value));
                GetNextToken();
            }
            else throw new CodeErrorException($"Unsupported delimiter: {_currentToken.Value}");
        }

        private void ProcessOpcode()
        {
            var opcode = (short) Enum.Parse<Instructions>((string)_currentToken.Value, true);
            _bytecode.Add(opcode);

            GetNextToken();

            ParseOperand();
        }

        private void ProcessExtensionOpcode()
        {
            var opcode = (string)_currentToken.Value;
            var wa = opcode.ToWordArray();
            _bytecode.Add(wa.Length);
            _bytecode.AddRange(wa);

            GetNextToken();

            ParseOperand();
        }

        private void ProcessLabel()
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

            //else
            //    throw new CodeErrorException($"Invalid token as Operand: {_currentToken.Value}");

            /*else if (_currentToken.Type == TokenType.Delimiter && (char) _currentToken.Value == ',')
            {
                _bytecode.Add(new Word((short)(char)_currentToken.Value));
                GetNextToken();
                ParseOperand();
            }*/
            //else
            //    throw new CodeErrorException($"Invalid token as Operand: {_currentToken.Value}");
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