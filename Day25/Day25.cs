using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day25
{
    public class Day25
    {

        public Day25()
        {
        }

        public void Part1()
        {
            long cardPublicKey = 13316116; //5764801;
            long doorPublicKey = 13651422; //17807724;
            long cardSubject = 7;
            long cardValue = 1;
            long doorSubject = 7;
            long doorValue = 1;
            long divisor = 20201227;

            long cardLoop;
            long doorLoop;

            for (cardLoop = 1; cardLoop < 100000000; cardLoop++)
            {
                cardValue = (cardValue * cardSubject) % divisor;
                if (cardValue == cardPublicKey)
                {
                    break;
                }
            }

            for (doorLoop = 1; doorLoop < 100000000; doorLoop++)
            {
                doorValue = (doorValue * doorSubject) % divisor;
                if (doorValue == doorPublicKey)
                {
                    break;
                }
            }

            long subject = cardPublicKey;
            long value = 1;
            for (int i = 0; i < doorLoop; i++)
            {
                value = (value * subject) % divisor;
            }

            Console.WriteLine("Part1: {0}", value);
        }

        public void Part2()
        {
            Console.WriteLine("Part2: Free Gold Star!!!");
        }
    }
}
