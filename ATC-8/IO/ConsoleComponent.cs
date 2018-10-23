using System;

namespace ATC8.IO
{
    public abstract class ConsoleComponent
    {
        public Bus Bus { get; }
        public IConsoleComponentManager ComponentManager { get; internal set; }

        public event EventHandler OnRequest;

        public ConsoleComponent(Bus bus)
        {
            Bus = bus;
        }

        public TransferAnswer SendRequest(TransferRequest request)
        {
            var src = request.Source;
            var dst = request.Destination;

            OnRequest?.Invoke(this, EventArgs.Empty);

            return new TransferAnswer();
        }
    }
}