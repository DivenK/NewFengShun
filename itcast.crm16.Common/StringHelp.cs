using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace itcast.crm16.Common
{
   public static class StringHelp
    {
        public static string GetContent(this string conters)
        {
            System.Text.RegularExpressions.Regex regImg = new Regex(@"<(.[^>]*)/>", RegexOptions.IgnoreCase);
            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(conters);
            foreach (var item in matches)
            {
               var ss= conters.Replace(item.ToString(),string.Empty);
                conters = ss;
            }
        
            return conters.Trim();
        }
        public static string Quot(this string _input, string _quot)
        {
            return _quot + _input + _quot;
        }

        public static DateTime First(this DateTime current)
          {
          var first = current.AddDays(1 - current.Day);
            return first;
         }
}
}
