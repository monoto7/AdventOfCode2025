using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdventOfCode2025
{
    internal static class Day5
    { 
        public static List<(Int64, Int64)> ranges;
        public static int count;
        public static void SelectInput()
        {
            ranges = new List<(Int64, Int64)>();
            String[] input = { };
            count = 0;

            Console.WriteLine("Select input, 1:Example, 2:Main, otherwise return");
            switch (Console.ReadLine())
            {
                case "1":
                    input = File.ReadAllLines("Day5Inputs/Day5Example.txt");
                    break;
                case "2":
                    input = File.ReadAllLines("Day5Inputs/Day5Input.txt");
                    break;
                default:
                    return;
            }
            int i = 0;
            while (input[i] != "")
            {
                AddRange(input[i]);
                i++;
            }
            i++;
            while (i < input.Length)
            {
                CheckInRange(Int64.Parse(input[i]));

                i++;
            }
            Console.WriteLine("total fresh:" + count);

            Int64 total = 0;
            int curCount = ranges.Count;
            foreach ((Int64, Int64) range in ranges)
            {
                total += range.Item2 - range.Item1 + 1;
            }
            Console.WriteLine("total in range:" + total);
        }

        public static void AddRange(String Range)
        {
            string[] ints = Range.Split("-");
            Int64 low = Int64.Parse(ints[0]);
            Int64 high = Int64.Parse(ints[1]);
            Int64 curLowest = low;
            Int64 curHighest = high;

            List<(Int64,Int64)> foundList = new List<(Int64,Int64)>();
            for (int i = 0; i < ranges.Count; i++)
            {
                Int64 lowI = ranges[i].Item1;
                Int64 highI = ranges[i].Item2;
                if (high >= highI && low <= highI)
                {
                    if (lowI < curLowest) curLowest = lowI;
                    foundList.Add(ranges[i]);
                }

                if (high >= lowI && low <= lowI)
                {
                    if (highI > curHighest) curHighest = highI;
                    foundList.Add(ranges[i]);
                }

                if (high <= highI && low >= lowI)
                {
                    if (highI > curHighest) curHighest = highI;
                    if (lowI < curLowest) curLowest = lowI;
                    foundList.Add(ranges[i]);
                }
            }

            

            foreach((Int64,Int64) range in foundList)
            {
                
                ranges.Remove(range);
            }
            ranges.Add((curLowest,  curHighest));
        }
        public static void CheckInRange(Int64 toCheck)
        {
            foreach((Int64,Int64) i in ranges)
            {
                if(toCheck>=i.Item1 && toCheck <= i.Item2)
                {
                    count++;
                    return;
                }
            }
        }
    }
}
