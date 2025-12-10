using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2025
{
    internal static class Day10
    {
        public static void SelectInput()
        {
            Console.WriteLine("Select input, 1:Example, 2:Main, otherwise return");
            String[] input = { };
            switch (Console.ReadLine())
            {
                case "1":
                    input = File.ReadAllLines("Day10Inputs/Day10Example.txt");
                    break;
                case "2":
                    input = File.ReadAllLines("Day10Inputs/Day10Input.txt");
                    break;
                default:
                    return;
            }
            int score = 0;
            foreach (String line in input)
            {
                string[] splitLine = line.Split(' ');
                bool[] target = GetTarget(splitLine[0]);
                List<Vector> vectors = ProcessVectors(splitLine, target.Length);
                score+= getShortestDijkstras(vectors, target).Count;
                
            }
            Console.WriteLine("Part 1:" + score);

            /*
            score = 0;
            foreach (String line in input)
            {
                string[] splitLine = line.Split(' ');
                int[] target = GetTargetInt(splitLine[splitLine.Length-1]);
                List<Vector> vectors = ProcessVectors(splitLine, target.Length);
                int temp = getShortestAStartPart2(vectors, target).Count;
                score += temp;
                Console.WriteLine(temp);
            }
            Console.WriteLine("Part 2:" + score);
            */
        }

        public static int heuristicInt(int[] current, int[] target)
        {
            int h = 0;
            for (int i = 0; i < current.Length; i++)
            {
                h += target[i] - current[i];
            }
            return h;
            

        }

        public static bool[] getLowest(List<bool[]> openset, Dictionary<bool[], int> fScore)
        {
            (bool[], int) cur = (openset[0], fScore[openset[0]]);
            foreach (bool[] key in openset)
            {
                if (fScore[key] < cur.Item2)
                {
                    cur = (key, fScore[key]);
                }
            }
            return cur.Item1;
        }
        public static List<Vector> getShortestAStartPart2(List<Vector> vectors, int[] target)
        {
            int[] start = new int[target.Length];

            List<int[]> inQueue = new List<int[]>();

            PriorityQueue<int[], int> openset = new PriorityQueue<int[], int>();
            openset.Enqueue(start, heuristicInt(start, target));
            inQueue.Add(start);

            Dictionary<int[], List<Vector>> camefrom = new Dictionary<int[], List<Vector>>(new IntArrayComparer());

            Dictionary<int[], List<Vector>> gScore = new Dictionary<int[], List<Vector>>(new IntArrayComparer());
            gScore.Add(start, new List<Vector>());




            while (openset.Count > 0)
            {
                int[] current = openset.Dequeue();
                CheckIfInIntRemove(inQueue, current);


                foreach (Vector v  in vectors)
                {

                    List<Vector> ten_gscore = gScore[current].ToList();
                    ten_gscore.Add(v);
                    int[] neighbour = getNeighborInt(current, v);

                    
                    if (!CheckIfValid(target,neighbour)) continue;
                    if (!gScore.ContainsKey(neighbour) || ten_gscore.Count < gScore[neighbour].Count)
                    {
                        if (!gScore.ContainsKey(neighbour))
                        {
                            gScore.Add(neighbour, ten_gscore);
                        }


                        gScore[neighbour] = ten_gscore;
                        if (!CheckIfInInt(inQueue,neighbour))
                        {
                            openset.Enqueue(neighbour, heuristicInt(neighbour,target));
                            inQueue.Add(neighbour);
                        }
                    }

                }
            }

            return gScore[target];
        }

        public static bool CheckIfValid(int[] target, int[] toCheck)
        {
            for (int i = 0; i< target.Length; i++)
            {
                if (toCheck[i] > target[i]  ) return false;
            }
            return true;
        }

        public static bool CheckIfInIntRemove(List<int[]> openset, int[] toCheck)
        {
            int[] found = new int[0];
            foreach (int[] b in openset)
            {
                if (checkIfEquivInt(b, toCheck) == true)
                {
                    found = b;
                    break;
                }
            }
            if(found.Length > 0)
            {
                openset.Remove(found);
                return true;
            }
            return false;
        }

        public static bool CheckIfInInt(List<int[]> openset, int[] toCheck)
        {
            int[] found = new int[0];
            foreach (int[] b in openset)
            {
                if (checkIfEquivInt(b, toCheck) == true)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool checkIfEquivInt(int[] set, int[] toCheck)
        {
            for (int i = 0; i < set.Length; i++)
            {
                if (toCheck[i] != set[i]) return false;
            }
            return true;
        }
        public static int[] getNeighborInt(int[] current, Vector v)
        {
            int[] neighbor = current.ToArray();
            for (int i = 0; i < current.Length; i++)
            {
                if (v.directions[i])
                {
                    neighbor[i] += 1;
                }

            }
            return neighbor;
        }
        public static List<Vector> getShortestDijkstras(List<Vector> vectors, bool[] target)
        {
            
            List<bool[]> openset = new List<bool[]>() { new bool[target.Length] };

            Dictionary<bool[], List<Vector>> camefrom = new Dictionary<bool[], List<Vector>>(new BoolArrayComparer());

            Dictionary<bool[], List<Vector>> gScore = new Dictionary<bool[], List<Vector>>(new BoolArrayComparer());
            gScore.Add(openset[0], new List<Vector>());


            while (openset.Count > 0)
            {
                bool[] current = openset[0];
                openset.Remove(current);
                

                foreach(Vector v in vectors)
                {

                    List<Vector> ten_gscore = gScore[current].ToList();
                    ten_gscore.Add(v);
                    bool[] neighbour = getNeighbor(current, v);

                    if (!gScore.ContainsKey(neighbour) || ten_gscore.Count < gScore[neighbour].Count)
                    {
                        if (!gScore.ContainsKey(neighbour))
                        {
                            gScore.Add(neighbour, ten_gscore);
                        }
                        
                        
                        gScore[neighbour] = ten_gscore;
                        if (!CheckIfIn(openset, neighbour))
                        {
                            openset.Add(neighbour);
                        }
                    }
                   
                }
            }
            
                return gScore[target];
        }


        public static bool CheckIfIn(List<bool[]> openset, bool[] toCheck)
        {
            foreach (bool[] b in openset)
            {
                if (checkIfEquiv(b, toCheck) == true) return true;
            }
            return false;
        }
        public static bool checkIfEquiv(bool[] set, bool[] toCheck)
        {
            for (int i = 0; i < set.Length; i++)
            {
                if (toCheck[i] != set[i]) return false;
            }
            return true;
        }


        
        public static bool[] getNeighbor(bool[] current, Vector v)
        {
            bool[] neighbor = current.ToArray();
            for(int i = 0; i < current.Length; i++)
            {
                if (v.directions[i])
                {
                    neighbor[i] = !current[i];
                }
                
            }
            return neighbor;
        }

        public static bool[] GetTarget(string input)
        {
            input = input.Trim('[');
            input = input.Trim(']');
            bool[] output = new bool[input.Length];
            for(int i = 0; i < input.Length; i++)
            {
                if (input[i] == '#')
                {
                    output[i] = true;
                }
            }
            return output;
        }

        public static int[] GetTargetInt(string input)
        {
            input = input.Trim('}');
            input = input.Trim('{');
            string[] inputs = input.Split(',');
            int[] output = new int[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                output[i] = int.Parse(inputs[i]);
            }
            return output;
        }
        public static List<Vector> ProcessVectors(string[] input, int length)
        {
            List<Vector> vectors = new List<Vector>();
            for (int i = 1; i < input.Length - 1; i++)
            {
                Vector v = new Vector(length);
                input[i] = input[i].Trim('(');
                input[i] = input[i].Trim(')');
                string[] switches = input[i].Split(',');
                foreach (String s in switches)
                {
                    v.directions[int.Parse(s)] = true;
                }
                vectors.Add(v);
            }
            return vectors;
        }

        
    }

    
    public class IntArrayComparer : IEqualityComparer<int[]>
    {
        public bool Equals(int[]? left, int[]? right)
        {
            if (left == null || right == null)
            {
                return left == right;
            }
            return left.SequenceEqual(right);
        }
        public int GetHashCode(int[] x)
        {
            int result = x.Length;
            foreach (int val in x)
            {
                result = unchecked(result * 17 + val);
            }
            return result;
        }
    }
    public class BoolArrayComparer : IEqualityComparer<bool[]>
    {
        public bool Equals(bool[]? left, bool[]? right)
        {
            if (left == null || right == null)
            {
                return left == right;
            }
            return left.SequenceEqual(right);
        }
        public int GetHashCode(bool[] x)
        {
            int result = 29;
            foreach (bool b in x)
            {
                if (b) { result++; }
                result *= 23;
            }
            return result;
        }
    }
    public class Vector
    {
        public Vector(int size)
        {
            directions = new bool[size];
        }
        public bool[] directions;
    }
}
