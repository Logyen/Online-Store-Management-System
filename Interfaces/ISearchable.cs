using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStore
{
    internal interface ISearchable
    {
         bool MatchesSearch(string keyword);
    }
}
