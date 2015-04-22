﻿using IDCM.Base.AbsInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDCM.VModule.GCM.ComPO;
using IDCM.SessionProvider.MsgDriver;
using IDCM.VModule.GCM.DataTansfer;

namespace IDCM.VModule.GCM.BGHandler
{
    public class XMLImportHandler : AbsBGHandler
    {
        public XMLImportHandler(DDBMH ddbmh, string fpath, ref Dictionary<string, string> dataMapping)
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
        public override object doWork(System.ComponentModel.BackgroundWorker worker, bool cancel, List<object> args)
        {
            bool res = false;
            try
            {
                DCMPublisher.noteJobProgress(this, 0);
                res = XMLDataImporter.parseXMLData(ddbmh, xlsPath, ref dataMapping);
            }
            catch (Exception ex)
            {
                log.Info("ERROR: XML文件导入失败！ ", ex);
                DCMPublisher.noteSimpleMsg("ERROR: XML文件导入失败！ " + ex.Message, IDCM.Base.ComPO.DCMMsgType.Alert);
            }
            return new object[] { res, xlsPath };
        }

        /// <summary>
        /// 后台任务执行结束，回调代码段
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="args"></param>
        public override void complete(DDBMH ddbmh, bool canceled, Exception error, List<Object> args)
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
        private Dictionary<string, string> dataMapping = null;
        private DDBMH ddbmh;
    }
}
