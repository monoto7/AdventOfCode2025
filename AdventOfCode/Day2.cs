using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Day2
    {
        public static Int64 countPart1 = 0;
        public static Int64 countPart2 = 0;
        public static void SelectInput()
        {
            Console.WriteLine("Select input, 1:Example, 2:Main, otherwise return");
            String[] input = { };
            switch (Console.ReadLine())
            {
                case "1":
                    input = File.ReadAllLines("Day2Inputs/Day2Example.txt");
                    break;
                case "2":
                    input = File.ReadAllLines("Day2Inputs/Day2Input.txt");
                    break;
                default:
                    return;
            }
            checkInput(input);
            Console.WriteLine("Count part1:" + countPart1);
            Console.WriteLine("Count part2:" + countPart2);
        }

        public static void checkInput(string[] input)
        {
            foreach(string inputLine in input)
            {
                String[] idRanges = inputLine.Split(',');
                foreach (String idRange in idRanges)
                {
                    String[] ids = idRange.Split("-");
                    Int64 min = Int64.Parse(ids[0]);
                    Int64 max = Int64.Parse((ids[1]));
                    for(Int64 i = min; i <= max; i++)
                    {
                        checkDoubled(i.ToString());
                        checkN(i.ToString());
                    }

                }
            }
        }

        public static void checkN(string input)
        {
            //Checks multiples for any length
            for (int i = 0; i < input.Length -1;)
            {
                i++;
                if (input.Length % i == 0)
                {
                    string first = input.Substring(0, i);
                    string total = "";
                    for (int stringLength = 0; stringLength < input.Length / i; stringLength++)
                    {
                        total += first;
                    }
                    if (total == input)
                    {
                        countPart2 += Int64.Parse(input);
                        return;
                    }
                }
            }

        }
        public static void checkDoubled(string input)
        {
            //lazily checks for doubled inputs
            if (input.Substring(0, input.Length / 2) == input.Substring(input.Length / 2))
            {
                countPart1 += Int64.Parse(input);
            }
        }
    }
}
