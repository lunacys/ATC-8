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

        public override bool Equals(object obj)
        {
            var tok = obj as Token;
            if (tok == null) return false;
            return tok.Type == Type && tok.Value == Value;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Type * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }
    }
}