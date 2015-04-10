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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gcmTabControl1 = new IDCM.GCMControlLib.GCMTabControl();
            this.tabPageEx1 = new IDCM.GCMControlLib.TabPageEx();
            this.tabPageEx2 = new IDCM.GCMControlLib.TabPageEx();
            this.tabPageEx3 = new IDCM.GCMControlLib.TabPageEx();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gcmTabControl1.SuspendLayout();
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
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 564);
            this.statusStrip1.Name = "statusStrip1";
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
            this.gcmTabControl1.Alignments = IDCM.GCMControlLib.GCMTabControl.TabAlignments.Bottom;
            this.gcmTabControl1.AllowDrop = true;
            this.gcmTabControl1.BackgroundHatcher.HatchType = System.Drawing.Drawing2D.HatchStyle.DashedVertical;
            this.gcmTabControl1.Controls.Add(this.tabPageEx1);
            this.gcmTabControl1.Controls.Add(this.tabPageEx2);
            this.gcmTabControl1.Controls.Add(this.tabPageEx3);
            this.gcmTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcmTabControl1.ItemSize = new System.Drawing.Size(0, 26);
            this.gcmTabControl1.Location = new System.Drawing.Point(0, 0);
            this.gcmTabControl1.Name = "gcmTabControl1";
            this.gcmTabControl1.SelectedIndex = 0;
            this.gcmTabControl1.Size = new System.Drawing.Size(998, 514);
            this.gcmTabControl1.TabGradient.ColorEnd = System.Drawing.Color.Gainsboro;
            this.gcmTabControl1.TabIndex = 0;
            this.gcmTabControl1.UpDownStyle = IDCM.GCMControlLib.GCMTabControl.UpDown32Style.Default;
            // 
            // tabPageEx1
            // 
            this.tabPageEx1.BackColor = System.Drawing.Color.White;
            this.tabPageEx1.Font = new System.Drawing.Font("Arial", 10F);
            this.tabPageEx1.Location = new System.Drawing.Point(1, 22);
            this.tabPageEx1.Name = "tabPageEx1";
            this.tabPageEx1.Size = new System.Drawing.Size(996, 461);
            this.tabPageEx1.TabIndex = 0;
            this.tabPageEx1.Text = "tabPageEx1";
            // 
            // tabPageEx2
            // 
            this.tabPageEx2.BackColor = System.Drawing.Color.White;
            this.tabPageEx2.Font = new System.Drawing.Font("Arial", 10F);
            this.tabPageEx2.Location = new System.Drawing.Point(1, 22);
            this.tabPageEx2.Name = "tabPageEx2";
            this.tabPageEx2.Size = new System.Drawing.Size(996, 462);
            this.tabPageEx2.TabIndex = 1;
            this.tabPageEx2.Text = "tabPageEx2";
            // 
            // tabPageEx3
            // 
            this.tabPageEx3.BackColor = System.Drawing.Color.White;
            this.tabPageEx3.Font = new System.Drawing.Font("Arial", 10F);
            this.tabPageEx3.Location = new System.Drawing.Point(1, 22);
            this.tabPageEx3.Name = "tabPageEx3";
            this.tabPageEx3.Size = new System.Drawing.Size(996, 462);
            this.tabPageEx3.TabIndex = 2;
            this.tabPageEx3.Text = "tabPageEx3";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
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
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel1;
        private IDCM.GCMControlLib.GCMTabControl gcmTabControl1;
        private IDCM.GCMControlLib.TabPageEx tabPageEx1;
        private IDCM.GCMControlLib.TabPageEx tabPageEx2;
        private IDCM.GCMControlLib.TabPageEx tabPageEx3;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}

