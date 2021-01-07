using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day13
{
    public class ValueAndOffset
    {
        public ValueAndOffset(long value, long offset)
        {
            Value = value;
            Offset = offset;
        }

        public long Value { get; private set; }
        public long Offset { get; private set; }
        public override string ToString()
        {
            return string.Format("Value: {0}, Offset: {1}", Value, Offset);
        }
    }

    public class Day13
    {

        public Day13()
        {
            LoadData();
        }

        public void Part1()
        {
            long nearestTime = int.MaxValue;
            long nearestBus = 0;

            for (int b = 0; b < Buses.Count; b++)
            {
                long time = 0;
                while (time < TimeStamp)
                {
                    time += Buses[b].Value;
                }
                long wait = time - TimeStamp;
                if (wait < nearestTime)
                {
                    nearestTime = wait;
                    nearestBus = Buses[b].Value;
                }
            }
            long rslt = nearestTime * nearestBus;

            Console.WriteLine("Part1: {0}", rslt);
        }

        public void Part2()
        {
            long largest = Buses[0].Value;
            long offsetOfLargest = 0;
            long anchorTimeStamp = 0;
            
            for (int i = 1; i < Buses.Count; i++)
            {
                if (Buses[i].Value > largest)
                {
                    largest = Buses[i].Value;
                    offsetOfLargest = Buses[i].Offset;
                }
            }

            long timeStamp = largest;
            bool finished = false;

            while (!finished)
            {
                anchorTimeStamp = timeStamp - offsetOfLargest;

                finished = true;
                for (int i = 0; i < Buses.Count; i++)
                {
                    if ((anchorTimeStamp + Buses[i].Offset) % Buses[i].Value != 0)
                    {
                        finished = false;
                        break;
                    }
                }

                if (timeStamp % 100000000 == 0)
                {
                    Console.WriteLine("{0} So far so good", timeStamp);
                }

                if (!finished)
                {
                    timeStamp += largest;
                }
            }

            Console.WriteLine("Part2: {0}", anchorTimeStamp);
            
        }

        private long TimeStamp { get; set; }
        private List<ValueAndOffset> Buses { get; set; }
        private void LoadData()
        {

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                line = file.ReadLine();
                TimeStamp = int.Parse(line);
                line = file.ReadLine();
                Buses = new List<ValueAndOffset>();
                string[] buses = line.Split(',');
                int offset = 0;
                foreach (string bus in buses)
                {
                    if (bus != "x")
                    {
                        Buses.Add(new ValueAndOffset(int.Parse(bus),offset));
                    }
                    offset++;
                }
                file.Close();
            }

        }

    }
}
