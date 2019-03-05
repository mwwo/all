﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fang.临时软件
{
    public partial class 连续出现 : Form
    {
        public 连续出现()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamReader sr = new StreamReader(openFileDialog1.FileName, Encoding.Default);
                //一次性读取完 
                string texts = sr.ReadToEnd();
                string[] text = texts.Split(new string[] { "\r\n" }, StringSplitOptions.None);

                for (int i = 0; i < text.Length; i++)
                {

                    ListViewItem lv1 = listView1.Items.Add(listView1.Items.Count.ToString()); //使用Listview展示数据
                    lv1.SubItems.Add(text[i]);


                }
            }
        }

        /// <summary>
        /// 获取第二列
        /// </summary>
        /// <returns></returns>
        public ArrayList getListviewValue1(ListView listview)

        {
            ArrayList values = new ArrayList();

            for (int i = 0; i < listview.Items.Count; i++)
            {
                ListViewItem item = listview.Items[i];

                values.Add(item.SubItems[1].Text);


            }

            return values;

        }

        ArrayList finishes = new ArrayList();
        #region  主函数
        public void run()

        {
            //StringBuilder sbz = new StringBuilder();
            //for (int i = 0; i <Convert.ToInt32(textBox1.Text); i++)
            //{
            //    sbz.Append("中");
            //}

            //StringBuilder sbc = new StringBuilder();
            //for (int i = 0; i < Convert.ToInt32(textBox1.Text); i++)
            //{
            //    sbc.Append("错");
            //}

          
            ArrayList lists = getListviewValue1(listView1);

            try

            {

                foreach (string list in lists)
                {
                    if (!finishes.Contains(list))
                    {
                        
                        finishes.Add(list);
                        string url = list.Replace("https://pk10.17500.cn/exp/index/eid/", "").Replace(".html", ""); ;

                        if (url == "")
                            return;
                         string html = method.GetUrl("https://pk10.17500.cn/exp/results.html?num=30&lotid=pk10&eid=" + url, "utf-8");
                        

                        string prttern = @"中|错";
                        MatchCollection matches = Regex.Matches(html, prttern, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                        
                        StringBuilder sb = new StringBuilder();
                        foreach (Match NextMatch in matches)
                        {

                            sb.Append(NextMatch.Groups[0].Value);

                        }

                        int a = 0;
                       
                        string prttern2 = @"中{2,}错{2,}";
                        MatchCollection matches2 = Regex.Matches(sb.ToString(), prttern2, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern3= @"中{2,}错{2,}中{2,}";
                        MatchCollection matches3 = Regex.Matches(sb.ToString(), prttern3, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern4 = @"中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection matches4 = Regex.Matches(sb.ToString(), prttern4, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern5 = @"中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection matches5 = Regex.Matches(sb.ToString(), prttern5, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern6 = @"中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection matches6 = Regex.Matches(sb.ToString(), prttern6, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern7 = @"中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection matches7 = Regex.Matches(sb.ToString(), prttern7, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern8 = @"中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection matches8 = Regex.Matches(sb.ToString(), prttern8, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern9 = @"中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection matches9 = Regex.Matches(sb.ToString(), prttern9, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern10 = @"中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection matches10 = Regex.Matches(sb.ToString(), prttern10, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern11 = @"中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection matches11 = Regex.Matches(sb.ToString(), prttern11, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern12 = @"中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection matches12 = Regex.Matches(sb.ToString(), prttern12, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern13 = @"中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection matches13 = Regex.Matches(sb.ToString(), prttern13, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern14 = @"中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection matches14 = Regex.Matches(sb.ToString(), prttern14, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern15 = @"中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection matches15 = Regex.Matches(sb.ToString(), prttern15, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern16 = @"中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection matches16 = Regex.Matches(sb.ToString(), prttern16, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                        if (matches2.Count > 0 && matches3.Count == 0)
                        {
                            a = 2;
                        }
                        else if (matches3.Count > 0 && matches4.Count == 0)
                        {
                            a = 3;
                        }
                        else if (matches4.Count > 0 && matches5.Count == 0)
                        {
                            a = 4;
                        }
                        else if (matches5.Count > 0 && matches6.Count == 0)
                        {
                            a = 5;
                        }
                        else if (matches6.Count > 0 && matches7.Count == 0)
                        {
                            a = 6;
                        }
                        else if (matches7.Count > 0 && matches8.Count == 0)
                        {
                            a = 7;
                        }
                        else if (matches8.Count > 0 && matches9.Count == 0)
                        {
                            a = 8;
                        }
                        else if (matches9.Count > 0 && matches10.Count == 0)
                        {
                            a = 9;
                        }
                        else if (matches10.Count > 0 && matches11.Count == 0)
                        {
                            a = 10;
                        }
                        else if (matches11.Count > 0 && matches12.Count == 0)
                        {
                            a = 11;
                        }
                        else if (matches12.Count > 0 && matches13.Count == 0)
                        {
                            a = 12;
                        }
                        else if (matches13.Count > 0 && matches14.Count == 0)
                        {
                            a = 13;
                        }
                        else if (matches14.Count > 0 && matches15.Count == 0)
                        {
                            a = 14;
                        }
                        else if (matches15.Count > 0 && matches16.Count == 0)
                        {
                            a = 15;
                        }


                        int b = 0;

                        string p2 = @"错{2,}中{2,}";
                        MatchCollection amatches2 = Regex.Matches(sb.ToString(), p2, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p3 = @"错{2,}中{2,}错{2,}";
                        MatchCollection amatches3 = Regex.Matches(sb.ToString(), p3, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p4 = @"错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection amatches4 = Regex.Matches(sb.ToString(), p4, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p5 = @"错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection amatches5 = Regex.Matches(sb.ToString(), p5, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p6 = @"错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection amatches6 = Regex.Matches(sb.ToString(), p6, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p7 = @"错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection amatches7 = Regex.Matches(sb.ToString(), p7, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p8 = @"错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection amatches8 = Regex.Matches(sb.ToString(), p8, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p9 = @"错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection amatches9 = Regex.Matches(sb.ToString(), p9, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p10 = @"错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection amatches10 = Regex.Matches(sb.ToString(), p10, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p11 = @"错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection amatches11 = Regex.Matches(sb.ToString(), p11, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p12 = @"错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection amatches12 = Regex.Matches(sb.ToString(), p12, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p13 = @"错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection amatches13 = Regex.Matches(sb.ToString(), p13, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p14 = @"错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection amatches14 = Regex.Matches(sb.ToString(), p14, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p15= @"错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}";
                        MatchCollection amatches15 = Regex.Matches(sb.ToString(), p15, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p16 = @"错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}错{2,}中{2,}";
                        MatchCollection amatches16 = Regex.Matches(sb.ToString(), p16, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                        if (amatches2.Count > 0 && amatches3.Count == 0)
                        {
                            b= 2;
                        }
                        else if (amatches3.Count > 0 && amatches4.Count == 0)
                        {
                            b = 3;
                        }
                        else if (amatches4.Count > 0 && amatches5.Count == 0)
                        {
                            b= 4;
                        }
                        else if (amatches5.Count > 0 && amatches6.Count == 0)
                        {
                            b = 5;
                        }
                        else if (amatches6.Count > 0 && amatches7.Count == 0)
                        {
                            b = 6;
                        }
                        else if (amatches7.Count > 0 && amatches8.Count == 0)
                        {
                            b= 7;
                        }
                        else if (amatches8.Count > 0 && amatches9.Count == 0)
                        {
                            b= 8;
                        }
                        else if (amatches9.Count > 0 && amatches10.Count == 0)
                        {
                            b = 9;
                        }
                        else if (amatches10.Count > 0 && amatches11.Count == 0)
                        {
                            b = 10;
                        }
                        else if (amatches11.Count > 0 && amatches12.Count == 0)
                        {
                            b = 11;
                        }
                        else if (amatches12.Count > 0 && amatches13.Count == 0)
                        {
                            b = 12;
                        }
                        else if (amatches13.Count > 0 && amatches14.Count == 0)
                        {
                            b = 13;
                        }
                        else if (amatches14.Count > 0 && amatches15.Count == 0)
                        {
                            b = 14;
                        }
                        else if (amatches15.Count > 0 && amatches16.Count == 0)
                        {
                            b = 15;
                        }

                        int c = a > b ? a : b;
                       

                        if (c >= Convert.ToInt32(textBox1.Text))
                        {
                            ListViewItem lv1 = listView2.Items.Add(list); //使用Listview展示数据
                            lv1.SubItems.Add(c.ToString());
                        }


                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion



        #region  主函数1
        public void run1()

        {


            ArrayList lists = getListviewValue1(listView1);

            try

            {

                foreach (string list in lists)
                {
                    if (!finishes.Contains(list))
                    {

                        finishes.Add(list);
                        string url = list.Replace("https://pk10.17500.cn/exp/index/eid/", "").Replace(".html", ""); ;

                        if (url == "")
                            return;
                        string html = method.GetUrl("https://pk10.17500.cn/exp/results.html?num=30&lotid=pk10&eid=" + url, "utf-8");


                        string prttern = @"中|错";
                        MatchCollection matches = Regex.Matches(html, prttern, RegexOptions.IgnoreCase | RegexOptions.Multiline);


                        StringBuilder sb = new StringBuilder();
                        foreach (Match NextMatch in matches)
                        {

                            sb.Append(NextMatch.Groups[0].Value);

                        }

                        int a = 0;

                        string prttern2 = @"错中{2}错{2}中";
                        MatchCollection matches2 = Regex.Matches(sb.ToString(), prttern2, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern3 = @"错中{2}错{2}中{2}错";
                        MatchCollection matches3 = Regex.Matches(sb.ToString(), prttern3, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern4 = @"错中{2}错{2}中{2}错{2}中";
                        MatchCollection matches4 = Regex.Matches(sb.ToString(), prttern4, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern5 = @"错中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection matches5 = Regex.Matches(sb.ToString(), prttern5, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern6 = @"错中{2}错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection matches6 = Regex.Matches(sb.ToString(), prttern6, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern7 = @"错中{2}错{2}中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection matches7 = Regex.Matches(sb.ToString(), prttern7, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern8 = @"错中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection matches8 = Regex.Matches(sb.ToString(), prttern8, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern9 = @"错中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection matches9 = Regex.Matches(sb.ToString(), prttern9, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern10 = @"错中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection matches10 = Regex.Matches(sb.ToString(), prttern10, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern11 = @"错中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection matches11 = Regex.Matches(sb.ToString(), prttern11, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern12 = @"错中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection matches12 = Regex.Matches(sb.ToString(), prttern12, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern13 = @"错中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection matches13 = Regex.Matches(sb.ToString(), prttern13, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern14 = @"错中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection matches14 = Regex.Matches(sb.ToString(), prttern14, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern15 = @"错中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection matches15 = Regex.Matches(sb.ToString(), prttern15, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string prttern16 = @"错中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection matches16 = Regex.Matches(sb.ToString(), prttern16, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                        if (matches2.Count > 0 && matches3.Count == 0)
                        {
                            a = 2;
                        }
                        else if (matches3.Count > 0 && matches4.Count == 0)
                        {
                            a = 3;
                        }
                        else if (matches4.Count > 0 && matches5.Count == 0)
                        {
                            a = 4;
                        }
                        else if (matches5.Count > 0 && matches6.Count == 0)
                        {
                            a = 5;
                        }
                        else if (matches6.Count > 0 && matches7.Count == 0)
                        {
                            a = 6;
                        }
                        else if (matches7.Count > 0 && matches8.Count == 0)
                        {
                            a = 7;
                        }
                        else if (matches8.Count > 0 && matches9.Count == 0)
                        {
                            a = 8;
                        }
                        else if (matches9.Count > 0 && matches10.Count == 0)
                        {
                            a = 9;
                        }
                        else if (matches10.Count > 0 && matches11.Count == 0)
                        {
                            a = 10;
                        }
                        else if (matches11.Count > 0 && matches12.Count == 0)
                        {
                            a = 11;
                        }
                        else if (matches12.Count > 0 && matches13.Count == 0)
                        {
                            a = 12;
                        }
                        else if (matches13.Count > 0 && matches14.Count == 0)
                        {
                            a = 13;
                        }
                        else if (matches14.Count > 0 && matches15.Count == 0)
                        {
                            a = 14;
                        }
                        else if (matches15.Count > 0 && matches16.Count == 0)
                        {
                            a = 15;
                        }


                        int b = 0;

                        string p2 = @"中错{2}中{2}错";
                        MatchCollection amatches2 = Regex.Matches(sb.ToString(), p2, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p3 = @"中错{2}中{2}错{2}中";
                        MatchCollection amatches3 = Regex.Matches(sb.ToString(), p3, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p4 = @"中错{2}中{2}错{2}中{2}错";
                        MatchCollection amatches4 = Regex.Matches(sb.ToString(), p4, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p5 = @"中错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection amatches5 = Regex.Matches(sb.ToString(), p5, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p6 = @"中错{2}中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection amatches6 = Regex.Matches(sb.ToString(), p6, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p7 = @"中错{2}中{2}错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection amatches7 = Regex.Matches(sb.ToString(), p7, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p8 = @"中错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection amatches8 = Regex.Matches(sb.ToString(), p8, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p9 = @"中错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection amatches9 = Regex.Matches(sb.ToString(), p9, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p10 = @"中错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection amatches10 = Regex.Matches(sb.ToString(), p10, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p11 = @"中错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection amatches11 = Regex.Matches(sb.ToString(), p11, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p12 = @"中错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection amatches12 = Regex.Matches(sb.ToString(), p12, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p13 = @"中错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection amatches13 = Regex.Matches(sb.ToString(), p13, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p14 = @"中错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection amatches14 = Regex.Matches(sb.ToString(), p14, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p15 = @"中错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中";
                        MatchCollection amatches15 = Regex.Matches(sb.ToString(), p15, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                        string p16 = @"中错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错{2}中{2}错";
                        MatchCollection amatches16 = Regex.Matches(sb.ToString(), p16, RegexOptions.IgnoreCase | RegexOptions.Multiline);

                        if (amatches2.Count > 0 && amatches3.Count == 0)
                        {
                            b = 2;
                        }
                        else if (amatches3.Count > 0 && amatches4.Count == 0)
                        {
                            b = 3;
                        }
                        else if (amatches4.Count > 0 && amatches5.Count == 0)
                        {
                            b = 4;
                        }
                        else if (amatches5.Count > 0 && amatches6.Count == 0)
                        {
                            b = 5;
                        }
                        else if (amatches6.Count > 0 && amatches7.Count == 0)
                        {
                            b = 6;
                        }
                        else if (amatches7.Count > 0 && amatches8.Count == 0)
                        {
                            b = 7;
                        }
                        else if (amatches8.Count > 0 && amatches9.Count == 0)
                        {
                            b = 8;
                        }
                        else if (amatches9.Count > 0 && amatches10.Count == 0)
                        {
                            b = 9;
                        }
                        else if (amatches10.Count > 0 && amatches11.Count == 0)
                        {
                            b = 10;
                        }
                        else if (amatches11.Count > 0 && amatches12.Count == 0)
                        {
                            b = 11;
                        }
                        else if (amatches12.Count > 0 && amatches13.Count == 0)
                        {
                            b = 12;
                        }
                        else if (amatches13.Count > 0 && amatches14.Count == 0)
                        {
                            b = 13;
                        }
                        else if (amatches14.Count > 0 && amatches15.Count == 0)
                        {
                            b = 14;
                        }
                        else if (amatches15.Count > 0 && amatches16.Count == 0)
                        {
                            b = 15;
                        }

                        int c = a > b ? a : b;


                        if (c >= Convert.ToInt32(textBox1.Text))
                        {
                            ListViewItem lv1 = listView2.Items.Add(list); //使用Listview展示数据
                            lv1.SubItems.Add(c.ToString());
                        }


                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        private void 连续出现_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                if (radioButton1.Checked == true)
                {
                    Thread thread = new Thread(new ThreadStart(run));
                    thread.Start();
                    timer1.Start();
                }

                else if (radioButton2.Checked == true)
                {
                    Thread thread = new Thread(new ThreadStart(run1));
                    thread.Start();
                    timer1.Start();
                }
            }
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true)
            {
                Thread thread = new Thread(new ThreadStart(run));
                thread.Start();
                timer1.Start();
            }

            else if (radioButton2.Checked == true)
            {
                Thread thread = new Thread(new ThreadStart(run1));
                thread.Start();
                timer1.Start();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
}
