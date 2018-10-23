using System;

namespace ATC8.Cpu
{
    [Flags]
    public enum RegisterName
    {
        /// <summary>
        /// Accumulator register
        /// </summary>
        Ax = 0,
        /// <summary>
        /// Base register
        /// </summary>
        Bx = 1,
        /// <summary>
        /// Counter register
        /// </summary>
        Cx = 2,
        /// <summary>
        /// Data register
        /// </summary>
        Dx = 4,
        /// <summary>
        /// Source index register
        /// </summary>
        Si = 8,
        /// <summary>
        /// Destination index register
        /// </summary>
        Di = 16,
        /// <summary>
        /// Stack pointer register
        /// </summary>
        Sp = 32,
        /// <summary>
        /// Base pointer register
        /// </summary>
        Bp = 64,
        /// <summary>
        /// Keys pressed
        /// </summary>
        Kp = 128,
        /// <summary>
        /// Keys unpressed
        /// </summary>
        Ku = 256
    }
}