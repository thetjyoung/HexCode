using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HexCode
{
    public class CodeLogic
    {
        private Dictionary<string, string> Mappings = new Dictionary<string, string>();
 
        public CodeLogic(Dictionary<string, string> mappings)
        {
            Mappings = mappings;
        }

        public int AnalyzeCode(string code, int index)
        {
            var retval = 0;
            //check 1 character code: if end of code increment count by returning 1 else recurse
            if (Mappings.ContainsKey(code.Substring(index, 1)))
            { retval += index + 1 < code.Length ? AnalyzeCode(code, index + 1) : 1; }

            //check 2 character code: if end of code increment count by returning 1 else recurse
            if (index + 1 < code.Length && Mappings.ContainsKey(code.Substring(index, 2)))
            { retval += index + 2 < code.Length ? AnalyzeCode(code, index + 2) : 1; }

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
            throw new NotImplementedException();
        }
    }
}
