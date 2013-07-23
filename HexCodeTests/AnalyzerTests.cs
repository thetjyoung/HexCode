using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HexCode;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace HexCodeTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AnalyzerTests
    {
        private static readonly Dictionary<string, string> Mappings = new Dictionary<string, string>()
            {
                { "1", "a" }, { "2", "b" }, { "3", "c" }, { "4", "d" }, { "5", "e" }, { "6", "f" }, { "7", "g" }, { "8", "h" }, { "9", "i" }, { "a", "j" }, { "b", "k" }, { "c", "l" }, { "d", "m" }, { "e", "n" },
                { "f", "o" }, { "10", "p" }, { "11", "q" }, { "12", "r" }, { "13", "s" }, { "14", "t" }, { "15", "u" }, { "16", "v" }, { "17", "w" }, { "18", "x" }, { "19", "y" }, { "1a", "z" }
            };

        private static string Message = "The Quick Brown Fox Jumped Over The Ravenous Running Rabbit On His Longboard Crazy";

        private static readonly List<string> CodedMessage = new List<string>()
            { "1485", "111593b", "212f17e", "6f18", "a15d1054", "f16512", "1485", "121165ef1513", "1215ee9e7", "12122914", "fe", "8913", "cfe72f1124", "31211a19" };

        private static readonly List<int> Permutations = new List<int>()
            { 2, 5, 4, 2, 2, 4, 2, 24, 4, 8, 1, 2, 3, 12 };

        [TestMethod]
        public void TestAnalyzer()
        {
            var analyzer = new CodeLogic(Mappings);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < CodedMessage.Count; i++)
                {
                    int tmpVal = analyzer.AnalyzeCode(CodedMessage[i]);
                    Assert.AreEqual(Permutations[i], tmpVal);
                }
            }
            stopwatch.Stop();
            var ts = stopwatch.Elapsed;
            
        }

        [TestMethod]
        public void TestEncoding()
        {
            var analyzer = new CodeLogic(Mappings);

            var codes = analyzer.Encode(Message);

            for (int i = 0; i < codes.Count; i++)
            {
                Assert.AreEqual(CodedMessage[i], codes[i]);   
            }
        }

        [TestMethod]
        public void TestEncodeInvalidDataException()
        {
            try
            {
                var analyzer = new CodeLogic(Mappings);
                var retval = analyzer.Encode("&7984093");
                Assert.Fail();
            }
            catch (InvalidDataException exc)
            {
                Assert.AreEqual("HexCode", exc.Source);
            }
        }

        [TestMethod]
        public void TestDecoding()
        {
            var analyzer = new CodeLogic(Mappings);
            var result = analyzer.Decode("111593b");
            Assert.AreEqual("quick", result);

            var result2 = analyzer.Decode("31211a19");
            Assert.AreEqual("crazy", result2);

            var result3 = analyzer.Decode(analyzer.Encode("abcdefghij")[0]);
            Assert.AreEqual("abcdefghij", result3);
        }
    }
}
