using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    public class RuleRange
    {
        public RuleRange(string name, int min1, int max1, int min2, int max2)
        {
            Name = name;
            Min1 = min1;
            Max1 = max1;
            Min2 = min2;
            Max2 = max2;
        }

        public bool MatchOK(int value)
        {
            return ((value >= Min1 && value <= Max1) || (value >= Min2 && value <= Max2 ));
        }

        public override string ToString()
        {
            return string.Format("{0}: {1}-{2} or {3}-{4}", Name, Min1, Max1, Min2, Max2);
        }
        public string Name { get; private set; }
        public int Min1 { get; private set; }
        public int Max1 { get; private set; }
        public int Min2 { get; private set; }
        public int Max2 { get; private set; }
    }
}
