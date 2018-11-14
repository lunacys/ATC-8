using System;
using System.Collections.Generic;
using System.Linq;
using ATC8.Cpu;
using ATC8.VirtualMachine.Lexer.Tokens;

namespace ATC8.VirtualMachine.Lexer
{
    public class LexerBase
    {
        private char _lastChar = ' ';
        private readonly InputStream _input;

        private readonly List<string> _keywords;
        private readonly List<string> _registers;

        private static string ValidDelimiters => "(),[]";
        private static string ValidOperators => "+-*/";
        private static string ValidIntegerChars => "AaBbCcDdEeFf0123456789bx";

        private bool EndOfStream => _input.EndOfStream && _lastChar == '\0';

        private static bool IsDelimiter(char ch) => ValidDelimiters.Contains(ch);
        private static bool IsOperator(char ch) => ValidOperators.Contains(ch);
        private static bool IsValidForInteger(char ch) => ValidIntegerChars.Contains(ch);
        
        private static bool IsLetterOrDigitOrLabel(char ch) => char.IsLetterOrDigit(ch) || ch == ':';
        private static bool IsIdentOrOpcodeOrLabelOrRegister(char ch) => char.IsLetter(ch);
        private static bool IsExtensionOpcode(char ch) => ch == '.';
        private static bool IsStringBeginning(char ch) => ch == '\"';
        private static bool IsCommentBeginning(char ch) => ch == ';';
        private static bool IsDebugPoint(char ch) => ch == '!';
        private static bool IsWhiteSpace(char ch) => ch == ' ' || ch == '\t'; // not including new line
        private static bool IsNewLine(char ch) => ch == '\n' || ch == '\r';

        public LexerBase(InputStream input)
        {
            _input = input;
            _keywords = new List<string>();
            _registers = new List<string>();

            foreach (var a in (Instructions[])Enum.GetValues(typeof(Instructions)))
                _keywords.Add(a.ToString().ToLower());
            
            foreach (var a in (RegisterName[]) Enum.GetValues(typeof(RegisterName)))
                _registers.Add(a.ToString().ToLower());
        }

        public Token GetToken()
        {
            while (IsWhiteSpace(_lastChar))
                _lastChar = _input.Read();

            if (IsDebugPoint(_lastChar))
            {
                char t = _lastChar;
                _lastChar = _input.Read();

                return new Token(TokenType.DebugPoint, t);
            }

            if (IsNewLine(_lastChar))
            {
                while (IsNewLine(_lastChar))
                    _lastChar = _input.Read();
                return new Token(TokenType.NewLine, null);
            }

            if (IsIdentOrOpcodeOrLabelOrRegister(_lastChar))
                return ReadIdentOrOpcodeOrLabelOrRegister();

            if (IsExtensionOpcode(_lastChar))
                return ReadExtensionOpcode();

            if (IsStringBeginning(_lastChar))
                return ReadString();

            if (IsOperator(_lastChar))
                return ReadOperator();

            if (char.IsDigit(_lastChar))
                return ReadDigit();

            if (IsDelimiter(_lastChar))
            {
                char t = _lastChar;
                _lastChar = _input.Read();

                return new Token(TokenType.Delimiter, t);
            }

            if (IsCommentBeginning(_lastChar))
            {
                SkipComment();

                if (!EndOfStream)
                    return GetToken();
            }

            if (EndOfStream)
                return new Token(TokenType.Eof, null);

            throw new SyntaxException($"Unknown character: {_lastChar}");
        }
        
        private IntegerType GetIntType(string val)
        {
            if (val.Contains("b"))
                return IntegerType.Binary;
            if (val.Contains("x"))
                return IntegerType.Hexadecimal;
            return IntegerType.Decimal;
        }

        private int GetIntFrom(string val, IntegerType type)
        {
            switch (type)
            {
                case IntegerType.Binary:
                    val = val.Replace("0b", "");
                    return Convert.ToInt32(val, 2);
                case IntegerType.Decimal:
                    return Convert.ToInt32(val, 10);
                case IntegerType.Hexadecimal:
                    val = val.Replace("0x", "");
                    return Convert.ToInt32(val, 16);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private string ReadWhile(Predicate<char> match)
        {
            var str = "";

            while (!EndOfStream && match(_lastChar = _input.Read()))
                str += _lastChar;

            return str;
        }

        private void SkipComment()
        {
            do _lastChar = _input.Read(); while (!EndOfStream 
                                         && _lastChar != '\n' 
                                         && _lastChar != '\r');
        }
        
        private Token ReadIdentOrOpcodeOrLabelOrRegister()
        {
            var identifierString = _lastChar + ReadWhile(IsLetterOrDigitOrLabel);

            if (identifierString.EndsWith(':'))
                return new Token(TokenType.Label, identifierString.Remove(identifierString.Length - 1, 1));

            if (_registers.Contains(identifierString.ToLower()))
                return new Token(TokenType.Register, Enum.Parse<RegisterName>(identifierString, true));

            if (_keywords.Contains(identifierString.ToLower()))
                return new Token(TokenType.Opcode, identifierString);

            return new Token(TokenType.Identifier, identifierString);
        }

        private Token ReadExtensionOpcode()
        {
            var opcode = ReadWhile(char.IsLetterOrDigit);

            return new Token(TokenType.ExtensionOpcode, opcode);
        }

        private Token ReadString()
        {
            var str = "";
            while ((_lastChar = _input.Read()) != '\"')
            {
                if (_lastChar == '\n' || _lastChar == '\r')
                    throw new SyntaxException("String is not closed!");

                str += _lastChar;
            }

            _lastChar = _input.Read();

            return new Token(TokenType.String, str);
        }

        private Token ReadOperator()
        {
            var op = _lastChar;
            _lastChar = _input.Read();

            return new Token(TokenType.Operator, op);
        }

        private Token ReadDigit()
        {
            string numStr = _lastChar + ReadWhile(IsValidForInteger);

            return new Token(TokenType.Integer, GetIntFrom(numStr, GetIntType(numStr)));
        }
    }
}