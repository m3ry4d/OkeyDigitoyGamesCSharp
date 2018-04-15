using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OkeyDigitoyGames
{
    internal  class SortByNumber : IComparer<Stone>
    {

        public  int Compare(Stone x, Stone y)
        {
            return x.Number.CompareTo(y.Number);
        }
    }
}
