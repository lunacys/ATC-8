using System;
using System.IO;

namespace ATC8.VirtualMachine
{
    public class InputStream : IDisposable
    {
        /// <summary>
        /// Gets or sets current line within the stream.
        /// </summary>
        public int Line { get; private set; } // TODO: Make it dependent to the Column and Position props

        /// <summary>
        /// Gets or sets current column within the stream.
        /// </summary>
        public int Column { get; private set; } // TODO: Make it dependent to the Line and Position props

        private StringReader _reader;

        private string _content;

        public bool EndOfStream => Peek() == 0xFFFF;

        public InputStream(string filename)
            : this(new StreamReader(filename))
        { }

        public InputStream(Stream stream)
            : this(new StreamReader(stream))
        { }

        public InputStream(StreamReader streamReader)
        {
            _content = streamReader.ReadToEnd();
            Line = 1;
            Column = 0;
            _reader = new StringReader(_content);
        }

        public InputStream(StringReader stringReader)
        {
            _reader = stringReader;
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

            var ch = (char) _reader.Read();
            
            if (ch == '\n' || ch == '\r')
            {
                Line++;
                Column = 0;
            }
            else
            {
                Column++;
            }

            return ch;
        }

        /// <summary>
        /// Returns the next available character but does not consume it.
        /// </summary>
        /// <returns>A character was peeked.</returns>
        public char Peek()
        {
            return (char)_reader.Peek();
        }

        public void Dispose()
        {
            _reader.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}