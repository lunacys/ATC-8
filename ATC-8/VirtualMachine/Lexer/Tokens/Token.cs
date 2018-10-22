namespace ATC8.VirtualMachine.Lexer.Tokens
{
    public class Token
    {
        public TokenType Type { get; }
        public object Value { get; }

        public Token(TokenType type, object value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{{Type: {Type}, Value: {Value}}}";
        }
    }
}