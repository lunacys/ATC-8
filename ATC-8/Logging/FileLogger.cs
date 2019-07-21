namespace ATC8.Logging
{
    public class FileLogger : LoggerBase
    {
        public FileLogger(string context) 
            : base(context)
        { }

        protected override void Log(string message)
        {
            throw new System.NotImplementedException();
        }
    }
}