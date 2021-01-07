using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day24
{    public class Day24
    {
        private char[,] grid1;
        private const int XSIZE = 400;
        private const int YSIZE = 400;
        private List<string> Input;
        int MinX;
        int MaxX;
        int MinY;
        int MaxY;

        public Day24()
        {
            LoadData();
            AnchorX = XSIZE / 2; // make anchors odd
            AnchorY = YSIZE / 2;
        }

        public void Part1()
        {
            grid1 = new char[XSIZE, YSIZE]; // double column width - columns go by 2's, rows by 1's.
            MinX = int.MaxValue;
            MaxX = int.MinValue;
            MinY = int.MaxValue;
            MaxY = int.MinValue;

            ClearGrid(grid1);

            foreach (string input in Input)
            {
                int curX = AnchorX;
                int curY = AnchorY;

                MoveToTile(input, ref curX, ref curY);
                MinX = Math.Min(MinX, curX);
                MaxX = Math.Max(MaxX, curX);
                MinY = Math.Min(MinY, curY);
                MaxY = Math.Max(MaxY, curY);

                if (grid1[curX, curY] == '#')
                {
                    Console.WriteLine(input);
                }
                grid1[curX, curY] = grid1[curX, curY] == '#' ? '.' : '#';
            }

            DumpTiles("main", grid1);

            int rslt = CountBlackTiles(grid1);

            Console.WriteLine("Part1: {0}", rslt);
        }

        private void DumpTiles(string gridName, char[,] grid)
        {
            Console.WriteLine(gridName);
            for (int y = MinY; y <= MaxY; y++)
            {
                for (int x = MinX; x <= MaxX; x++)
                {
                    Console.Write(grid[x, y] == '\0' ? '_' : grid[x, y]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }


        public void Part2()
        {
            char[,] grid2 = new char[XSIZE, YSIZE]; // double column width - columns go by 2's, rows by 1's.

            int workingGrid = 1;

            for (int i = 0; i < 100; i++)
            {
                if (workingGrid == 1)
                {
                    FlipTiles(grid1, grid2);
                    workingGrid = 2;                    
                }
                else
                {
                    FlipTiles(grid2, grid1);
                    workingGrid = 1;
                }
            }

            int rslt = CountBlackTiles(grid1);

            Console.WriteLine("Part2: {0}", rslt);
        }

        private void MoveToTile(string commands, ref int curX, ref int curY)
        {
            string[] cmds = commands.Trim().Split(' ');
            foreach (string cmd in cmds)
            {
                switch (cmd)
                {
                    case "e":
                        curX += 2;
                        break;
                    case "w":
                        curX -= 2;
                        break;
                    case "ne":
                        curX++;
                        curY--;
                        break;
                    case "nw":
                        curX--;
                        curY--;
                        break;
                    case "se":
                        curX++;
                        curY++;
                        break;
                    case "sw":
                        curX--;
                        curY++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("Unexpected command");
                }
            }
        }

        private int CountAdjacentBlack(char[,] grid, int x, int y)
        {
            int sum = 0;

            sum += grid[x + 2, y] == '#' ? 1 : 0; // e
            sum += grid[x - 2, y] == '#' ? 1 : 0; // w
            sum += grid[x + 1, y - 1] == '#' ? 1 : 0; // ne
            sum += grid[x - 1, y - 1] == '#' ? 1 : 0; // nw
            sum += grid[x + 1, y + 1] == '#' ? 1 : 0; // se
            sum += grid[x - 1, y + 1] == '#' ? 1 : 0; // sw

            return sum;
        }

        private int CountBlackTiles(char[,] grid)
        {
            int sum = 0;

            for (int y = 0; y < YSIZE; y++)
            {
                for (int x = 0; x < XSIZE; x++)
                {
                    sum += grid[x, y] == '#' ? 1 : 0;
                }
            }

            return sum;
        }

        private void FlipTiles(char[,] src, char[,] dst)
        {
            ClearGrid(dst);

            for (int y = 1; y < YSIZE - 1; y++)
            {
                int xStart = y % 2 == 1 ? 1 : 0; // odd x's on odd y's, even on evens
                xStart += 2;

                for (int x = xStart; x < XSIZE - 2; x += 2)
                {
                    char tile = src[x, y];

                    int NAdjacentBlack = CountAdjacentBlack(src, x, y);

                    if (tile == '#' && (NAdjacentBlack == 0 || NAdjacentBlack > 2))
                    {
                        tile = '.';
                    }
                    else if (tile == '.' && NAdjacentBlack == 2)
                    {
                        tile = '#';
                    }

                    dst[x, y] = tile;
                }
            }

            CopyGrid(dst, src);
        }

        private void ClearGrid(char[,] grid)
        {
            for (int y = 0; y < YSIZE; y++)
            {
                for (int x = 0; x < XSIZE; x++)
                {
                    grid[x, y] = '.';
                }
            }
        }

        private void CopyGrid(char[,] src, char[,] dst)
        {
            for (int y = 0; y < YSIZE; y++)
            {
                for (int x = 0; x < XSIZE; x++)
                {
                    dst[x, y] = src[x, y];
                }
            }
        }


        private int AnchorX { get; set; }
        private int AnchorY { get; set; }

        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\test.txt";

            if (File.Exists(inputFile))
            {
                Input = new List<string>();
                string line;
                StreamReader file = new StreamReader(inputFile);
                StringBuilder sb = new StringBuilder();
                while ((line = file.ReadLine()) != null)
                {
                    int i = 0;
                    while (i < line.Length)
                    {
                        switch (line[i])
                        {
                            case 'e':
                            case 'w':
                                sb.Append(line[i++]);
                                sb.Append(' ');
                                break;
                            case 'n':
                            case 's':
                                sb.Append(line[i++]);
                                sb.Append(line[i++]);
                                sb.Append(' ');
                                break;
                            default:
                                throw new ArgumentOutOfRangeException("Unexpected command");
                        }
                    }
                    Input.Add(sb.ToString());
                    sb.Clear();
                }

                file.Close();
            }
        }
    }
}
