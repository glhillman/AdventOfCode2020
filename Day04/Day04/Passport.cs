using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Day04
{
    public class Passport
    {

        public Passport()
        {
            ByrIsValid = false;
            IyrIsValid = false;
            EyrIsValid = false;
            HgtIsValid = false;
            HclIsValid = false;
            EclIsValid = false;
            PidIsValid = false;
        }

        public void ProcessInput(string inputStr)
        {
            string[] pairs = inputStr.Split(' ');

            foreach (string pair in pairs)
            {
                if (pair.Contains(":") && pair.Length > 4)
                {
                    string valueStr = pair.Substring(4);

                    switch (pair.Substring(0, 4))
                    {
                        case "byr:":
                            ByrIsValid = ValidIntRange(valueStr, 1920, 2002);
                            break;
                        case "iyr:":
                            IyrIsValid = ValidIntRange(valueStr, 2010, 2020);
                            break;
                        case "eyr:":
                            EyrIsValid = ValidIntRange(valueStr, 2020, 2030);
                            break;
                        case "hgt:":
                            HgtIsValid = ValidHeight(valueStr);
                            break;
                        case "hcl:":
                            HclIsValid = ValidHairColor(valueStr);
                            break;
                        case "ecl:":
                            EclIsValid = ValidEyeColor(valueStr);
                            break;
                        case "pid:":
                            PidIsValid = ValidPassportID(valueStr);
                            break;
                        case "cid:":
                            // ignored
                            break;
                    }
                }
            }
        }

        public bool IsValid()
        {
            return ByrIsValid && IyrIsValid && EyrIsValid && HgtIsValid && HclIsValid && EclIsValid && PidIsValid;
        }
        private bool ValidIntRange(string valueStr, int min, int max)
        {
            int value;
            bool valid = false;

            if (int.TryParse(valueStr, out value))
            {
                valid = value >= min && value <= max;    
            }

            return valid;
        }

        private bool ValidHeight(string valueStr)
        {
            bool valid = false;

            if (valueStr.Length >= 4 && (valueStr.EndsWith("cm") || valueStr.EndsWith("in")))
            {
                string value = valueStr.Substring(0, valueStr.Length - 2);
                if (valueStr.EndsWith("cm"))
                {
                    valid = ValidIntRange(value, 150, 193);
                }
                else
                {
                    valid = ValidIntRange(value, 59, 76);
                }
            }
            return valid;
        }

        private bool ValidHairColor(string valueStr)
        {
            bool valid = false;

            if (valueStr[0] == '#' && valueStr.Length == 7)
            {
                string value = valueStr.Substring(1);
                Regex r = new Regex(@"^[0-9a-f]+$");
                valid = r.IsMatch(value);
            }
            return valid;
        }

        private bool ValidEyeColor(string valueStr)
        {
            bool valid;

            switch (valueStr)
            {
                case "amb":
                case "blu":
                case "brn":
                case "gry":
                case "grn":
                case "hzl":
                case "oth":
                    valid = true;
                    break;
                default:
                    valid = false;
                    break;
            }

            return valid;
        }

        private bool ValidPassportID(string valueStr)
        {
            bool valid = false;

            if (valueStr.Length == 9)
            {
                Regex r = new Regex(@"^[0-9]+$");
                valid = r.IsMatch(valueStr);
            }

            return valid;
        }


        private bool ByrIsValid { get; set; }
        private bool IyrIsValid { get; set; }
        private bool EyrIsValid { get; set; }
        private bool HgtIsValid { get; set; }
        private bool HclIsValid { get; set; }
        private bool EclIsValid { get; set; }
        private bool PidIsValid { get; set; }
    }
}
