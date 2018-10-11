using System;
using System.IO;

namespace ATC8.Emulator.Common.Logging
{
    // TODO: https://stackoverflow.com/questions/3261451/using-a-bitmask-in-c-sharp
    [Flags]
    public enum LogTarget
    {
        /// <summary>
        /// Console target provided by <see cref="Console"/> class
        /// </summary>
        Console = 1,
        /// <summary>
        /// File target provided by <see cref="FileStream"/> class
        /// </summary>
        File = 2
    }
}

