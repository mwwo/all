﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main._2019_7
{
    public partial class 网易充值 : Form
    {
        public 网易充值()
        {
            InitializeComponent();
        }

     

        public static string COOKIE;
        bool zanting = true;

        private void splitContainer1_Panel2_MouseEnter(object sender, EventArgs e)
        {
           

        }



        #region 主程序
        public void run()
        {
            COOKIE = textBox1.Text;
            try
            {
                for (int i = 1; i < 9999; i++)
                {
     
                    string Url = "http://ecard.163.com/account/query_point?begin_time=2019-04-21+00%3A00%3A00&end_time=2019-07-21+23%3A59%3A59&page=" + i;

                    string html = method.GetUrlWithCookie(Url,COOKIE ,"gb2312");
                    
                    MatchCollection ids = Regex.Matches(html, @"<td class=""red"">([\s\S]*?)<br>", RegexOptions.IgnoreCase | RegexOptions.Multiline);

                    if (ids.Count == 0)
                    {
                        label1.Text = "抓取结束";
                        return;
                        
                    }
                        
                    label1.Text = "正在抓取第"+i+"页，未发现充值";
                    for (int j = 0; j < ids.Count; j++)
                    {
                        if (ids[j].Groups[1].Value.Contains("充值"))
                        {

                            ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count).ToString()); //使用Listview展示数据         
                            lv1.SubItems.Add(ids[j].Groups[1].Value.Trim());
                            if (listView1.Items.Count > 2)
                            {
                                listView1.EnsureVisible(listView1.Items.Count - 1);  //滚动到指定位置
                            }
                        }
                    }

                    while (this.zanting == false)
                        {
                            Application.DoEvents();//如果loader是false表明正在加载,,则Application.DoEvents()意思就是处理其他消息。阻止当前的队列继续执行。
                        }
                    
                        //Thread.Sleep(1000);
                    }

                }
            

            catch (System.Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            zanting = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            zanting = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(run));
            Control.CheckForIllegalCrossThreadCalls = false;
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            method.DataTableToExcel(method.listViewToDataTable(this.listView1), "Sheet1", true);
        }

        private void 网易充值_Load(object sender, EventArgs e)
        {

        }
    }
}
