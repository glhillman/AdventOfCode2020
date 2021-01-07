using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    public class Password
    {
        public Password(int min, int max, char required, string pwd)
        {
            MinOccurance = min;
            MaxOccurance = max;
            RequiredChar = required;
            Pwd = pwd;
        }
        public int MinOccurance { get; private set; }
        public int MaxOccurance { get; private set; }
        public char RequiredChar { get; private set; }
        public string Pwd { get; private set; }
    }
    class Day02
    {
        static void Main(string[] args)
        {
            ProcessPasswords pgm = new ProcessPasswords();
            
            List<Password> passwords = pgm.LoadPasswords();
            int validCount1 = 0;
            int validCount2 = 0;

            foreach (Password pwd in passwords)
            {
                // process test 1
                int count = pwd.Pwd.Count(c => c == pwd.RequiredChar);
                if (count >= pwd.MinOccurance && count <= pwd.MaxOccurance)
                {
                    validCount1++;
                }

                // process test 2
                if (pwd.Pwd.Length >= pwd.MaxOccurance)
                {
                    bool first = pwd.Pwd[pwd.MinOccurance - 1] == pwd.RequiredChar;
                    bool second = pwd.Pwd[pwd.MaxOccurance - 1] == pwd.RequiredChar;
                    // xor to ensure only one is true
                    validCount2 += (first ^ second) ? 1 : 0;
                }
            }

            Console.WriteLine("Valid count1 = {0}", validCount1);
            Console.WriteLine("Valid count2 = {0}", validCount2);
        }
    }

    public class ProcessPasswords
    {
        public List<Password> LoadPasswords()
        {
            List<Password> passwords = new List<Password>();

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Day02Passwords.txt";

            if (File.Exists(inputFile))
            {
                string line;
                System.IO.StreamReader file = new System.IO.StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ');
                    if (parts.Length == 3)
                    {
                        string[] subParts = parts[0].Split('-');
                        int min = int.Parse(subParts[0]);
                        int max = int.Parse(subParts[1]);

                        char req = parts[1][0];

                        passwords.Add(new Password(min, max, req, parts[2]));
                    }
                }

                file.Close();
            }

            return passwords;
        }

    }

}
