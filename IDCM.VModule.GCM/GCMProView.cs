using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDCM.VModule.GCM.ViewManager;
using IDCM.IDB;
using IDCM.Base;
using IDCM.Base.Utils;
using System.Configuration;
using System.IO;
using IDCM.DynamicDB;
using IDCM.VModule.GCM.ComponentUtil;

namespace IDCM.VModule.GCM
{
    public partial class GCMProView : UserControl
    {
        public GCMProView()
        {
            InitializeComponent();
            InitializeGCMPro();
            startDataRender();
        }
        /// <summary>
        /// 初始化流程
        /// </summary>
        private void InitializeGCMPro()
        {
            string initEnvDir = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
            string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string defaultCfgPath = Path.GetDirectoryName(exePath) + Path.DirectorySeparatorChar + Path.GetFileName(exePath).Replace(".vshost.exe", ".exe") + ".config";
            try
            {
                string dbPath = ConfigurationManager.AppSettings[SysConstants.LastWorkSpace];
                if (dbPath == null || dbPath.Trim().Length < 1)
                {
                    string wsDir = initEnvDir + "/GCMPro/";
                    Directory.CreateDirectory(wsDir);
                    dbPath = wsDir + CUIDGenerator.getUID(CUIDGenerator.Radix_32) + SysConstants.DB_SUFFIX;
                    ConfigurationHelper.SetAppConfig(SysConstants.LastWorkSpace, dbPath, defaultCfgPath);
                }
                dbManager = new IDBManager(dbPath);
                ddbManager = new DynamicDBManager(dbManager);
                localServManager = new LocalServManager(dbManager, ddbManager);
                gcmServManager = new GCMServManager(dbManager);
                abcServManager = new ABCServManager(dbManager);
                servInvoker = new AsyncServInvoker();
                this.dcmDataGridView_local.CellValueChanged += dataGridView_local_CellValueChanged;
                this.dcmDataGridView_local.DragEnter += dataGridView_items_DragEnter;
                this.dcmDataGridView_local.DragDrop += dataGridView_items_DragDrop;
                servInvoker.OnDataPrepared += OnDataPrepared;
                this.Enabled = true;
            }
            catch (Exception ex)
            {
                this.Enabled = false;
                ConfigurationHelper.SetAppConfig(SysConstants.LastWorkSpace, "", defaultCfgPath);
                log.Error("GCM Pro View Initialize Failed!",ex);
                MessageBox.Show("GCM Pro View Initialize Failed!");
            }
            finally
            {
                this.Visible = this.Enabled;
            }
        }
        private void startDataRender()
        {
            if (!localServManager.checkDTableSetting())
            {
                log.Error("Start Local Data View Failed!!");
                MessageBox.Show("Start Local Data View Failed!");
                this.Enabled = false;
                this.Visible = false;
                return;
            }
            else
            {
                localServManager.loadDGVColumns(dcmDataGridView_local);
                localServManager.loadDGVData(dcmDataGridView_local);
            }
        }

        private void OnDataPrepared(object sender, IDCMAsyncEventArgs e)
        {
        }
        /// <summary>
        /// 单元格的值改变后，执行更新或插入操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_local_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex > 0)
            {
                DataGridViewRow dgvr = dcmDataGridView_local.Rows[e.RowIndex];
                DataGridViewCell idCell = dgvr.Cells[localServManager.CTD_RID];
                DataGridViewCell cell = dgvr.Cells[e.ColumnIndex];
                if (cell != null && idCell != null  && cell.Visible)
                {
                    string rid = idCell.FormattedValue.ToString();
                    string cellVal = cell.FormattedValue.ToString();
                    string attrName = dcmDataGridView_local.Columns[e.ColumnIndex].Name;
                    if (rid != null && cellVal != null)
                    {
                        localServManager.updateAttrVal(rid, cellVal, attrName);
                    }
                }
            }
        }

        /// <summary>
        /// 拖拽事件运行时的鼠标状态切换方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_items_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Link;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        /// <summary>
        /// 文件拖拽后事件处理方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView_items_DragDrop(object sender, DragEventArgs e)
        {
            String[] recvs = (String[])e.Data.GetData(DataFormats.FileDrop, false);
            for (int i = 0; i < recvs.Length; i++)
            {
                if (recvs[i].Trim() != "")
                {
                    String fpath = recvs[i].Trim();
                    bool exists = System.IO.File.Exists(fpath);
                    if (exists == true)
                    {
                        localServManager.importData(fpath,this.dcmDataGridView_local);
                    }
                }
            }
        }

        private static NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
        private AsyncServInvoker servInvoker = null;
        private IDBManager dbManager = null;
        private DynamicDBManager ddbManager = null;
        private GCMServManager gcmServManager = null;
        private LocalServManager localServManager = null;
        private ABCServManager abcServManager = null;
    }
}
