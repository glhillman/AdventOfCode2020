using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    public class Day19
    {
        private List<string> Messages;
        private Rule[] Rules;
        public Day19()
        {
            Messages = new List<string>();
            List<Rule> ruleList = new List<Rule>();

            LoadData(ruleList);

            ruleList.Sort((r1, r2) => r1.ID < r2.ID ? -1 : r1.ID > r2.ID ? 1 : 0);

            Rules = ruleList.ToArray();
        }

        public void Part1()
        {
            int nMatch = 0;
            List<int> testRules = new List<int>();
            foreach (string message in Messages)
            {
                testRules.Clear();
                testRules.Add(0);
                nMatch += IsMatch(message, testRules) ? 1 : 0;
            }

            Console.WriteLine("Part1: {0}", nMatch);
        }

        public void Part2()
        {
            List<int> ruleSet = new List<int>();
            ruleSet.Add(42);
            ruleSet.Add(8);
            Rules[8].RuleSets.Add(ruleSet);
            ruleSet = new List<int>();
            ruleSet.Add(42);
            ruleSet.Add(11);
            ruleSet.Add(31);
            Rules[11].RuleSets.Add(ruleSet);

            int nMatch = 0;
            List<int> testRules = new List<int>();
            foreach (string message in Messages)
            {
                testRules.Clear();
                testRules.Add(0);
                nMatch += IsMatch(message, testRules) ? 1 : 0;
            }

            Console.WriteLine("Part2: {0}", nMatch);
        }


        private bool IsMatch(string message, List<int> testRules)
        {
            if (message == string.Empty && testRules.Count == 0)
            {
                return true; // match!
            }
            else if ((message == string.Empty && testRules.Count > 0) ||
                     (message.Length > 0 && testRules.Count == 0))
            {
                return false; // no match
            }
            else if (Rules[testRules[0]].Value == ' ')
            {
                foreach (List<int> ruleSet in Rules[testRules[0]].RuleSets)
                {
                    List<int> newTestRules = new List<int>();
                    foreach (int id in ruleSet)
                    {
                        newTestRules.Add(id);
                    }
                    newTestRules.AddRange(testRules.GetRange(1, testRules.Count - 1));
                    if (IsMatch(message, newTestRules))
                    {
                        return true;
                    }
                }
                return false;
            }
            else if (Rules[testRules[0]].Value == message[0] )
            {
                return IsMatch(message.Substring(1), testRules.GetRange(1, testRules.Count - 1));
            }
            else
            {
                return false;
            }
        }
        private void LoadData(List<Rule> ruleList)
        {

            string inputFile = @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                Rule rule;
                bool messages = false;

                while ((line = file.ReadLine()) != null)
                {
                    if (messages)
                    {
                        Messages.Add(line);
                    }
                    else
                    {
                        if (line.Length > 1)
                        {
                            List<List<int>> ruleSets = new List<List<int>>();

                            int index = line.IndexOf(':');
                            int id = int.Parse(line.Substring(0, index));

                            string str = line.Substring(index + 1).Trim();
                            if (str[0] != '"')
                            {
                                List<int> ruleSet = new List<int>();

                                string[] options = str.Split('|');
                                string[] left = options[0].Trim().Split(' ');
                                foreach (string ruleId in left)
                                {
                                    ruleSet.Add(int.Parse(ruleId));
                                }
                                ruleSets.Add(ruleSet);

                                if (options.Length > 1)
                                {
                                    string[] right = options[1].Trim().Split(' ');
                                    ruleSet = new List<int>();
                                    foreach (string ruleId in right)
                                    {
                                        ruleSet.Add(int.Parse(ruleId));
                                    }
                                    ruleSets.Add(ruleSet);
                                }

                                rule = new Rule(id, ruleSets);
                            }
                            else
                            {
                                rule = new Rule(id, str.Substring(0).Replace("\"", "")[0]);
                            }
                            ruleList.Add(rule);
                        }
                        else
                        {
                            messages = true;
                        }
                    }
                }

                file.Close();
            }

        }

    }
}
