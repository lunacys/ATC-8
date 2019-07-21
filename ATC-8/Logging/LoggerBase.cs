namespace ATC8.Logging
{
    public abstract class LoggerBase
    {
        protected string Context { get; }

        protected LoggerBase(string context)
        {
            Context = context;
        }

        protected abstract void Log(string message);

        public virtual void Info(string message)
        {
            Log(message);
        }

        public virtual void Debug(string message)
        {
            Log(message);
        }

        public virtual void Warning(string message)
        {
            Log(message);
        }

        public virtual void Error(string message)
        {
            Log(message);
        }

        public void Info(object message)
        {
            Info(message.ToString());
        }

        public void Debug(object message)
        {
            Debug(message.ToString());
        }

        public void Warning(object message)
        {
            Warning(message.ToString());
        }

        public void Error(object message)
        {
            Error(message.ToString());
        }
    }
}