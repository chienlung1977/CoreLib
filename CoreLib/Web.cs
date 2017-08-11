using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Security.Cryptography;

namespace CoreLib
{
    public class Web
    {

        public static void Alert(Page page, string msg)
        {

            page.ClientScript.RegisterClientScriptBlock(page.GetType(), new Guid().ToString(), "<script>alert('" + msg + "');</script>");

        }

        public static void Alert(Page page, string msg, string url)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), new Guid().ToString(), "<script>alert('" + msg + "');location.href='" + url + "'</script>");
        }


        /// <summary>
        /// MD5密碼字串加密
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private static string getComputeHash(string password)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
            try
            {
                byte[] inputData = md5Hasher.ComputeHash(System.Text.Encoding.Default.GetBytes(password));
                StringBuilder sBuilder = new StringBuilder(inputData.Length);

                for (int i = 0; i < inputData.Length; i++)
                {
                    sBuilder.Append(inputData[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

    }
}
