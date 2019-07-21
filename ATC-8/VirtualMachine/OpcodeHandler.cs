using System;
using System.Collections.Generic;
using ATC8.Cpu;
using ATC8.VirtualMachine.Lexer.Tokens;

namespace ATC8.VirtualMachine
{
    public class OpcodeHandler
    {
        public CpuBase Cpu { get; }
        public LabelStorage LabelStorage { get; }

        public OpcodeHandler(CpuBase cpu, LabelStorage labelStorage)
        {
            Cpu = cpu;
            LabelStorage = labelStorage;
        }

        public void Handle(Queue<Word> data)
        {
            var opcode = (Instructions) data.Dequeue().Value;

            switch (opcode)
            {
                case Instructions.Add:
                    var firstParamType = (TokenType)data.Dequeue().Value;
                    var firstParam = data.Dequeue();
                    var secondParamType = (TokenType)data.Dequeue().Value;
                    var secondParam = data.Dequeue();

                    if (firstParamType == TokenType.Register)
                    {
                        if (secondParamType == TokenType.Integer)
                        {
                            Cpu[(RegisterName) firstParam.Value] += secondParam;
                            Console.WriteLine($" ADDING: REGISTER {(RegisterName)firstParam.Value} + {secondParam}");
                        }
                    }

                    break;
                default:
                    Console.WriteLine($"Unknown opcode: " + opcode);
                    break;
            }

            if (data.Count != 0)
                throw new InvalidVmOperationException($"Got more parameters than expected: {data.Count / 2} more left ({data.Count} words total)");
        }

        public void HandleExtension(Queue<Word> data)
        {
            var size = data.Dequeue().Value;

            string str = "";

            for (int i = 0; i < size; i++)
            {
                str += (char) data.Dequeue().Value;
            }

            Console.WriteLine($"To handle...");
            Console.WriteLine($"ext opcode is: " + str);
        }
    }
}