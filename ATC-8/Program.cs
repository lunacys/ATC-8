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

            //cx.Value += dx.Value;

            Console.WriteLine(((ax.Value<<16)>>12)&0xffff);

            using (var input = new InputStream("test.txt"))
            {
                var lexer = new LexerBase(input);
                Token tok;

                while ((tok = lexer.GetToken()).Type != TokenType.Eof)
                {
                    switch (tok.Type)
                    {
                        case TokenType.Opcode:
                            Console.WriteLine($"Got a function: {tok.Value} ({input.Line}:{input.Column})");
                            break;
                        case TokenType.Identifier:
                            Console.WriteLine($"Got an identifier: {tok.Value} ({input.Line}:{input.Column})");
                            break;
                        case TokenType.Integer:
                            Console.WriteLine($"Got an integer: {tok.Value} ({input.Line}:{input.Column})");
                            break;
                        case TokenType.Other:
                            Console.WriteLine($"Got some other thing: {tok.Value} ({input.Line}:{input.Column})");
                            break;
                        case TokenType.Eof:
                            Console.WriteLine($"Got Eof ({input.Line}:{input.Column})");
                            break;
                        case TokenType.String:
                            Console.WriteLine($"Got string: {tok.Value} ({input.Line}:{input.Column})");
                            break;
                        case TokenType.ExtensionOpcode:
                            Console.WriteLine($"Got ext opcode: {tok.Value}");
                            break;
                        case TokenType.Address:
                            Console.WriteLine($"Got an address: {tok.Value}");
                            break;
                        case TokenType.Delimiter:
                            Console.WriteLine($"Got a delimiter: {tok.Value}");
                            break;
                        case TokenType.Label:
                            Console.WriteLine($"Got a label: {tok.Value}");
                            break;
                        case TokenType.Operator:
                            Console.WriteLine($"Got an operator: {tok.Value}");
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
