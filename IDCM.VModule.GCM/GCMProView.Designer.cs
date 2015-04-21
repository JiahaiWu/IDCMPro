namespace IDCM.VModule.GCM
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GCMProView));
            this.gcmTabControl_GCM = new DCMControlLib.GCM.GCMTabControl();
            this.tabPageEx_Local = new DCMControlLib.GCM.TabPageEx();
            this.tabPageEx_GCM = new DCMControlLib.GCM.TabPageEx();
            this.tabPage_ABC = new DCMControlLib.GCM.TabPageEx();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.splitContainer_local = new System.Windows.Forms.SplitContainer();
            this.splitContainer_GCM = new System.Windows.Forms.SplitContainer();
            this.splitContainer_abc = new System.Windows.Forms.SplitContainer();
            this.dcmDataGridView1 = new DCM.DCMDataGridView();
            this.dcmDataGridView2 = new DCM.DCMDataGridView();
            this.abcBrowser1 = new DCMControlLib.GCM.ABCBrowser();
            this.gcmTabControl_GCM.SuspendLayout();
            this.tabPageEx_Local.SuspendLayout();
            this.tabPageEx_GCM.SuspendLayout();
            this.tabPage_ABC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_local)).BeginInit();
            this.splitContainer_local.Panel1.SuspendLayout();
            this.splitContainer_local.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_GCM)).BeginInit();
            this.splitContainer_GCM.Panel1.SuspendLayout();
            this.splitContainer_GCM.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_abc)).BeginInit();
            this.splitContainer_abc.Panel1.SuspendLayout();
            this.splitContainer_abc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView2)).BeginInit();
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
            this.gcmTabControl_GCM.SelectedIndex = 0;
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
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "abc.png");
            this.imageList1.Images.SetKeyName(1, "gcm_logo.png");
            this.imageList1.Images.SetKeyName(2, "local.png");
            // 
            // splitContainer_local
            // 
            this.splitContainer_local.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_local.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_local.Name = "splitContainer_local";
            // 
            // splitContainer_local.Panel1
            // 
            this.splitContainer_local.Panel1.Controls.Add(this.dcmDataGridView1);
            this.splitContainer_local.Panel2Collapsed = true;
            this.splitContainer_local.Size = new System.Drawing.Size(714, 476);
            this.splitContainer_local.SplitterDistance = 492;
            this.splitContainer_local.TabIndex = 0;
            // 
            // splitContainer_GCM
            // 
            this.splitContainer_GCM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_GCM.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_GCM.Name = "splitContainer_GCM";
            // 
            // splitContainer_GCM.Panel1
            // 
            this.splitContainer_GCM.Panel1.Controls.Add(this.dcmDataGridView2);
            this.splitContainer_GCM.Panel2Collapsed = true;
            this.splitContainer_GCM.Size = new System.Drawing.Size(714, 476);
            this.splitContainer_GCM.SplitterDistance = 475;
            this.splitContainer_GCM.TabIndex = 0;
            // 
            // splitContainer_abc
            // 
            this.splitContainer_abc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_abc.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_abc.Name = "splitContainer_abc";
            // 
            // splitContainer_abc.Panel1
            // 
            this.splitContainer_abc.Panel1.Controls.Add(this.abcBrowser1);
            this.splitContainer_abc.Panel2Collapsed = true;
            this.splitContainer_abc.Size = new System.Drawing.Size(714, 476);
            this.splitContainer_abc.SplitterDistance = 486;
            this.splitContainer_abc.TabIndex = 0;
            // 
            // dcmDataGridView1
            // 
            this.dcmDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dcmDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dcmDataGridView1.Name = "dcmDataGridView1";
            this.dcmDataGridView1.RowTemplate.Height = 23;
            this.dcmDataGridView1.Size = new System.Drawing.Size(714, 476);
            this.dcmDataGridView1.TabIndex = 0;
            // 
            // dcmDataGridView2
            // 
            this.dcmDataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dcmDataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmDataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dcmDataGridView2.Name = "dcmDataGridView2";
            this.dcmDataGridView2.RowTemplate.Height = 23;
            this.dcmDataGridView2.Size = new System.Drawing.Size(714, 476);
            this.dcmDataGridView2.TabIndex = 0;
            // 
            // abcBrowser1
            // 
            this.abcBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.abcBrowser1.Location = new System.Drawing.Point(0, 0);
            this.abcBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.abcBrowser1.Name = "abcBrowser1";
            this.abcBrowser1.Size = new System.Drawing.Size(714, 476);
            this.abcBrowser1.TabIndex = 0;
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
            this.tabPageEx_GCM.ResumeLayout(false);
            this.tabPage_ABC.ResumeLayout(false);
            this.splitContainer_local.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_local)).EndInit();
            this.splitContainer_local.ResumeLayout(false);
            this.splitContainer_GCM.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_GCM)).EndInit();
            this.splitContainer_GCM.ResumeLayout(false);
            this.splitContainer_abc.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_abc)).EndInit();
            this.splitContainer_abc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView2)).EndInit();
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
        private DCM.DCMDataGridView dcmDataGridView1;
        private DCM.DCMDataGridView dcmDataGridView2;
        private DCMControlLib.GCM.ABCBrowser abcBrowser1;
    }
}
