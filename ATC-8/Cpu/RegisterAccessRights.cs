using System;

namespace ATC8.Cpu
{
    [Flags]
    public enum RegisterAccessRights
    {
        None = 0,
        Read = 1,
        Write = 2
    }
}