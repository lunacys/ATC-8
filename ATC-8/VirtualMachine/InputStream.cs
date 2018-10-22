using System;
using System.IO;

namespace ATC8.VirtualMachine
{
    public class InputStream : IDisposable
    {
        /// <summary>
        /// Gets or sets current position within the stream.
        /// </summary>
        public long Position // TODO: Make it dependent to the Line and Column props
        {
            get { return Reader.BaseStream.Position; }
            private set { Reader.BaseStream.Position = value; }
        }

        /// <summary>
        /// Gets or sets current line within the stream.
        /// </summary>
        public int Line { get; private set; } // TODO: Make it dependent to the Column and Position props

        /// <summary>
        /// Gets or sets current column within the stream.
        /// </summary>
        public int Column { get; private set; } // TODO: Make it dependent to the Line and Position props

        public StreamReader Reader { get; }

        public bool EndOfStream => Reader.EndOfStream;

        public InputStream(string filename)
            : this(new StreamReader(filename))
        { }

        public InputStream(Stream stream)
            : this(new StreamReader(stream))
        { }

        public InputStream(StreamReader streamReader)
        {
            Reader = streamReader;
            Line = 1;
            Column = 0;
        }

        /// <summary>
        /// Reads the next character from the input stream and advances the character position by one character.
        /// </summary>
        /// <returns>A character was read.</returns>
        public char Read()
        {
            if (EndOfStream) return '\0';
            return (char)Reader.Read();
        }

        /// <summary>
        /// Returns the next available character but does not consume it.
        /// </summary>
        /// <returns>A character was peeked.</returns>
        public char Peek()
        {
            if (EndOfStream) return '\0';
            return (char)Reader.Peek();
        }

        public void Dispose()
        {
            Reader.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}