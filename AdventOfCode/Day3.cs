using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Day3
    {
        static int part1Count = 0;
        static Int64 part2Count = 0;
        public static void SelectInput()
        {
            part1Count = 0;
            part2Count = 0;
            Console.WriteLine("Select input, 1:Example, 2:Main, otherwise return");
            String[] input = { };
            switch (Console.ReadLine())
            {
                case "1":
                    input = File.ReadAllLines("Day3Inputs/Day3Example.txt");
                    break;
                case "2":
                    input = File.ReadAllLines("Day3Inputs/Day3Input.txt");
                    break;
                default:
                    return;
            }
            foreach (string line in input)
            {
                getHighest(line);
                getHighest12(line);
            }
            Console.WriteLine(part1Count);
            Console.WriteLine(part2Count);
        }

        public static void getHighest12(string inputLine)
        {
            char[] array = inputLine.ToCharArray();
            int[] integers = new int[12];
            for (int i = 0; i < array.Length; i++)
            {
                int parsed = array[i] - '0';
                int iterator = i - array.Length + integers.Length;
                if (iterator < 0)
                {
                    iterator = 0;
                }
                while (iterator < integers.Length)
                {
                    if (parsed > integers[iterator])
                    {
                        integers[iterator] = parsed;
                        for (int entryToZero = iterator + 1; entryToZero < integers.Length; entryToZero++)
                        {
                            integers[entryToZero] = 0;
                        }
                        break;
                    }
                    iterator++;
                }
            }
            string lazyStringToParse = "";
            for (int i = 0; i < integers.Length; i++)
            {
                lazyStringToParse += integers[i];
            }
            part2Count += Int64.Parse(lazyStringToParse);
        }
        public static void getHighest(string inputLine)
        {
            char[] array = inputLine.ToCharArray();
            int firstint = 0;
            int secondint = -1;
            for (int i = 0; i < array.Length; i++) {
                int parsed = array[i] - '0';
                if (parsed > firstint && i!= array.Length-1)
                {
                    firstint = parsed;
                    secondint = 0;
                }
                else if (parsed > secondint)
                {
                    secondint = parsed;
                }
                
            }
            string lazyStringToParse = firstint + "" + secondint;
            part1Count += int.Parse(lazyStringToParse);
        }
    }
}
