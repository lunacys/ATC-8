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
        /*
        for (int i = 0; i < _bytecode.Length; i++)
            {
                TokenType tokenType = (TokenType)(sbyte)_bytecode[i];
                switch (tokenType)
                {
                    case TokenType.Eof:
                        return;
                    case TokenType.Opcode:
                        var opcode = (Instructions) (byte) _bytecode[++i];
                        HandleOpcode(opcode, ref i);
                        break;
                    case TokenType.Identifier:
                        var identSize = _bytecode[++i];
                        var identStr = new Word[identSize];
                        for (int j = 0; j < identSize; j++)
                            identStr[j] = _bytecode[++i];
                        HandleIdentifier(identStr.FromWordArray());
                        break;
                    case TokenType.Integer:
                        HandleInteger(_bytecode[++i]);
                        break;
                    case TokenType.String:
                        var stringSize = _bytecode[++i];
                        var stringStr = new Word[stringSize];
                        for (int j = 0; j < stringSize; j++)
                            stringStr[j] = _bytecode[++i];
                        HandleString(stringStr.FromWordArray());
                        break;
                    case TokenType.ExtensionOpcode:
                        var extOpcodeSize = _bytecode[++i];
                        var extOpcodeStr = new Word[extOpcodeSize];
                        for (int j = 0; j < extOpcodeSize; j++)
                            extOpcodeStr[j] = _bytecode[++i];
                        HandleExtOpcode(extOpcodeStr.FromWordArray());
                        break;
                    case TokenType.Delimiter:
                        HandleDelimiter((char)_bytecode[++i]);
                        break;
                    case TokenType.Label:
                        var labelSize = _bytecode[++i];
                        var labelStr = new Word[labelSize];
                        for (int j = 0; j < labelSize; j++)
                            labelStr[j] = _bytecode[++i];
                        HandleLabel(labelStr.FromWordArray(), _currentPosition);
                        break;
                    case TokenType.Operator:
                        HandleOperator((char)_bytecode[++i]);
                        break;
                    case TokenType.Register:
                        var register = (RegisterName) (short) _bytecode[++i];
                        HandleRegister(register);
                        break;
                    case TokenType.DebugPoint:
                        HandleDebugPoint(_bytecode[++i]);
                        break;
                    case TokenType.NewLine:
                        //Console.WriteLine("New Line");
                        Process();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                _currentPosition = i;
            }*/
    }
}