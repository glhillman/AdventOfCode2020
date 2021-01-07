using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day05
{
    public class Day05
    {
        private List<string> _seats;
        public Day05()
        {
            ReadSeatingData();

        }
        public void Puzzle1()
        {
            int maxSeatID = int.MinValue;

            foreach (string seating in _seats)
            {
                int seatID = BinaryPartition(seating.Substring(0, 7), 128, 'F') * 8 + BinaryPartition(seating.Substring(7), 8, 'L');
                maxSeatID = Math.Max(seatID, maxSeatID);
            }

            Console.WriteLine("Puzzle1 Max SeatID: {0}", maxSeatID);
        }

        public void Puzzle2()
        {
            bool[] occupied = Enumerable.Repeat(false, 128 * 8).ToArray();

            foreach (string seating in _seats)
            {
                int seatID = BinaryPartition(seating.Substring(0, 7), 128, 'F') * 8 + BinaryPartition(seating.Substring(7), 8, 'L');
                occupied[seatID] = true;
            }

            // skip over the empy seats at the front
            int index = 0;
            while (index < occupied.Length && occupied[index++] == false)
            { }
            
            while (index < occupied.Length && occupied[index++])
            { }
            index--;

            Console.WriteLine("Puzzle2 My seatID: {0}", index);
        }

        private int BinaryPartition(string directions, int max, char lowEnd)
        {
            int start = 1;
            int end = max;

            for (int i = 0; i < directions.Length; i++)
            {
                if (directions[i] == lowEnd)
                {
                    end -= (end - start + 1) / 2;
                }
                else
                {
                    start += (end - start + 1) / 2;
                }
            }
            return Math.Min(start, end) - 1;
        }
        
        private void ReadSeatingData()
        {
            _seats = new List<string>();

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\SeatingData.txt";

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    _seats.Add(line);
                }

                file.Close();
            }

        }
    }
}
