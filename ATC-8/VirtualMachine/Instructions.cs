namespace ATC8.VirtualMachine
{
    public enum Instructions : byte
    {
        /// <summary>
        /// Define variable
        /// </summary>
        Dvr = 0x00,
        /// <summary>
        /// Set value
        /// </summary>
        Set = 0x01,
        /// <summary>
        /// Addition
        /// </summary>
        Add = 0x02,
        /// <summary>
        /// Subtraction
        /// </summary>
        Sub = 0x03,
        /// <summary>
        /// Unsigned multiplying
        /// </summary>
        Mul = 0x04,
        /// <summary>
        /// Signed multiplying
        /// </summary>
        Mli = 0x05,
        /// <summary>
        /// Unsigned division
        /// </summary>
        Div = 0x06,
        /// <summary>
        /// Signed division
        /// </summary>
        Dvi = 0x07,
        /// <summary>
        /// Unsigned modulo
        /// </summary>
        Mod = 0x08,
        /// <summary>
        /// Signed modulo
        /// </summary>
        Mdi = 0x09,
        /// <summary>
        /// Bitwise AND
        /// </summary>
        And = 0x0A,
        /// <summary>
        /// Bitwise OR
        /// </summary>
        Bor = 0x0B,
        /// <summary>
        /// Bitwise XOR
        /// </summary>
        Xor = 0x0C,
        /// <summary>
        /// Logical shift
        /// </summary>
        Shr = 0x0D,
        /// <summary>
        /// Arithmetic shift
        /// </summary>
        Asr = 0x0E,
        /// <summary>
        /// a>>b
        /// </summary>
        Shl = 0x0F,
        /// <summary>
        /// Performs next instruction only if (a & b) != 0, instead skips it
        /// </summary>
        Ifb = 0x10,
        /// <summary>
        /// Performs next instruction only if (a & b) == 0, instead skips it
        /// </summary>
        Ifc = 0x11,
        /// <summary>
        /// Performs next instruction only if a == b, instead skips it
        /// </summary>
        Ife = 0x12,
        /// <summary>
        /// Performs next instruction only if a != b, instead skips it
        /// </summary>
        Ifn = 0x13,
        /// <summary>
        /// Performs next instruction only if a > b (unsigned), instead skips it
        /// </summary>
        Ifg = 0x14,
        /// <summary>
        /// Performs next instruction only if a > b (signed), instead skips it
        /// </summary>
        Ifa = 0x15,
        /// <summary>
        /// Performs next instruction only if a < b (unsigned), instead skips it
        /// </summary>
        Ifl = 0x16,
        /// <summary>
        /// Performs next instruction only if a < b (signed), instead skips it
        /// </summary>
        Ifu = 0x17,
        // 0x18, 0x19 are reserved
        /// <summary>
        /// Sets a to a+b+EX, sets EX to 0x0001 if there is an overflow, 0x0 otherwise
        /// </summary>
        Adx = 0x1A,
        /// <summary>
        /// Sets a to a-b+EX, sets EX to 0xFFFF if there is an underflow, 0x0 otherwise
        /// </summary>
        Sbx = 0x1B,
        /// <summary>
        /// Sets a to b, then increases SP and BP by one
        /// </summary>
        Sti = 0x1C,
        /// <summary>
        /// Sets a to b, then decreases SP and BP by one
        /// </summary>
        Std = 0x1D,
        // 0x1E, 0x1F are reserved
        /// <summary>
        /// Pushes the address of the next instruction to the stack, then sets PC to a
        /// </summary>
        Jsr = 0x20,
        /// <summary>
        /// Triggers a software interrupt with message a
        /// </summary>
        Int = 0x21,
        /// <summary>
        /// Sets a to IA
        /// </summary>
        Iag = 0x22,
        /// <summary>
        /// Sets IA to a
        /// </summary>
        Ias = 0x23,
        /// <summary>
        /// Disables interrupt queueing, pops a from stack, then pops PC from the stack
        /// </summary>
        Rfi = 0x24,
        /// <summary>
        /// If a is nonzero, interrupts will be added to the queue instead of triggered.
        /// If a is zero, interrupts will be triggered as normal again
        /// </summary>
        Iaq = 0x25,
        /// <summary>
        /// Increments a by one and sets result to AX
        /// </summary>
        Inc = 0x26,
        /// <summary>
        /// Decrements a by one and sets result to AX
        /// </summary>
        Dec = 0x27,
        // 0x26-0x2F are reserved
        /// <summary>
        /// Transfers execution unconditionally to a label
        /// </summary>
        Jmp = 0x30,
        /// <summary>
        /// Transfers execution to a label if JD>=0
        /// </summary>
        Jgx = 0x31,
        /// <summary>
        /// Transfers execution to a label if JD<=0
        /// </summary>
        Jlx = 0x32,
        /// <summary>
        /// Transfers execution to a label if JD==0
        /// </summary>
        Jex = 0x33,
        /// <summary>
        /// Transfers execution to a label if JD>0
        /// </summary>
        Jsg = 0x34,
        /// <summary>
        /// Transfers execution to a label if JD<0
        /// </summary>
        Jsl = 0x35,
        /// <summary>
        /// Transfers execution to a label if JD!=0
        /// </summary>
        Jne = 0x36,
        /// <summary>
        /// Transfers execution to index a. a can be either negative or positive.
        /// If a is 0, do nothing
        /// </summary>
        Jti = 0x37
        // 0x38-0xFF are reserved
    }
}