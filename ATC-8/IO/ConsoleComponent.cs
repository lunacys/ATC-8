namespace ATC8.IO
{
    public abstract class ConsoleComponent
    {
        protected Bus Bus { get; }
        protected IConsoleComponentManager ComponentManager;

        public ConsoleComponent(Bus bus)
        {
            Bus = bus;
        }

        
    }
}