using System;
using ATC8.VirtualMachine;
using ATC8.VirtualMachine.Lexer;
using ATC8.VirtualMachine.Lexer.Tokens;
using NUnit.Framework;

namespace ATC_8.Tests
{
    [TestFixture]
    public class LexerTest
    {
        private LexerBase _lexer;
        private InputStream _input;

        public LexerTest()
        {
            _input = new InputStream("LexerTest.txt");
            _lexer = new LexerBase(_input);
        }

        [Test]
        public void TestLexer_ReadToken()
        {
            Token t;

            // Label
            Assert.That((t = N).Type == TokenType.Label && (string)t.Value == "label", $"Expected label 'label', but got: {t.Type} | {t.Value}");

            // Identifiers
            Assert.That((t = N).Type == TokenType.Identifier && (string)t.Value == "ident1", "1");
            Assert.That((t = N).Type == TokenType.Identifier && (string)t.Value == "ident2", "2");
            Assert.That((t = N).Type == TokenType.Identifier && (string)t.Value == "ident3", "3");

            // Memory addresses
            Assert.That((t = N).Type == TokenType.Address && (int)t.Value == Convert.ToInt32("F0", 16), "4");
            Assert.That((t = N).Type == TokenType.Address && (int)t.Value == Convert.ToInt32("0F", 16), "5");
            Assert.That((t = N).Type == TokenType.Address && (int)t.Value == 123, "6");
            Assert.That((t = N).Type == TokenType.Address && (int)t.Value == 0, "7");
            Assert.That((t = N).Type == TokenType.Address && (int)t.Value == Convert.ToInt32("1111", 2), "8");
            Assert.That((t = N).Type == TokenType.Address && (int)t.Value == Convert.ToInt32("0000", 2), "9");

            // Integers
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == 12345, "10");
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == Convert.ToInt32("F0", 16), "11");
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == Convert.ToInt32("0F", 16), "12");
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == Convert.ToInt32("1111", 2), "13");
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == Convert.ToInt32("0000", 2), "14");
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == 0, "15");

            // Delimiters
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == '(', "(");
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == ')', "1)");
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == ',', ",");
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == '(', "(");
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == ',', ",");
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == ')', "2)");

            // Opcodes 
            Assert.That((t = N).Type == TokenType.Opcode && (string)t.Value == "dvar");
            Assert.That((t = N).Type == TokenType.Opcode && (string)t.Value == "mov");
            Assert.That((t = N).Type == TokenType.Opcode && (string)t.Value == "jnz");
            Assert.That((t = N).Type == TokenType.Opcode && (string)t.Value == "jmp");

            // Ext opcodes 
            Assert.That((t = N).Type == TokenType.ExtensionOpcode && (string)t.Value == "bank");
            Assert.That((t = N).Type == TokenType.ExtensionOpcode && (string)t.Value == "org");
            Assert.That((t = N).Type == TokenType.ExtensionOpcode && (string)t.Value == "incbin");
            Assert.That((t = N).Type == TokenType.ExtensionOpcode && (string)t.Value == "move");
            Assert.That((t = N).Type == TokenType.ExtensionOpcode && (string)t.Value == "draw");

            // Strings 
            Assert.That((t = N).Type == TokenType.String && (string)t.Value == "");
            Assert.That((t = N).Type == TokenType.String && (string)t.Value == "123");
            Assert.That((t = N).Type == TokenType.String && (string)t.Value == "mystring");
            Assert.That((t = N).Type == TokenType.String && (string)t.Value == "My Very Very Long String!@#$%^&*(),");
            Assert.That((t = N).Type == TokenType.String && (string)t.Value == " asd ");
            Assert.Throws<SyntaxException>(() => t = N, "Expected Exception");

            // Operators
            Assert.That((t = N).Type == TokenType.Operator && (char)t.Value == '+', "+");
            Assert.That((t = N).Type == TokenType.Operator && (char)t.Value == '-', "-");
            Assert.That((t = N).Type == TokenType.Operator && (char)t.Value == '*', "*");
            Assert.That((t = N).Type == TokenType.Operator && (char)t.Value == '/', "/");

            Close();
        }

        private Token N => 
            _lexer.GetToken();

        private void Close()
        {
            _input.Dispose();
        }
    }
}