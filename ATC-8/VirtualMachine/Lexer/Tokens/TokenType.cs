namespace ATC8.VirtualMachine.Lexer.Tokens
{
    public enum TokenType
    {
        Eof = -1,
        Opcode = 0,
        Identifier = 2,
        Integer = 3,
        Other = 4,
        String = 5,
        ExtensionOpcode = 6,
        Address = 7,
        Delimiter,
        Label,
        Operator,
        Register
    }
}