using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day21
{
    public class Allergen
    {
        public Allergen(string name, List<string> potentialIngredients)
        {
            Name = name;
            PotentialIngredients = potentialIngredients;
        }

        public string Name { get; private set; }
        public List<string> PotentialIngredients { get; private set; }
        public override string ToString()
        {
            return string.Format("{0} {1} ingredients", Name, PotentialIngredients.Count);
        }
    }
}
