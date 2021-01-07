using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day07
{
    public class Day07
    {
        private List<Bag> _bags = new List<Bag>();
        const string TargetString = "shiny gold";

        public Day07()
        {
            ReadBagData();
        }
        
        public void Part1()
        {
            int canContainCount = 0;

            foreach (Bag bag in _bags)
            {
                if (bag.Name != TargetString)
                {
                    canContainCount += RecursiveCanContain(bag, TargetString) ? 1 : 0;
                }
            }

            Console.WriteLine("Part 1 Count = {0}", canContainCount);
        }

        public void Part2()
        {
            Bag bag = _bags.FirstOrDefault(b => b.Name == TargetString);

            int bagCount = RecursiveBagCount(bag);

            Console.WriteLine("Part 2 Count = {0}", bagCount);
        }

        private int RecursiveBagCount(Bag bag)
        {
            int count = 0;

            if (bag.ContainsCount > 0)
            {
                for (int i = 0; i < bag.ContainsCount; i++)
                {
                    Bag containedBag = bag.NthContainedBag(i);
                    Bag completeBag = _bags.FirstOrDefault(b => b.Name == containedBag.Name);
                    if (completeBag.ContainsCount > 0)
                    {
                        int subCount = RecursiveBagCount(completeBag);
                        subCount *= containedBag.BagCount;
                        count += subCount;
                    }
                    count += containedBag.BagCount;
                }
            }

            return count;
        }

        private bool RecursiveCanContain(Bag bag, string targetBagName)
        {
            bool contains = false;

            if (bag.Name == targetBagName)
            {
                contains = true;
            }
            else
            {
                for (int i = 0; contains == false && i < bag.ContainsCount; i++)
                {
                    Bag containedBag = bag.NthContainedBag(i);
                    Bag completeBag = _bags.FirstOrDefault(b => b.Name == containedBag.Name);
                    contains = RecursiveCanContain(completeBag, targetBagName); 
                }
            }

            return contains;
        }

         
        private void ReadBagData()
        {

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\BagData.txt";

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    int index = line.IndexOf(" bags contain");
                    Bag bag = new Bag(line.Substring(0, index));
                    bag.AddContainedBags(line.Substring(index + " bags contain ".Length));
                    _bags.Add(bag);
                }

                file.Close();
            }
        }
    }
}
