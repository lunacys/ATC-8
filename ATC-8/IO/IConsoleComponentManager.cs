namespace ATC8.IO
{
    public interface IConsoleComponentManager
    {
        T FindComponent<T>() where T : ConsoleComponent;
    }
}