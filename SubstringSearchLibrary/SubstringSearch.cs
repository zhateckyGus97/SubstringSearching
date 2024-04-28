using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.ComTypes;
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

            for (int i = 0; i <= text.Length - pattern.Length; i++)
            {
                if (text.Substring(i, pattern.Length) == pattern)
                {
                    Positions.Add(i);
                }
            }

            return Positions;
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
                    for (int j = 0; j < SymbolsDisplacement.Count; j++)
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
        int[] PrefixArray;

        public KnutMorrisPratSearching() { }

        public List<int> FindSubstring(string text, string pattern)
        {
            List<int> result = new List<int>();
            PrefixArray = new int[pattern.Length];
            PrefixArray = PrefixFunction(pattern);
            int k = 0;
            for(int i=0;i<text.Length;i++)
            {
                while (k > 0 && pattern[k] != text[i])
                {
                    k = PrefixArray[k-1];
                }
                if (text[i] == pattern[k])
                {
                    k+=1;
                }
                if(k==pattern.Length)
                {
                    result.Add(i-pattern.Length+1);
                    k = 0;
                }
            }
            return result;
        }
        private int[] PrefixFunction(string text)
        { 
            int n= text.Length;
            int[] Func = new int[n];
            for (int i = 1;i<n;i++)
            {
                int k = Func[i-1];
                while (k > 0 && text[i] != text[k])
                {
                    k = Func[k - 1];
                }
                if (text[k] == text[i])
                {
                    k += 1;
                }
                Func[i] = k;
            }
            return Func;
        }
    }
    public class RabinKarpSearching : ISubstringSearch
    {
        private readonly GetPrimeNumber _primeNumber = new GetPrimeNumber();
        private int d = 0;//размерность алфавита
        private int q = 0;//значение для ХЭША
        private int p = 0;//код паттерна
        private int t = 0;//код подстроки текста
        private int n = 0;
        private int m = 0;
        private int h = 0;
        private Dictionary<char, int> Alphabet = new Dictionary<char, int>();
        public RabinKarpSearching() 
        {
            q = _primeNumber.Min();
        }

        public List<int> FindSubstring(string text, string pattern)
        {
            List<int>Answer=new List<int>();
            m=pattern.Length;
            n=text.Length;
            FillAlphabet(text);
            h=Convert.ToInt32((Math.Pow(d,m-1)))%q;
            for(int i=0;i<m;i++)
            {
                p += (d * p + Alphabet[pattern[i]]) % q;
                t += (d * t + Alphabet[text[i]]) % q;
            }
            for(int s=0;s<n-m+1;s++)
            {
                if(p==t)
                {
                    string subst = "";
                    for(int i=0;i<m;i++)
                    {
                        subst += text[i+s];
                    }
                    if(subst==pattern)
                    {
                        Answer.Add(s);
                    }
                }
                if(s<n-m)
                {
                    #region 1 способ
                    //t = int.Parse(((d * (t - Alphabet[text[s + 1]] * h)).ToString() + Alphabet[text[s + m]].ToString())) % q;
                    #endregion
                    #region 2 способ
                    t = 0;
                    for (int i = s + 1; i < s + m + 1; i++)
                    {
                        t += (d * t + Alphabet[text[i]]) % q;
                    }
                    #endregion
                    #region 3 способ
                    //t = 10 * (s - Convert.ToInt32(Math.Pow(10, m - 1)) * Alphabet[text[s + 1]]) + Alphabet[text[s + m]];
                    #endregion
                }
            }
            return Answer;
        }
        private void FillAlphabet(string text)
        {
            int cnt = 0;
            foreach (char c in text)
            {
                if (!Alphabet.ContainsKey(c))
                {
                    Alphabet.Add(c, cnt);
                    cnt++;
                    if(cnt>q)
                    {
                        q=_primeNumber.Next();
                    }
                }
            }
            d = Alphabet.Count();
        }
    }
    public class GetPrimeNumber
    {
        public int _current;
        readonly int[] _primes = { 11, 29, 61, 127, 257, 523, 1087,
            2213, 4519, 9619, 19717, 40009, 62851, 75431, 90523,
            108631, 130363, 156437,  187751, 225307, 270371, 324449,
            389357, 467237, 560689, 672827, 807403, 968897,
            1162687, 1395263, 1674319, 2009191, 2411033, 2893249, 3471899, 4166287,
            4999559, 5999471, 7199369};
        public int Min() 
        {
            _current = 0;
            return _primes[_current];
        }
        public int Next()
        {
            if (_current < _primes.Length)
            {
                var value = _primes[_current];
                _current++;
                return value;
            }
            _current++;
            return (_current - _primes.Length) * _primes[_primes.Length - 1];
        }
    }
}
