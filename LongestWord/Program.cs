using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LongestWord
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] lines;

            Console.Write("Run in Parallel? (Y/N):");
            string parallel = Console.ReadLine();

            if (args.Length > 0)
            {
                lines = File.ReadAllLines(args[0]);
            }
            else
            {
                lines = File.ReadAllLines("wikipediaaa.txt");
            }
             
            string biggest = string.Empty;

            Stopwatch sw = new Stopwatch();

            if (parallel.ToUpper() == "Y")
            {                
                sw.Start();

                Parallel.ForEach(lines, line =>
                {
                    foreach (var word in line.Split())
                    {
                        if (word.Length > biggest.Length)
                        {
                            biggest = word;
                        }
                    }
                });
            }
            else
            {
                sw.Start();

                foreach (var line in lines)
                {
                    foreach (var word in line.Split())
                    {
                        if (word.Length > biggest.Length)
                        {
                            biggest = word;
                        }
                    }
                }
            }

            sw.Stop();

            Console.WriteLine($"Longest Word: {biggest}");
            Console.WriteLine($"Longest Word Length: {biggest.Length}");
            Console.WriteLine($"Time Searching: {sw.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }
    }
}
