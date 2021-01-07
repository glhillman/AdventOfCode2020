using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day12
{
    public class Day12
    {
        
        public Day12()
        {
            LoadData();
        }

        public void Part1()
        {
            Turtle turtle = new Turtle(0, 0);
            foreach (Instruction instruction in Instructions)
            {
                turtle.Cmd1(instruction);
            }

            double rslt = turtle.ManhattanDistance;

            Console.WriteLine("Part1: {0}", rslt);
        }

        public void Part2()
        {
            Turtle turtle = new Turtle(10, 1);
            foreach (Instruction instruction in Instructions)
            {
                turtle.Cmd2(instruction);
            }

            double rslt = turtle.ManhattanDistance;

            Console.WriteLine("Part2: {0}", rslt);
        }

        public List<Instruction> Instructions { get; private set; }

        private void LoadData()
        {

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                Instructions = new List<Instruction>();
                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    Instructions.Add(new Instruction(line));
                }

                file.Close();
            }

        }

    }
}
