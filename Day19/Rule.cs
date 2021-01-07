using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day19
{
    public class Rule
    {
        public Rule(int id, List<List<int>> ruleSets)
        {
            ID = id;
            Value = ' ';
            RuleSets = ruleSets;
        }

        public Rule(int id, char value)
        {
            ID = id;
            Value = value;
            RuleSets = null;
        }

        public override string ToString()
        {
            if (Value == ' ')
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ID.ToString() + ": ");
                for (int i = 0; i < RuleSets.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.Append("| ");
                    }
                    foreach (int id in RuleSets[i])
                    {
                        sb.Append(id.ToString() + " ");
                    }
                }
                return sb.ToString();
            }
            else
            {
                return string.Format("{0}: {1}", ID, Value);
            }
        }

        public int ID { get; private set; }
        public char Value { get; private set; }
        public List<List<int>> RuleSets;
    }
}
