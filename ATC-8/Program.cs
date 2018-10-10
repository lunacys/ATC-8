using System;
using ATC8.VirtualMachine;

namespace ATC8
{
    class Program
    {
        static void Main(string[] args)
        {
            var parser = new Parser();
            var bytecode = parser.ParseFile("test.txt");
            var vm = new VirtualMachine.VirtualMachine();
            vm.Interpret(bytecode);

            Console.WriteLine("Hello World!");
        }
    }
}
