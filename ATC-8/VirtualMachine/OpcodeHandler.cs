using System;
using System.Collections.Generic;
using ATC8.Cpu;
using ATC8.VirtualMachine.Lexer.Tokens;

namespace ATC8.VirtualMachine
{
    public class OpcodeHandler
    {
        public CpuBase Cpu { get; }

        public OpcodeHandler(CpuBase cpu)
        {
            Cpu = cpu;
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
        }
    }
}