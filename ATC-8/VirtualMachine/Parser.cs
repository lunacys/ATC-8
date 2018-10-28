using System;
using System.Collections.Generic;
using System.IO;
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
                    _currentToken = GetNextToken();
                    switch (_currentToken.Type)
                    {
                        case TokenType.Eof:
                            return bytecode.ToArray();
                        case TokenType.Opcode:
                            if (((byte) ((Instructions) _currentToken.Value)) <= 0x1F)
                            {
                                // instructions with 2 parameters
                                _currentToken = GetNextToken();
                                if (_currentToken.Type != TokenType.Identifier ||
                                    _currentToken.Type != TokenType.Integer ||
                                    (_currentToken.Type != TokenType.Delimiter && (char)_currentToken.Value != '[') ||
                                    _currentToken.Type != TokenType.Register ||
                                    _currentToken.Type != TokenType.String)
                                    throw new CodeErrorException("Invalid token type");
                                    


                                _currentToken = GetNextToken();
                                if (_currentToken.Type != TokenType.Delimiter && (char)_currentToken.Value != ',')
                                    throw new CodeErrorException("No comma after first parameter");

                            }
                            else
                            {
                                // instructions with 1 parameter (>0x1F)

                            }
                            break;
                        case TokenType.Identifier:
                            break;
                        case TokenType.Integer:
                            bytecode.Add((short)_currentToken.Type);
                            bytecode.Add((short)_currentToken.Value);
                            break;
                        case TokenType.String:
                            break;
                        case TokenType.ExtensionOpcode:
                            break;

                        case TokenType.Delimiter:
                            break;
                        case TokenType.Label:
                            break;
                        case TokenType.Operator:
                            break;
                        case TokenType.Register:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(_currentToken));
                    }
                }
            }
            
            /*var bytecode = new List<byte>();

            using (StreamReader sr = new StreamReader(filename))
            {
                var content = sr.ReadToEnd();
                var lines = content.Split('\n');

                for (var i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    var l = line.TrimStart(' ', '\t');
                    // Skip empty and comment lines
                    if (string.IsNullOrWhiteSpace(l) || l.StartsWith('#'))
                        continue;

                    var inst = l.Split(' ');

                    if (!Enum.TryParse<Instructions>(inst[0], true, out var type))
                        throw new Exception($"Invalid instruction '{inst[0]}' at line {i + 1}.");

                    bytecode.Add((byte) type);

                    if (type == Instructions.Directive)
                    { 
                        var lit = byte.Parse(inst[1]);
                        bytecode.Add(lit);
                    }
                    else if (type == Instructions.Move)
                    {
                        var lit = byte.Parse(inst[2]);
                        var reg = byte.Parse(inst[1]);
                        bytecode.Add(lit);
                        bytecode.Add(reg);
                    }
                }
            }

            return bytecode.ToArray();*/
        }

        private Token GetNextToken()
        {
            return _lexer.GetToken();
        }
    }
}