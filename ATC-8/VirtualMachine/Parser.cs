using System;
using System.Collections.Generic;
using System.IO;

namespace ATC8.VirtualMachine
{
    public class Parser
    {
        public byte[] ParseFile(string filename)
        {
            var bytecode = new List<byte>();

            using (StreamReader sr = new StreamReader(filename))
            {
                var content = sr.ReadToEnd();
                var lines = content.Split('\n');

                foreach (var line in lines)
                {
                    var inst = line.Split(' ');
                    var type = Enum.Parse<Instructions>(inst[0]);
                    bytecode.Add((byte)type);
                    switch (type)
                    {
                        case Instructions.Literal:
                            var lit = byte.Parse(inst[1]);
                            bytecode.Add(lit);
                            break;
                    }
                }
            }

            return bytecode.ToArray();
        }
    }
}