﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDCM.GCMControlLib;
using IDCM.TmpTest;

namespace IDCMPro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            new QuickTest_Data().test();
        }
    }
}
