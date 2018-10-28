namespace ATC8.VirtualMachine.Lexer.Tokens
{
    public enum TokenType : sbyte
    {
        Eof = -1,
        Opcode = 0,
        Identifier = 1,
        Integer = 2,
        String,
        ExtensionOpcode,
        Delimiter,
        Label,
        Operator,
        Register
    }
}