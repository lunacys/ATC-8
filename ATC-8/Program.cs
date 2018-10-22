using System;
using System.Collections.Generic;
using System.IO;
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
            var binopPrecedence = new Dictionary<char, int>();
            binopPrecedence['<'] = 10;
            binopPrecedence['+'] = 20;
            binopPrecedence['-'] = 20;
            binopPrecedence['*'] = 40;  // highest.
            var input = new InputStream("test.txt");
            LexerBase lexer = new LexerBase(new StreamReader("test.txt"), binopPrecedence);
            
            do
            {
                int res;
                var tok = lexer.GetNextToken();
                var suc = Enum.TryParse(tok.ToString(), out res);
                if (suc)
                    Console.WriteLine((TokenType) res);
                else
                    Console.WriteLine((char) tok);
            } while (lexer.CurrentToken != (int)TokenType.EOF);
           
            Console.WriteLine("Hello World!");

            Console.ReadKey();
        }
    }
}
