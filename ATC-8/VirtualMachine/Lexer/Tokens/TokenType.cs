namespace ATC8.VirtualMachine.Lexer.Tokens
{
    public enum TokenType : sbyte
    {
        Eof = -1,
        Opcode = 0,
        Identifier = 1,
        Integer = 2,
        String = 3,
        ExtensionOpcode = 4,
        Delimiter = 5,
        Label = 6,
        Operator = 7,
        Register = 8,
        DebugPoint = 9,
        NewLine = 10,
    }
}