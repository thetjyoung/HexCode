using System;
using System.Collections.Generic;

namespace HexCode
{
    class Program
    {
        private static readonly Dictionary<string,string> Mappings = new Dictionary<string, string>()
            {
                {"1", "a"}, {"2", "b"}, {"3", "c"}, {"4", "d"}, {"5", "e"}, {"6", "f"}, {"7", "g"}, {"8", "h"}, {"9", "i"}, {"a", "j"}, {"b", "k"}, {"c", "l"}, {"d", "m"}, {"e", "n"},
                {"f", "o"}, {"10", "p"}, {"11", "q"}, {"12", "r"}, {"13", "s"}, {"14", "t"}, {"15", "u"}, {"16", "v"}, {"17", "w"}, {"18", "x"}, {"19", "y"}, {"1a", "z"}
            };
        static void Main(string[] args)
        {
            var codes = new List<string>();
            Console.WriteLine("Input Codes For Analysis:\n");

            while (true)
            {
                string input = Console.ReadLine().ToLower().Trim();
                if (input == "0")
                {
                    Console.WriteLine("\nCode Analysis:\n");
                    foreach (var code in codes)
                    {
                        Console.WriteLine(AnalyzeCode(code, 0));
                    }
                    break;
                }
                codes.Add(input);
            }

            Console.WriteLine("\nPress Any Key To Exit....");
            Console.ReadKey();
        }

        private static int AnalyzeCode(string code, int index)
        {
            var retval = 0;
            //check 1 character code
            if (Mappings.ContainsKey(code.Substring(index, 1)))
            {
                //if end of code increment count by returning 1 else recurse
                retval += index + 1 < code.Length ? AnalyzeCode(code, index + 1) : 1;
            }
            //check 2 character code
            if (index + 1 < code.Length)
            {
                if (Mappings.ContainsKey(code.Substring(index, 2)))
                {
                    //if end of code increment count by returning 1 else recurse
                    retval += index + 2 < code.Length ? AnalyzeCode(code, index + 2) : 1;
                }
            }
            return retval;
        }
    }
}
