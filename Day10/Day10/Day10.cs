using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    public class Day10
    {
        List<int> _data;

        public Day10()
        {
            LoadData();
            _data.Sort();
        }

        public void Part1()
        {
            long[] gaps = new long[5];

            // count the gaps
            int diff = _data[0];
            if (diff < 4)
            {
                gaps[diff]++; // plugged into wall
            }
            for (int i = 1; i < _data.Count; i++)
            {
                diff = _data[i] - _data[i - 1];
                if (diff < 4)
                {
                    gaps[diff]++; // [0] will count identical numbers
                }
                else
                {
                    gaps[4]++; // look for leaps too far - should be 0 
                }
            }
            gaps[3]++; // my adapter

            long rslt = gaps[1] * gaps[3];

            Console.WriteLine("Part1: {0}", rslt);
        }

        public void Part2()
        {
            // solution from Jeff. He doesn't know why it works either.

            int finalValue = _data[_data.Count - 1];
            long[] factors = new long[finalValue+1];

            // for each adapter, look back up to 3 times and accumulate the number of jumps
            factors[0] = 1;
            for (int i=0; i <= _data.Count-1; i++)
            {
                int adapter = _data[i];

                for (int j = 1; j <= 3; j++)
                {
                    if (adapter - j >= 0) // less than start
                    {
                        // the adapter at _data[i-j] is within the required 3 steps of the current one
                        // accumulate the factors from as far back (within 3) as we have
                        factors[adapter] += factors[adapter - j];
                    }    
                }
            }

            long rslt = factors[factors.Length - 1];
            Console.WriteLine("Part2: {0}", factors[factors.Length-1]);

            Console.WriteLine("Part2: {0}", rslt);
        }

        
        private void LoadData()
        {
            _data = new List<int>();

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Input.txt";

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    _data.Add(int.Parse(line));
                }

                file.Close();
            }

        }

    }
}
