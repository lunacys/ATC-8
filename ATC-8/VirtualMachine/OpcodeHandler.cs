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

        public void Handle(Stack<Word> data)
        {
            var opcode = (Instructions) data.Pop().Value;

            switch (opcode)
            {
                case Instructions.Add:
                    var secondParam = data.Pop();
                    var secondParamType = (TokenType)data.Pop().Value;
                    var firstParam = data.Pop();
                    var firstParamType = (TokenType)data.Pop().Value;

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