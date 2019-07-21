using System;
using System.Diagnostics;
using ATC8.Cpu;
using ATC8.Logging;
using ATC8.VirtualMachine;
using ATC8.VirtualMachine.Lexer.Tokens;

namespace ATC8.Interpreter
{
    class Program
    {
        private static LoggerBase _logger => LoggerFactory.Get("Interpreter.Program");

        static void Main(string[] args)
        {
            Tests();

            _logger.Info("Run your ATC-8 Assembly commands: ");

            try
            {
                var parser = new Parser();
                var vm = new VirtualMachine.VirtualMachine();

                while (true)
                {
                    var currLine = Console.ReadLine();
                    if (string.IsNullOrEmpty(currLine))
                    {
                        _logger.Warning("The line is empty");
                        continue;
                    }

                    if (currLine.ToLower() == "help" || currLine.ToLower() == "?")
                    { }
                    else if (currLine.ToLower() == "quit" || currLine.ToLower() == "exit" || currLine.ToLower() == "q")
                        break;

                    bool shouldViewBytecode = false;
                    if (currLine.EndsWith("|d"))
                    {
                        shouldViewBytecode = true;
                        currLine = currLine.Replace("|d", "");
                    }

                    Word[] bytecode;

                    try
                    {
                        bytecode = parser.ParseString(currLine + "\n");
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.Message);
                        continue;
                    }
                    
                    if (shouldViewBytecode)
                    {

                        var maxBytesOnLine = 20;
                        
                        Console.WriteLine();

                        for (int i = 0; i < bytecode.Length; i++)
                        {
                            if (maxBytesOnLine != 0 && i != 0 && i % maxBytesOnLine == 0)
                                Console.WriteLine();
                            Console.Write($"{((byte) bytecode[i]).ToHexString().ToUpper()} ");
                        }

                        Console.WriteLine("\n");
                    }

                    try
                    {
                        vm.Interpret(bytecode);
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e.Message);
                    }

                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                Environment.Exit(1);
            }
            

            Console.WriteLine("Success! Press any key to continue..");
            Console.ReadKey();
        }

        [Conditional("DEBUG")]
        private static void Tests()
        {
            Console.WriteLine(" ===== TESTS ===== \n");

            var ax = new Register(RegisterName.Ax, 0xFA); // 0b11111010
            var bx = new Register(RegisterName.Bx, 0x00); // 0b00000000
            var cx = new Register(RegisterName.Cx, 0xFF); // 0b11111111
            var dx = new Register(RegisterName.Dx, 0b10101010);
            Console.WriteLine($"{ax} {bx} {cx} {dx}");

            Console.WriteLine(((ax.Value << 16) >> 12) & 0xffff);

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
                        Console.Write($"{(char)bytecode[i]} ");
                    else
                        Console.Write($"{bytecode[i]} ");
                }

                bytecode.SaveToFile("bytecode.txt", 20);

                Console.WriteLine("\n");
                VirtualMachine.VirtualMachine vm = new VirtualMachine.VirtualMachine();
                //vm.Interpret(bytecode);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                Environment.Exit(1);
            }

            Console.WriteLine(" ===== END TESTS ===== \n");
        }
    }
}
