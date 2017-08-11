using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Xml;

namespace CoreLib
{
    public class Xml
    {
        XmlDocument xdoc;
        XmlNode xnode;

        public Xml(string xml, string firstNodeName)
        {
            xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            string nodeName = "//" + firstNodeName;
            xnode = xdoc.DocumentElement.SelectSingleNode(nodeName);
            if (xnode == null)
            {
                throw new Exception("初始化xml失敗，找不到根節點<" + firstNodeName + ">");
            }

        }


        public string getNodeValue(string nodeName)
        {

            return xnode.SelectSingleNode(nodeName).InnerText;

        }


    }

    public class Xmls
    {

        XmlDocument xdoc;
        XmlNodeList xnodes;

        public Xmls(string xml, string firstNodeName)
        {
            xdoc = new XmlDocument();
            xdoc.LoadXml(xml);
            string nodeName = "//" + firstNodeName;
            xnodes = xdoc.DocumentElement.SelectNodes(nodeName);
            if (xnodes == null)
            {
                throw new Exception("初始化xml失敗，找不到根節點<" + firstNodeName + ">");
            }

        }

        /*
                public string getNodeValue(List<string> nodeName)
                {

                    List<string> l = new List<string>();

                    foreach (XmlNode node in xnodes) {

                    }


                }
                */

    }
}
