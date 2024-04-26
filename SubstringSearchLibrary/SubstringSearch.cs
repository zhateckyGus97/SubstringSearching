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
        List<int> Positions = new List<int>();
        List<KeyValuePair<char, int>> displacement = new List<KeyValuePair<char, int>>();
    
        public BouyerMoorSearching() { }

        private void DisplacementTableCreating(string pattern)
        {
            bool IsContain = false;
            char c;
            for (int i = pattern.Length - 2; i >= 0; i--)
            {
                IsContain = false;
                c = pattern[i];
                foreach (var item in displacement)
                {
                    if (item.Key == c)
                    {
                        displacement.Insert(0, new KeyValuePair<char, int>(c, item.Value));
                        IsContain = true;
                        break;
                    }
                }
                if (!IsContain)
                {
                    displacement.Insert(0, new KeyValuePair<char, int>(c, pattern.Length - i - 1));
                }
            }

            IsContain = false;
            c = pattern[pattern.Length - 1];
            foreach (var item in displacement)
            {
                if (item.Key == c)
                {
                    displacement.Add(new KeyValuePair<char, int>(c, item.Value));
                    IsContain = true;
                    break;
                }
            }
            if (!IsContain)
            {
                displacement.Add(new KeyValuePair<char, int>(c, pattern.Length));
            }
        }

        public List<int> FindSubstring(string text, string pattern)
        {
            int match_cnt = 0, pattern_len = pattern.Length, pattern_pos = pattern_len - 1;
            bool IsJump = false;
            DisplacementTableCreating(pattern);

            for(int i = pattern.Length - 1; i < text.Length;)
            {
                IsJump = false;
                match_cnt = 0;
                pattern_pos = pattern_len - 1;
                while (i >= 0 && pattern_pos >= 0 && text[i] == pattern[pattern_pos])
                {
                    match_cnt++;
                    i--;
                    pattern_pos--;
                }

                if (match_cnt == pattern.Length)
                {
                    Positions.Add(i);
                    i += pattern_len + 1;
                }
                else
                {
                    for(int j = 0; j < displacement.Count; j++)
                    {
                        if (text[i] == displacement[j].Key)
                        {
                            i += displacement[j].Value ;
                            IsJump = true;
                            break;
                        }
                    }
                    if (!IsJump)
                    {
                        i += pattern_len;
                    }
                }
            }

            return Positions;
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
