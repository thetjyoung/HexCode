using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HexCode
{
    public class CodeLogic
    {
        private Dictionary<string, string> Mappings = new Dictionary<string, string>();
        List<string> dict = new List<string>(); 
 
        public CodeLogic(Dictionary<string, string> mappings)
        {
            Mappings = mappings;
            LoadDictionary();
        }

        private void LoadDictionary()
        {
            string[] lines = System.IO.File.ReadAllLines(@"..\..\..\dictionary.txt");
            foreach (var line in lines)
            {
                dict.Add(line.ToLower().Trim());
            }
        }

        public int AnalyzeCode(string code)
        {
            return AnalyzeRecursively(code, 0);
        }

        private int AnalyzeRecursively(string code, int index)
        {
            var retval = 0;
            //check 1 character code: if end of code increment count by returning 1 else recurse
            if (Mappings.ContainsKey(code.Substring(index, 1)))
            { retval += index + 1 < code.Length ? AnalyzeRecursively(code, index + 1) : 1; }

            //check 2 character code: if end of code increment count by returning 1 else recurse
            if (index + 1 < code.Length && Mappings.ContainsKey(code.Substring(index, 2)))
            { retval += index + 2 < code.Length ? AnalyzeRecursively(code, index + 2) : 1; }

            return retval;
        }

        public List<string> Encode(string message)
        {
            var codes = new List<string>();
            var words = message.ToLower().Split(null);
            foreach (var word in words)
            {
                try
                {
                    string tmpCode = "";
                    for (int i = 0; i < word.Length; i++)
                    {
                        var myChar = Mappings.FirstOrDefault(x => x.Value == word[i].ToString()).Key;
                        if (myChar != null)
                        {
                            tmpCode += myChar;
                        }
                        else
                        {
                            throw new InvalidDataException(word);
                        }
                    }
                    codes.Add(tmpCode);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Invalid input:" + ex.Message);
                }
            }
            return codes;
        }

        public string Decode(string Code)
        {
            List<string> words = GetAllPermutations(Code, 0, "", new List<string>());
            foreach (var word in words)
            {
                if (dict.Contains(word))
                {
                    return word;
                }
            }
            return words[0];
        }

        private List<string> GetAllPermutations(string code, int index, string word, List<string> words)
        {
            //check 1 character code: if end of code increment count by returning 1 else recurse
            if (Mappings.ContainsKey(code.Substring(index, 1)))
            {
                if (index + 1 < code.Length)
                {
                    GetAllPermutations(code, index + 1, (word + Mappings[code.Substring(index, 1)]), words);
                }
                else
                {
                    words.Add(word + Mappings[code.Substring(index, 1)]);
                }
            }

            //check 2 character code: if end of code increment count by returning 1 else recurse
            if (index + 1 < code.Length && Mappings.ContainsKey(code.Substring(index, 2)))
            {
                if (index + 2 < code.Length)
                {
                    GetAllPermutations(code, index + 2, (word + Mappings[code.Substring(index, 2)]), words);
                }
                else
                {
                    words.Add(word + Mappings[code.Substring(index, 2)]);
                }
            }
            return words;
        }
    }
}
