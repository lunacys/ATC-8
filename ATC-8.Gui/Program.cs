namespace ATC8.Gui
{
    public static class Program
    {
        [System.STAThread]
        private static void Main()
        {
            using (var game = new GameRoot())
                game.Run();
        }
    }
}
