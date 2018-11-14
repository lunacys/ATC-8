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

                    if (bytecode[i] == ',' || bytecode[i] == '[' || bytecode[i] == ']')
                        Console.Write($"{(char) bytecode[i]} ");
                    else 
                        Console.Write($"{bytecode[i]} ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                Environment.Exit(1);
            }

            VirtualMachine.VirtualMachine vm = new VirtualMachine.VirtualMachine();
            //vm.Interpret(bytecode);

            Console.ReadKey();
        }
    }
}