using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    public class Day21
    {
        public List<Allergen> Allergens;
        public Dictionary<string, int> IngredientCount;
        public Day21()
        {
            LoadData();
        }

        public void Part1And2()
        {
            var groups = Allergens.OrderBy(a => a.Name).ToList();
            int i = 0;
            while (i < groups.Count() - 1)
            {
                if (groups[i].Name == groups[i + 1].Name)
                {
                    string name = groups[i].Name;
                    List<string> common = groups[i].PotentialIngredients.Intersect(groups[i + 1].PotentialIngredients).ToList();
                    i += 2;
                    while (i < groups.Count && groups[i].Name == groups[i - 1].Name)
                    {
                        common = groups[i].PotentialIngredients.Intersect(common).ToList();
                        i++;
                    }
                    // common now has a boiled down list of ingredients common to group of allergens
                    // pull all instances of the allergen out of Allergens list & replace with minimized version
                    Allergens.RemoveAll(a => a.Name == name);
                    Allergens.Add(new Allergen(name, common));
                }
            }

            // at lease one of the Allergens now has a single ingredient. That ingredient can be eliminated from any other allergen's list
            // loop through doing that over and over until nothing changes.
            // Allergens will then be a list of allergens with only one ingredient each.
            bool changesMade = false;
            do
            {
                changesMade = false;
                List<Allergen> singles = Allergens.Where(a => a.PotentialIngredients.Count() == 1).ToList();
                foreach (Allergen single in singles)
                {
                    List<Allergen> others = Allergens.Where(a => a.Name != single.Name && a.PotentialIngredients.Contains(single.PotentialIngredients[0])).ToList();
                    foreach (Allergen a in others)
                    {
                        a.PotentialIngredients.Remove(single.PotentialIngredients[0]);
                        changesMade = true;
                    }
                }
            } while (changesMade);

            // pull the allergens out of the dictionary keeping track of ingredient counts
            foreach (Allergen a in Allergens)
            {
                IngredientCount.Remove(a.PotentialIngredients[0]);
            }

            List<int> ingredientCounts = IngredientCount.Values.ToList();
            int sum = 0;
            foreach (int count in ingredientCounts)
            {
                sum += count;
            }

            Console.WriteLine("Part1: {0}", sum);

            // Part2
            // sort the allergens by name
            List<Allergen> sorted = Allergens.OrderBy(a => a.Name).ToList();
            StringBuilder sb = new StringBuilder();
            // build a string of the allergen's single ingredients
            for (int a = 0; a < sorted.Count(); a++)
            {
                sb.Append(sorted[a].PotentialIngredients[0]);
                if (a < sorted.Count - 1)
                {
                    sb.Append(",");
                }
            }

            Console.WriteLine("Part2: {0}", sb.ToString());
        }

        private void LoadData()
        {

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                Allergens = new List<Allergen>();
                IngredientCount = new Dictionary<string, int>();

                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    string[] parts = line.Split(' ', '(', ')', ',');
                    List<string> ingredients = new List<string>();
                    int i = 0;
                    while (i < parts.Length && parts[i] != "contains")
                    {
                        if (parts[i].Length > 0)
                        {
                            ingredients.Add(parts[i]);
                            // count every time an ingredient appears
                            if (IngredientCount.ContainsKey(parts[i]))
                            {
                                IngredientCount[parts[i]]++;
                            }
                            else
                            {
                                IngredientCount[parts[i]] = 1;
                            }
                        }
                        i++;
                    }
                    if (parts[i] == "contains")
                    {
                        i++;
                        while (i < parts.Length)
                        {
                            if (parts[i].Length > 0)
                            {
                                Allergen allergen = new Allergen(parts[i], ingredients);
                                Allergens.Add(allergen);
                            }
                            i++;
                        }
                    }
                }

                file.Close();
            }
        }
    }
}
