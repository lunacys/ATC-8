using System.Collections.Generic;

namespace ATC8.Logging
{
    public static class LoggerFactory
    {
        private static Dictionary<string, LoggerBase> _loggerDictionary = new Dictionary<string, LoggerBase>();

        private static LogTargets _targets = LogTargets.None;
        public static LogTargets Targets
        {
            get { return _targets; }
            set { _targets = value; }
        }

        // TODO: Implement multiple log targets at the same time
        // TODO: Add log levels
        public static LoggerBase Get(string context)
        {
            if (!_loggerDictionary.ContainsKey(context))
            {
                _loggerDictionary[context] = new ConsoleLogger(context);
            }

            return _loggerDictionary[context];
        }
    }
}