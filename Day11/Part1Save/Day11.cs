using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day11
{
    public class Day11
    {
        private const char EMPTY = 'L';
        private const char OCCUPIED = '#';
        
        private char[,] _seats;
        private char[,] _changedStates;
        public Day11()
        {
            _changedStates = new char[Cols, Rows];

            AdjacentSeats = new List<AdjacentSeat>();
            AdjacentSeats.Add(new AdjacentSeat("Up", 0, -1, Cols - 1, Rows - 1));
            AdjacentSeats.Add(new AdjacentSeat("Down", 0, 1, Cols - 1, Rows - 1));
            AdjacentSeats.Add(new AdjacentSeat("Left", -1, 0, Cols - 1, Rows - 1));
            AdjacentSeats.Add(new AdjacentSeat("Right", 1, 0, Cols - 1, Rows - 1)); 
            AdjacentSeats.Add(new AdjacentSeat("UpRight", 1, -1, Cols - 1, Rows - 1)); 
            AdjacentSeats.Add(new AdjacentSeat("DownRight", 1, 1, Cols - 1, Rows - 1)); 
            AdjacentSeats.Add(new AdjacentSeat("UpLeft", -1, -1, Cols - 1, Rows - 1));
            AdjacentSeats.Add(new AdjacentSeat("DownLeft", -1, 1, Cols - 1, Rows - 1)); 
        }

        public void Part1()
        {
            LoadData();

            do
            {
                InitChangeStates();
                for (int col = 0; col < Cols; col++)
                {
                    for (int row = 0; row < Rows; row++)
                    {
                        if (_seats[col, row] == EMPTY)
                        {
                            if (AdjacentSeatsEmpty(col, row))
                            {
                                _changedStates[col, row] = OCCUPIED;
                                StateChanged = true;
                            }
                        }
                        else if (_seats[col, row] == OCCUPIED)
                        {
                            if (NAdjacentSeatsOccupied(col, row) >= 4)
                            {
                                _changedStates[col, row] = EMPTY;
                                StateChanged = true;
                            }
                        }
                    }
                }
                if (StateChanged)
                {
                    ChangeStates();
                }
            } while (StateChanged);

            int nOccupied = 0;
            for (int col = 0; col < Cols; col++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    nOccupied += _seats[col, row] == OCCUPIED ? 1 : 0;
                }
            }

            Console.WriteLine("Part1 Occupied seats = {0}", nOccupied);
        }

        public void Part2()
        {
            LoadData();

            do
            {
                InitChangeStates();
                for (int col = 0; col < Cols; col++)
                {
                    for (int row = 0; row < Rows; row++)
                    {
                        if (_seats[col, row] == EMPTY)
                        {
                            if (AdjacentSeatsEmpty(col, row))
                            {
                                _changedStates[col, row] = OCCUPIED;
                                StateChanged = true;
                            }
                        }
                        else if (_seats[col, row] == OCCUPIED)
                        {
                            if (NAdjacentSeatsOccupied(col, row) >= 4)
                            {
                                _changedStates[col, row] = EMPTY;
                                StateChanged = true;
                            }
                        }
                    }
                }
                if (StateChanged)
                {
                    ChangeStates();
                }
            } while (StateChanged);

            int nOccupied = 0;
            for (int col = 0; col < Cols; col++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    nOccupied += _seats[col, row] == OCCUPIED ? 1 : 0;
                }
            }

            Console.WriteLine("Part1 Occupied seats = {0}", nOccupied);

        }

        private int Rows { get; set; }
        private int Cols { get; set; }
        private List<AdjacentSeat> AdjacentSeats { get; set; }
        private void InitChangeStates()
        {
            StateChanged = false;
            for (int col = 0; col < Cols; col++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    _changedStates[col, row] = ' ';
                }
            }
        }

        private bool AdjacentSeatsEmpty(int col, int row)
        {
            bool adjacentsEmpty = true;

            foreach (AdjacentSeat seat in AdjacentSeats)
            {
                if (!seat.IsFloor(_seats, col, row) && seat.IsEmpty(_seats, col, row) == false)
                {
                    adjacentsEmpty = false;
                    break;
                }
            }

            return adjacentsEmpty;
        }

        private int NAdjacentSeatsOccupied(int col, int row)
        {
            int nOccupied = 0;

            foreach (AdjacentSeat seat in AdjacentSeats)
            {
                if (seat.IsFloor(_seats, col, row) == false)
                {
                    nOccupied += seat.IsOccupied(_seats, col, row) ? 1 : 0;
                }
            }

            return nOccupied;
        }

        private void ChangeStates()
        {
            for (int col = 0; col < Cols; col++)
            {
                for (int row = 0; row < Rows; row++)
                {
                    char changedState = _changedStates[col, row];
                    if (changedState != ' ')
                    {
                        _seats[col, row] = changedState;
                    }
                }
            }
        }

        private bool StateChanged { get; set; }
        
        private void LoadData()
        {
            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\Test.txt";

            if (File.Exists(inputFile))
            {
                List<string> seats = new List<string>();

                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    seats.Add(line);
                }
                file.Close();

                Cols = seats[0].Length;
                Rows = seats.Count;

                // allocate an array & load with the data
                _seats = new char[Cols, Rows];
                for (int row = 0; row < Rows; row++)
                {
                    for (int col = 0; col < Cols; col++)
                    {
                        _seats[col, row] = seats[row][col];
                    }
                }
            }

        }

    }
}
