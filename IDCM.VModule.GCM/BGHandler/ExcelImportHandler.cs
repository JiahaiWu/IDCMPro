﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.Base.AbsInterfaces;
using IDCM.DynamicDB;
using IDCM.IDB;
using IDCM.SessionProvider.MsgDriver;
using IDCM.VModule.GCM.ComPO;
using IDCM.VModule.GCM.DataTansfer;

namespace IDCM.VModule.GCM.BGHandler
{
    /// <summary>
    /// 将目标excel文档导入至目标数据库
    /// </summary>
    public class ExcelImportHandler : AbsBGHandler
    {
        public ExcelImportHandler(DDBMH ddbmh, string fpath, ref Dictionary<string, string> dataMapping)
        {
            this.xlsPath = System.IO.Path.GetFullPath(fpath);
            this.dataMapping = dataMapping;
            this.ddbmh = ddbmh;
        }
        /// <summary>
        /// 后台任务执行方法的主体部分，异步执行代码段！
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="args"></param>
        public override Object doWork(bool cancel, List<Object> args)
        {
            bool res = false;
            try{
                DCMPublisher.noteJobProgress(this, 0);
                res = ExcelDataImporter.parseExcelData(ddbmh, xlsPath, ref dataMapping);
            }
            catch (Exception ex)
            {
                log.Info("ERROR: Excel文件导入失败！ ", ex);
                DCMPublisher.noteSimpleMsg("ERROR: Excel文件导入失败！ " + ex.Message, IDCM.Base.ComPO.DCMMsgType.Alert);
            }
            return new object[] { res, xlsPath };
        }
        /// <summary>
        /// 后台任务执行结束，回调代码段
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="args"></param>
        public override void complete(bool canceled, Exception error, List<Object> args)
        {
            DCMPublisher.noteJobProgress(this, 100);
            DCMPublisher.noteJobFeedback(this, Base.ComPO.AsyncMsgNotice.LocalDataImported);
            if (canceled)
                return;
            if (error != null)
            {
                log.Error(error);
                return;
            }
        }
        public override void addHandler(AbsBGHandler nextHandler)
        {
            base.addHandler(nextHandler);
        }
        private string xlsPath = null;
        private long lid = -1;
        private long plid = -1;
        private Dictionary<string, string> dataMapping = null;
        private DDBMH ddbmh;
    }
}
