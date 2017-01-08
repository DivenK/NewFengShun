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

        /// <summary>
        /// 过滤HTML标签的元素
        /// </summary>
        /// <param name="conters"></param>
        /// <returns></returns>
        public static string GetContent(this string conters)
        {
            System.Text.RegularExpressions.Regex regImg = new Regex(@"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>", RegexOptions.IgnoreCase);
            // 搜索匹配的字符串 
            MatchCollection matches = regImg.Matches(conters);
            foreach (var item in matches)
            {
                var ss = conters.Replace(item.ToString(), string.Empty); 
                conters = ss;
            }
            var result = conters.Replace("&nbsp;", string.Empty).Replace("&nbsp",string.Empty);
 
            return result.Trim();
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

        public static string ToyyyyMMddDateTime(this DateTime current)
        {
            return current.ToString("yyyy-MM-dd");
        }

        public static bool IsEmpty(this string Content)
        {
            if (string.IsNullOrWhiteSpace(Content))
            {
                return true;
            }
            return false;
        }

        public static string GetSubValue(this string content, int length)
        {
            if (length>=content.Length)
            {
                return content;
            }
            return content.Substring(0, length).ToString();
        }
    }
}
