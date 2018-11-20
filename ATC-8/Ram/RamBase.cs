namespace ATC8.Ram
{
    public class RamBase
    {
        public int Size { get; }

        private readonly Word[] _words;

        public Word this[int address]
        {
            get => Get(address);
            set => Set(address, value);
        }

        public RamBase(int size)
        {
            Size = size;
            _words = new Word[size / Word.Size];
        }

        public void ClearAll()
        {
            for (int i = 0; i < _words.Length; i++)
                _words[i] = 0;
        }

        public void Set(int address, Word value)
        {
            _words[address] = value;
        }

        public Word Get(int address)
        {
            return _words[address];
        }
    }
}