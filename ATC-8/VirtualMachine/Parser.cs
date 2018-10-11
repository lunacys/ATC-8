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

                for (var i = 0; i < lines.Length; i++)
                {
                    var line = lines[i];
                    var l = line.TrimStart(' ', '\t');
                    // Skip empty and comment lines
                    if (string.IsNullOrWhiteSpace(l) || l.StartsWith('#'))
                        continue;

                    var inst = l.Split(' ');

                    if (!Enum.TryParse<Instructions>(inst[0], true, out var type))
                        throw new Exception($"Invalid instruction '{inst[0]}' at line {i + 1}.");

                    bytecode.Add((byte) type);

                    if (type == Instructions.Directive)
                    { 
                        var lit = byte.Parse(inst[1]);
                        bytecode.Add(lit);
                    }
                    else if (type == Instructions.Move)
                    {
                        var lit = byte.Parse(inst[2]);
                        var reg = byte.Parse(inst[1]);
                        bytecode.Add(lit);
                        bytecode.Add(reg);
                    }
                }
            }

            return bytecode.ToArray();
        }
    }
}