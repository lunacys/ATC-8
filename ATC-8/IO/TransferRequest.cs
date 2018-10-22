namespace ATC8.IO
{
    public class TransferRequest
    {
        public ConsoleComponent Source { get; set; }
        public ConsoleComponent Destination { get; set; }

        public byte[] Message { get; set; }

        public TransferRequest()
        {
            
        }
    }
}