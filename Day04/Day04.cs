using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    public class Day04
    {
        public void Puzzel1()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\PassportData.txt";

            if (File.Exists(inputFile))
            {
                string line;
                bool passPortComplete = false;
                int validPassports = 0;
                bool byr = false;
                bool iyr = false;
                bool eyr = false;
                bool hgt = false;
                bool hcl = false;
                bool ecl = false;
                bool pid = false;

                System.IO.StreamReader file = new System.IO.StreamReader(inputFile);

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        validPassports += passPortComplete ? 1 : 0;
                        passPortComplete = false;
                        byr = false;
                        iyr = false;
                        eyr = false;
                        hgt = false;
                        hcl = false;
                        ecl = false;
                        pid = false;
                    }
                    else
                    {
                        if (line.Contains("byr:"))
                            byr = true;
                        if (line.Contains("iyr:"))
                            iyr = true;
                        if (line.Contains("eyr:"))
                            eyr = true;
                        if (line.Contains("hgt:"))
                            hgt = true;
                        if (line.Contains("hcl:"))
                            hcl = true;
                        if (line.Contains("ecl:"))
                            ecl = true;
                        if (line.Contains("pid:"))
                            pid = true;

                        passPortComplete = byr && iyr && eyr && hgt && hcl && ecl && pid;

                        if (file.EndOfStream)
                        {
                            validPassports += passPortComplete ? 1 : 0;
                        }
                    }

                }

                file.Close();

                Console.WriteLine("Puzzel1 - Valid Passports = {0}", validPassports);
            }

        }

        public void Puzzle2()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\PassportData.txt";

            if (File.Exists(inputFile))
            {
                string line;
                int validPassports = 0;
                Passport passport = new Passport(); ;

                System.IO.StreamReader file = new System.IO.StreamReader(inputFile);

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        validPassports += passport.IsValid() ? 1 : 0;
                        passport = new Passport();
                    }
                    else
                    {
                        passport.ProcessInput(line);

                        if (file.EndOfStream)
                        {
                            validPassports += passport.IsValid() ? 1 : 0;
                        }
                    }

                }

                file.Close();

                Console.WriteLine("Puzzel2 - Valid Passports = {0}", validPassports);
            }

        }
    }
}
