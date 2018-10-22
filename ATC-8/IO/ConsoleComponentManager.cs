using System.Collections.Generic;
using System.Linq;

namespace ATC8.IO
{
    public class ConsoleComponentManager : IConsoleComponentManager
    {
        private readonly List<ConsoleComponent> _components;

        public ConsoleComponentManager()
        {
            _components = new List<ConsoleComponent>();
        }

        public void Add(ConsoleComponent component)
        {
            _components.Add(component);
        }

        public T FindComponent<T>() where T : ConsoleComponent
        {
            return _components.OfType<T>().FirstOrDefault();
        }

        public TransferAnswer SendRequest(TransferRequest request)
        {
            var src = request.Source;
            var dst = request.Destination;

            return new TransferAnswer();
        }
    }
}