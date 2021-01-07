using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    public class View
    {
        public View(string top, string bottom, string left, string right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }

        public bool IsHorizontalMatch(View other)
        {
            return this.Right == other.Left;
        }

        public bool IsVerticalMatch(View other)
        {
            return this.Bottom == other.Top;
        }

        public string Top { get; private set; }
        public string Bottom { get; private set; } 
        public string Left { get; private set; }
        public string Right { get; private set; }
    }
}
