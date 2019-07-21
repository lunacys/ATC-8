using System.Collections.Generic;

namespace ATC8.VirtualMachine
{
    public static class QueueExtensions
    {
        public static void EnqueueStringAsWords(this Queue<Word> queue, string str)
        {
            var words = str.ToWordArray();

            foreach (var word in words)
            {
                queue.Enqueue(word);
            }
        }

        public static void EnqueueWordArray(this Queue<Word> queue, Word[] array)
        {
            foreach (var word in array)
            {
                queue.Enqueue(word);
            }
        }
    }
}