using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.IO;

namespace CoreLib
{
    #region 底層

    public class Log
    {

    }

    #endregion

    #region DbLog_Area

    public class DbLog : Log
    {

    }

    #endregion


    #region FileLog_Area

    /// <summary>
    /// 將錯誤記錄記在檔案中
    /// </summary>
    public class FileLog : Log
    {

        static string fileName;
        static string fullMsg;
        static string filePath;

        public enum LEVEL { INFO, WARN,ERROR };

        /// <summary>
        /// 記錄意外錯誤
        /// </summary>
        /// <param name="error"></param>
        public static void LogError(Exception error)
        {
            string fullMsg = error.Source + ":" + error.Message + "\n\n" + error.ToString();
            LogMsg(LEVEL.ERROR,fullMsg);
        }


        /// <summary>
        /// 預設為INFO層級
        /// </summary>
        /// <param name="msg"></param>
        public static void LogMsg(string msg)
        {
            LogMsg(LEVEL.INFO, msg);
        }

        /// <summary>
        /// 自訂警告層級
        /// </summary>
        /// <param name="level"></param>
        /// <param name="msg"></param>
        public static void LogMsg(LEVEL level, string msg)
        {

            fileName = DateTime.Now.ToString("yyyyMMdd") + "_LOG.txt";
            string space = "\t";
            fullMsg = level.ToString() + space.PadLeft(2) + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + space.PadLeft(2) + msg;
            filePath = System.AppDomain.CurrentDomain.BaseDirectory + fileName;
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(fullMsg);
                sw.Close();
            }

        }

       
    }

    /// <summary>
    /// 提供給有過程的Log，例如：上傳或下載
    /// </summary>
    public class ProcessLog
    {

        private string fileName;
        private string filePath;
        private string fullMsg;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName">LOG的檔案名稱</param>
        public ProcessLog(string fileName)
        {

            this.fileName = fileName;

        }


        private void logMsg(string msg)
        {

            filePath = System.AppDomain.CurrentDomain.BaseDirectory + this.fileName;
            using (StreamWriter sw = new StreamWriter(filePath, true))
            {
                sw.WriteLine(msg);
                sw.Close();
            }
        }

        public void startLog()
        {
            this.startLog("開始執行");
        }

        public void startLog(string info)
        {
            string line = Strings.StrDup(30, "=");
            logMsg(line);
            logMsg(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + info);
            logMsg(line);
        }


        public void logMessage(string msg)
        {
            fullMsg = "INFO_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + msg;
            logMsg(msg);
        }

        public void logError(Exception ex)
        {
            fullMsg = "ERROR_" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ":" + ex.Message + "\n\n\r" + ex.ToString();
            logMsg(fullMsg);
        }


        public void endLog()
        {
            string line = Strings.StrDup(30, "=");
            logMsg(line);
            logMsg(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " 結束執行");
            logMsg(line);

        }

    }

    #endregion




}
