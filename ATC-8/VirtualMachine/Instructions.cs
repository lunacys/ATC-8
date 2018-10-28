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
        Ifb = 0x10,
        Ifc = 0x11,
        Ife = 0x12,
        Ifn = 0x13,
        Ifg = 0x14,
        Ifa = 0x15,
        Ifl = 0x16,
        Ifu = 0x17,
        // 0x18, 0x19 are reserved
        Adx = 0x1A,
        Sbx = 0x1B,
        Sti = 0x1C,
        Std = 0x1D,
        // 0x1E, 0x1F are reserved
        Jsr = 0x20,
        Int = 0x21,
        Iag = 0x22,
        Ias = 0x23,
        Rfi = 0x24,
        Iaq = 0x25,
        Inc = 0x26,
        Dec = 0x27,
        // 0x26-0x2F are reserved
        Jmp = 0x30,
        Jgx = 0x31,
        Jlx = 0x32,
        Jex = 0x33,
        Jsg = 0x34,
        Jsl = 0x35,
        Jne = 0x36,
        // 0x37-0xFF are reserved
    }
}