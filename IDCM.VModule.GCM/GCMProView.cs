using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDCM.VModule.GCM
{
    public partial class GCMProView : UserControl
    {
        public GCMProView()
        {
            InitializeComponent();
            this.dcmDataGridView_local.CellValueChanged += dataGridView_local_CellValueChanged;
            this.dcmDataGridView_local.DragEnter += dataGridView_items_DragEnter;
            this.dcmDataGridView_local.DragDrop += dataGridView_items_DragDrop;
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
                DataGridViewCell idCell = dgvr.Cells[CTDRecordA.CTD_RID];
                DataGridViewCell cell = dgvr.Cells[e.ColumnIndex];
                if (cell != null && idCell != null  && cell.Visible)
                {
                    string rid = idCell.FormattedValue.ToString();
                    string cellVal = cell.FormattedValue.ToString();
                    string attrName = dcmDataGridView_local.Columns[e.ColumnIndex].Name;
                    if (rid != null && cellVal != null)
                    {
                        manager.updateAttrVal(rid, cellVal, attrName);
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
                        manager.importData(fpath);
                    }
                }
            }
        }
    }
}
