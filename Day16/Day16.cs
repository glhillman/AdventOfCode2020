using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day16
{
    public class Day16
    {
        private List<RuleRange> Rules;
        private List<List<int>> Tickets { get; set; }
        private List<int> YourTicket { get; set; }


        public Day16()
        {
            Rules = new List<RuleRange>();
            Tickets = new List<List<int>>();
            LoadData();
        }

        public void Part1()
        {
            int invalidSum = 0;
            int index = 0;

            while (index < Tickets.Count)
            {
                int nInvalid = NInvalidFields(Tickets[index]);
                if (nInvalid > 0)
                {
                    invalidSum += nInvalid;
                    Tickets.RemoveAt(index); // remove invalid tickets as part of Part2
                }
                else
                {
                    index++;
                }
            }

            Console.WriteLine("Part1: {0}", invalidSum);
        }

        public void Part2()
        {
            List<bool[,]> matrixes = new List<bool[,]>();
            int nFields = Tickets[0].Count;
            int nRules = Rules.Count;

            for (int ticketIndex = 0; ticketIndex < Tickets.Count; ticketIndex++)
            {
                bool[,] matrix = new bool[Rules.Count, nFields];
                for (int ruleIndex = 0; ruleIndex < Rules.Count; ruleIndex++)
                {
                    for (int fieldIndex = 0; fieldIndex < nFields; fieldIndex++)
                    {
                        matrix[ruleIndex, fieldIndex] = Rules[ruleIndex].MatchOK(Tickets[ticketIndex][fieldIndex]);
                    }
                }
                matrixes.Add(matrix);
            }

            bool?[,] combined = new bool?[nRules, nFields];
            for (int i = 0; i < nRules; i++)
                for (int j = 0; j < nFields; j++)
                    combined[i, j] = true;

            for (int m = 0; m < matrixes.Count; m++)
            {
                for (int i = 0; i < nRules; i++)
                    for (int j = 0; j < nFields; j++)
                        combined[i, j] &= matrixes[m][i,j];
            }

            // combined matrix has fields going across & rules going down
            // look for a column (field) with only one true - that is the field for that rule.
            // match it up, and null out all the other columns for that rule, then repeat
            Stack<int> trueRules = new Stack<int>();
            int[] identifiedFields = new int[nFields];
            for (int i = 0; i < nFields; i++)
            {
                identifiedFields[i] = -1;
            }

            for (int fieldIndex = 0; fieldIndex < nFields; fieldIndex++)
            {
                if (identifiedFields[fieldIndex] == -1)
                {
                    trueRules.Clear();
                    for (int ruleIndex = 0; ruleIndex < nRules; ruleIndex++)
                    {
                        if (combined[ruleIndex, fieldIndex].HasValue)
                        {
                            if (combined[ruleIndex, fieldIndex].Value == true)
                            {
                                trueRules.Push(ruleIndex);
                            }
                        }
                    }
                    if (trueRules.Count == 1)
                    {
                        // mark the field that follows the rule
                        int rule = trueRules.Pop();
                        identifiedFields[fieldIndex] = rule;
                        // null out the other fields for that rule
                        for (int iField = 0; iField < nFields; iField++)
                        {
                            combined[rule, iField] = null;
                        }
                        // start over 
                        fieldIndex = -1;
                    }
                }
            }

            // at this point, identifiedFields[i] = nth Rule
            for (int i = 0; i < identifiedFields.Length; i++)
            {
                Console.WriteLine("Field {0} is rule {1}", i, Rules[identifiedFields[i]].Name);
            }

            long rslt = 1;
            for (int i = 0; i < identifiedFields.Length; i++)
            {
                if (Rules[identifiedFields[i]].Name.StartsWith("departure"))
                {
                    rslt *= YourTicket[i];
                }
            }

            Console.WriteLine("Part2: {0}", rslt);

        }

        private int NInvalidFields(List<int> ticket)
        {
            bool matchOK;
            int invalidSum = 0;

            foreach (int value in ticket)
            {
                matchOK = false;
                foreach (RuleRange rule in Rules)
                {
                    matchOK |= rule.MatchOK(value);
                    if (matchOK)
                    {
                        break;
                    }
                }
                if (matchOK == false)
                {
                    invalidSum += value;
                }
            }

            return invalidSum;
        }

        private void ParseRule(string rule)
        {
            char[] delimiters = { '-', ' ', 'o', 'r' };
            string name = rule.Substring(0, rule.IndexOf(':'));
            string[] ranges = rule.Substring(rule.IndexOf(':') + 2).Split(delimiters);

            Rules.Add(new RuleRange(name, int.Parse(ranges[0]), int.Parse(ranges[1]), int.Parse(ranges[5]), int.Parse(ranges[6])));
        }

        private List<int> ParseTicket(string strValues)
        {
            List<int> ticket = new List<int>();

            string[] values = strValues.Split(',');
            foreach (string value in values)
            {
                ticket.Add(int.Parse(value));
            }

            return ticket;
        }

        private void LoadData()
        {

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                // read the rules first
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length > 1)
                    {
                        ParseRule(line);
                    }
                    else
                    {
                        break;
                    }
                }
                line = file.ReadLine();
                line = file.ReadLine();
                YourTicket = ParseTicket(line);
                line = file.ReadLine(); // blank
                line = file.ReadLine(); // "nearby tickets"
                while ((line = file.ReadLine()) != null)
                {
                    Tickets.Add(ParseTicket(line));
                }

                file.Close();
            }

        }

    }
}
