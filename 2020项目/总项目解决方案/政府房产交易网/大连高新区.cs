﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 政府房产交易网
{
    public partial class 大连高新区 : Form
    {
        public 大连高新区()
        {
            InitializeComponent();
        }

        fang_method md = new fang_method();

        public void setlog(string str)
        {
            if (logtxt.Text.Length > 100)
            {
                logtxt.Text = "";

            }
            logtxt.Text += str + Environment.NewLine;
        }
        private void 大连高新区_Load(object sender, EventArgs e)
        {
            logtxt.Text = DateTime.Now.ToString() + "程序已启动，正在执行......";

            #region 通用检测

            string html = md.GetUrl("http://www.acaiji.com/index/index/vip.html", "utf-8");

            if (!html.Contains(@"fangchanxinxi"))
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }

            #endregion



            Control.CheckForIllegalCrossThreadCalls = false;
            md.getlogs += new fang_method.GetLogs(setlog);

            md.dlgxqz_start();
           
        }
    }
}
