using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{    
    public class Bag
    {
        List<Bag> _contains;
        public Bag(string name, int bagCount = 1)
        {
            _contains = new List<Bag>();

            Name = name;
            BagCount = bagCount;
        }

        public string Name { get; private set; }

        public int BagCount { get; private set; }
        
        public void AddContainedBags(string bagdata)
        {
            if (bagdata.StartsWith("no ") == false)
            {
                // parse out the input string & insert contained bags
                string[] bagAndCounts = bagdata.Split(',');
                foreach (string bagAndCount in bagAndCounts)
                {
                    string bag = bagAndCount.Trim();
                    int index = bag.IndexOf(' ');
                    int numBags = int.Parse(bag.Substring(0, index));
                    string bagName = bag.Substring(index + 1);
                    bagName = bagName.Substring(0, bagName.LastIndexOf("bag") - 1);

                    _contains.Add(new Bag(bagName, numBags));
                }
            }
        }

        public int ContainsCount
        {
            get
            {
                return _contains.Count;
            }
        }

        public Bag NthContainedBag(int i)
        {
            // no bounds checking - I trust puzzle guy completely!
            return _contains[i];
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
