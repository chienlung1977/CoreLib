using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

namespace CoreLib
{
    /// <summary>
    /// 驗證用
    /// </summary>
    public class Valid
    {
        /// <summary>
        /// 將字串回傳只有數字的內容
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string GetNumber(string word)
        {
            return Regex.Replace(word, "[\\D_]+", "");
        }

        /// <summary>
        /// 只回傳合法字串
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public static string GetString(string word)
        {
            return Regex.Replace(word, "[\\W_]+", "");
        }


    }
}
