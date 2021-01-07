using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day15
{
    public class Day15
    {
        public int[] _input = { 6, 4, 12, 1, 20, 0, 16 };
        public int[] _test = { 0, 3, 6 };
        public Dictionary<int, int> Referenced;

        public Day15()
        {
        }

        public void Part1()
        {
            SeedSequence(_input);

            int rtrn = NthSpoken(2020);

            Console.WriteLine("Part1: {0}", rtrn);
        }

        public void Part2()
        {
            SeedSequence(_input);

            int rtrn = NthSpoken(30000000);

            Console.WriteLine("Part2: {0}", rtrn);

            for (int i = 0; i < 30000000; i++)
            {

            }
        }

        private int NthSpoken(int n)
        {
            int currCount = SeedLength;
            int previous;
            bool lastIsUnique = true;

            while (currCount < n)
            {
                if (lastIsUnique)
                {
                    LastSpoken = 0;
                }
                else
                {
                    previous = Referenced[LastSpoken];
                    int diff = currCount - previous;
                    Referenced[LastSpoken] = currCount;
                    LastSpoken = diff;
                }

                lastIsUnique = Referenced.ContainsKey(LastSpoken) == false;
                if (lastIsUnique)
                {
                    Referenced[LastSpoken] = currCount + 1;
                }

                currCount++;
            }

            return LastSpoken;
        }

        private void SeedSequence(int[] seed)
        {
            Referenced = new Dictionary<int, int>();

            for (int i = 0; i < seed.Length; i++)
            {
                Referenced[seed[i]] = i+1;
                LastSpoken = seed[i];
            }

            SeedLength = seed.Length;
        }

        private int LastSpoken { get; set; }
        private int SeedLength { get; set; }

    }
}
