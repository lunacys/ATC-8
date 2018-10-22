using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ATC8.VirtualMachine
{
    public class VirtualMachine
    {
        private const int MaxStackSize = 32;

        private int StackSize => _stack.Count;
        //private int[] _stack;
        private readonly Stack<int> _stack;

        private readonly ReadOnlyDictionary<string, int> _registers;

        public VirtualMachine()
        {
            //_stackSize = 0;
            //_stack = new int[MaxStackSize];
            _stack = new Stack<int>(MaxStackSize);
            var regs = new Dictionary<string, int>()
            {
                {"A", 0},
                {"B", 0},
                {"C", 0},
                {"D", 0}
            };
            _registers = new ReadOnlyDictionary<string, int>(regs);

            StreamReader sr = new StreamReader("hello.txt");
            
        }

        public void Interpret(byte[] bytecode)
        {
            for (int i = 0; i < bytecode.Length; i++)
            {
                var inst = (Instructions)bytecode[i];

                switch (inst)
                {
                    case Instructions.Move:

                        break;
                    case Instructions.Directive:
                        int value = bytecode[++i];
                        _stack.Push(value);
                        Console.WriteLine($"Got literal: {value}");
                        break;
                    case Instructions.Addition:
                        int b = _stack.Pop();
                        int a = _stack.Pop();
                        _stack.Push(a + b);
                        Console.WriteLine($"Got addition {a} + {b}: {a + b}");
                        break;
                    case Instructions.SetHealth:
                        int amount = _stack.Pop();
                        int wizard = _stack.Pop();
                        Console.WriteLine($"Setting health of wizard {wizard} to {amount}");
                        break;
                    case Instructions.SetWisdom:
                        int wamount = _stack.Pop();
                        int wwizard = _stack.Pop();
                        Console.WriteLine($"Setting wisdom of wizard {wwizard} to {wamount}");
                        break;
                    case Instructions.SetAgility:
                        int aamount = _stack.Pop();
                        int awizard = _stack.Pop();
                        Console.WriteLine($"Setting agility of wizard {awizard} to {aamount}");
                        break;
                    case Instructions.PlaySound:
                        var soundId = _stack.Pop();
                        Console.WriteLine($"Playing sound {soundId}");
                        break;
                    case Instructions.SpawnParticles:
                        var partId = _stack.Pop();
                        Console.WriteLine($"Spawning particles {partId}");
                        break;
                    case Instructions.GetHealth:
                        int wiz = _stack.Pop();
                        _stack.Push(123);
                        break;
                    case Instructions.GetWisdom:
                        int wizz = _stack.Pop();
                        _stack.Push(321);
                        break;
                    case Instructions.GetAgility:
                        int wizzz = _stack.Pop();
                        _stack.Push(213);
                        break;
                    case Instructions.Print:
                        Console.WriteLine("STACK:");
                        foreach (var val in _stack)
                        {
                            Console.Write($"{val} ");
                        }

                        Console.WriteLine();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}