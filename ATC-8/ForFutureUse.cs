namespace ATC8
{
    public sealed class ForFutureUse
    {
        /*for (int i = 0; i < bytecode.Length; i++)
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
            }*/



        /*try
            {
                Parser parser = new Parser();
                var bytecode = parser.ParseFile("test2.txt");

                Console.WriteLine(Convert.ToString((sbyte)TokenType.Eof, 2));

                for (int i = 0; i < bytecode.Length; i++)
                {
                    if (i % 20 == 0)
                        Console.WriteLine();
                    Console.Write($"{bytecode[i]} ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadKey();
                Environment.Exit(1);
            }*/


        /*if (i % 2 == 0 || i == 0)
                    {
                        prevType = (TokenType) (short) bytecode[i];
                        Console.Write($"{(TokenType) (short) bytecode[i]} ");
                    }
                    else
                    {*/
        /*prevType = (TokenType)(short)bytecode[i];
        Console.Write($"{(TokenType)(short)bytecode[i]} ");
        i++;
        switch (prevType)
        {
            case TokenType.Eof:
                Console.Write("EOF");
                break;
            case TokenType.Opcode:
                Console.Write($"{(Instructions) (short) bytecode[i]}\n");

                break;
            case TokenType.String:
            case TokenType.ExtensionOpcode:
            case TokenType.Label:
            case TokenType.Identifier:
                Word size = bytecode[i];
                Word[] str = new Word[size];

                for (int j = 0; j < size; j++)
                    str[j] = bytecode[++i];

                Console.Write($"'{size}: {str.FromWordArray()}'\n");
                break;
            case TokenType.Integer:
                Console.Write($"{bytecode[i]}\n");
                break;
            case TokenType.Delimiter:
                Console.Write($"'{(char) bytecode[i]}'\n");
                break;
            case TokenType.Operator:
                Console.Write($"{(char) bytecode[i]}\n");
                break;
            case TokenType.Register:
                Console.Write($"{(RegisterName) (short) bytecode[i]}\n");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }*/
        //}
    }
}