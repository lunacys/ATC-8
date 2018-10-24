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
        public string IdentifierString { get; private set; }
        public int IntValue { get; private set; }

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
            while (char.IsWhiteSpace(_lastChar))
                _lastChar = _input.Read();

            if (char.IsLetter(_lastChar))
            {
                IdentifierString = "" + _lastChar;
                while (char.IsLetterOrDigit(_lastChar = _input.Read()))
                    IdentifierString += _lastChar;

                if (_keywords.Contains(IdentifierString))
                    return new Token(TokenType.Function, IdentifierString);

                return new Token(TokenType.Identifier, IdentifierString);
            }

            if (char.IsDigit(_lastChar) || _lastChar == '.')
            {
                string numStr = "";
                do
                {
                    numStr += _lastChar;
                    _lastChar = _input.Read();
                } while (char.IsDigit(_lastChar) || _lastChar == '.');

                IntValue = int.Parse(numStr);
                return new Token(TokenType.Integer, IntValue);
            }
            // BUG: Comments don't work
            if (_lastChar == ';')
            {
                do
                {
                    _lastChar = _input.Read();
                } while (_input.EndOfStream && _lastChar != '\n' && _lastChar != '\r');

                if (!_input.EndOfStream)
                    return GetToken();
            }

            if (_input.EndOfStream)
                return new Token(TokenType.Eof, null);

            char thisChar = _lastChar;
            _lastChar = _input.Read();
            return new Token(TokenType.Other, thisChar);
        }
    }
}