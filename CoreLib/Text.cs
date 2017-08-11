using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public class Text
    {

        #region 取字元


        /// <summary>
        /// 從右邊取幾個字元
        /// </summary>
        /// <param name="text"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string Right(string text, int len)
        {
            return text.Substring(text.Length - len);
        }

        /// <summary>
        /// 取字串中文字算成兩個byte
        /// </summary>
        /// <param name="line"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SubStringB(string line, int start, int length)
        {
            byte[] lineB = System.Text.Encoding.Default.GetBytes(line);
            string result = System.Text.Encoding.Default.GetString(lineB, start - 1, length);
            return result;
        }

        #endregion



    }
}
