using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace CoreLib
{
    public class Tcp
    {
        private TcpClient myTcp;
        private string ipAddress;
        private int port;

        public Tcp(string ip, int port)
        {
            myTcp = new TcpClient();
            this.ipAddress = ip;
            this.port = port;
        }


        /// <summary>
        /// 發送訊息到遠端主機
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Boolean Send(string sendText, out string returnText)
        {
            myTcp.Connect(ipAddress, port);
            NetworkStream networkStream = myTcp.GetStream();
            Byte[] myByte = Encoding.Default.GetBytes(sendText);
            networkStream.Write(myByte, 0, myByte.Length);
            int bufferSize = myTcp.ReceiveBufferSize;
            byte[] myRcvBufferBytes = new Byte[bufferSize];
            networkStream.Read(myRcvBufferBytes, 0, bufferSize);

            string result = Encoding.Default.GetString(myRcvBufferBytes, 0, bufferSize);
            returnText = result;
            return true;

        }


    }
}
