using ATC8.IO;

namespace ATC8.Input
{
    public class InputHandler
    {
        public byte PressedKeys { get; private set; }
        public byte UnpressedKeys { get; private set; }

        public InputHandler(Bus bus)
        {
            PressedKeys = 0x00;
            UnpressedKeys = 0xFF;
        }

        public void PressButton(GamepadButtons button)
        {
            PressedKeys |= (byte) button;
        }
    }
}