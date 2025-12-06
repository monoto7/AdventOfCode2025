using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal static class Day6
    {
        static List<String[]> strings;

        static Int64 total = 0;
        public static void SelectInput()
        {
            strings = new List<String[]>();
            total = 0;
            Console.WriteLine("Select input, 1:Example, 2:Main, otherwise return");
            String[] input = { };
            switch (Console.ReadLine())
            {
                case "1":
                    input = File.ReadAllLines("Day6Inputs/Day6Example.txt");
                    break;
                case "2":
                    input = File.ReadAllLines("Day6Inputs/Day6Input.txt");
                    break;
                default:
                    return;
            }
            int operationCount = 0;
            foreach (String line in input)
            {
                String[] array = line.Split(" ",StringSplitOptions.RemoveEmptyEntries);
                operationCount = array.Length;
                strings.Add(array);
            }

            for (int i = 0; i < operationCount; i++)
            {
                List<string> inputs = new List<string>();
                foreach (String[] array in strings)
                {
                    inputs.Add(array[i]);
                }
                CalcOutput(inputs);
            }
           
            Console.WriteLine("part 1:" + total);
            total = 0;
            //Part 2

            char[] operatorString = input[input.Length - 1].ToCharArray();
            int startposition = 0;

            char curOperator = operatorString[0];
            for (int position = 1; position < operatorString.Length; position++)
            {

                if (operatorString[position] != ' ' || position == operatorString.Length - 1)
                {
                    curOperator = operatorString[startposition];
                    if (position == operatorString.Length - 1)
                    {
                        position++;
                    }
                    String[] inputs = new string[position - startposition];
                    for (int i = 0; i < input.Length - 1; i++)
                    {
                        char[] lineInput = input[i].Substring(startposition, position - startposition).ToCharArray();
                        for (int i1 = 0; i1 < lineInput.Length; i1++)
                        {
                            if (lineInput[i1] != ' ')
                            {
                                inputs[i1] += lineInput[i1];
                            }
                        }
                    }
                    List<String> compiledNumbersWithOperator = new List<String>();
                    foreach (string inputstring in inputs)
                    {
                        if (inputstring != null)
                        {
                            compiledNumbersWithOperator.Add(inputstring);
                        }
                    }
                    compiledNumbersWithOperator.Add(curOperator.ToString());

                    CalcOutput(compiledNumbersWithOperator);
                    startposition = position;
                }

            }

            Console.WriteLine("part 2:" + total);

        }

        public static void CalcOutput(List<string> input)
        {
            Int64 curValue = int.Parse(input[0]);
            for (int i = 1; i < input.Count - 1; i++)
            {
                if (input[input.Count-1] == "*")
                {
                    curValue*=int.Parse(input[i]);
                }
                if (input[input.Count - 1] == "+")
                {
                    curValue += int.Parse(input[i]);
                }
            }
            total += curValue;
        }
    }
}
