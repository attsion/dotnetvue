using System;
using System.Collections.Generic;
using System.Text;

namespace EEQG.Tools
{
    public static class UtilExtensions
    {
        public static string ToSplitString(this IEnumerable<string> ls, char splite)
        {
            return string.Join(splite,ls);
        }
       
    }
}
