using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal static class Day11
    {
        public static Dictionary<string, Node> nodes;

        public static Int64 total;
        public static void SelectInput()
        {
            nodes = new Dictionary<string, Node>();
            total = 0;
            Console.WriteLine("Select input, 1:Example, 2:Main, otherwise return");
            String[] input = { };
            switch (Console.ReadLine())
            {
                case "1":
                    input = File.ReadAllLines("Day11Inputs/Day11Example.txt");
                    break;
                case "2":
                    input = File.ReadAllLines("Day11Inputs/Day11Input.txt");
                    break;
                default:
                    return;
            }

            foreach (string line in input)
            {
                string[] parts = line.Split(' ');
                parts[0] = parts[0].Trim(':');
                Node node = new Node();
                for (int i = 1; i < parts.Length; i++)
                {
                    node.targets.Add(parts[i]);
                }
                nodes.Add(parts[0], node);
            }

            //doesn't work with second example
            getAll("you", new List<string>());
            Console.WriteLine("part 1: " + total);

            //makes the assumption that fft comes before dac
            List<string> s = new List<string>();
            (Int64, Int64, Int64) total2 = getAllPart2("svr");
            (Int64, Int64, Int64) total3 = getAllPart2("fft");
            (Int64, Int64, Int64) total4 = getAllPart2("dac");
            Console.WriteLine("part 2: "+ (total4.Item1*total3.Item2*total2.Item3));

        }

        private static void getAll(string start, List<string> parts)
        {
            foreach(string s in nodes[start].targets)
            {
                
                if (parts.Contains(s))
                {
                     return;
                }
                if (s != "out")
                {
                    List<string> party = parts.ToList();
                    party.Add(start);
                    getAll(s, party);
                }
                else
                {
                    total++;
                }
            }
        }

        private static (Int64,Int64,Int64) getAllPart2(string start)
        {

            Int64 amount = 0;
            Int64 amountDac = 0;
            Int64 amountfft = 0;
            foreach (string s in nodes[start].targets)
            {
                if(s == "dac")
                {
                    amountDac++;
                }
                else if(s == "fft")
                {
                    amountfft++;
                }
                else if (s == "out")
                {
                    amount++;
                    continue;
                }
                if(nodes[s].count != -1)
                {
                    amount += nodes[s].count;
                    amountDac += nodes[s].countToDac;
                    amountfft += nodes[s].counttofft;
                    continue;
                }
                (Int64, Int64, Int64) items = getAllPart2(s);
                amount += items.Item1;
                amountDac += items.Item2;
                amountfft += items.Item3;
                Console.WriteLine("start:" + start);
                Console.WriteLine(amount);
                Console.WriteLine(amountDac);
            }
            nodes[start].count = amount;
            nodes[start].countToDac = amountDac;
            nodes[start].counttofft = amountfft;
            return (amount, amountDac, amountfft);
        }
        public class Node()
        {
           public  List<string> targets = new List<string>();
            public Int64 count = -1;
            public Int64 countToDac = -1;
            public Int64 counttofft = -1;

        }
    }
}
