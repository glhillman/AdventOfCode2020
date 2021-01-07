using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day22
{
    public class Day22
    {
        LinkedList<int> Player1;
        LinkedList<int> Player2;

        public Day22()
        {
        }

        public void Part1()
        {
            LoadData();

            while (Player1.Count > 0 && Player2.Count > 0)
            {
                int card1 = Player1.First();
                Player1.RemoveFirst();
                int card2 = Player2.First();
                Player2.RemoveFirst();

                if (card1 > card2)
                {
                    Player1.AddLast(card1);
                    Player1.AddLast(card2);
                }
                else
                {
                    Player2.AddLast(card2);
                    Player2.AddLast(card1);
                }
            }

            List<int> winner = Player1.Count == 0 ? Player2.ToList() : Player1.ToList();

            int rslt = 0;
            int multiplier = winner.Count;
            for (int i = 0; i < winner.Count; i++)
            {
                rslt += winner[i] * multiplier;
                multiplier--;
            }

            Console.WriteLine("Part1: {0}", rslt);
        }

        public void Part2()
        {
            LoadData();
            List<List<int>> player1History = new List<List<int>>();
            List<List<int>> player2History = new List<List<int>>();

            RecursiveCombat(Player1, Player2, player1History, player2History);

            List<int> winner = Player1.Count == 0 ? Player2.ToList() : Player1.ToList();

            int rslt = 0;
            int multiplier = winner.Count;
            for (int i = 0; i < winner.Count; i++)
            {
                rslt += winner[i] * multiplier;
                multiplier--;
            }

            Console.WriteLine("Part2: {0}", rslt);
        }

        private void RecursiveCombat(LinkedList<int> player1, LinkedList<int> player2, List<List<int>> player1History, List<List<int>> player2History)
        {
            while (player1.Count > 0 && player2.Count > 0)
            {
                int duplicateIndex = -1;

                if ((duplicateIndex = HistoryRepeats(player1.ToList(), player1History)) >= 0)
                {
                    if (HistoryRepeats(player2.ToList(), player2History, duplicateIndex) == duplicateIndex)
                    {
                        // infinite loop - bail out
                        return;
                    }
                }

                player1History.Add(player1.ToList());
                player2History.Add(player2.ToList());

                int card1 = player1.First();
                player1.RemoveFirst();
                int card2 = player2.First();
                player2.RemoveFirst();

                if (card1 <= player1.Count && card2 <= player2.Count)
                {
                    // start a recursive match
                    // create a backtrace to pass in
                    // make copies of remaining decks to pass in
                    LinkedList<int> subPlayer1 = new LinkedList<int>();
                    int[] temp = player1.ToArray();
                    for (int i = 0; i < card1; i++)
                        subPlayer1.AddLast(temp[i]);

                    LinkedList<int> subPlayer2 = new LinkedList<int>();
                    temp = player2.ToArray();
                    for (int i = 0; i < card2; i++)
                        subPlayer2.AddLast(temp[i]);

                    List<List<int>> subPlayer1History = new List<List<int>>();
                    List<List<int>> subPlayer2History = new List<List<int>>();
                    
                    RecursiveCombat(subPlayer1, subPlayer2, subPlayer1History, subPlayer2History);

                    if (subPlayer1.Count == 0)
                    {
                        player2.AddLast(card2);
                        player2.AddLast(card1);
                    }
                    else
                    {
                        player1.AddLast(card1);
                        player1.AddLast(card2);
                    }
                }
                else
                {
                    if (card1 > card2)
                    {
                        player1.AddLast(card1);
                        player1.AddLast(card2);
                    }
                    else
                    {
                        player2.AddLast(card2);
                        player2.AddLast(card1);
                    }
                }
            }
        }

        private int HistoryRepeats(List<int> player, List<List<int>> history, int requiredIndex = -1)
        {
            int index = -1;
            bool match = false;

            for (int i = 0; i < history.Count && match == false; i++)
            {
                if (requiredIndex >= 0)
                {
                    i = requiredIndex;
                }
                List<int> h = history[i];
                if (h.Count == player.Count)
                {
                    match = true;
                    for (int j = 0; j < h.Count && match; j++)
                    {
                        if (h[j] != player[j])
                        {
                            match = false;
                            break;
                        }
                    }
                }
                if (match)
                {
                    index = i;
                }
                if (requiredIndex >= 0)
                {
                    break;
                }
            }
            return index;
        }

        private void LoadData()
        {

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                Player1 = new LinkedList<int>();
                Player2 = new LinkedList<int>();

                string line;
                StreamReader file = new StreamReader(inputFile);
                line = file.ReadLine(); // Player 1
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        break;
                    }
                    else
                    {
                        Player1.AddLast(int.Parse(line));
                    }
                }
                line = file.ReadLine(); // Player 2
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Length == 0)
                    {
                        break;
                    }
                    else
                    {
                        Player2.AddLast(int.Parse(line));
                    }
                }

                file.Close();
            }

        }

    }
}
