namespace ATC8.VirtualMachine
{
    public enum Instructions : byte
    {
        // Commands
        Move = 0x00,
        Exchange = 0x01,
        Addition = 0x02,
        Subtraction = 0x03,
        Multiplying = 0x04,
        Division = 0x05,
        Incrementation = 0x06,
        Directive = 0x07,

        // Operations
        SetHealth = 0x10,
        SetWisdom = 0x11,
        SetAgility = 0x12,
        PlaySound = 0x13,
        SpawnParticles = 0x14,
        GetHealth = 0x15,
        GetWisdom = 0x16,
        GetAgility = 0x17,
        Print = 0x18
    }
}