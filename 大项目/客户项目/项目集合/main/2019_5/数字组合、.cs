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

namespace main._2019_5
{
    public partial class 数字组合 : Form
    {
        public 数字组合()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

     

        ArrayList finishes = new ArrayList();

        public void run()
        {
            for (long a = 0; a < 999999999999; a++)
            {
                if (listView1.Items.Count == 2400)
                    return;

                ArrayList list2 = new ArrayList();
                ArrayList list = new ArrayList();
                list.Add("1");
                list.Add("2");
                list.Add("3");
                list.Add("4");
                list.Add("5");
                list.Add("6");
                list.Add("7");
                list.Add("8");
                list.Add("9");
                list.Add("10");
                list.Add("11");
                list.Add("12");
                list.Add("13");
                list.Add("14");
                list.Add("15");
                list.Add("16");
                list.Add("17");
                list.Add("18");
                list.Add("19");
                list.Add("20");
                list.Add("21");
                list.Add("22");
                list.Add("23");
                list.Add("24");
                list.Add("25");
                list.Add("26");
                list.Add("27");
                list.Add("28");
                list.Add("29");
                list.Add("30");
                list.Add("31");
                list.Add("32");
                list.Add("33");
                list.Add("34");
                list.Add("35");
                list.Add("36");
                list.Add("37");
                list.Add("38");
                list.Add("39");
                list.Add("40");
                list.Add("41");
                list.Add("42");
                list.Add("43");
                list.Add("44");
                list.Add("45");
                list.Add("46");
                list.Add("47");
                list.Add("48");


                for (int i = 0; i < 999999; i++)
                {
                    if (list.Count == 0)
                        break;
                   

                    Random random = new Random();
                    var shuzhi = random.Next(list.Count);
                    list2.Add(list[shuzhi]);
                    list.Remove(list[shuzhi]);
                    if (list.Count == 24 && list2.Count == 24)
                    {
                        StringBuilder sb = new StringBuilder();
                        StringBuilder sb1 = new StringBuilder();
                        for (int j = 0; j < 24; j++)
                        {

                            sb.Append("," + list[j].ToString());
                            sb1.Append("," + list2[j].ToString());
                        }
                        if (!finishes.Contains(sb.ToString()))
                        {
                            finishes.Add(sb.ToString());

                            ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count + 1).ToString()); //使用Listview展示数据         
                            lv1.SubItems.Add(sb.ToString()+",");
                            ListViewItem lv2 = listView2.Items.Add((listView2.Items.Count + 1).ToString()); //使用Listview展示数据         
                            lv2.SubItems.Add(sb1.ToString()+",");
                            if (this.listView1.Items.Count > 2)
                            {
                                this.listView1.EnsureVisible(this.listView1.Items.Count - 1);
                            }
                            if (this.listView2.Items.Count > 2)
                            {
                                this.listView2.EnsureVisible(this.listView2.Items.Count - 1);
                            }

                            break;
                        }
                      
                        
                    }



                }
            }
        }

       

        private void 数字组合_Load(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory;
            StreamReader sr = new StreamReader(path + "A.txt", Encoding.Default);
            //一次性读取完 
            string texts = sr.ReadToEnd();
            string[] text = texts.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < text.Length; i++)
            {

                ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count + 1).ToString()); //使用Listview展示数据
                lv1.SubItems.Add(text[i]);


            }

            StreamReader sr1 = new StreamReader(path + "B.txt", Encoding.Default);
            //一次性读取完 
            string texts1 = sr1.ReadToEnd();
            string[] text1 = texts1.Split(new string[] { "\r\n" }, StringSplitOptions.None);

            for (int i = 0; i < text1.Length; i++)
            {

                ListViewItem lv2 = listView2.Items.Add((listView2.Items.Count + 1).ToString()); //使用Listview展示数据
                lv2.SubItems.Add(text1[i]);


            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           
                Thread thread = new Thread(new ThreadStart(run));
                thread.Start();
           


        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }


     

        private void Button2_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                listView1.Items[i].BackColor = Color.White;
            }
            for (int i = 0; i < listView2.Items.Count; i++)
            {
                listView2.Items[i].BackColor = Color.White;
            }



            for (int i =0; i < listView1.Items.Count; i++)
            {

                for (int j = 0; j < listView1.Columns.Count; j++)
                {
                    if (listView1.Items[i].SubItems[j].Text.Contains("," + textBox1.Text.Trim() + ","))

                    {
                        listView1.Items[i].BackColor = Color.Red;
                    }
                }

            }


            for (int i = 0; i < listView2.Items.Count; i++)
            {
                for (int j = 0; j < listView2.Columns.Count; j++)
                {
                    if (listView2.Items[i].SubItems[j].Text.Contains("," + textBox1.Text.Trim() + ","))

                    {
                        listView2.Items[i].BackColor = Color.Red;
                    }
                }
            }

         //   foreach (int item in list)
         //   {
         //       for (int i = 0; i < listView1.Items.Count; i++)
         //       {
         //           if (listView1.Items[i].SubItems[0].Text.Trim() == item.ToString().Trim())
         //           {
         //               listView1.Items.RemoveAt(i);
         //           }

         //       }
         //}
               
           




        }

        private void Button3_Click(object sender, EventArgs e)
        {
           

            //for (int i = 0; i < listView1.Items.Count; i++)
            //{
            //    if (listView1.Items[i].BackColor != Color.Red)
            //    {
            //        listView1.Items[i].SubItems[1].Text = "";
            //        listView1.Items[i].SubItems[0].Text = "";

            //    }
            //}

            //for (int i = 0; i < listView2.Items.Count; i++)
            //{
            //    if (listView2.Items[i].BackColor != Color.Red)
            //    {
            //        listView2.Items[i].SubItems[1].Text = "";
            //        listView2.Items[i].SubItems[0].Text = "";
            //    }
            //}

            ArrayList list1 = new ArrayList();
            ArrayList list2 = new ArrayList();
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                if (listView1.Items[i].BackColor == Color.Red)
                {
                    list1.Add(listView1.Items[i].SubItems[0].Text+"-"+listView1.Items[i].SubItems[1].Text);

                }
            }

            for (int i = 0; i < listView2.Items.Count; i++)
            {
                if (listView2.Items[i].BackColor == Color.Red)
                {
                    list2.Add(listView2.Items[i].SubItems[0].Text+"-"+listView2.Items[i].SubItems[1].Text);
                }
            }
            listView1.Items.Clear();
            listView2.Items.Clear();
            for (int i = 0; i < list1.Count; i++)
            {
                string[] text = list1[i].ToString().Split(new string[] { "-" }, StringSplitOptions.None);

                ListViewItem lv1 = listView1.Items.Add((text[0]).ToString()); //使用Listview展示数据
                lv1.SubItems.Add(text[1].ToString());


            }

            for (int i = 0; i < list2.Count; i++)
            {
                string[] text = list2[i].ToString().Split(new string[] { "-" }, StringSplitOptions.None);
                ListViewItem lv1 = listView2.Items.Add((text[0]).ToString()); //使用Listview展示数据
                lv1.SubItems.Add(text[1].ToString());


            }



        }

        private void Button4_Click(object sender, EventArgs e)
        {
            method.DataTableToExcel(method.listViewToDataTable(this.listView1), "Sheet1", true);
            method.DataTableToExcel(method.listViewToDataTable(this.listView2), "Sheet1", true);
        }

      
    }
}
