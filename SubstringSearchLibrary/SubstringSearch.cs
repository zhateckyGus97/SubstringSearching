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
        List<KeyValuePair<char, int>> SymbolsDisplacement = new List<KeyValuePair<char, int>>();
        Dictionary<string, int> SufficsDisplacement = new Dictionary<string, int>();

        public BouyerMoorSearching() { }

        private void DisplacementSymbolsTableCreating(string pattern)
        {
            bool IsContain = false;
            char c;
            for (int i = pattern.Length - 2; i >= 0; i--)
            {
                IsContain = false;
                c = pattern[i];
                foreach (var item in SymbolsDisplacement)
                {
                    if (item.Key == c)
                    {
                        SymbolsDisplacement.Insert(0, new KeyValuePair<char, int>(c, item.Value));
                        IsContain = true;
                        break;
                    }
                }
                if (!IsContain)
                {
                    SymbolsDisplacement.Insert(0, new KeyValuePair<char, int>(c, pattern.Length - i - 1));
                }
            }

            // проводим такую же операцию для последнего символа в паттерне
            IsContain = false;
            c = pattern[pattern.Length - 1];
            foreach (var item in SymbolsDisplacement)
            {
                if (item.Key == c)
                {
                    SymbolsDisplacement.Add(new KeyValuePair<char, int>(c, item.Value));
                    IsContain = true;
                    break;
                }
            }
            if (!IsContain)
            {
                SymbolsDisplacement.Add(new KeyValuePair<char, int>(c, pattern.Length));
            }
        }

        private void DisplacementSufficsTableCreating(string pattern)
        {
            bool IsContain = false;
            string suffics = "";
            int suffics_len = 1;

            for (int i = pattern.Length - 1; i >= 0; i--) // выделяем каждый суффикс
            {
                suffics = pattern.Substring(i, suffics_len);
                IsContain = false;

                for (int j = i - 1; j >= 0; j--)
                {
                    if (pattern.Substring(j, suffics_len) == suffics)
                    {
                        SufficsDisplacement.Add(suffics, i - j);
                        IsContain = true; 
                        break;
                    }
                }

                if (!IsContain)
                {
                    SufficsDisplacement.Add(suffics, pattern.Length);
                }

                suffics_len++; // увеличиаем длину суффикса
            }

        }

        public List<int> FindSubstring(string text, string pattern)
        {
            int match_cnt = 0, pattern_len = pattern.Length, pattern_pos = pattern_len - 1;
            bool IsJump = false;
            string current_suffics;
            DisplacementSymbolsTableCreating(pattern);
            DisplacementSufficsTableCreating(pattern);

            for (int i = pattern.Length - 1; i < text.Length;)
            {
                IsJump = false;
                match_cnt = 0;
                pattern_pos = pattern_len - 1;
                current_suffics = "";
                while (i >= 0 && pattern_pos >= 0 && text[i] == pattern[pattern_pos]) // пока буква теккста совпадает с буквой паттерна
                {
                    match_cnt++;
                    current_suffics = current_suffics.Insert(0, text[i].ToString());
                    i--;
                    pattern_pos--;
                }

                if (match_cnt == pattern.Length) // совпадение найдено
                {
                    Positions.Add(i);
                    i += pattern_len + 1;
                }
                else
                {
                    for(int j = 0; j < SymbolsDisplacement.Count; j++)
                    {
                        if (text[i] == SymbolsDisplacement[j].Key)
                        {
                            if (!SufficsDisplacement.ContainsKey(current_suffics)) // если нету такого суффикса в словаре
                                i += SymbolsDisplacement[j].Value;
                            else
                                i += Math.Max(SymbolsDisplacement[j].Value, SufficsDisplacement[current_suffics]);
                            IsJump = true;
                            break;
                        }
                    }
                    if (!IsJump) // если не нашли символ из смещений
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
