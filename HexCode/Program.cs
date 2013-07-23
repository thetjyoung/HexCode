using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace HexCode
{
    [ExcludeFromCodeCoverage]
    class Program
    {
        private static readonly Dictionary<string,string> Mappings = new Dictionary<string, string>()
            {
                {"1", "a"}, {"2", "b"}, {"3", "c"}, {"4", "d"}, {"5", "e"}, {"6", "f"}, {"7", "g"}, {"8", "h"}, {"9", "i"}, {"a", "j"}, {"b", "k"}, {"c", "l"}, {"d", "m"}, {"e", "n"},
                {"f", "o"}, {"10", "p"}, {"11", "q"}, {"12", "r"}, {"13", "s"}, {"14", "t"}, {"15", "u"}, {"16", "v"}, {"17", "w"}, {"18", "x"}, {"19", "y"}, {"1a", "z"}
            };
        
        static void Main(string[] args)
        {
            var analyzer = new CodeLogic(Mappings);
            var stopWatch = new Stopwatch();
            Console.WriteLine("Input A Message For Analysis:\n");
            string input = Console.ReadLine()
                .ToLower()
                .Trim();
            input = Regex.Replace(input, @"[^\w\s]", "");

            stopWatch.Start();
            var codes = analyzer.Encode(input);
            Console.WriteLine("\n{0,-12}{1,8}","Code","Decode Permutations");
            Console.WriteLine("-------------------------------");
            foreach (var code in codes)
            {
                var totalCount = analyzer.AnalyzeCode(code);
                Console.WriteLine("{0,-12}{1,8}", code, totalCount);
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            Console.WriteLine("\nAnalysis Completed in: {0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);

            //while (true)
            //{
            //    string input = Console.ReadLine().ToLower().Trim();
            //    if (input == "0")
            //    {
            //        Console.WriteLine("\nCode Analysis:\n");
            //        stopWatch.Start();
            //        foreach (var code in codes)
            //        { Console.WriteLine(analyzer.AnalyzeCode(code, 0)); }
            //        stopWatch.Stop();
            //        TimeSpan ts = stopWatch.Elapsed;
            //        Console.WriteLine("Analysis Completed in: {0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            //        break;
            //    }
            //    codes.Add(input);
            //}

            Console.WriteLine("\nAttempted Decode:");
            foreach (var code in codes)
            {
                Console.Write(analyzer.Decode(code) + " ");
            }

            Console.WriteLine("\n\nPress Any Key To Exit....");
            Console.ReadKey();
        }
    }
}
