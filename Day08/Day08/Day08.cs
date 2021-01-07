using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    public class Day08
    {
        List<Instruction> _instructions;

        public Day08()
        {
            LoadInstructions();
        }

        public void Part1()
        {
            RunCode();
            Console.WriteLine("Part1 Accumulator = {0}", Accumulator);
        }

        public void Part2()
        {
            int swapIndex = 0;
            bool complete = false;

            complete = SwapAndTest(OpCodeEnum.jmp, OpCodeEnum.nop, ref swapIndex);
            if (complete)
            {
                Console.WriteLine("Part2 Accumulator = {0} ({1} switched to {2} at {3})", Accumulator, OpCodeEnum.jmp, OpCodeEnum.nop, swapIndex);
            }
            else
            {
                complete = SwapAndTest(OpCodeEnum.nop, OpCodeEnum.jmp, ref swapIndex);
                if (complete)
                {
                    Console.WriteLine("Part2 Accumulator = {0} ({1} switched to {2} at {3})", Accumulator, OpCodeEnum.nop, OpCodeEnum.jmp, swapIndex);
                }
            }
        }

        private bool RunCode()
        {
            Accumulator = 0;
            int instructionIndex = 0;

            ResetVisited();

            while (instructionIndex < _instructions.Count && _instructions[instructionIndex].Visited == false)
            {
                _instructions[instructionIndex].Visited = true;
                switch (_instructions[instructionIndex].OpCode)
                {
                    case OpCodeEnum.acc:
                        Accumulator += _instructions[instructionIndex].Arg;
                        instructionIndex++;
                        break;
                    case OpCodeEnum.jmp:
                        instructionIndex += _instructions[instructionIndex].Arg;
                        break;
                    case OpCodeEnum.nop:
                        instructionIndex++;
                        break;
                }
            }

            return instructionIndex >= _instructions.Count ? false : true; // true if endless loop, false if normal termination
        }

        private bool SwapAndTest(OpCodeEnum oldOpCode, OpCodeEnum newOpCode, ref int swapIndex)
        {
            swapIndex = 0;
            bool complete = false;

            // change old opcode to new opcode
            swapIndex = FindNextMatchingOpCode(oldOpCode, swapIndex);
            while (swapIndex >= 0 && !complete)
            {
                SwapOpCode(swapIndex, newOpCode);
                if (RunCode() == false)
                {
                    complete = true;
                    break;
                }
                else
                {
                    SwapOpCode(swapIndex++, oldOpCode);
                    swapIndex = FindNextMatchingOpCode(oldOpCode, swapIndex);
                }
            }

            return complete;
        }

        private void ResetVisited()
        {
            for (int i = 0; i < _instructions.Count; i++)
            {
                _instructions[i].Visited = false;
            }
        }

        private int FindNextMatchingOpCode(OpCodeEnum opCode, int index)
        {
            while (index < _instructions.Count && _instructions[index].OpCode != opCode)
            {
                index++;
            }

            return index < _instructions.Count ? index : -1;
        }

        private void SwapOpCode(int index, OpCodeEnum newOpcode)
        {
            _instructions[index].OpCode = newOpcode;
        }

        private int Accumulator { get; set; }
        
        private void LoadInstructions()
        {
            _instructions = new List<Instruction>();

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\CodeInput.txt";

            if (File.Exists(inputFile))
            {
                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    _instructions.Add(new Instruction(line));
                }

                file.Close();
            }
        }
    }
}
