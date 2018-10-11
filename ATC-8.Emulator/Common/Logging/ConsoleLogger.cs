﻿using System.Threading.Tasks;

namespace ATC8.Emulator.Common.Logging
{
    public class ConsoleLogger : Logger
    {
        public override void Log(string message)
        {
            lock (LockObject)
            {
                System.Console.WriteLine(message);
            }
        }

        public override async Task LogAsync(string message)
        {
            await Task.Run(() => System.Console.WriteLine(message));
        }
    }
}
