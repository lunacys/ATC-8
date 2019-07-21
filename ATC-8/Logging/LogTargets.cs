using System;

namespace ATC8.Logging
{
    [Flags]
    public enum LogTargets
    {
        None = 0,
        Console = 1,
        File = 2
    }
}