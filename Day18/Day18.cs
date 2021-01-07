using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    public class Day18
    {
        public Day18()
        {
            LoadData();
        }

        public void Part1()
        {

            long sum = 0;
            foreach (string equation in Equations)
            {
                int index = 0;
                long result = Eval(equation.Replace(" ", string.Empty), ref index, false);
                sum += result;
            }

            Console.WriteLine("Part1: {0}", sum);
        }

        public void Part2()
        {
            long sum = 0;
            foreach (string equation in Equations)
            {
                int index = 0;
                long result = Eval(equation.Replace(" ", string.Empty), ref index, true);
                sum += result;
            }

            Console.WriteLine("Part2: {0}", sum);
        }

        private long Eval(string equation, ref int index, bool withPrecedence)
        {
            Stack<char> operators = new Stack<char>();
            Stack<long> operands = new Stack<long>();
            bool finished = false;

            while (index < equation.Length && !finished)
            {
                char ch = equation[index++];
                switch (ch)
                {
                    case '(':
                        long subResult = Eval(equation, ref index, withPrecedence);
                        operands.Push(subResult);
                        Combine(operators, operands, withPrecedence);
                        break;
                    case '+':
                    case '*':
                        operators.Push(ch);
                        break;
                    case ')':
                        Reduce(operators, operands);
                        finished = true;
                        break;
                    default:
                        {
                            long value = long.Parse(ch.ToString());
                            operands.Push(value);
                            Combine(operators, operands, withPrecedence);
                        }
                        break;
                }
            }

            Reduce(operators, operands);

            return operands.Pop();
        }

        private void Combine(Stack<char> operators, Stack<long> operands, bool withPrecedence)
        {
            if ((withPrecedence && (operators.Count > 0 && operators.Peek() == '*')) == false)
            {
                long value = operands.Pop();
                bool finished = false;

                while (operators.Count > 0 && !finished)
                {
                    char op = operators.Pop();
                    long operand = operands.Pop();
                    switch (op)
                    {
                        case '+':
                            value += operand;
                            break;
                        case '*':
                            if (withPrecedence)
                            {
                                operators.Push(op);
                                operands.Push(operand);// ??
                                finished = true;
                            }
                            else
                            {
                                value *= operand;
                            }
                            break;
                        default:
                            bool ug = true;
                            break;
                    }
                }
                operands.Push(value);
            }
        }

        private void Reduce(Stack<char> operators, Stack<long> operands)
        {
            long value = operands.Pop();
            bool finished = false;

            while (operators.Count > 0 && !finished)
            {
                char op = operators.Pop();
                long operand = operands.Pop();
                switch (op)
                {
                    case '+':
                        value += operand;
                        break;
                    case '*':
                        value *= operand;
                        break;
                    default:
                        bool ug = true;
                        break;
                }
            }
            operands.Push(value);
        }

        List<string> Equations;

        private void LoadData()
        {

            string inputFile = AppDomain.CurrentDomain.BaseDirectory + @"..\..\input.txt";

            if (File.Exists(inputFile))
            {
                Equations = new List<string>();
                string line;
                StreamReader file = new StreamReader(inputFile);
                while ((line = file.ReadLine()) != null)
                {
                    Equations.Add(line);
                }

                file.Close();
            }
        }
    }
}
