using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day03
{
    public class Day03
    {
        public List<string> stringMap;
        public void Puzzel1()
        {
            LoadStringMap(3);

            UInt64 treeCount = TraverseMap(3, 1);

            Console.WriteLine("Puzzel1 tree count = {0}", treeCount);
        }

        public void Puzzel2()
        {
            LoadStringMap(7);

            UInt64 treeCount = TraverseMap(1, 1);
            treeCount *= TraverseMap(3, 1);
            treeCount *= TraverseMap(5, 1);
            treeCount *= TraverseMap(7, 1);
            treeCount *= TraverseMap(1, 2);

            Console.WriteLine("Puzzel2 tree count = {0}", treeCount);
        }

        private UInt64 TraverseMap(int right, int down)
        {
            UInt64 treeCount = 0;
            int rightIndex = right;
            int downIndex = down;

            while (downIndex < stringMap.Count)
            {
                if (stringMap[downIndex][rightIndex] == '#')
                {
                    treeCount++;
                }
                rightIndex += right;
                downIndex += down;
            }

            return treeCount;
        }

        public void LoadStringMap(int extendBy)
        {
            int nthString = 1;

            stringMap = new List<string>();

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Day03TreeMap.txt";
            
            if (File.Exists(inputFile))
            {
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    // each succeeding line will need to be <extendBy> places longer than the previous string - after reaching its end
                    // do that extension now to make traversing easy. Add on in full chunks
                    int targetLength = extendBy * nthString++ + 2;
                    StringBuilder sb = new StringBuilder(line);
                    while (sb.Length < targetLength)
                    {
                        sb.Append(line);
                    }
                    stringMap.Add(sb.ToString());
                }

                file.Close();
            }
        }
    }
}
