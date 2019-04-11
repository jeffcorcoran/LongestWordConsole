using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LongestWord
{
    class Program
    {
        private static string[] _lines;
        private static string _biggest = string.Empty;

        static void Main(string[] args)
        {
            Console.Write("Run in Parallel? (Y/N):");
            string parallel = Console.ReadLine();

            _lines = FindFile(args);
            
            Stopwatch sw = new Stopwatch();

            if (parallel.ToUpper() == "Y")
            {
                sw.Start();
                RunParallelThreads();
            }
            else
            {
                sw.Start();
                RunSingleThread();
            }

            sw.Stop();

            Console.WriteLine($"Longest Word: {_biggest}");
            Console.WriteLine($"Longest Word Length: {_biggest.Length}");
            Console.WriteLine($"Time Searching: {sw.ElapsedMilliseconds} ms");
            Console.ReadKey();
        }

        static string[] FindFile(string[] args)
        {
            if (args.Length > 0)
            {
                return File.ReadAllLines(args[0]);
            }
            else
            {
                return File.ReadAllLines("wikipediaaa.txt");
            }
        }

        static void RunSingleThread()
        {
            foreach (var line in _lines)
            {
                if (_biggest.Length > line.Length)
                    continue;

                foreach (var word in line.Split())
                {
                    if (word.Length > _biggest.Length)
                    {
                        _biggest = word;
                    }
                }
            }
        }

        static void RunParallelThreads()
        {
            Parallel.ForEach(_lines.Where(x => x.Length > _biggest.Length), line =>
            {
                if (_biggest.Length > line.Length)
                    return;

                foreach (var word in line.Split())
                {
                    if (word.Length > _biggest.Length)
                    {
                        _biggest = word;
                    }
                }
            });
        }
    }
}
