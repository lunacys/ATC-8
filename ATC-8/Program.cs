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
            /*var parser = new Parser();
            byte[] bytecode;
            try
            {
                bytecode = parser.ParseFile("test.txt");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
            
            var vm = new VirtualMachine.VirtualMachine();
            vm.Interpret(bytecode);*/
            /*var binopPrecedence = new Dictionary<char, int>();
            binopPrecedence['<'] = 10;
            binopPrecedence['+'] = 20;
            binopPrecedence['-'] = 20;
            binopPrecedence['*'] = 40;  // highest.
            var input = new InputStream("test.txt");*/
            //LexerBase lexer = new LexerBase(new StreamReader("test.txt"), binopPrecedence);
            
            /*do
            {
                int res;
                var tok = lexer.GetNextToken();
                var suc = Enum.TryParse(tok.ToString(), out res);
                if (suc)
                    Console.WriteLine((TokenType) res);
                else
                    Console.WriteLine((char) tok);
            } while (lexer.CurrentToken != (int)TokenType.EOF);*/
           
            Console.WriteLine("Hello World!");

            var ax = new Register(RegisterName.Ax, 0xFA); // 0b11111010
            var bx = new Register(RegisterName.Bx, 0x00); // 0b00000000
            var cx = new Register(RegisterName.Cx, 0xFF); // 0b11111111
            var dx = new Register(RegisterName.Dx, 0b10101010);
            Console.WriteLine($"{ax} {bx} {cx} {dx}");

            cx.Value += dx.Value;

            Console.ReadKey();
        }
    }
}
