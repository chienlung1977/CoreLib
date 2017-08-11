using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using System.Net;
using System.IO;

namespace CoreLib
{
    public class Ftp
    {
        string ftpUrl;
        string fullName;
        string userName;
        string password;
        byte[] data;

        public Ftp(string ftpUrl, string username, string password)
        {
            //new  Ftp(ftpUrl, 21,username, password );
            this.ftpUrl = ftpUrl;
            this.userName = username;
            this.password = password;
        }

        /// <summary>
        /// 將FTP上的檔案讀到記憶體中
        /// </summary>
        /// <param name="fileName">檔案名稱</param>
        public void downToMemory(string fileName)
        {

            FtpWebResponse response = null;

            if (Text.Right(this.ftpUrl, 1) != "/")
            {
                this.ftpUrl += "/";
            }

            this.fullName = ftpUrl + fileName;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(this.fullName));
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = new NetworkCredential(this.userName, this.password);

            response = (FtpWebResponse)request.GetResponse();

            using (Stream reader = response.GetResponseStream())
            {
                //response.BufferOutput = false;   // to prevent buffering 
                byte[] buffer = new byte[1024];    //每次讀取1024 bytes
                int bytesRead = 0;
                MemoryStream memStream = new MemoryStream();
                while ((bytesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }

                memStream.Position = 0;
                this.data = memStream.ToArray();
                //byte[] downloadedData = memStream.ToArray();                                              
                reader.Close();
                memStream.Close();
                response.Close();
            }

        }


        /// <summary>
        /// 將downloadSingleFile記體中的檔案寫入I/O中
        /// </summary>
        /// <param name="savePath">要儲存的路徑</param>
        /// <param name="filename">要儲存的檔案名稱</param>
        public void saveToFile(string savePath, string filename)
        {

            string mySavePath = savePath;

            //記憶體寫入檔案
            if (Text.Right(mySavePath, 1) != @"\")
            {
                mySavePath += @"\";
            }
            string myFullPath = mySavePath + "\\" + filename;
            FileStream newFile = new FileStream(myFullPath, FileMode.Create);
            newFile.Write(this.data, 0, this.data.Length);
            newFile.Close();
        }





        #region 掃檔方式下載

        /// <summary>
        /// 取得這個資料夾下的檔案清單
        /// </summary>
        /// <returns></returns>
        public string[] getFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;
            try
            {
                FtpWebRequest reqFTP;
                // string myurl = this.ftpUrl + ":" + this.port.ToString();
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(this.ftpUrl));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(this.userName, this.password);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                reqFTP.Proxy = null;
                reqFTP.KeepAlive = false;
                reqFTP.UsePassive = false;
                response = reqFTP.GetResponse();
                reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (response != null)
                {
                    response.Close();
                }
                downloadFiles = null;
                return downloadFiles;
            }
        }




        private void getFile(string file)
        {

            /*
          
                string uri = "";
                //string uri = "ftp://" + ftpServerIP + "/" + remoteDir + "/" + file;
                Uri serverUri = new Uri(uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return;
                }
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + remoteDir + "/" + file));
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Proxy = null;
                reqFTP.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream = new FileStream(localDestnDir + "\" + file, FileMode.Create);                
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0)
                {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();
           
           
    */

        }


        #endregion 

        /// <summary>
        /// 在資料流中直接解壓縮
        /// </summary>
        /// <param name="zipMemStream">壓縮的資料流</param>
        /// <param name="password">解壓縮密碼</param>
        /// <returns>解壓縮後的資料流</returns>
        private static MemoryStream DeCompressionToStreamByPassword(MemoryStream zipMemStream, string password)
        {

            MemoryStream outputUnzipMemStream = new MemoryStream();
            //Console.WriteLine("ZipInutStream Size = " + zipMemStream.Length);
            ZipInputStream zipStream = new ZipInputStream(zipMemStream);
            zipStream.Password = password;

            //需要透過ZipEntry此類別才能對Zip Input/Output Stream作壓縮解壓縮動作
            ZipEntry entry = zipStream.GetNextEntry();
            StreamUtils.Copy(zipStream, outputUnzipMemStream, new byte[4096]);

            return outputUnzipMemStream;

        }



    }
}
