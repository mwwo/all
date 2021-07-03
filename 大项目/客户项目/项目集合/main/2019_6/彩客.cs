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

namespace main._2019_6
{
    public partial class 彩客 : Form
    {
        public 彩客()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 获取当前的时间戳
        /// </summary>
        /// <returns></returns>
        public static string Timestamp()
        {
            long ts = ConvertDateTimeToInt(DateTime.Now);
            return ts.ToString();
        }

        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            //long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            long t = (time.Ticks - 621356256000000000) / 10000;
            return t;
        }

        private void 彩客_Load(object sender, EventArgs e)
        {

        }
        bool zanting = true;
        #region  运行程序
        public void run()
        {


            string URL = "http://www.310win.com/info/match/data/goal3.xml?" + Timestamp()+ "000";

            string html = method.GetUrl(URL, "utf-8");

            Match ids = Regex.Match(html, @"<ids>([\s\S]*?)</ids>");

            string[] IDS = ids.Groups[1].Value.Split(new string[] { "," }, StringSplitOptions.None);
          
            for (int i = 0; i < IDS.Length-1; i++)
            {
                string url = "http://1x2d.win007.com/"+IDS[i]+".js";
                string strhtml = method.GetUrl(url, "utf-8");
                Match aaas = Regex.Match(strhtml, @"Vcbet\|([\s\S]*?)伟德");
                //Match bbbs = Regex.Match(strhtml, @"Expekt\|([\s\S]*?)Expekt");
                //Match cccs = Regex.Match(strhtml, @"Marathon\|([\s\S]*?)马博");
                Match ddds = Regex.Match(strhtml, @"William Hill\|([\s\S]*?)威廉希尔");
            

                Match zhu = Regex.Match(strhtml, @"hometeam_cn=""([\s\S]*?)""");
                Match ke = Regex.Match(strhtml, @"guestteam_cn=""([\s\S]*?)""");
                Match date = Regex.Match(strhtml, @"MatchTime=""([\s\S]*?)""");

                string[] time = date.Groups[1].Value.Split(new string[] { "," }, StringSplitOptions.None);//获取时间格式
                string TIME = "无";
                if (time.Length >4)
                {
                    int h = Convert.ToInt32(time[3]) + 8;
                   TIME = time[0] + "年" + time[1].Replace("-1", "") + "月" + time[2] + "日" + h + ":" + time[4];
                }
                
              






                string[] aaa = aaas.Groups[1].Value.Split(new string[] { "|" }, StringSplitOptions.None);
                //string[] bbb = bbbs.Groups[1].Value.Split(new string[] { "|" }, StringSplitOptions.None);
                //string[] ccc = cccs.Groups[1].Value.Split(new string[] { "|" }, StringSplitOptions.None);
                string[] ddd = ddds.Groups[1].Value.Split(new string[] { "|" }, StringSplitOptions.None);
                
                if (aaa.Length > 6)
                {
                    ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count).ToString()); //使用Listview展示数据         

                    lv1.SubItems.Add(aaa[0]);   //比分
                    lv1.SubItems.Add(aaa[1]);   //比分
                    lv1.SubItems.Add(aaa[2]);   //比分
                    lv1.SubItems.Add(aaa[6]);   //比分

                    //for (int j = 0; j < 7; j++)
                    //{
                    //    lv1.SubItems.Add(aaa[j]);   //比分
                    //}

                    lv1.SubItems.Add(zhu.Groups[1].Value + "：" + ke.Groups[1].Value);
                    lv1.SubItems.Add("伟德");  
                    lv1.SubItems.Add(TIME);   //时间

                }
                //if (bbb.Length > 6)
                //{
                //    ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count).ToString()); //使用Listview展示数据         


                //    lv1.SubItems.Add(bbb[0]);   //比分
                //    lv1.SubItems.Add(bbb[1]);   //比分
                //    lv1.SubItems.Add(bbb[2]);   //比分
                //    lv1.SubItems.Add(bbb[6]);   //比分
                  

                //    //for (int j = 0; j < 7; j++)
                //    //{       
                //    //    lv1.SubItems.Add(bbb[j]);   //比分
                //    //}
                //    lv1.SubItems.Add(zhu.Groups[1].Value+"："+ke.Groups[1].Value);
                //    lv1.SubItems.Add("Expekt（瑞典）");
                //    lv1.SubItems.Add(TIME);   //时间

                //}
                //if (ccc.Length > 6)
                //{
                //    ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count).ToString()); //使用Listview展示数据         

                //    lv1.SubItems.Add(ccc[0]);   //比分
                //    lv1.SubItems.Add(ccc[1]);   //比分
                //    lv1.SubItems.Add(ccc[2]);   //比分
                //    lv1.SubItems.Add(ccc[6]);   //比分
                   

                //    //for (int j = 0; j < 7; j++)
                //    //{
                //    //    lv1.SubItems.Add(ccc[j]);   //比分
                //    //}
                //    lv1.SubItems.Add(zhu.Groups[1].Value + "：" + ke.Groups[1].Value);
                //    lv1.SubItems.Add("马博（荷属安的列斯群岛）");   
                //    lv1.SubItems.Add(TIME);   //时间
                //}
                if (ddd.Length > 6)
                {
                    ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count).ToString()); //使用Listview展示数据         


                    lv1.SubItems.Add(ddd[0]);   //比分
                    lv1.SubItems.Add(ddd[1]);   //比分
                    lv1.SubItems.Add(ddd[2]);   //比分
                    lv1.SubItems.Add(ddd[6]);   //比分
                   

                    //for (int j = 0; j < 7; j++)
                    //{
                    //    lv1.SubItems.Add(ddd[j]);   //比分
                    //}
                    lv1.SubItems.Add(zhu.Groups[1].Value + "：" + ke.Groups[1].Value);
                    lv1.SubItems.Add("威廉希尔");   
                    lv1.SubItems.Add(TIME);   //时间
                }

          

                if ( aaa.Length > 6 || ddd.Length > 6)
                {
                    ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count).ToString()); //使用Listview展示数据  
                    for (int j = 0; j < 9; j++)
                    {

                        lv1.SubItems.Add("---------------------------");   //比分
                    }
                }
                while (this.zanting == false)
                {
                    label1.Text = "已暂停....";
                    Application.DoEvents();//如果loader是false表明正在加载,,则Application.DoEvents()意思就是处理其他消息。阻止当前的队列继续执行。
                }


                if (listView1.Items.Count > 2)
                {
                    listView1.EnsureVisible(listView1.Items.Count - 1);  //滚动到指定位置
                }
                Thread.Sleep(1000);
            }
           

           
            label1.Text = "抓取结束，请点击导出，文本名为【导出结果】";
        }

        #endregion



        private void Button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(run));
            thread.Start();
            Control.CheckForIllegalCrossThreadCalls = false;
            label1.Text = "软件已经开始运行请勿重复点击....";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            zanting = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            zanting = true;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            method.DataTableToExcel(method.listViewToDataTable(this.listView1), "Sheet1", true);
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void 彩客_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要关闭吗？", "关闭", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;//点取消的代码 
            }
        }
    }
}


