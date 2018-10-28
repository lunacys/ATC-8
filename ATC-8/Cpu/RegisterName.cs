using System;

namespace ATC8.Cpu
{
    [Flags]
    public enum RegisterName : byte
    {
        // Common
        Ax = 0x00,
        Bx = 0x01,
        Cx = 0x02,
        Dx = 0x03,
        Acx = 0x04,
        Bcx = 0x05,
        /// <summary>
        /// Jump data
        /// </summary>
        Jd = 0x10,
        /// <summary>
        /// Stack pointer
        /// </summary>
        Sp = 0x11,
        /// <summary>
        /// Base pointer
        /// </summary>
        Bp = 0x12,
        /// <summary>
        /// Extra/excess
        /// </summary>
        Ex = 0x13,
        /// <summary>
        /// Interrupt address
        /// </summary>
        Ia = 0x14,
        /// <summary>
        /// Program counter
        /// </summary>
        Pc = 0x15
    }
}