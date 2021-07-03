﻿using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace main._2019_4
{
    public partial class 拼多多 : Form
    {
        public 拼多多()
        {
            InitializeComponent();
        }

        private void 拼多多_Load(object sender, EventArgs e)
        {

        }


        #region  拼多多
        public void run()
        {

            try
            {
                string[] ids = textBox1.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

              

                foreach (string id in ids)
                {

                    String Url = "http://mobile.yangkeduo.com/goods.html?goods_id=" + id;

                    string html = method.GetUrl(Url, "utf-8");


                    Match titles = Regex.Match(html, @"""goodsName"":""([\s\S]*?)""");

                    string title = titles.Groups[1].Value;
                   
       
                    for (int i = 0; i < Convert.ToInt32(textBox3.Text); i++)
                    {
                       
                        ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count + 1).ToString()); //使用Listview展示数据
                        lv1.SubItems.Add(Url);
                        lv1.SubItems.Add(title.Trim());

                        listView1.EnsureVisible(listView1.Items.Count - 1);  //滚动到指定位置
             
                    }


                    Thread.Sleep(Convert.ToInt32(500));   //内容获取间隔，可变量        
                }


            }

            


            catch (System.Exception ex)
            {

                textBox1.Text = ex.ToString();
            }

        }

        #endregion
        private void button1_Click(object sender, EventArgs e)
        {
            //#region   读取注册码信息才能运行软件！

            //RegistryKey rsg = Registry.CurrentUser.OpenSubKey("zhucema"); //true表可修改                
            //if (rsg != null && rsg.GetValue("mac") != null)  //如果值不为空
            //{
            //    Thread thread = new Thread(new ThreadStart(run));
            //    Control.CheckForIllegalCrossThreadCalls = false;
            //    thread.Start();

            //}

            //else
            //{
            //    MessageBox.Show("请注册软件！");
            //    register lg = new register();
            //    lg.Show();
            //}

            //#endregion
            Thread thread = new Thread(new ThreadStart(run));
            Control.CheckForIllegalCrossThreadCalls = false;
            thread.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                for (int i = 0; i < Convert.ToInt32(textBox3.Text); i++)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (ListViewItem item in listView1.Items)
                    {
                        if ((item.Index + Convert.ToInt32(textBox3.Text) + 1) % Convert.ToInt32(textBox3.Text) == i)
                        {
                            List<string> list = new List<string>();
                            string temp = item.SubItems[1].Text;
                            string temp1 = item.SubItems[2].Text;
                            list.Add(temp + "-----" + temp1);

                            foreach (string tel in list)
                            {
                                sb.AppendLine(tel);
                            }

                        }

                    }

                    string path = "";
                    if (i == 0)
                    {
                        
                        path = dialog.SelectedPath + "\\"+textBox10.Text + textBox3.Text + ".txt";
                        
                    }
                    else
                    {
                        path = dialog.SelectedPath +"\\" +textBox10.Text + i + ".txt";
                    }


                    System.IO.File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

                }
                MessageBox.Show("导出完成");
            }
        }

        private static void export(List<string> list)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] keys = textBox4.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            foreach (ListViewItem item in listView1.Items)
            {
                foreach (string key in keys)
                {
                    item.SubItems[2].Text = item.SubItems[2].Text.Replace(key.Trim(), "").Replace(" ","");
                }


            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.SubItems[2].Text = textBox7.Text + item.SubItems[2].Text + textBox8.Text;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                decimal a = (Convert.ToDecimal((100 - Convert.ToInt32(textBox2.Text))) / 100);
                item.SubItems[2].Text = item.SubItems[2].Text.Substring(0, Convert.ToInt32(item.SubItems[2].Text.Length * a)); 

            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string url2 = "http://www.ciwuyou.com/fenci/";
            string datautf8= HttpUtility.UrlEncode(textBox9.Text, Encoding.UTF8); ;
            string postdata = "text="+ datautf8 + "&set_ignore=1&do_fork=1&Submit=%E5%88%86+%E8%AF%8D";

            string strhtml = method.PostUrl(url2,postdata,"", "utf-8");
            
            MatchCollection keywords = Regex.Matches(strhtml, @"<td width=""70"">([\s\S]*?)</td>([\s\S]*?)<td width=""71"">([\s\S]*?)</td>");

            for (int i = 0; i < keywords.Count; i++)
            {
                ListViewItem lv2 = listView2.Items.Add((listView2.Items.Count + 1).ToString()); //使用Listview展示数据
                lv2.SubItems.Add(keywords[i].Groups[3].ToString());
               
            }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ArrayList keys =new ArrayList();

            foreach (ListViewItem item in listView2.Items)
            {
                keys.Add(item.SubItems[1].Text);
            }


                Random rd = new Random();
            foreach (ListViewItem item in listView1.Items)
            {

                while (item.SubItems[2].Text.Length < 29)
                {
                    int suiji = rd.Next(0, keys.Count);
                    if (!item.SubItems[2].Text.Contains(keys[Convert.ToInt32(suiji)].ToString()))  //包含重复的继续下次随机
                    {
                        
                        item.SubItems[2].Text = item.SubItems[2].Text + keys[Convert.ToInt32(suiji)].ToString();
                    }
                }
            }
           

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string[] keys = textBox5.Text.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            foreach (ListViewItem item in listView1.Items)
            {
                foreach (string key in keys)
                {
                    item.SubItems[2].Text = item.SubItems[2].Text.Replace(key, textBox6.Text); ;
                }
                
            }
        }

        private void listView2_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Index = 0;
            if (this.listView2.SelectedItems.Count > 0)//判断listview有被选中项
            {
                Index = this.listView2.SelectedItems[0].Index;//取当前选中项的index,SelectedItems[0]这必须为0
                listView2.Items[Index].Remove();
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            textBox9.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            textBox1.Text = "";
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem lvi = listView1.SelectedItems[0];
            TextBox tb = new TextBox();
            tb.Width = 1000;
            tb.LostFocus += new EventHandler(this.Text_Blur);
            tb.Location = new Point(lvi.SubItems[2].Bounds.Left, lvi.SubItems[2].Bounds.Top);
            listView1.Controls.Add(tb);
            tb.Focus();
            tb.Text = lvi.SubItems[2].Text;

        }

        private void Text_Blur(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;
            ListViewItem lvi = listView1.SelectedItems[0];
            lvi.SubItems[2].Text = tb.Text;
            tb.Parent.Controls.Remove(tb);
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            
                
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            
        }

        private void 访问网址ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem lvi = listView1.SelectedItems[0];
            System.Diagnostics.Process.Start(lvi.SubItems[1].Text);
        }

        private void 删除该行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Index = 0;
            if (this.listView1.SelectedItems.Count > 0)//判断listview有被选中项
            {
                Index = this.listView1.SelectedItems[0].Index;//取当前选中项的index,SelectedItems[0]这必须为0
                listView1.Items[Index].Remove();
            }
        }

       
        }
    }
