﻿namespace IDCM.VModule.GCM
{
    partial class GCMProView
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GCMProView));
            this.gcmTabControl_GCM = new DCMControlLib.GCM.GCMTabControl();
            this.tabPageEx_Local = new DCMControlLib.GCM.TabPageEx();
            this.splitContainer_local = new System.Windows.Forms.SplitContainer();
            this.dcmDataGridView_local = new DCM.DCMDataGridView();
            this.tabPageEx_GCM = new DCMControlLib.GCM.TabPageEx();
            this.splitContainer_GCM = new System.Windows.Forms.SplitContainer();
            this.dcmDataGridView_gcm = new DCM.DCMDataGridView();
            this.tabPage_ABC = new DCMControlLib.GCM.TabPageEx();
            this.splitContainer_abc = new System.Windows.Forms.SplitContainer();
            this.abcBrowser_abc = new DCMControlLib.GCM.ABCBrowser();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gcmTabControl_GCM.SuspendLayout();
            this.tabPageEx_Local.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_local)).BeginInit();
            this.splitContainer_local.Panel1.SuspendLayout();
            this.splitContainer_local.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView_local)).BeginInit();
            this.tabPageEx_GCM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_GCM)).BeginInit();
            this.splitContainer_GCM.Panel1.SuspendLayout();
            this.splitContainer_GCM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView_gcm)).BeginInit();
            this.tabPage_ABC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_abc)).BeginInit();
            this.splitContainer_abc.Panel1.SuspendLayout();
            this.splitContainer_abc.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcmTabControl_GCM
            // 
            this.gcmTabControl_GCM.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.gcmTabControl_GCM.Alignments = DCMControlLib.GCM.GCMTabControl.TabAlignments.Bottom;
            this.gcmTabControl_GCM.AllowDrop = true;
            this.gcmTabControl_GCM.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.gcmTabControl_GCM.BackgroundHatcher.HatchType = System.Drawing.Drawing2D.HatchStyle.DashedVertical;
            this.gcmTabControl_GCM.Controls.Add(this.tabPageEx_Local);
            this.gcmTabControl_GCM.Controls.Add(this.tabPageEx_GCM);
            this.gcmTabControl_GCM.Controls.Add(this.tabPage_ABC);
            this.gcmTabControl_GCM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcmTabControl_GCM.ImageList = this.imageList1;
            this.gcmTabControl_GCM.IsCaptionVisible = false;
            this.gcmTabControl_GCM.ItemSize = new System.Drawing.Size(150, 30);
            this.gcmTabControl_GCM.Location = new System.Drawing.Point(0, 0);
            this.gcmTabControl_GCM.Name = "gcmTabControl_GCM";
            this.gcmTabControl_GCM.SelectedIndex = 2;
            this.gcmTabControl_GCM.Size = new System.Drawing.Size(716, 513);
            this.gcmTabControl_GCM.TabGradient.ColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(223)))), ((int)(((byte)(246)))));
            this.gcmTabControl_GCM.TabGradient.ColorStart = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(67)))), ((int)(((byte)(164)))));
            this.gcmTabControl_GCM.TabGradient.GradientStyle = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gcmTabControl_GCM.TabGradient.TabPageSelectedTextColor = System.Drawing.Color.White;
            this.gcmTabControl_GCM.TabGradient.TabPageTextColor = System.Drawing.Color.DimGray;
            this.gcmTabControl_GCM.TabIndex = 0;
            this.gcmTabControl_GCM.UpDownStyle = DCMControlLib.GCM.GCMTabControl.UpDown32Style.Default;
            // 
            // tabPageEx_Local
            // 
            this.tabPageEx_Local.BackColor = System.Drawing.Color.White;
            this.tabPageEx_Local.Controls.Add(this.splitContainer_local);
            this.tabPageEx_Local.Font = new System.Drawing.Font("Arial", 10F);
            this.tabPageEx_Local.ImageIndex = 2;
            this.tabPageEx_Local.Location = new System.Drawing.Point(1, 1);
            this.tabPageEx_Local.Name = "tabPageEx_Local";
            this.tabPageEx_Local.Size = new System.Drawing.Size(714, 476);
            this.tabPageEx_Local.TabIndex = 0;
            this.tabPageEx_Local.Text = "Local DataSet";
            // 
            // splitContainer_local
            // 
            this.splitContainer_local.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_local.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_local.Name = "splitContainer_local";
            // 
            // splitContainer_local.Panel1
            // 
            this.splitContainer_local.Panel1.Controls.Add(this.dcmDataGridView_local);
            this.splitContainer_local.Panel2Collapsed = true;
            this.splitContainer_local.Size = new System.Drawing.Size(714, 476);
            this.splitContainer_local.SplitterDistance = 492;
            this.splitContainer_local.TabIndex = 0;
            // 
            // dcmDataGridView_local
            // 
            this.dcmDataGridView_local.AllowUserToAddRows = false;
            this.dcmDataGridView_local.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dcmDataGridView_local.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dcmDataGridView_local.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dcmDataGridView_local.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmDataGridView_local.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dcmDataGridView_local.EnableHeadersVisualStyles = false;
            this.dcmDataGridView_local.Location = new System.Drawing.Point(0, 0);
            this.dcmDataGridView_local.Name = "dcmDataGridView_local";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dcmDataGridView_local.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dcmDataGridView_local.RowHeadersWidth = 100;
            this.dcmDataGridView_local.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dcmDataGridView_local.RowTemplate.Height = 23;
            this.dcmDataGridView_local.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dcmDataGridView_local.ShowEditingIcon = false;
            this.dcmDataGridView_local.Size = new System.Drawing.Size(714, 476);
            this.dcmDataGridView_local.TabIndex = 0;
            // 
            // tabPageEx_GCM
            // 
            this.tabPageEx_GCM.BackColor = System.Drawing.Color.White;
            this.tabPageEx_GCM.Controls.Add(this.splitContainer_GCM);
            this.tabPageEx_GCM.Font = new System.Drawing.Font("Arial", 10F);
            this.tabPageEx_GCM.ImageIndex = 1;
            this.tabPageEx_GCM.Location = new System.Drawing.Point(1, 1);
            this.tabPageEx_GCM.Name = "tabPageEx_GCM";
            this.tabPageEx_GCM.Size = new System.Drawing.Size(714, 476);
            this.tabPageEx_GCM.TabIndex = 1;
            this.tabPageEx_GCM.Text = "GCM Publish";
            // 
            // splitContainer_GCM
            // 
            this.splitContainer_GCM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_GCM.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_GCM.Name = "splitContainer_GCM";
            // 
            // splitContainer_GCM.Panel1
            // 
            this.splitContainer_GCM.Panel1.Controls.Add(this.dcmDataGridView_gcm);
            this.splitContainer_GCM.Panel2Collapsed = true;
            this.splitContainer_GCM.Size = new System.Drawing.Size(714, 476);
            this.splitContainer_GCM.SplitterDistance = 475;
            this.splitContainer_GCM.TabIndex = 0;
            // 
            // dcmDataGridView_gcm
            // 
            this.dcmDataGridView_gcm.AllowUserToAddRows = false;
            this.dcmDataGridView_gcm.AllowUserToDeleteRows = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dcmDataGridView_gcm.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dcmDataGridView_gcm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dcmDataGridView_gcm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmDataGridView_gcm.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dcmDataGridView_gcm.EnableHeadersVisualStyles = false;
            this.dcmDataGridView_gcm.Location = new System.Drawing.Point(0, 0);
            this.dcmDataGridView_gcm.Name = "dcmDataGridView_gcm";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dcmDataGridView_gcm.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dcmDataGridView_gcm.RowHeadersWidth = 100;
            this.dcmDataGridView_gcm.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dcmDataGridView_gcm.RowTemplate.Height = 23;
            this.dcmDataGridView_gcm.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dcmDataGridView_gcm.ShowEditingIcon = false;
            this.dcmDataGridView_gcm.Size = new System.Drawing.Size(714, 476);
            this.dcmDataGridView_gcm.TabIndex = 0;
            // 
            // tabPage_ABC
            // 
            this.tabPage_ABC.BackColor = System.Drawing.Color.White;
            this.tabPage_ABC.Controls.Add(this.splitContainer_abc);
            this.tabPage_ABC.Font = new System.Drawing.Font("Arial", 10F);
            this.tabPage_ABC.ImageIndex = 0;
            this.tabPage_ABC.Location = new System.Drawing.Point(1, 1);
            this.tabPage_ABC.Name = "tabPage_ABC";
            this.tabPage_ABC.Size = new System.Drawing.Size(714, 476);
            this.tabPage_ABC.TabIndex = 2;
            this.tabPage_ABC.Text = "ABC Browser";
            // 
            // splitContainer_abc
            // 
            this.splitContainer_abc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_abc.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_abc.Name = "splitContainer_abc";
            // 
            // splitContainer_abc.Panel1
            // 
            this.splitContainer_abc.Panel1.Controls.Add(this.abcBrowser_abc);
            this.splitContainer_abc.Panel2Collapsed = true;
            this.splitContainer_abc.Size = new System.Drawing.Size(714, 476);
            this.splitContainer_abc.SplitterDistance = 486;
            this.splitContainer_abc.TabIndex = 0;
            // 
            // abcBrowser_abc
            // 
            this.abcBrowser_abc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.abcBrowser_abc.Location = new System.Drawing.Point(0, 0);
            this.abcBrowser_abc.MinimumSize = new System.Drawing.Size(20, 20);
            this.abcBrowser_abc.Name = "abcBrowser_abc";
            this.abcBrowser_abc.Size = new System.Drawing.Size(714, 476);
            this.abcBrowser_abc.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "abc.png");
            this.imageList1.Images.SetKeyName(1, "gcm_logo.png");
            this.imageList1.Images.SetKeyName(2, "local.png");
            // 
            // GCMProView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gcmTabControl_GCM);
            this.Name = "GCMProView";
            this.Size = new System.Drawing.Size(716, 513);
            this.gcmTabControl_GCM.ResumeLayout(false);
            this.tabPageEx_Local.ResumeLayout(false);
            this.splitContainer_local.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_local)).EndInit();
            this.splitContainer_local.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView_local)).EndInit();
            this.tabPageEx_GCM.ResumeLayout(false);
            this.splitContainer_GCM.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_GCM)).EndInit();
            this.splitContainer_GCM.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView_gcm)).EndInit();
            this.tabPage_ABC.ResumeLayout(false);
            this.splitContainer_abc.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_abc)).EndInit();
            this.splitContainer_abc.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DCMControlLib.GCM.GCMTabControl gcmTabControl_GCM;
        private DCMControlLib.GCM.TabPageEx tabPageEx_Local;
        private DCMControlLib.GCM.TabPageEx tabPageEx_GCM;
        private DCMControlLib.GCM.TabPageEx tabPage_ABC;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer_local;
        private System.Windows.Forms.SplitContainer splitContainer_GCM;
        private System.Windows.Forms.SplitContainer splitContainer_abc;
        private DCM.DCMDataGridView dcmDataGridView_local;
        private DCM.DCMDataGridView dcmDataGridView_gcm;
        private DCMControlLib.GCM.ABCBrowser abcBrowser_abc;
    }
}
