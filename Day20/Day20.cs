using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day20
{
    public class Day20
    {
        private List<Tile> Tiles { get; set; }
        List<Tuple<IdView, IdView>> HorizontalCandidates = new List<Tuple<IdView, IdView>>();
        List<Tuple<IdView, IdView>> VerticalCandidates = new List<Tuple<IdView, IdView>>();
        private int SideLength { get; set; }
        private IdView[,] Part1IdViews;
        private long Result { get; set; }
        public Day20()
        {
            LoadData();
            SideLength = (int)Math.Sqrt(Tiles.Count);
        }

        public void Part1()
        {
            List<Tile> candidates = new List<Tile>(Tiles);


            IdView[,] idViews = new IdView[SideLength, SideLength];

            FindPairings(HorizontalCandidates, VerticalCandidates);

            List<long> usedTiles = new List<long>();

            foreach (Tuple<IdView, IdView> pair in HorizontalCandidates)
            {
                usedTiles.Clear();
                usedTiles.Add(pair.Item1.Id);
                idViews[0, 0] = pair.Item1;

                if (RecursiveMatch(usedTiles, idViews, 0, 0))
                {
                    break;
                }
            }

            Console.WriteLine("Part1: {0}", Result);
        }

        public void Part2()
        {
            char[,] masterGrid = new char[SideLength * 8, SideLength * 8];
            // make a copy of the arranged tiles with the edges removed
            int xOffset = 0;
            int yOffset = 0;

            for (int i = 0; i < SideLength; i++)
            {
                for (int j = 0; j < SideLength; j++)
                {
                    IdView idView = Part1IdViews[i, j];
                    Tile tile = Tiles.FirstOrDefault(t => t.Id == idView.Id);
                    char[,] grid = tile.Transform(idView.ViewNum);

                    for (int x = 0; x < 8; x++)
                    {
                        for (int y = 0; y < 8; y++)
                        {
                            masterGrid[x + xOffset, y + yOffset] = grid[x, y];
                        }
                    }

                    yOffset += 8;
                }
                xOffset += 8;
                yOffset = 0;
            }
            
            int rotations = 0;
            int nSerpents;

            // locate the view that has serpents by rotating or flipping the grid, then searching
            while ((nSerpents = FindSerpents(masterGrid)) == 0 && rotations < 8)
            {
                if (rotations == 4)
                {
                    // we've gone all the way around. Flip it & search the flipped rotations
                    masterGrid = Tile.Flip(masterGrid);
                }
                else
                {
                    masterGrid = Tile.Rotate(masterGrid);
                }
                rotations++;
            }

            // Finally! count the non-serpent marks
            int nonMonster = 0;
            for (int y = 0; y < masterGrid.GetLength(0); y++)
            {
                for (int x = 0; x < masterGrid.GetLength(0); x++)
                {
                    nonMonster += masterGrid[x, y] == '#' ? 1 : 0;
                }
            }

            Console.WriteLine("Part2: {0}", nonMonster);
        }


        private int FindSerpents(char[,] grid)
        {
            int size = grid.GetLength(0);
            int nSerpents = 0;

             char[][] body = new char[][]
            {
                "                  #".ToCharArray(),
                "#    ##    ##    ###".ToCharArray(),
                " #  #  #  #  #  #".ToCharArray()
            };

            // match on middle string of serpent, as it has the most characters
            int row = 1;
            bool foundOnRow = false;
            int index = 0;

            while (row < size - 2)
            {
                foundOnRow = false;
                if ((index = IndexOfBodyMatch(grid, body[1], row, index, false)) >= 0)
                {
                    // check above & below for rest of body
                    if (IndexOfBodyMatch(grid, body[0], row - 1, index, true) == index &&
                        IndexOfBodyMatch(grid, body[2], row + 1, index, true) == index)
                    {
                        // match found at index, row
                        // mark the serpent
                        MarkSerpent(grid, body[0], row - 1, index);
                        MarkSerpent(grid, body[1], row, index);
                        MarkSerpent(grid, body[2], row + 1, index);
                        nSerpents++;
                        foundOnRow = true;
                    }
                }
                if (foundOnRow)
                {
                    index++;
                }
                else
                {
                    index = 0;
                    row++;
                }
            }

            return nSerpents;
        }

        private void MarkSerpent(char[,] grid, char[] body, int row, int startIndex)
        {
            for (int x = startIndex; x < startIndex + body.Length; x++)
            {
                if (body[x - startIndex] == '#')
                {
                    grid[x, row] = 'O';
                }
            }
        }

        private int IndexOfBodyMatch(char[,] grid, char[] body, int row, int startIndex, bool singlePass)
        {
            int foundIndex = -1;
            int startMax = (grid.GetLength(0) - body.Length) - 1;

            while (startIndex < startMax && foundIndex < 0)
            {
                for (int x = startIndex; x < startIndex + body.Length; x++)
                {
                    if (body[x - startIndex] == '#')
                    {
                        if (grid[x, row] == '#' || grid[x, row] == 'O')
                        {
                            if (x == startIndex + body.Length - 1)
                            {
                                foundIndex = startIndex;
                                break;
                            }
                        }
                        else
                        {
                            // nope
                            break;
                        }
                    }
                }
                if (singlePass)
                {
                    break;
                }
                else
                {
                    startIndex++;
                }
            }

            return foundIndex;
        }

        private bool RecursiveMatch(List<long> usedTiles, IdView[,] idViews, int x, int y)
        { 
            // always match sideways until end of row (x), then start at next column (y)
            if (usedTiles.Count == Tiles.Count)
            {
                // result is in the 4 corners of idViews
                Result = idViews[0, 0].Id * idViews[SideLength - 1, 0].Id * idViews[0, SideLength - 1].Id * idViews[SideLength - 1, SideLength - 1].Id;
                // save idViews for part 2
                Part1IdViews = (IdView[,])idViews.Clone();
                return true; // all tiles arranged!
            }
            else if (x < SideLength - 1)
            {
                IdView idView = idViews[x, y];
                var horizontals = HorizontalCandidates.Where(p => idView.Id == p.Item1.Id &&
                                                                  idView.ViewNum == p.Item1.ViewNum &&
                                                                  usedTiles.Contains(p.Item2.Id) == false);
                foreach (Tuple<IdView, IdView> pair in horizontals)
                {
                    IdView[,] partialViews = (IdView[,])idViews.Clone();
                    List<long> tmpUsedTiles = new List<long>(usedTiles);
                    tmpUsedTiles.Add(pair.Item2.Id);
                    partialViews[x + 1, y] = pair.Item2;
                    if (RecursiveMatch(tmpUsedTiles, partialViews, x+1, y))
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                x = 0;
                IdView idView = idViews[x, y];
                var verticals = VerticalCandidates.Where(p => idView.Id == p.Item1.Id &&
                                                              idView.ViewNum == p.Item1.ViewNum &&
                                                              usedTiles.Contains(p.Item2.Id) == false);
                foreach (Tuple<IdView, IdView> pair in verticals)
                {
                    IdView[,] partialViews = (IdView[,])idViews.Clone();
                    List<long> tmpUsedTiles = new List<long>(usedTiles);
                    tmpUsedTiles.Add(pair.Item2.Id);
                    partialViews[x, y+1] = pair.Item2;
                    if (RecursiveMatch(tmpUsedTiles, partialViews, x, y+1))
                    {
                        return true;
                    }
                }
                return false; // temp
            }
        }

        public void FindPairings(List<Tuple<IdView, IdView>> horizCandidates, List<Tuple<IdView, IdView>> vertCandidates)
        {
            
            foreach (Tile tile1 in Tiles)
            {
                foreach (Tile tile2 in Tiles)
                {
                    if (tile1.Id != tile2.Id)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (tile1.Views[i].IsHorizontalMatch(tile2.Views[j]))
                                {
                                    IdView one = new IdView(tile1.Id, i);
                                    IdView two = new IdView(tile2.Id, j);
                                    Tuple<IdView, IdView> pair = new Tuple<IdView, IdView>(one, two);
                                    horizCandidates.Add(pair);
                                }
                                if (tile1.Views[i].IsVerticalMatch(tile2.Views[j]))
                                {
                                    IdView one = new IdView(tile1.Id, i);
                                    IdView two = new IdView(tile2.Id, j);
                                    Tuple<IdView, IdView> pair = new Tuple<IdView, IdView>(one, two);
                                    vertCandidates.Add(pair);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void LoadData()
        {

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\test.txt";

            if (File.Exists(inputFile))
            {
                Tiles = new List<Tile>();
                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        // read tile line
                        string[] parts = line.Split(' ', ':');
                        int id = int.Parse(parts[1]);
                        char[,] grid = new char[10, 10];
                        for (int y = 0; y < 10; y++)
                        {
                            line = file.ReadLine();
                            for (int x = 0; x < 10; x++)
                            {
                                grid[x, y] = line[x];
                            }
                        }
                        Tiles.Add(new Tile(id, grid));
                    }
                }

                file.Close();
            }
        }
    }
}
