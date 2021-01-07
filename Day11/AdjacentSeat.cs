using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    public class AdjacentSeat
    {
        public AdjacentSeat(string name, int colOffset, int rowOffset, int maxCol, int maxRow)
        {
            Name = name;
            ColOffset = colOffset;
            RowOffset = rowOffset;
            MaxRow = maxRow;
            MaxCol = maxCol;
        }

        public bool IsOccupied(char[,] seats, int col, int row, bool extend)
        {
            if (extend)
            {
                while (IsFloor(seats, col, row))
                {
                    col += ColOffset;
                    row += RowOffset;
                }
            }

            return IsMatch(false, '#', seats, col, row);
        }

        public bool IsEmpty(char[,] seats, int col, int row, bool extend)
        {
            while (IsFloor(seats, col, row))
            {
                col += ColOffset;
                row += RowOffset;
            }

            return IsMatch(true, 'L', seats, col, row);
        }

        public bool IsFloor(char[,] seats, int col, int row)
        {
            return IsMatch(false, '.', seats, col, row);
        }

        public override string ToString()
        {
            return string.Format("{0} ({1},{2})", Name, ColOffset, RowOffset);
        }

        private bool IsMatch(bool assumption, char charToMatch, char[,] seats, int col, int row)
        {
            bool isMatch = assumption;

            int newCol = col + ColOffset;
            int newRow = row + RowOffset;

            if (newCol >= 0 && newCol <= MaxCol && newRow >= 0 && newRow <= MaxRow)
            {
                isMatch = seats[newCol, newRow] == charToMatch;
            }

            return isMatch;
        }


        private string Name { get; set; }
        private int ColOffset { get; set; }
        private int RowOffset { get; set; }
        private int MaxRow { get; set; }
        private int MaxCol { get; set; }
    }
}
