using System;
using System.Collections.Generic;
using System.IO;
using ATC8.Cpu;
using ATC8.VirtualMachine;
using ATC8.VirtualMachine.Lexer;
using ATC8.VirtualMachine.Lexer.Tokens;

namespace ATC8
{
    class Program
    {
        static void Main(string[] args)
        {
            var ax = new Register(RegisterName.Ax, 0xFA); // 0b11111010
            var bx = new Register(RegisterName.Bx, 0x00); // 0b00000000
            var cx = new Register(RegisterName.Cx, 0xFF); // 0b11111111
            var dx = new Register(RegisterName.Dx, 0b10101010);
            Console.WriteLine($"{ax} {bx} {cx} {dx}");

            Console.WriteLine(((ax.Value<<16)>>12)&0xffff);

            string test = "abcdefg";
            var b = test.ToWordArray();
            foreach (var word in b)
            {
                Console.Write($"{word} ");
            }

            Console.WriteLine();

            Parser parser = new Parser();
            var bytecode = parser.ParseFile("test2.txt");

            Console.WriteLine(Convert.ToString((sbyte)TokenType.Eof, 2));

            TokenType prevType = TokenType.Eof;

            for (int i = 0; i < bytecode.Length; i++)
            {
                if (i % 2 == 0 || i == 0)
                {
                    prevType = (TokenType) (short) bytecode[i];
                    Console.Write($"{(TokenType) (short) bytecode[i]} ");
                }
                else
                {
                    switch (prevType)
                    {
                        case TokenType.Eof:
                            Console.Write("EOF");
                            break;
                        case TokenType.Opcode:
                            Console.Write($"{(Instructions)(short)bytecode[i]} ");
                            break;
                        case TokenType.Identifier:
                            break;
                        case TokenType.Integer:
                            break;
                        case TokenType.String:
                            break;
                        case TokenType.ExtensionOpcode:
                            break;
                        case TokenType.Delimiter:
                            break;
                        case TokenType.Label:
                            break;
                        case TokenType.Operator:
                            break;
                        case TokenType.Register:
                            Console.Write($"{(RegisterName)(short)bytecode[i]} ");
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            Console.ReadKey();
        }
    }
}
