using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.DynamicDB;
using IDCM.IDB;
using NPOI.SS.UserModel;
using System.IO;
using IDCM.SessionProvider.MsgDriver;
using IDCM.Base.ComPO;
using IDCM.VModule.GCM.ComPO;

namespace IDCM.VModule.GCM.DataTansfer
{
    class ExcelDataImporter
    {
        /// <summary>
        /// 解析指定的Excel文档，执行数据转换.
        /// 本方法调用对类功能予以线程包装，用于异步调用如何方法。
        /// 在本线程调用下的控件调用，需通过UI控件的Invoke/BegainInvoke方法更新。
        /// </summary>
        /// <param name="fpath"></param>
        /// <returns>返回请求流程是否执行完成</returns>
        public static bool parseExcelData(DDBMH ddbmh, string fpath, ref Dictionary<string, string> dataMapping)
        {
            if (fpath == null || fpath.Length < 1)
                return false;
            string fullPath = System.IO.Path.GetFullPath(fpath);
            IWorkbook workbook = null;
            using (FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read))
            {
                workbook = WorkbookFactory.Create(fs);
                ISheet dataSheet = workbook.GetSheet("Core Datasets");
                if (dataSheet == null)
                    dataSheet = workbook.GetSheetAt(0);
                parseSheetInfo(ddbmh, dataSheet, ref dataMapping);
            }
            return true;
        }
        /// <summary>
        /// 通过NPOI读取Excel文档，转换可识别内容至本地数据库中
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dgv"></param>
        public static void parseSheetInfo(DDBMH ddbmh, ISheet sheet, ref Dictionary<string, string> dataMapping)
        {
            int skipIdx = 1;
            if (sheet == null || sheet.LastRowNum < skipIdx) //no data
                return;
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
            if (dataMapping != null && dataMapping.Count > 0)
            {
                for (int i = skipIdx; i <= rowSize; ++i)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null) continue; //没有数据的行默认是null　
                    ICell headCell = row.GetCell(row.FirstCellNum);
                    if (headCell == null || headCell.ToString().Length == 0 || headCell.ToString().Equals("end!"))
                        break;
                    Dictionary<string, string> mapValues = new Dictionary<string, string>();
                    for (int j = row.FirstCellNum; j < columnSize; j++)
                    {
                        if (row.GetCell(j) != null && xlscols[j] != null)
                        {
                            string cellData = row.GetCell(j).ToString().Trim();
                            string mapName = null;
                            dataMapping.TryGetValue(xlscols[j], out mapName);
                            if (mapName != null)
                            {
                                mapValues[mapName] = cellData;
                            }
                        }
                    }
                    long nuid = ddbmh.DDBManager.mergeRecord(ddbmh.DBmanger, ddbmh.TableName, mapValues);
                }
            }
        }

        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
    }
}
