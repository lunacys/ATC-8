namespace ATC8.Emulator.Screens
{
    public interface IScreenManager
    {
        T FindScreen<T>() where T : Screen;
    }
}