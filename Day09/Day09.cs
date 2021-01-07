using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day09
{
    public class Day09
    {
        private List<long> _values;

        public Day09()
        {
            LoadData();
            SlidingWindowSize = 25;
        }

        public void Part1()
        {
            int start = 0;
            int end = SlidingWindowSize - 1;
            long target = _values[end + 1];

            while (end < _values.Count - 1)
            {
                bool found = false;

                for (int i = start; i < end && !found; i++)
                {
                    for (int j = i + 1; j <= end && !found; j++)
                    {
                        if (_values[i] + _values[j] == target)
                        {
                            found = true; ;
                        }
                    }
                }
                if (found)
                {
                    start++;
                    end++;
                    target = _values[end + 1];
                }
                else
                {
                    Target = target;
                    break;
                }
            }

            Console.WriteLine("Part1: {0} not found", Target);
        }

        public void Part2()
        {
            int start = 0;
            bool found = false;

            while (!found)
            {
                long sum = 0;
                int index = start;
                
                while (sum < Target)
                {
                    sum += _values[index++];
                }
                
                if (sum == Target)
                {
                    found = true;
                    List<long> range = _values.GetRange(start, index - start);
                    Console.WriteLine("Part2: {0}", range.Min() + range.Max());
                    break;
                }
                else
                {
                    start++;
                }
            }
        }

        private int SlidingWindowSize { get; set; }

        private long Target { get; set; }
        
        private void LoadData()
        {
            _values = new List<long>();

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Data.txt";

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    _values.Add(long.Parse(line));
                }

                file.Close();
            }
        }
    }
}
