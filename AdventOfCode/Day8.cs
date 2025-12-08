using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal class Day8
    {
        public static void SelectInput()
        {
            Console.WriteLine("Select input, 1:Example, 2:Main, otherwise return");
            String[] input = { };
            int inputLines = 10;
            switch (Console.ReadLine())
            {
                case "1":
                    input = File.ReadAllLines("Day8Inputs/Day8Example.txt");
                    inputLines = 10;
                    break;
                case "2":
                    input = File.ReadAllLines("Day8Inputs/Day8Input.txt");
                    inputLines = 1000;
                    break;
                default:
                    return;
            }
            part1(input,inputLines);
            //part 2 was so similar to part one with just a few tiny differences, but it had to run after part 1 would have finished, so while it's possible to optimize this, idc it only takes 1 minute don't @ me
            part2(input);

            
        }

        public static void part1(string[] input, int inputLines)
        {
            List<Junction> junctions = new List<Junction>();
            SortedList<double, List<(Junction, Junction)>> connections = new SortedList<double, List<(Junction, Junction)>>();
            foreach (String line in input)
            {
                string[] split = line.Split(',');
                Junction junc = new Junction();
                junc.x = int.Parse(split[0]);
                junc.y = int.Parse(split[1]);
                junc.z = int.Parse(split[2]);
                calcToAdd(connections, junctions, junc);
                junctions.Add(junc);
            }
            List<List<Junction>> circuits = new List<List<Junction>>();
            foreach (double d in connections.Keys)
            {


                foreach ((Junction, Junction) juncCon in connections[d])
                {
                    inputLines--;
                    circuits.Add(new List<Junction> { juncCon.Item1, juncCon.Item2 });
                    
                    bool unfinished = true;
                    while (unfinished)
                    {
                        bool found = false;
                        int first = 0;
                        int second = 0;
                        foreach (List<Junction> circuit in circuits)
                        {
                            foreach (List<Junction> circuit2 in circuits)
                            {
                                if (circuit != circuit2)
                                {
                                    found = circuit.Intersect(circuit2).Any();
                                }
                                if (found)
                                {
                                    first = circuits.IndexOf(circuit);
                                    second = circuits.IndexOf(circuit2);
                                    break;
                                }
                            }
                            if (found) break;
                        }

                        if (found)
                        {
                            circuits[first].AddRange(circuits[second]);
                            circuits[first] = circuits[first].Distinct().ToList();
                            circuits.RemoveAt(second);

                        }
                        else
                        {
                            unfinished = false;
                        }
                    }
                }
                if (inputLines == 0) break;

            }
            SortedSet<Int64> values = new SortedSet<Int64>();
            foreach (List<Junction> circuit in circuits)
            {
                if (!values.Contains(circuit.Count))
                {
                    values.Add(circuit.Count);
                }
            }


            List<Int64> last3 = values.Skip(Math.Max(0, values.Count() - 3)).ToList();
            Int64 total = 1;
            foreach (Int64 value in last3)
            {
                total *= value;
            }
            Console.WriteLine("Part 1 : " + total);


        }
        public static void part2(string[] input)
        {
            List<Junction> junctions = new List<Junction>();
            SortedList<double, List<(Junction, Junction)>> connections = new SortedList<double, List<(Junction, Junction)>>();
            foreach (String line in input)
            {
                string[] split = line.Split(',');
                Junction junc = new Junction();
                junc.x = int.Parse(split[0]);
                junc.y = int.Parse(split[1]);
                junc.z = int.Parse(split[2]);
                calcToAdd(connections, junctions, junc);
                junctions.Add(junc);
            }

            List<List<Junction>> circuits = new List<List<Junction>>();
            bool last = false;
            foreach (double d in connections.Keys)
            {


                foreach ((Junction, Junction) juncCon in connections[d])
                {
                    circuits.Add(new List<Junction> { juncCon.Item1, juncCon.Item2 });
                    
                    bool unfinished = true;
                    while (unfinished)
                    {
                        bool found = false;
                        int first = 0;
                        int second = 0;
                        foreach (List<Junction> circuit in circuits)
                        {
                            foreach (List<Junction> circuit2 in circuits)
                            {
                                if (circuit != circuit2)
                                {
                                    found = circuit.Intersect(circuit2).Any();
                                }
                                if (found)
                                {
                                    first = circuits.IndexOf(circuit);
                                    second = circuits.IndexOf(circuit2);
                                    break;
                                }
                            }
                            if (found) break;
                        }

                        if (found)
                        {
                            
                            
                            circuits[first].AddRange(circuits[second]);
                            circuits[first] = circuits[first].Distinct().ToList();
                            circuits.RemoveAt(second);

                        }
                        else
                        {
                            unfinished = false;
                        }
                        if (junctions.All(circuits[0].Contains))
                        {
                            Console.WriteLine(juncCon.Item1.x + "," + juncCon.Item1.y + "," + juncCon.Item1.z + "-" + juncCon.Item2.x + "," + juncCon.Item2.y + "," + juncCon.Item2.z);

                            Console.WriteLine("Part 2 : " + (juncCon.Item1.x * juncCon.Item2.x));
                            last = true;
                        }
                    }
                }
                if (last) break;

            }
        }

        public static void calcToAdd(SortedList<double,List<(Junction,Junction)>> connections, List<Junction> everyJunction, Junction junctionToAdd)
        {
            foreach(Junction junction in everyJunction)
            {
                Double xSquare = junction.x - junctionToAdd.x;
                xSquare *= xSquare;
                Double ySquare = junction.y - junctionToAdd.y;
                ySquare *= ySquare;
                Double zSquare = junction.z - junctionToAdd.z;
                zSquare*= zSquare;
                Double distance = Math.Sqrt(xSquare+ySquare+zSquare);
                if (!connections.ContainsKey(distance))
                {
                    connections.Add(distance,new List<(Junction, Junction)>());  
                }
                connections[distance].Add((junction, junctionToAdd));
            }
        }


        public class Junction()
        {
            public int x;
            public int y;
            public int z;
        }
    }
}
