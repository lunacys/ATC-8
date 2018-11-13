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

            var test2 = b.FromWordArray();
            foreach (char ch in test2)
            {
                Console.Write($"{ch} ");
            }

            Console.WriteLine();

            try
            {
                Parser parser = new Parser();
                var bytecode = parser.ParseFile("test2.txt");

                Console.WriteLine(Convert.ToString((sbyte)TokenType.Eof, 2));

                for (int i = 0; i < bytecode.Length; i++)
                {
                    if (i % 20 == 0)
                        Console.WriteLine();
                    Console.Write(
                        bytecode[i] == ',' || bytecode[i] == '[' || bytecode[i] == ']' 
                            ? $"{(char)bytecode[i]} " 
                            : $"{bytecode[i]} ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                Environment.Exit(1);
            }

            Console.ReadKey();
        }
    }
}


/*if (i % 2 == 0 || i == 0)
                    {
                        prevType = (TokenType) (short) bytecode[i];
                        Console.Write($"{(TokenType) (short) bytecode[i]} ");
                    }
                    else
                    {*/
/*prevType = (TokenType)(short)bytecode[i];
Console.Write($"{(TokenType)(short)bytecode[i]} ");
i++;
switch (prevType)
{
    case TokenType.Eof:
        Console.Write("EOF");
        break;
    case TokenType.Opcode:
        Console.Write($"{(Instructions) (short) bytecode[i]}\n");

        break;
    case TokenType.String:
    case TokenType.ExtensionOpcode:
    case TokenType.Label:
    case TokenType.Identifier:
        Word size = bytecode[i];
        Word[] str = new Word[size];

        for (int j = 0; j < size; j++)
            str[j] = bytecode[++i];

        Console.Write($"'{size}: {str.FromWordArray()}'\n");
        break;
    case TokenType.Integer:
        Console.Write($"{bytecode[i]}\n");
        break;
    case TokenType.Delimiter:
        Console.Write($"'{(char) bytecode[i]}'\n");
        break;
    case TokenType.Operator:
        Console.Write($"{(char) bytecode[i]}\n");
        break;
    case TokenType.Register:
        Console.Write($"{(RegisterName) (short) bytecode[i]}\n");
        break;
    default:
        throw new ArgumentOutOfRangeException();
}*/
//}