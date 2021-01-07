using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{
    public class Day23
    {
        long[] realInput = { 2,1,9,7,4,8,3,6,5 };
        long[] test = { 3,8,9,1,2,5,4,6,7 };
        long[] input;

        public Day23()
        {
            input = new long[realInput.Length];
            realInput.CopyTo(input, 0);
        }

        public void Part1()
        {
            Cup root = new Cup(input[0]);
            Cup prev = root;
            Cup[] cups = new Cup[input.Length+1];
            cups[root.Value] = root;

            for (int i = 1; i < input.Length; i++)
            {
                Cup cup = new Cup(input[i]);
                cups[cup.Value] = cup;
                prev.AppendCup(root, cup);
                prev = cup;
            }

            Cup currentCup = root;

            for (int i = 0; i < 100; i++)
            {
                currentCup = currentCup.Move(cups);
            }

            currentCup = cups[1].Next;


            long rslt = 0;
            while (currentCup.Value != 1)
            {
                rslt *= 10;
                rslt += currentCup.Value;
                currentCup = currentCup.Next;
            }

            Console.WriteLine("Part1: {0}", rslt);
        }

        public void Part2()
        {
            Cup root = new Cup(input[0]);
            Cup prev = root;
            Cup[] cups = new Cup[1_000_001];
            cups[root.Value] = root;

            for (int i = 1; i < input.Length; i++)
            {
                Cup cup = new Cup(input[i]);
                cups[cup.Value] = cup;
                prev.AppendCup(root, cup);
                prev = cup;
            }

            for (long i = 10; i <= 1_000_000; i++)
            {
                Cup cup = new Cup(i);
                cups[i] = cup;
                prev.AppendCup(root, cup);
                prev = cup;
            }

            Cup currentCup = root;

            for (int i = 0; i < 10_000_000; i++)
            {
                currentCup = currentCup.Move(cups);
            }

            currentCup = cups[1];
            long l1 = currentCup.Next.Value;
            long l2 = currentCup.Next.Next.Value;

            long rslt = l1 * l2;

            Console.WriteLine("Part2: {0}", rslt);
        }
    }
}
