namespace IDCMPro
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gcmTabControl1 = new DCMControlLib.GCM.GCMTabControl();
            this.tabPage_local = new DCMControlLib.GCM.TabPageEx();
            this.splitContainer_local = new System.Windows.Forms.SplitContainer();
            this.dcmDataGridView1 = new DCM.DCMDataGridView();
            this.StrainNumber = new DCM.DCMDGV.DCMTextDGVColumn();
            this.tabPage_gcm = new DCMControlLib.GCM.TabPageEx();
            this.splitContainer_gcm = new System.Windows.Forms.SplitContainer();
            this.dcmDataGridView2 = new DCM.DCMDataGridView();
            this.tabPage_abc = new DCMControlLib.GCM.TabPageEx();
            this.splitContainer_abc = new System.Windows.Forms.SplitContainer();
            this.abcBrowser1 = new DCMControlLib.GCM.ABCBrowser();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gcmTabControl1.SuspendLayout();
            this.tabPage_local.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_local)).BeginInit();
            this.splitContainer_local.Panel1.SuspendLayout();
            this.splitContainer_local.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView1)).BeginInit();
            this.tabPage_gcm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_gcm)).BeginInit();
            this.splitContainer_gcm.Panel1.SuspendLayout();
            this.splitContainer_gcm.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView2)).BeginInit();
            this.tabPage_abc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_abc)).BeginInit();
            this.splitContainer_abc.Panel1.SuspendLayout();
            this.splitContainer_abc.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(998, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.toolStripStatusLabel2});
            this.statusStrip1.Location = new System.Drawing.Point(0, 564);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(998, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(998, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gcmTabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 50);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(998, 514);
            this.panel1.TabIndex = 3;
            // 
            // gcmTabControl1
            // 
            this.gcmTabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.gcmTabControl1.Alignments = DCMControlLib.GCM.GCMTabControl.TabAlignments.Bottom;
            this.gcmTabControl1.AllowDrop = true;
            this.gcmTabControl1.BackgroundHatcher.HatchType = System.Drawing.Drawing2D.HatchStyle.DashedVertical;
            this.gcmTabControl1.Controls.Add(this.tabPage_local);
            this.gcmTabControl1.Controls.Add(this.tabPage_gcm);
            this.gcmTabControl1.Controls.Add(this.tabPage_abc);
            this.gcmTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcmTabControl1.ImageList = this.imageList1;
            this.gcmTabControl1.IsCaptionVisible = false;
            this.gcmTabControl1.ItemSize = new System.Drawing.Size(150, 32);
            this.gcmTabControl1.Location = new System.Drawing.Point(0, 0);
            this.gcmTabControl1.Name = "gcmTabControl1";
            this.gcmTabControl1.SelectedIndex = 0;
            this.gcmTabControl1.Size = new System.Drawing.Size(998, 514);
            this.gcmTabControl1.TabGradient.ColorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(223)))), ((int)(((byte)(246)))));
            this.gcmTabControl1.TabGradient.ColorStart = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(67)))), ((int)(((byte)(164)))));
            this.gcmTabControl1.TabGradient.GradientStyle = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gcmTabControl1.TabGradient.TabPageSelectedTextColor = System.Drawing.Color.White;
            this.gcmTabControl1.TabGradient.TabPageTextColor = System.Drawing.Color.DimGray;
            this.gcmTabControl1.TabHOffset = -2;
            this.gcmTabControl1.TabIndex = 0;
            this.gcmTabControl1.TabPageCloseIconColor = System.Drawing.Color.White;
            this.gcmTabControl1.UpDownStyle = DCMControlLib.GCM.GCMTabControl.UpDown32Style.KRBBlue;
            // 
            // tabPage_local
            // 
            this.tabPage_local.BackColor = System.Drawing.Color.White;
            this.tabPage_local.Controls.Add(this.splitContainer_local);
            this.tabPage_local.Font = new System.Drawing.Font("Arial", 10F);
            this.tabPage_local.ImageIndex = 2;
            this.tabPage_local.Location = new System.Drawing.Point(1, 1);
            this.tabPage_local.Name = "tabPage_local";
            this.tabPage_local.Size = new System.Drawing.Size(996, 475);
            this.tabPage_local.TabIndex = 0;
            this.tabPage_local.Text = "Local DataSet";
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
            this.splitContainer_local.Size = new System.Drawing.Size(996, 475);
            this.splitContainer_local.SplitterDistance = 819;
            this.splitContainer_local.TabIndex = 0;
            // 
            // dcmDataGridView1
            // 
            this.dcmDataGridView1.AllowUserToAddRows = false;
            this.dcmDataGridView1.AllowUserToDeleteRows = false;
            this.dcmDataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dcmDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dcmDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.StrainNumber});
            this.dcmDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmDataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dcmDataGridView1.Name = "dcmDataGridView1";
            this.dcmDataGridView1.ReadOnly = true;
            this.dcmDataGridView1.RowTemplate.Height = 23;
            this.dcmDataGridView1.Size = new System.Drawing.Size(996, 475);
            this.dcmDataGridView1.TabIndex = 0;
            // 
            // StrainNumber
            // 
            this.StrainNumber.HeaderText = "Strain Number";
            this.StrainNumber.Name = "StrainNumber";
            this.StrainNumber.ReadOnly = true;
            this.StrainNumber.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.StrainNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // tabPage_gcm
            // 
            this.tabPage_gcm.BackColor = System.Drawing.Color.White;
            this.tabPage_gcm.Controls.Add(this.splitContainer_gcm);
            this.tabPage_gcm.Font = new System.Drawing.Font("Arial", 10F);
            this.tabPage_gcm.ImageIndex = 1;
            this.tabPage_gcm.Location = new System.Drawing.Point(1, 1);
            this.tabPage_gcm.Name = "tabPage_gcm";
            this.tabPage_gcm.Size = new System.Drawing.Size(996, 475);
            this.tabPage_gcm.TabIndex = 1;
            this.tabPage_gcm.Text = "GCM Publish";
            // 
            // splitContainer_gcm
            // 
            this.splitContainer_gcm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer_gcm.Location = new System.Drawing.Point(0, 0);
            this.splitContainer_gcm.Name = "splitContainer_gcm";
            // 
            // splitContainer_gcm.Panel1
            // 
            this.splitContainer_gcm.Panel1.Controls.Add(this.dcmDataGridView2);
            this.splitContainer_gcm.Size = new System.Drawing.Size(996, 475);
            this.splitContainer_gcm.SplitterDistance = 750;
            this.splitContainer_gcm.TabIndex = 0;
            // 
            // dcmDataGridView2
            // 
            this.dcmDataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dcmDataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dcmDataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dcmDataGridView2.Name = "dcmDataGridView2";
            this.dcmDataGridView2.RowTemplate.Height = 23;
            this.dcmDataGridView2.Size = new System.Drawing.Size(750, 475);
            this.dcmDataGridView2.TabIndex = 0;
            // 
            // tabPage_abc
            // 
            this.tabPage_abc.BackColor = System.Drawing.Color.White;
            this.tabPage_abc.Controls.Add(this.splitContainer_abc);
            this.tabPage_abc.Font = new System.Drawing.Font("Arial", 10F);
            this.tabPage_abc.ImageIndex = 0;
            this.tabPage_abc.Location = new System.Drawing.Point(1, 1);
            this.tabPage_abc.Name = "tabPage_abc";
            this.tabPage_abc.Size = new System.Drawing.Size(996, 475);
            this.tabPage_abc.TabIndex = 2;
            this.tabPage_abc.Text = "ABC Refer";
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
            this.splitContainer_abc.Size = new System.Drawing.Size(996, 475);
            this.splitContainer_abc.SplitterDistance = 819;
            this.splitContainer_abc.TabIndex = 0;
            // 
            // abcBrowser1
            // 
            this.abcBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.abcBrowser1.Location = new System.Drawing.Point(0, 0);
            this.abcBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.abcBrowser1.Name = "abcBrowser1";
            this.abcBrowser1.Size = new System.Drawing.Size(996, 475);
            this.abcBrowser1.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "abc.png");
            this.imageList1.Images.SetKeyName(1, "gcm_logo.png");
            this.imageList1.Images.SetKeyName(2, "local.png");
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(32, 17);
            this.toolStripStatusLabel2.Text = "Test";
            this.toolStripStatusLabel2.Click += new System.EventHandler(this.toolStripStatusLabel2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 586);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gcmTabControl1.ResumeLayout(false);
            this.tabPage_local.ResumeLayout(false);
            this.splitContainer_local.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_local)).EndInit();
            this.splitContainer_local.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView1)).EndInit();
            this.tabPage_gcm.ResumeLayout(false);
            this.splitContainer_gcm.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_gcm)).EndInit();
            this.splitContainer_gcm.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dcmDataGridView2)).EndInit();
            this.tabPage_abc.ResumeLayout(false);
            this.splitContainer_abc.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer_abc)).EndInit();
            this.splitContainer_abc.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel1;
        private DCMControlLib.GCM.GCMTabControl gcmTabControl1;
        private DCMControlLib.GCM.TabPageEx tabPage_local;
        private DCMControlLib.GCM.TabPageEx tabPage_gcm;
        private DCMControlLib.GCM.TabPageEx tabPage_abc;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.SplitContainer splitContainer_local;
        private DCM.DCMDataGridView dcmDataGridView1;
        private DCM.DCMDGV.DCMTextDGVColumn StrainNumber;
        private System.Windows.Forms.SplitContainer splitContainer_gcm;
        private DCM.DCMDataGridView dcmDataGridView2;
        private System.Windows.Forms.SplitContainer splitContainer_abc;
        private DCMControlLib.GCM.ABCBrowser abcBrowser1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    }
}

