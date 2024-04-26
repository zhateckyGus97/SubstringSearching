using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SubstringSearchLibrary
{
    public class BruteForceSearching : ISubstringSearch
    {
        List<int> Positions = new List<int>();

        public BruteForceSearching() { }

        public List<int> FindSubstring(string text, string pattern)
        {
            char[] buffer = new char[pattern.Length];
            
            for(int  i = 0; i <= text.Length - pattern.Length; i++)
            {
                if (text.Substring(i, pattern.Length) == pattern)
                {
                    Positions.Add(i);
                }
            }

            return Positions;
        }
    }

    public class RabinKarpSearching : ISubstringSearch
    {
        public RabinKarpSearching() { }

        public List<int> FindSubstring(string text, string pattern)
        {


            return null;
        }
    }

    public class BouyerMoorSearching : ISubstringSearch
    {
        public BouyerMoorSearching() { }

        public List<int> FindSubstring(string text, string pattern)
        {

            return null;
        }
    }

    public class KnutMorrisPratSearching : ISubstringSearch
    {
        public KnutMorrisPratSearching() { }

        public List<int> FindSubstring(string text, string pattern)
        {


            return null;
        }
    }
}
