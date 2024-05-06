using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using SubstringSearchLibrary;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

namespace SubstringSearchUnitTests
{
    [TestClass]
    public class SearchingLibTests
    {
        [TestMethod]
        public void SearchTest()
        {
            var algms = new List<ISubstringSearch>()
            {
                new BouyerMoorSearching(),
                new BruteForceSearching(),
                new RabinKarpSearching(),
                new KnutMorrisPratSearching()
            };
            string text = "aaaaaaaaaa"; //10
            string pattern = "aa";
            var expected = Enumerable.Range(0, 9).ToList();
            foreach (var algm in algms)
            {
                var actual = algm.FindSubstring(text, pattern);
                CollectionAssert.AreEqual(expected, actual);
            }
        }

        [TestMethod]
        public void SearchBagOfWordsOnAnnaTxt()
        {
            var algms = new List<ISubstringSearch>()
            {
                new BouyerMoorSearching(),
                new BruteForceSearching(),
                new RabinKarpSearching(),
                new KnutMorrisPratSearching()
            };

            string text;
            using (var sr = new StreamReader("anna.txt"))
            {
                text = sr.ReadToEnd().ToLower();
            }

            int number = 150;
            Regex rg = new Regex(@"\w+");
            var bag = new HashSet<string>();
            var matches = rg.Matches(text);
            foreach (var match in matches)
            {
                bag.Add(match.ToString());
                if (bag.Count > number) break;
            }
            foreach (var pattern in bag)
            {
                var BF = new BruteForceSearching();
                var expected = BF.FindSubstring(text, pattern);
                foreach (var algm in algms)
                {
                    var actual = algm.FindSubstring(text, pattern);
                    CollectionAssert.AreEqual(expected, actual);
                }
            }
        }

        [TestMethod]
        public void NotExistingPattern()
        {
            var algms = new List<ISubstringSearch>()
            {
                new BouyerMoorSearching(),
                new BruteForceSearching(),
                new RabinKarpSearching(),
                new KnutMorrisPratSearching()
            };

            string text;
            string pattern = "jhsdgfushdfshddfguhge";
            using (var sr = new StreamReader("anna.txt"))
            {
                text = sr.ReadToEnd().ToLower();
            }
            int expected = 0;

            foreach (var algm in algms)
            {
                var actual = algm.FindSubstring(text, pattern);
                Assert.AreEqual(expected, actual.Count);
            }
        }

        [TestMethod]
        public void EmptyPattern()
        {
            var algms = new List<ISubstringSearch>()
            {
                new BouyerMoorSearching(),
                new BruteForceSearching(),
                new RabinKarpSearching(),
                new KnutMorrisPratSearching()
            };

            string text;
            string pattern = "";
            using (var sr = new StreamReader("anna.txt"))
            {
                text = sr.ReadToEnd().ToLower();
            }
            int expected = 0;

            foreach (var algm in algms)
            {
                var actual = algm.FindSubstring(text, pattern);
                Assert.AreEqual(expected, actual.Count);
            }
        }
    }
}
