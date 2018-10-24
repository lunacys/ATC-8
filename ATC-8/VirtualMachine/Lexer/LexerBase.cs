using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using ATC8.VirtualMachine.Lexer.Tokens;

namespace ATC8.VirtualMachine.Lexer
{
    public class LexerBase
    {

        private char _lastChar = ' ';
        private InputStream _input;

        private List<string> _keywords;

        public LexerBase(InputStream input)
        {
            _input = input;
            _keywords = new List<string>
            {
                "bank", "org", "incbin", "dvar", "mov", "jnz", "move", "draw", "jmp"
            };
        }

        public Token GetToken()
        {
            //Console.WriteLine(_input.ReadAllText());

            while (char.IsWhiteSpace(_lastChar))
                _lastChar = _input.Read();

            if (char.IsLetter(_lastChar))
            {
                var identifierString = "" + _lastChar;
                while (char.IsLetterOrDigit(_lastChar = _input.Read()))
                    identifierString += _lastChar;

                if (_keywords.Contains(identifierString))
                    return new Token(TokenType.Function, identifierString);

                return new Token(TokenType.Identifier, identifierString);
            }

            // name         - opcode
            // .name        - extension opcode
            // 127          - decimal
            // 0b10101010   - binary
            // 0x00FF       - hexadecimal
            // "string"     - string
            // [0x00FF]     - memory address (hexadecimal)
            // ,            - delimiter

            if (char.IsDigit(_lastChar) || _lastChar == '.')
            {
                string numStr = "";
                do
                {
                    numStr += _lastChar;
                    _lastChar = _input.Read();
                } while (char.IsDigit(_lastChar) || _lastChar == '.');

                var intValue = int.Parse(numStr);
                return new Token(TokenType.Integer, intValue);
            }
            if (_lastChar == ';')
            {
                do
                {
                    _lastChar = _input.Read();
                } while (_lastChar != '\0' && _lastChar != '\n' && _lastChar != '\r');

                if (!_input.EndOfStream)
                    return GetToken();
            }

            if (_input.EndOfStream && _lastChar == '\0') // TODO: Refactor this
                return new Token(TokenType.Eof, null);

            char thisChar = _lastChar;
            _lastChar = _input.Read();
            return new Token(TokenType.Other, thisChar);
        }
    }
}