using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.VModule.GCM.ComPO;
using System.Xml;

namespace IDCM.VModule.GCM.DataTansfer
{
    class XMLDataImporter
    {
        /// <summary>
        /// 解析指定的Excel文档，执行数据转换.
        /// 本方法调用对类功能予以线程包装，用于异步调用如何方法。
        /// 在本线程调用下的控件调用，需通过UI控件的Invoke/BegainInvoke方法更新。
        /// </summary>
        /// <param name="fpath"></param>
        /// <returns>返回请求流程是否执行完成</returns>
        public static bool parseXMLData(DDBMH ddbmh, string fpath, ref Dictionary<string, string> dataMapping)
        {
            if (fpath == null || fpath.Length < 1)
                return false;
            string fullPaht = System.IO.Path.GetFullPath(fpath);
            XmlDocument xDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            using (XmlReader xRead = XmlReader.Create(fullPaht))
            {
                xDoc.Load(xRead);
                parseXMLMappingInfo(ddbmh, xDoc, ref dataMapping);
                return true;
            }
        }
        public static void parseXMLMappingInfo(DDBMH ddbmh, XmlDocument xDoc, ref Dictionary<string, string> dataMapping)
        {
            XmlNodeList strainChildNodes = xDoc.DocumentElement.ChildNodes;
            while (strainChildNodes.Count > 0)
            {
                XmlNode node = strainChildNodes[0];
                if (node.ChildNodes.Count <= 0)
                    break;
                strainChildNodes = node.ChildNodes;
            }
            XmlNode strainNode = strainChildNodes[0].ParentNode;//获取第一个strainNode
            /////////////////////////////////////////////////////////////////////////////
            if (dataMapping != null && dataMapping.Count > 0)
            {
                while (strainNode != null)
                {
                    Dictionary<string, string> mapValues = new Dictionary<string, string>();
                    foreach (XmlNode attrNode in strainNode.ChildNodes)//循环的是strain -> strainAttr
                    {
                        string xmlAttrName = attrNode.Name;
                        string dbName = dataMapping[xmlAttrName];
                        string xmlAttrValue = attrNode.InnerText;
                        if (dbName != null && xmlAttrValue != null && xmlAttrValue.Length > 0)
                            mapValues[dbName] = xmlAttrValue;
                    }
                    long nuid = ddbmh.DDBManager.mergeRecord(ddbmh.DBmanger,ddbmh.TableName, mapValues);
                    strainNode = nextStrainNode(strainNode);
                }
            }
        }
        private static XmlNode nextStrainNode(XmlNode strainNode)
        {
            return strainNode.NextSibling;
        }
        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
    }
}
