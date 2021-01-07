using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    class TestGroup
    {
        private int[] _yesAnswers;
        private int _groupSizeCount;

        public  TestGroup()
        {
            _yesAnswers = new int[26];
            Init();
        }

        public void MarkAnswers(string yeses)
        {
            _groupSizeCount++;
            foreach (char c in yeses)
            {
                int index = c - (int)'a';
                _yesAnswers[index] += 1;
            }
        }

        public int AnyYesCount
        {
            get
            {
                return _yesAnswers.Count(y => y > 0);
            }
        }

        public int GroupYesCount
        {
            get
            {
                return _yesAnswers.Count(y => y == _groupSizeCount);
            }
        }

        public void Init()
        {
            for (int i = 0; i < _yesAnswers.Length; i++)
            {
                _yesAnswers[i] = 0;
            }
            _groupSizeCount = 0;
        }
    }
}
