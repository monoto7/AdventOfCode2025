using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal static class Day7
    {
        public static int total = 0;
        public static void SelectInput()
        {
            Console.WriteLine("Select input, 1:Example, 2:Main, otherwise return");
            String[] input = { };
            switch (Console.ReadLine())
            {
                case "1":
                    input = File.ReadAllLines("Day7Inputs/Day7Example.txt");
                    break;
                case "2":
                    input = File.ReadAllLines("Day7Inputs/Day7Input.txt");
                    break;
                default:
                    return;
            }
            char[] prevline = new char[] { };
            foreach (string line in input)
            {
                char[] lineArray = line.ToCharArray();
                if (prevline.Length != 0)
                {
                    readline(lineArray, prevline);
                }
                prevline = lineArray;
                
            }
            Console.WriteLine("part 1:" + total);


            List<Int64> prevInts = new List<Int64>();
            foreach (string line in input)
            {
                List<Int64> intList = TurnToInts(line.ToCharArray());
                if (prevInts.Count != 0)
                {
                    readintLine(intList, prevInts);
                }
                prevInts = intList;
            }
            Int64 endTotal = 0;
            foreach (Int64 i in prevInts)
            {
                endTotal += i;
            }
            Console.WriteLine("part 2:" + endTotal);
            
        }

        private static List<Int64> TurnToInts(char[] line)
        {
            List<Int64> ints = new List<Int64>();
            foreach(char c in line)
            {
                if(c == '.')
                {
                    ints.Add(0);
                }
                else if (c == 'S')
                {
                    ints.Add(-1);
                }
                else if(c=='^')
                {
                    ints.Add(-2);
                }
            }
            return ints;
        }

        private static void readline(char[] curline, char[] prevline)
        {
            for (int i = 0; i < curline.Length; i++)
            {
                if(prevline[i] == 'S' || prevline[i] == '|')
                {
                    if (curline[i] == '^')
                    {
                        if (i != 0) curline[i - 1] = '|';
                        if (i != curline.Length - 1) curline[i + 1] = '|';
                        total++;
                    }
                    else
                    {
                        curline[i] = '|';
                    }
                }
            }
            
        }

        private static void readintLine(List<Int64> curline, List<Int64> prevline)
        {
            for (int i = 0; i < curline.Count; i++)
            {
                if (prevline[i] != -2)
                {

                    if (curline[i] == -2)
                    {
                        if (i != 0)
                        {
                                curline[i - 1] = (curline[i - 1] + prevline[i]);

                        }
                        if (i != curline.Count - 1)
                        {
                           
                            
                                curline[i + 1] = (curline[i + 1] + prevline[i]);
                        }
                    }
                    else
                    {
                        if (prevline[i] == -1)
                        {
                            curline[i] = 1;
                        }
                        else
                        {
                            
                            curline[i] = curline[i] + prevline[i];


                        }
                    }
                }

            }
        }
    }
}
