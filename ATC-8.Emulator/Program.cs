namespace ATC8.Emulator
{
    public static class Program
    {
        [System.STAThread]
        static void Main()
        {
            using (var emulator = new Emulator())
                emulator.Run();
        }
    }
}
