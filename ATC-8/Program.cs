using System;
using ATC8.VirtualMachine;

namespace ATC8
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
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
            vm.Interpret(bytecode);

            Console.WriteLine("Hello World!");
        }
    }
}
