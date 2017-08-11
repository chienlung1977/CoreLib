using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace CoreLib
{
    public class Config
    {

        /// <summary>
        /// 取得appSettings的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string getAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 取得connectionStringSeciion中的連線字串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string getConnectionString(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ConnectionString; 
        }

    }
}
