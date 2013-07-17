using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexCode
{
    class Program
    {
        private static Dictionary<string,string> Mappings = new Dictionary<string, string>()
            {
                {"1", "a"},
                {"2", "b"},
                {"3", "c"},
                {"4", "d"},
                {"5", "e"},
                {"6", "f"},
                {"7", "g"},
                {"8", "h"},
                {"9", "i"},
                {"a", "j"},
                {"b", "k"},
                {"c", "l"},
                {"d", "m"},
                {"e", "n"},
                {"f", "o"},
                {"10", "p"},
                {"11", "q"},
                {"12", "r"},
                {"13", "s"},
                {"14", "t"},
                {"15", "u"},
                {"16", "v"},
                {"17", "w"},
                {"18", "x"},
                {"19", "y"},
                {"1a", "z"}
            };
        static void Main(string[] args)
        {
            var codes = new List<string>();
            Console.WriteLine("Input Codes For Analysis:\n");
            while (true)
            {
                string tmp = Console.ReadLine().ToLower().Trim();
                if (tmp == "0")
                {
                    Console.WriteLine("\nCode Analysis:\n");
                    foreach (var code in codes)
                    {
                        int totalPermutations = AnalyzeCode(code, 0);
                        Console.WriteLine(totalPermutations);
                    }
                    break;
                }
                codes.Add(tmp);
            }

            Console.WriteLine("\nPress Enter To Exit....");
            Console.Read();
        }

        private static int AnalyzeCode(string code, int index)
        {
            int retval = 0;
            string tmp1 = code[index].ToString();
            if (Mappings.ContainsKey(tmp1))
            {
                if (index + 1 < code.Length)
                {
                    retval += AnalyzeCode(code, index + 1);
                }
                else
                {
                    retval++;
                }
            }

            if (index + 1 < code.Length)
            {
                string tmp2 = tmp1 + code[index + 1];
                if (Mappings.ContainsKey(tmp2))
                {
                    if (index + 2 < code.Length)
                    {
                        retval += AnalyzeCode(code, index + 2);
                    }
                    else
                    {
                        retval++;
                    }
                }
            }
            return retval;
        }
    }
}
