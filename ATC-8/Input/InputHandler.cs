using System;
using ATC8.IO;

namespace ATC8.Input
{
    public class InputHandler : ConsoleComponent
    {
        private readonly Func<GamepadButtons, bool> _buttonHandler;

        public InputHandler(Bus bus, Func<GamepadButtons, bool> buttonHandler)
            : base(bus)
        {
            _buttonHandler = buttonHandler;
        }

        /*public TransferAnswer Send(TransferRequest request)
        {

        }*/
    }
}