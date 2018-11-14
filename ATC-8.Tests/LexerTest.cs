using System;
using ATC8;
using ATC8.Cpu;
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

            // Registers
            Assert.That((t = N).Type == TokenType.Register && (RegisterName)t.Value == RegisterName.Ax, "1");
            Assert.That((t = N).Type == TokenType.Register && (RegisterName)t.Value == RegisterName.Bx, "2");
            Assert.That((t = N).Type == TokenType.Register && (RegisterName)t.Value == RegisterName.Cx, "3");
            Assert.That((t = N).Type == TokenType.Register && (RegisterName)t.Value == RegisterName.Dx, "1");
            Assert.That((t = N).Type == TokenType.Register && (RegisterName)t.Value == RegisterName.Sp, "2");
            Assert.That((t = N).Type == TokenType.Register && (RegisterName)t.Value == RegisterName.Pc, "3");

            // Memory Addresses 
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == '[');
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == 0b11110000);
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == ']');
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == '[');
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == 0b00001111);
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == ']');
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == '[');
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == 123);
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == ']');
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == '[');
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == 0);
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == ']');
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == '[');
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == 0b1111);
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == ']');
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == '[');
            Assert.That((t = N).Type == TokenType.Integer && (int)t.Value == 0b0000);
            Assert.That((t = N).Type == TokenType.Delimiter && (char)t.Value == ']');

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
            Assert.That((t = N).Type == TokenType.Opcode && (string)t.Value == "dvr");
            Assert.That((t = N).Type == TokenType.Opcode && (string)t.Value == "set");
            Assert.That((t = N).Type == TokenType.Opcode && (string)t.Value == "jne");
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

        private Token N
        {
            get
            {
                Token tok;
                while ((tok = _lexer.GetToken()).Type == TokenType.NewLine)
                {

                }
                return tok;
            }
        }
            

        private void Close()
        {
            _input.Dispose();
        }
    }
}