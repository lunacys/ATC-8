using System;

namespace ATC8.Logging
{
    public sealed class ConsoleLogger : LoggerBase
    {
        private object _lockObject = new object();

        public ConsoleLogger(string context) 
            : base(context)
        { }

        protected override void Log(string message)
        {
            lock (message)
            {
                Console.WriteLine($"[{Context}]: {message}");
            }
        }

        public override void Debug(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            base.Debug(message);

            Console.ResetColor();
        }

        public override void Info(string message)
        {
            base.Info(message);
        }

        public override void Warning(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;

            base.Warning(message);

            Console.ResetColor();
        }

        public override void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;

            base.Error(message);

            Console.ResetColor();
        }
    }
}