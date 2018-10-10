namespace ATC8.VirtualMachine
{
    public enum Instructions : byte
    {
        SetHealth = 0x00,
        SetWisdom = 0x01,
        SetAgility = 0x02,
        PlaySound = 0x03,
        SpawnParticles = 0x04,
        Literal = 0x05,
        Addition = 0x06,
        GetHealth = 0x07,
        GetWisdom = 0x08,
        GetAgility = 0x09,
        Print = 0x0A
    }
}