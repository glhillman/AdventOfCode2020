using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day23
{

    public class Cup
    {
        public Cup(long value)
        {
            Value = value;
            Next = this;
            Prev = this;
        }

        public void AppendCup(Cup root, Cup other)
        {
            other.Prev = this;
            other.Next = this.Next;
            this.Next = other;
            root.Prev = other;
        }

        public Cup RemoveNext3()
        {
            Cup firstOfThree = this.Next;
            Cup lastOfThree = firstOfThree.Next.Next;
            
            // point around the three
            lastOfThree.Next.Prev = this;
            this.Next = lastOfThree.Next;

            return firstOfThree;
        }

        public void Insert3(Cup firstOfThree)
        {
            Cup lastOfThree = firstOfThree.Next.Next;
            lastOfThree.Next = this.Next;
            this.Next.Prev = lastOfThree;
            this.Next = firstOfThree;
            firstOfThree.Prev = this;
        }

        public Cup Move(Cup[] cups)
        {
            // assumption: 'this' is the current cup 
            Cup three = RemoveNext3();
            Cup destination = LocateDestination(this.Value - 1, three, cups);
            destination.Insert3(three);

            // return is the new current cup
            return this.Next;
        }

        public Cup LocateDestination(long value, Cup three, Cup[] cups)
        {
            Cup threeNext = three.Next;
            Cup threeNextNext = threeNext.Next;
            
            long target = value;
            Cup destination;

            while (target >= 1 && (target == three.Value || target == threeNext.Value || target == threeNextNext.Value))
            {
                target--;
            }
            if (target >= 1)
            {
                destination = cups[target];
            }
            else
            {
                // find maximum value in the ring
                long maxValue = cups.Length - 1;
                while (maxValue == three.Value || maxValue == threeNext.Value || maxValue == threeNextNext.Value)
                {
                    maxValue--;
                }
                destination = cups[maxValue];
            }

            return destination;
        }

        public override string ToString()
        {
            return string.Format("{0} Prev: {1}, Next: {2}", Value, Prev.Value, Next.Value);
        }
        public long Value;
        public Cup Next;
        public Cup Prev;
    }
}
