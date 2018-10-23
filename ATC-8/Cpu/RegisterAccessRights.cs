using System;

namespace ATC8.Cpu
{
    [Flags]
    public enum RegisterAccessRights
    {
        Read = 0,
        Write = 1
    }
}