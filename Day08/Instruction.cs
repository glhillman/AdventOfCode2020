using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    public enum OpCodeEnum
    {
        acc,
        jmp,
        nop
    };

    public class Instruction
    {
        public Instruction(string instruction)
        {
            string[] split = instruction.Split(' ');
            switch (split[0])
            {
                case "acc":
                    OpCode = OpCodeEnum.acc;
                    break;
                case "jmp":
                    OpCode = OpCodeEnum.jmp;
                    break;
                case "nop":
                    OpCode = OpCodeEnum.nop;
                    break;
                default:
                    throw new ArgumentException("Unexpected OpCode: {0}", split[0]);
            }

            Arg = int.Parse(split[1]);
            Visited = false;
        }

        public OpCodeEnum OpCode { get; set; }
        public int Arg { get; private set; }
        public bool Visited { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} visited: {2}", OpCode.ToString(), Arg, Visited);
        }
    }
}
