using System;

namespace ATC8.VirtualMachine
{
    public class VirtualMachine
    {
        private const int MaxStackSize = 128;
        private int _stackSize;
        private int[] _stack;

        public VirtualMachine()
        {
            _stackSize = 0;
            _stack = new int[MaxStackSize];
        }

        public void Push(int value)
        {
            if (_stackSize >= MaxStackSize)
                throw new IndexOutOfRangeException();

            _stack[_stackSize++] = value;
        }

        public int Pop()
        {
            if (_stackSize <= 0)
                throw new IndexOutOfRangeException();

            return _stack[--_stackSize];
        }

        public void Interpret(byte[] bytecode)
        {
            for (int i = 0; i < bytecode.Length; i++)
            {
                var inst = (Instructions)bytecode[i];

                switch (inst)
                {
                    case Instructions.Literal:
                        int value = bytecode[++i];
                        Push(value);
                        Console.WriteLine($"Got literal: {value}");
                        break;
                    case Instructions.Addition:
                        int b = Pop();
                        int a = Pop();
                        Push(a + b);
                        Console.WriteLine($"Got addition {a} + {b}: {a + b}");
                        break;
                    case Instructions.SetHealth:
                        int amount = Pop();
                        int wizard = Pop();
                        Console.WriteLine($"Setting health of wizard {wizard} to {amount}");
                        break;
                    case Instructions.SetWisdom:
                        int wamount = Pop();
                        int wwizard = Pop();
                        Console.WriteLine($"Setting wisdom of wizard {wwizard} to {wamount}");
                        break;
                    case Instructions.SetAgility:
                        int aamount = Pop();
                        int awizard = Pop();
                        Console.WriteLine($"Setting agility of wizard {awizard} to {aamount}");
                        break;
                    case Instructions.PlaySound:
                        var soundId = Pop();
                        Console.WriteLine($"Playing sound {soundId}");
                        break;
                    case Instructions.SpawnParticles:
                        var partId = Pop();
                        Console.WriteLine($"Spawning particles {partId}");
                        break;
                    case Instructions.GetHealth:
                        int wiz = Pop();
                        Push(123);
                        break;
                    case Instructions.GetWisdom:
                        int wizz = Pop();
                        Push(321);
                        break;
                    case Instructions.GetAgility:
                        int wizzz = Pop();
                        Push(213);
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