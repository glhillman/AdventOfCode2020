using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day17
{
    public class Day17
    {
        public const int MAX = 30;

        public int _masterCube;
        private int _startIndex;
        private int _xySize;
        private int _zwSize;

        public Day17()
        {
        }

        public void Part1()
        {
            char[,,] cube1 = new char[MAX, MAX, MAX];
            char[,,] cube2 = new char[MAX, MAX, MAX];

            ClearCube3(cube1);
            ClearCube3(cube2);
            _masterCube = 1;
            _startIndex = MAX / 2;
            _zwSize = 1;

            LoadData3(cube1);

            // set up to start looking 1 away from edges
            _xySize += 2;
            _zwSize += 2;
            _startIndex--;

            for (int i = 0; i < 6; i++)
            {
                if (_masterCube == 1)
                {
                    Cycle1(cube1, cube2);
                    _masterCube = 2;
                }
                else
                {
                    Cycle1(cube2, cube1);
                    _masterCube = 1;
                }
            }

            int nActive;
            if (_masterCube == 1)
            {
                nActive = CountAllActive3(cube1);
            }
            else
            {
                nActive = CountAllActive3(cube2);
            }

            Console.WriteLine("Part1: {0}", nActive);
        }

        public void Part2()
        {
            char[,,,] cube1 = new char[MAX, MAX, MAX, MAX];
            char[,,,] cube2 = new char[MAX, MAX, MAX, MAX];

            ClearCube4(cube1);
            ClearCube4(cube2);
            _masterCube = 1;
            _startIndex = MAX / 2;
            _zwSize = 1;

            LoadData4(cube1);

            _xySize += 2;
            _zwSize += 2;
            _startIndex--;

            for (int i = 0; i < 6; i++)
            {
                if (_masterCube == 1)
                {
                    Cycle2(cube1, cube2);
                    _masterCube = 2;
                }
                else
                {
                    Cycle2(cube2, cube1);
                    _masterCube = 1;
                }
            }

            int nActive;
            if (_masterCube == 1)
            {
                nActive = CountAllActive4(cube1);
            }
            else
            {
                nActive = CountAllActive4(cube2);
            }

            Console.WriteLine("Part2: {0}", nActive);
        }

        private void Cycle1(char[,,] src, char[,,] dst)
        {
            ClearCube3(dst);

            for (int x = _startIndex; x < _startIndex + _xySize; x++)
            {
                for (int y = _startIndex; y < _startIndex + _xySize; y++)
                {
                    for (int z = _startIndex; z < _startIndex + _zwSize; z++)
                    {
                        int nActiveNeighbors = CountActiveNeighbors3(src, x, y, z);

                        if (src[x, y, z] == '#')
                        {
                            dst[x, y, z] = nActiveNeighbors == 2 || nActiveNeighbors == 3 ? '#' : '.';
                        }
                        else
                        {
                            dst[x, y, z] = nActiveNeighbors == 3 ? '#' : '.';
                        }
                    }
                }
            }

            _startIndex--;
            _xySize += 2;
            _zwSize += 2;
        }

        private void Cycle2(char[,,,] src, char[,,,] dst)
        {
            ClearCube4(dst);

            for (int x = _startIndex; x < _startIndex + _xySize; x++)
            {
                for (int y = _startIndex; y < _startIndex + _xySize; y++)
                {
                    for (int z = _startIndex; z < _startIndex + _zwSize; z++)
                    {
                        for (int w = _startIndex; w < _startIndex + _zwSize; w++)
                        {
                            int nActiveNeighbors = CountActiveNeighbors4(src, x, y, z, w);

                            if (src[x, y, z, w] == '#')
                            {
                                dst[x, y, z, w] = nActiveNeighbors == 2 || nActiveNeighbors == 3 ? '#' : '.';
                            }
                            else
                            {
                                dst[x, y, z, w] = nActiveNeighbors == 3 ? '#' : '.';
                            }
                        }
                    }
                }
            }

            _startIndex--;
            _xySize += 2;
            _zwSize += 2;
        }

        private int CountActiveNeighbors3(char[,,] cube, int x, int y, int z)
        {
            int nActive = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    for (int k = z - 1; k <= z + 1; k++)
                    {
                        if (i != x || j != y || k != z)
                        {
                            nActive += cube[i, j, k] == '#' ? 1 : 0;
                        }
                    }
                }
            }

            return nActive;
        }

        private int CountActiveNeighbors4(char[,,,] cube, int x, int y, int z, int w)
        {
            int nActive = 0;

            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    for (int k = z - 1; k <= z + 1; k++)
                    {
                        for (int l = w - 1; l <= w + 1; l++)
                        {
                            if (i != x || j != y || k != z || l != w)
                            {
                                nActive += cube[i, j, k, l] == '#' ? 1 : 0;
                            }
                        }
                    }
                }
            }

            return nActive;
        }
        private void ClearCube3(char[,,] cube)
        {
            for (int x = 0; x < MAX; x++)
                for (int y = 0; y < MAX; y++)
                    for (int z = 0; z < MAX; z++)
                        cube[x, y, z] = '.';
        }

        private void ClearCube4(char[,,,] cube)
        {
            for (int x = 0; x < MAX; x++)
                for (int y = 0; y < MAX; y++)
                    for (int z = 0; z < MAX; z++)
                        for (int w = 0; w < MAX; w++)
                            cube[x, y, z, w] = '.';
        }

        private int CountAllActive3(char[,,] cube)
        {
            int nActive = 0;

            for (int x = 0; x < MAX; x++)
            {
                for (int y = 0; y < MAX; y++)
                {
                    for (int z = 0; z < MAX; z++)
                    {
                        nActive += cube[x, y, z] == '#' ? 1 : 0;
                    }
                }
            }

            return nActive;
        }

        private int CountAllActive4(char[,,,] cube)
        {
            int nActive = 0;

            for (int x = 0; x < MAX; x++)
            {
                for (int y = 0; y < MAX; y++)
                {
                    for (int z = 0; z < MAX; z++)
                    {
                        for (int w = 0; w < MAX; w++)
                        {
                            nActive += cube[x, y, z, w] == '#' ? 1 : 0;
                        }
                    }
                }
            }

            return nActive;
        }

        private string _filePath = @"..\..\input.txt";
        private void LoadData3(char[,,] cube)
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + _filePath;

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                int y = _startIndex;
                int z = _startIndex;
                while ((line = file.ReadLine()) != null)
                {
                    _xySize = line.Length; // redundant, but whatever
                    for (int x = _startIndex; x < _xySize + _startIndex; x++)
                    {
                        cube[x, y, z] = line[x - _startIndex];
                    }
                    y++;
                }

                file.Close();
            }
        }

        private void LoadData4(char[,,,] cube)
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + _filePath;

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                int y = _startIndex;
                int z = _startIndex;
                int w = _startIndex;
                while ((line = file.ReadLine()) != null)
                {
                    _xySize = line.Length; // redundant, but whatever
                    for (int x = _startIndex; x < _xySize + _startIndex; x++)
                    {
                        cube[x, y, z, w] = line[x - _startIndex];
                    }
                    y++;
                }

                file.Close();
            }
        }
    }
}
