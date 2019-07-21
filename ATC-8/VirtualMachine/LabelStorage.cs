using System.Collections.Generic;

namespace ATC8.VirtualMachine
{
    public class LabelStorage
    {
        private readonly Dictionary<string, int> _labelDictionary;

        public int this[string labelName]
        {
            get => _labelDictionary[labelName];
            set => _labelDictionary[labelName] = value;
        }

        public LabelStorage()
        {
            _labelDictionary = new Dictionary<string, int>();
        }

        public void Add(string labelName, int position)
        {
            _labelDictionary[labelName] = position;
        }

        public int Get(string labelName)
        {
            return _labelDictionary[labelName];
        }
    }
}