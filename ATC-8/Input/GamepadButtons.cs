using System;

namespace ATC8.Input
{
    [Flags]
    public enum GamepadButtons : byte
    {
        A      = 0b0000000, // 0
        B      = 0b0000001, // 1
        Up     = 0b0000010, // 2
        Right  = 0b0000100, // 4
        Down   = 0b0001000, // 8
        Left   = 0b0010000, // 16
        Start  = 0b0100000, // 32
        Reset  = 0b1000000  // 64
    }
}