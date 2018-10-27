namespace ATC8.Cartridge
{
    public class CartridgeBase
    {
        public static int MaxSpriteStorageSize => 16384; //16 * 1024;
        public static int MaxDataStorageSize => 16384; //16 * 1024;
        public static int MaxSize => MaxSpriteStorageSize + MaxDataStorageSize;

        private Word[] _data;
        private int _offset;

        public CartridgeBase()
        {
            _data = new Word[MaxSize];
            _offset = 0;
        }
    }
}