namespace ATC8.VirtualMachine
{
    public enum Instructions : byte
    {
        /// <summary>
        /// Define variable
        /// </summary>
        Dvr = 0x00,
        /// <summary>
        /// Addition
        /// </summary>
        Add = 0x01,
        /// <summary>
        /// Subtraction
        /// </summary>
        Sub = 0x02,
        /// <summary>
        /// Unsigned multiplying
        /// </summary>
        Mul = 0x03,
        /// <summary>
        /// Signed multiplying
        /// </summary>
        Mli,
        /// <summary>
        /// Unsigned division
        /// </summary>
        Div,
        /// <summary>
        /// Signed division
        /// </summary>
        Dvi,
        /// <summary>
        /// Unsigned modulo
        /// </summary>
        Mod,
        /// <summary>
        /// Signed modulo
        /// </summary>
        Mdi,
        /// <summary>
        /// Bitwise AND
        /// </summary>
        And,
        /// <summary>
        /// Bitwise OR
        /// </summary>
        Bor,
        /// <summary>
        /// Bitwise XOR
        /// </summary>
        Xor,
        /// <summary>
        /// Logical shift
        /// </summary>
        Shr,
        /// <summary>
        /// Arithmetic shift
        /// </summary>
        Asr,
        /// <summary>
        /// a>>b
        /// </summary>
        Shl,
        Ifb,
        Ifc,
        Ife,
        Ifn,
        Ifg,
        Ifa,
        Ifl,
        Ifu,
        Inc,
        Dec,
        Adx,
        Sbx,
        Sti,
        Std = 0x1D,
        // 0x1E, 0x1F are reserved
        Jsr = 0x20,
        Int,
        Iag,
        Ias,
        Rfi,
        Iaq = 0x25,
        // 0x26-0x2F are reserved
        Jmp = 0x30,
        Jgx,
        Jlx,
        Jex,
        Jsg,
        Jsl,
        Jne = 0x36,
        // 0x37-0xFF are reserved
    }
}