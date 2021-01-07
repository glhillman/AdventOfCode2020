using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    public class Tile
    {
        public Tile(int id, char[,] grid)
        {
            Id = id;
            Grid = grid;
            Views = new List<View>();
            _sideLength = Grid.GetLength(0);

            CaptureSides();
        }

        private void CaptureSides()
        {
            StringBuilder sbTop = new StringBuilder();
            StringBuilder sbBottom = new StringBuilder();
            StringBuilder sbLeft = new StringBuilder();
            StringBuilder sbRight = new StringBuilder();
            string top;
            string bottom;
            string left;
            string right;

            for (int i = 0; i < _sideLength; i++)
            {
                sbTop.Append(Grid[i, 0]);
                sbBottom.Append(Grid[i, _sideLength - 1]);
                sbLeft.Append(Grid[0, i]);
                sbRight.Append(Grid[_sideLength - 1, i]);
            }

            top = sbTop.ToString();
            bottom = sbBottom.ToString();
            left = sbLeft.ToString();
            right = sbRight.ToString();

            // normal view
            Views.Add(new View(top, bottom, left, right));
            // rotate left
            Views.Add(new View(right, left, Rev(top), Rev(bottom)));
            // rotate left
            Views.Add(new View(Rev(bottom), Rev(top), Rev(right), Rev(left)));
            // rotate left
            Views.Add(new View(Rev(left), Rev(right), bottom, top));
            // flip normal view
            Views.Add(new View(Rev(top), Rev(bottom), right, left));
            // rotate left
            Views.Add(new View(left, right, top, bottom));
            // rotate left
            Views.Add(new View(bottom, top, Rev(left), Rev(right)));
            // rotate left
            Views.Add(new View(Rev(right), Rev(left), Rev(bottom), Rev(top)));
        }

        
        public char[,] Transform(int version)
        {
            char[,] grid = Grid;
            int nRotations = version;
           
            if (version >= 4)
            {
                grid = Flip(grid);
                nRotations -= 4;
            }

            while (nRotations-- > 0)
            {
                grid = Rotate(grid);
            }

            // now trim the borders off the result
            int size = grid.GetLength(0);

            char[,] rslt = new char[size - 2, size - 2];

            for (int x = 1; x < size-1; x++)
            {
                for (int y = 1; y < size-1; y++)
                {
                    rslt[x - 1, y - 1] = grid[x, y];
                }
            }

            return rslt;
        }
        
        public static char[,] Flip(char [,] input)
        {
            int max = input.GetLength(0) - 1;

            char[,] output = new char[max + 1, max + 1];

            for (int y = 0; y <= max; y++)
            {
                for (int x = 0; x <= max; x++)
                {
                    output[max - x, y] = input[x, y];
                }
            }

            return output;

        }
        public static char[,] Rotate(char[,] input)
        {
            int max = input.GetLength(0) - 1;

            char[,] output = new char[max+1, max+1];

            for (int y = 0; y <= max; y++)
            {
                for (int x = 0; x <= max; x++)
                {
                    output[y, max - x] = input[x, y];
                }
            }

            return output;
        }

        private string Rev(string str)
        {
            return new string(str.ToCharArray().Reverse().ToArray());
        }

        public override string ToString()
        {
            return Id.ToString();
        }

        public long Id;
        private char[,] Grid;
        private int _sideLength;
        public List<View> Views;
    }
}
