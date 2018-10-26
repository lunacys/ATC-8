namespace ATC8.VirtualMachine.Lexer.Tokens
{
    public enum TokenType
    {
        Eof = -1,
        Opcode = 0,
        Identifier = 1,
        Integer = 2,
        String,
        ExtensionOpcode,
        Address,
        Delimiter,
        Label,
        Operator,
        Register
    }
}