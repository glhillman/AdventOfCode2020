using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day06
{
    public class Day06
    {
        public void Part1()
        {
            int anyYesCount = 0;
            int groupYesCount = 0;

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\YesData.txt";

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                TestGroup testGroup = new TestGroup();
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        anyYesCount += testGroup.AnyYesCount;
                        groupYesCount += testGroup.GroupYesCount;
                        testGroup.Init();
                    }
                    else
                    {
                        testGroup.MarkAnswers(line);
                    }

                    if (file.EndOfStream)
                    {
                        anyYesCount += testGroup.AnyYesCount;
                        groupYesCount += testGroup.GroupYesCount;
                    }
                }

                file.Close();
            }

            Console.WriteLine("Part1 - Yes count = {0}", anyYesCount);
            Console.WriteLine("Part2 - Group yes count = {0}", groupYesCount);
        }
    }
}
