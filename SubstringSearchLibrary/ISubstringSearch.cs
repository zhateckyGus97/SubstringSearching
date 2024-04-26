using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubstringSearchLibrary
{
    public interface ISubstringSearch
    {
        List<int> FindSubstring(string text, string pattern);
    }
}
