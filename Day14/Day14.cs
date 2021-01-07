using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day14
{
    public class Day14
    {

        public Day14()
        {
            LoadData();
        }

        public void Part1()
        {
            Values = new Dictionary<long, long>();
            string mask = string.Empty;

            for (int i = 0; i < RawData.Count; i++)
            {
                string line = RawData[i];

                if (line.StartsWith("mask"))
                {
                    mask = line.Substring(7);
                }
                else
                {
                    int indexofLeftBracket = line.IndexOf('[') + 1;
                    int indexofRightBracket = line.IndexOf(']');

                    long key = int.Parse(line.Substring(indexofLeftBracket, indexofRightBracket - indexofLeftBracket));
                    long value = long.Parse(line.Substring(indexofRightBracket + 3)); // " = ".Length

                    value = ApplyMask1(mask, value);
                    Values[key] = value;
                }
            }

            long sum = Values.Values.ToList().Sum();

            Console.WriteLine("Part1: {0}", sum);
        }

        public void Part2()
        {
            Values = new Dictionary<long, long>();
            string mask = string.Empty;

            for (int i = 0; i < RawData.Count; i++)
            {
                string line = RawData[i];

                if (line.StartsWith("mask"))
                {
                    mask = line.Substring(7);
                }
                else
                {
                    int indexofLeftBracket = line.IndexOf('[') + 1;
                    int indexofRightBracket = line.IndexOf(']');

                    long key = int.Parse(line.Substring(indexofLeftBracket, indexofRightBracket - indexofLeftBracket));
                    long value = long.Parse(line.Substring(indexofRightBracket + 3)); // " = ".Length

                    ApplyMask2(mask, key, value);
                }
            }

            long sum = Values.Values.ToList().Sum();

            Console.WriteLine("Part2: {0}", sum);
        }

        private long ApplyMask1(string mask, long value)
        {
            long result = value;
            long bit = 1;

            for (int i = mask.Length - 1; i >= 0; i--)
            {
                switch (mask[i])
                {
                    case '1':
                        result |= bit;
                        break;
                    case '0':
                        result &= ~bit;
                        break;
                    default:
                        break;
                }
                bit <<= 1;
            }

            return result;
        }

        private void ApplyMask2(string mask, long keyMaster, long value)
        {
            long bit = 1;
            List<long> floaters = new List<long>();

            for (int i = mask.Length - 1; i >= 0; i--)
            {
                switch (mask[i])
                {
                    case '1':
                        keyMaster |= bit;
                        break;
                    case '0':
                        break;
                    case 'X':
                        floaters.Add(bit);
                        break;
                    default:
                        break;
                }
                bit <<= 1;
            }

            long nCombinations = (int)Math.Pow(2, floaters.Count);

            for (long i = 0; i < nCombinations; i++)
            {
                long key = keyMaster;
                bit = 1;

                for (long j = 0; j < floaters.Count; j++)
                {
                    if ((bit & i) > 0)
                    {
                        key |= floaters[(int)j];
                    }
                    else
                    {
                        key &= ~floaters[(int)j];
                    }
                    bit <<= 1;
                }

                Values[key] = value;
            }
        }
        
        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                RawData = new List<string>();

                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    RawData.Add(line);
                }

                file.Close();
            }
        }

        private List<string> RawData { get; set; }
        private Dictionary<long, long> Values { get; set; }
    }
}
