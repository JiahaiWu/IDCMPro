using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.IDB;
using IDCM.Base.ComPO;
using IDCM.DynamicDB;
using IDCM.Base;
using IDCM.Base.Utils;
using System.IO;
using System.Configuration;
using IDCM.Base.AbsInterfaces;
using IDCM.JobDriver.Core;
using NPOI.SS.UserModel;
using System.Windows.Forms;
using System.Xml;
using IDCM.Forms;
using IDCM.VModule.GCM.ComPO;
using IDCM.VModule.GCM.BGHandler;
using IDCM.VModule.GCM.ComponentUtil;
using DCMControlLib;

namespace IDCM.VModule.GCM.ViewManager
{
    class LocalServManager
    {
        public LocalServManager(IDBManager dbManager, DynamicDBManager ddbManager)
        {
            this.ddbmh = new DDBMH(ddbManager, dbManager,SysConstants.GCMProLocal_CTable);
        }
        /// <summary>
        /// 加载数据表头展示
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="attrs"></param>
        private void loadDGVColumns(DataGridView dgv)
        {
            List<CustomTColDef> colMaps = ddbmh.DDBManager.getViewMappingAttrDef(ddbmh.DBmanger,ddbmh.TableName);
            DGVAsyncUtil.syncClearAll(dgv);
            foreach (CustomTColDef ctcd in colMaps)
            {
                DCMControlLib.DCMTextDGVColumn dgvCol = new DCMControlLib.DCMTextDGVColumn();
                dgvCol.Name = ctcd.Attr;
                dgvCol.HeaderText = CVNameWrapper.toViewName(ctcd.Attr);
                dgvCol.Visible = ctcd.Corder > 0;
                DGVAsyncUtil.syncAddCol(dgv, dgvCol);
                if (viewOrder != dgvCol.Index)
                    LocalRecordMHub.updateViewOrder(DataSourceHolder.DataSource, attr, dgvCol.Index);
            }
        }
        /// <summary>
        /// 更新数据记录
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="cellVal"></param>
        /// <param name="attrName"></param>
        /// <returns></returns>
        public int updateAttrVal(string rid, string cellVal, string attrName)
        {
            return ddbmh.DDBManager.updateAttrVal(ddbmh.DBmanger, ddbmh.TableName, Convert.ToInt64(rid), cellVal, attrName);
        }
        /// <summary>
        /// 导入Excel数据文档
        /// </summary>
        /// <param name="fpath"></param>
        public void importData(string fpath,DataGridView dgv)
        {
            Dictionary<string, string> dataMapping = new Dictionary<string, string>();
            AbsBGHandler eih = null;
            if (fpath.ToLower().EndsWith("xls") || fpath.ToLower().EndsWith(".xlsx"))
            {
                if (checkForExcelImport(fpath, ref dataMapping))
                    eih = new ExcelImportHandler(ddbmh, fpath, ref dataMapping);
            }
            if (fpath.ToLower().EndsWith("xml") || fpath.ToLower().EndsWith(".xml"))
            {
                if (checkForXMLImport(fpath, ref dataMapping))
                    eih = new XMLImportHandler(ddbmh, fpath, ref dataMapping);
            }
            BGWorkerInvoker.pushHandler(eih);
        }
        /// <summary>
        /// 解析指定的Excel文档，验证数据转换的属性映射条件.
        /// </summary>
        /// <param name="fpath"></param>
        /// <returns></returns>
        private bool checkForExcelImport(string fpath, ref Dictionary<string, string> dataMapping)
        {
            if (fpath == null || fpath.Length < 1)
                return false;
            string fullPath = System.IO.Path.GetFullPath(fpath);
            IWorkbook workbook = null;
            try
            {
                using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
                {
                    workbook = WorkbookFactory.Create(fs);
                    ISheet dataSheet = workbook.GetSheet("Core Datasets");
                    if (dataSheet == null)
                        dataSheet = workbook.GetSheetAt(0);
                    return fetchSheetMappingInfo(dataSheet, ref dataMapping) && dataMapping.Count > 0;
                }
            }
            catch (Exception ex)
            {
                log.Info("ERROR: Excel文件导入失败！ ", ex);
                MessageBox.Show("ERROR: Excel文件导入失败！ " + ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 解析指定的XML文档，验证数据转换的属性映射条件.
        /// </summary>
        /// <param name="fpath"></param>
        /// <returns></returns>
        private bool checkForXMLImport(string fpath, ref Dictionary<string, string> dataMapping)
        {
            if (fpath == null || fpath.Length < 1)
                return false;
            string fullPaht = System.IO.Path.GetFullPath(fpath);
            try
            {
                XmlDocument xDoc = new XmlDocument();
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreComments = true;
                using (XmlReader xRead = XmlReader.Create(fullPaht))
                {
                    xDoc.Load(xRead);
                    return fetchXMLMappingInfo(xDoc, ref dataMapping) && dataMapping.Count > 0;
                }
            }
            catch (Exception ex)
            {
                log.Info("ERROR: XML文件导入失败！ ", ex);
                MessageBox.Show("ERROR: XML文件导入失败！ " + ex.Message + "\n" + ex.StackTrace);
            }
            return false;
        }
        private bool fetchXMLMappingInfo(XmlDocument xDoc, ref Dictionary<string, string> dataMapping)
        {
            XmlNodeList strainChildNodes = xDoc.DocumentElement.ChildNodes;
            //一直向下探索，直到某个节点下没有子节点，说明这个节点是个attrNode,
            //因为按正常的逻辑，属性节点应该是最小的节点单位了
            //attrNode的集合就是strainChildNodes  
            while (strainChildNodes.Count > 0)
            {
                XmlNode node = strainChildNodes[0];
                if (node.ChildNodes.Count <= 0)
                    break;
                strainChildNodes = node.ChildNodes;
            }

            //节点探测代码
            XmlNode strainNode = strainChildNodes[0].ParentNode;//获取第一个strainNode
            List<string> attrNameList = new List<string>(strainChildNodes.Count);
            int cursor = Convert.ToInt32(ConfigurationManager.AppSettings.Get(SysConstants.Cursor));
            int detectDepth = Convert.ToInt32(ConfigurationManager.AppSettings.Get(SysConstants.DetectDepth));
            double GrowthFactor = Convert.ToDouble(ConfigurationManager.AppSettings.Get(SysConstants.GrowthFactor));
            while (!(strainNode == null))
            {
                if (cursor > detectDepth)
                    break;
                if (mergeAttrList(attrNameList, strainNode.ChildNodes))//如果这个节点下有新属性出现，使探测深度增加2倍
                    detectDepth = (int)(detectDepth * GrowthFactor);
                strainNode = nextStrainNode(strainNode);
                cursor++;
            }

            ///////////////////////////////////////////////////////////////
            using (AttrMapOptionDlg amoDlg = new AttrMapOptionDlg())
            {
                amoDlg.BringToFront();
                amoDlg.setInitCols(attrNameList, ddbManager.getAttrViewMapping(GCMProLocal_CTable, false).Keys, ref dataMapping);
                amoDlg.ShowDialog();
                ///////////////////////////////////////////
                if (amoDlg.DialogResult == DialogResult.OK)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 通过NPOI读取Excel文档，转换可识别内容至本地数据库中
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dgv"></param>
        private bool fetchSheetMappingInfo(ISheet sheet, ref Dictionary<string, string> dataMapping)
        {
            int skipIdx = 1;
            if (sheet == null || sheet.LastRowNum < skipIdx) //no data
                return false;
            /////////////////////////////////////////////////////////
            IRow titleRow = sheet.GetRow(skipIdx - 1);
            int columnSize = titleRow.LastCellNum;
            int rowSize = sheet.LastRowNum;
            List<string> xlscols = new List<string>(columnSize);
            for (int i = titleRow.FirstCellNum; i < columnSize; i++)
            {
                ICell titleCell = titleRow.GetCell(i);
                if (titleCell != null && titleCell.ToString().Length > 0)
                {
                    string cellData = titleCell.ToString();
                    xlscols.Add(CVNameWrapper.toViewName(cellData.Trim().ToLower()));
                }
                else
                {
                    xlscols.Add(null);
                }
            }
            ///////////////////////////////////////////////////////////////
            using (AttrMapOptionDlg amoDlg = new AttrMapOptionDlg())
            {
                amoDlg.BringToFront();
                amoDlg.setInitCols(xlscols, ddbManager.getAttrViewMapping(GCMProLocal_CTable, false).Keys, ref dataMapping);
                amoDlg.ShowDialog();
                ///////////////////////////////////////////
                if (amoDlg.DialogResult == DialogResult.OK)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 获取下一个XmlNode兄弟节点
        /// </summary>
        /// <param name="strainNode"></param>
        /// <returns></returns>
        private XmlNode nextStrainNode(XmlNode strainNode)
        {
            return strainNode.NextSibling;
        }
        private bool mergeAttrList(List<string> attrNameList, XmlNodeList attrNodeList)
        {
            int startLeng = attrNameList.Count;
            foreach (XmlNode strainChildNode in attrNodeList)
            {
                if (!attrNameList.Contains(strainChildNode.Name))
                    attrNameList.Add(strainChildNode.Name);
            }
            int endLeng = attrNameList.Count;
            if (startLeng != endLeng)
                return true;
            return false;
        }
        /// <summary>
        /// 检查数据表配置存在与否，如不存在则创建默认表属性设定
        /// </summary>
        /// <param name="picker"></param>
        /// <returns></returns>
        internal bool checkDTableSetting()
        {
            try
            {
                if (!ddbManager.existTable(dbManager, GCMProLocal_CTable))
                {
                    return buildDTable(GCMProLocal_CTable);
                }
            }
            catch (Exception ex)
            {
                log.Error("checkDTableSetting Exception!", ex);
                return false;
            }
            return true;
        }
        private bool buildDTable(string tableName,string comments=null)
        {
            List<CustomColDef> ccds=getCustomTableDef(ConfigurationManager.AppSettings[SysConstants.CTableDef]);
            if (ccds != null && ccds.Count > 0)
            {
                CustomTDS ctds = new CustomTDS();
                ctds.TName = tableName;
                ctds.Comments = comments;
                ctds.ccds = ccds;
                return ddbManager.buildTable(dbManager, ctds);
            }
            return false;
        }
        private List<CustomColDef> getCustomTableDef(string settingPath)
        {
            List<CustomColDef> ctcds = new List<CustomColDef>();
            if (File.Exists(settingPath))
            {
                string[] lines = FileUtil.readAsUTF8Text(settingPath).Split(new char[] { '\n', '\r' });
                foreach (string line in lines)
                {
                    if (line.Length < 1 || line.StartsWith("#"))
                        continue;
                    if (line.StartsWith("[") && line.TrimEnd().EndsWith("]"))
                        continue;
                    if (line.StartsWith(">>Def"))
                    {
                        string ver = line.Substring(9).Trim();
                        ctcds.Clear();
                        continue;
                    }
                    CustomColDef ctcd = formatSettingLine(line);
                    if (ctcd != null)
                    {
                        ctcds.Add(ctcd);
                        //set default col order for ctcd attr
                        ctcd.Corder = ctcds.Count;
                    }
                }
                return ctcds;
            }
            else
            {
                log.Fatal("The setting file note exist! @Path=" + settingPath);
                throw new IDCMDataException("The setting file note exist! @Path=" + settingPath);
            }
        }
        private CustomColDef formatSettingLine(string line)
        {
            string[] vals = line.Split(new char[] { ',' });
            if (vals.Length > 0)
            {
                CustomColDef ctcd = new CustomColDef();
                ctcd.Attr = CVNameWrapper.toDBName(vals[0]);
                ctcd.AttrType = vals.Length > 1 ? vals[1] : AttrTypeConverter.IDCM_String;
                ctcd.IsRequire = vals.Length > 2 ? Convert.ToBoolean(vals[2]) : false;
                ctcd.IsUnique = vals.Length > 3 ? Convert.ToBoolean(vals[3]) : false;
                ctcd.Restrict = vals.Length > 4 ? vals[4] : null;
                ctcd.DefaultVal = vals.Length > 5 ? vals[5] : null;
                ctcd.Comments = vals.Length > 6 ? vals[6] : null;
                return ctcd;
            }
            return null;
        }

        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        private DDBMH ddbmh = null;
    }
}
