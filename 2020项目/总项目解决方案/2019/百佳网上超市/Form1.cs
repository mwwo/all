﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using helper;


namespace 百佳网上超市
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        bool zanting = true;


        #region  主程序(旧)
        public void run_old()
        {
            if (textBox3.Text == "")

            {
                MessageBox.Show("请输入分类网址");
                return;
            }
            try
            {
                textBox2.Text += "已启动正在采集......" + "\r\n";

                //https://www.parknshop.com/zh-hk/biscuits-snacks-confectionery/other-snacks/c/030500
                for (int i = 1; i < 100; i = i + 1)
                {

                    string url = textBox3.Text + "?q=:igcBestSeller&page=" + i + "&resultsForPage=35&text=&sort=&category2nd=1&category3rd=0&minSel=9.0&maxSel=178.0&minSlider=9.0&maxSlider=178.0&_=1569719729085";
                    string html = method.GetUrl(url, "utf-8");

                    MatchCollection ids = Regex.Matches(html, @"<div class=""name"">([\s\S]*?)<a href=""([\s\S]*?)""");

                    if (ids.Count == 0)
                        break;
                    for (int j = 0; j < ids.Count; j++)
                    {


                        string URL = "https://www.parknshop.com" + ids[j].Groups[2].Value;
                        textBox2.Text += "正在采集......" + URL + "\r\n";
                        if (!URL.Contains("-:"))
                        {
                            string strhtml = method.GetUrl(URL, "utf-8");  //中文源码
                            string enhtml = method.GetUrl(URL.Replace("zh-hk", "en"), "utf-8");  //中文源码


                            Match mingzi = Regex.Match(strhtml, @"<div class=""itemName"">([\s\S]*?)</div>");
                            Match price = Regex.Match(strhtml, @"<div class=""price discount"">([\s\S]*?)</span>");
                            Match miaoshu1 = Regex.Match(strhtml, @"itemprop=""description"">([\s\S]*?)</div>");

                            Match name = Regex.Match(enhtml, @"<div class=""itemName"">([\s\S]*?)</div>");
                            Match description1 = Regex.Match(enhtml, @"itemprop=""description"">([\s\S]*?)</div>");


                            string miaoshu2 = Regex.Replace(miaoshu1.Groups[1].Value, @"<table>[\s\S]*</table>", "");
                            string miaoshu = Regex.Replace(miaoshu2, "<[^>]+>", "").Replace("Ingredient:", "").Replace("-->", ""); ;


                            string description2 = Regex.Replace(description1.Groups[1].Value, @"<table>[\s\S]*</table>", "");
                            string description = Regex.Replace(description2, "<[^>]+>", "").Replace("Ingredient:", "").Replace("-->", "");


                            ListViewItem listViewItem = this.listView1.Items.Add((listView1.Items.Count + 1).ToString());

                            listViewItem.SubItems.Add(Regex.Replace(mingzi.Groups[1].Value, "<[^>]+>", ""));
                            listViewItem.SubItems.Add(Regex.Replace(price.Groups[1].Value, "<[^>]+>", ""));
                            listViewItem.SubItems.Add(miaoshu);
                            listViewItem.SubItems.Add(Regex.Replace(name.Groups[1].Value, "<[^>]+>", ""));
                            listViewItem.SubItems.Add(description);

                            //构造下载地址，下载图片


                            Match shuzi = Regex.Match(ids[j].Groups[2].Value, @"\d{5,}");

                            string downUrl = "https://www.parknshop.com" + ids[j].Groups[2].Value + "/showGalleryImages?&codeVarSel=" + shuzi.Groups[0].Value;


                            getimage(removeValid(Regex.Replace(mingzi.Groups[1].Value, "<[^>]+>", "")), downUrl);
                            while (this.zanting == false)
                            {
                                Application.DoEvents();//如果loader是false表明正在加载,,则Application.DoEvents()意思就是处理其他消息。阻止当前的队列继续执行。
                            }

                            Thread.Sleep(100);



                        }

                    }



                }
                textBox2.Text += "抓取结束";

            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        #endregion


        #region GET请求
        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="Url">网址</param>
        /// <returns></returns>
        public static string GetUrl(string Url, string charset)
        {
            string html = "";
            string COOKIE = "";
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //获取不到加上这一条
                //ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;  //用于验证服务器证书
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);  //创建一个链接
                //request.Proxy = null;//防止代理抓包
                //request.AllowAutoRedirect = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.113 Safari/537.36";
                //request.Referer = Url;
                //添加头部
                //WebHeaderCollection headers = request.Headers;
                //headers.Add("sec-fetch-mode:navigate");
                request.Headers.Add("Cookie", COOKIE);
                request.Headers.Add("Accept-Encoding", "gzip");
                //request.KeepAlive = true;
                request.Accept = "*/*";
                //request.Timeout = 5000;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;  //获取反馈

                // request.Accept = "application/json, text/javascript, */*; q=0.01"; //返回中文问号参考
                if (response.Headers["Content-Encoding"] == "gzip")
                {

                    GZipStream gzip = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);//解压缩
                    StreamReader reader = new StreamReader(gzip, Encoding.GetEncoding(charset));
                    html = reader.ReadToEnd();
                    reader.Close();
                }
                else
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(charset)); //reader.ReadToEnd() 表示取得网页的源码流 需要引用 using  IO
                    html = reader.ReadToEnd();
                    reader.Close();
                }

                response.Close();
                return html;



            }
            catch (System.Exception ex)
            {
                return ex.ToString();

            }



        }
        #endregion

        #region  主程序
        public void run()
        {
            if (textBox3.Text == "")

            {
                MessageBox.Show("请输入分类网址");
                return;
            }
            try
            {
                textBox2.Text += "已启动正在采集......" + "\r\n";


                string cateid = Regex.Match(textBox3.Text, @"\d{6,9}").Groups[0].Value;

               
                for (int i = 0; i < 101; i = i + 1)
                {


                    string url = "https://api.parknshop.com/api/v2/pnshk/products/search?fields=FULL&query=%3AbestSeller%3Acategory%3A"+cateid+"&pageSize=18&currentPage="+i+"&sort=bestSeller&lang=zh_HK&curr=HKD";
                    string html = GetUrl(url, "utf-8");


                
                  
                    MatchCollection ids = Regex.Matches(html, @"""topPromotion""([\s\S]*?)""url"" : ""([\s\S]*?)""");
                   // MessageBox.Show(ids.Count.ToString());
                    if(ids.Count==0)
                    {
                        ids = Regex.Matches(html, @"</topPromotion>([\s\S]*?)<url>([\s\S]*?)</url>");
                    }

                    if (ids.Count == 0)
                        break;
                    for (int j = 0; j < ids.Count; j++)
                    {
                        string URL = "https://www.parknshop.com/zh-hk" + ids[j].Groups[2].Value;
                      



                      
                        
                        textBox2.Text = "正在采集......" + URL + "\r\n";
                        if (!URL.Contains("-:"))
                        {
                            string strhtml = GetUrl(URL, "utf-8");  //中文源码
                            string enhtml = GetUrl(URL.Replace("zh-hk", "en"), "utf-8");  //中文源码

                          
                            string name = Regex.Match(strhtml, @"<title>([\s\S]*?)\|").Groups[1].Value.Replace(" ","").Trim();
                            string guige = Regex.Match(strhtml, @"<div class=""product-unit"">([\s\S]*?)</div>").Groups[1].Value.Trim();
                            name = name + guige;


                            string price = Regex.Match(strhtml, @"currentPrice"">([\s\S]*?)</span>").Groups[1].Value;
                            string description = Regex.Match(strhtml, @"描述</span></div>([\s\S]*?)<div class=""description-banner"">").Groups[1].Value;



                            string name_en = Regex.Match(enhtml, @"<title>([\s\S]*?)\|").Groups[1].Value.Replace(" ", "").Trim();

                            name_en = name_en + guige;
                            string description_en= Regex.Match(enhtml, @"Description</span></div>([\s\S]*?)<div class=""description-banner"">").Groups[1].Value;





                            ListViewItem listViewItem = this.listView1.Items.Add((listView1.Items.Count + 1).ToString());
                            listViewItem.SubItems.Add(Regex.Replace(name, "<[^>]+>", ""));
                            listViewItem.SubItems.Add(Regex.Replace(price, "<[^>]+>", ""));
                            listViewItem.SubItems.Add(Regex.Replace(description, "<[^>]+>", ""));
                            listViewItem.SubItems.Add(Regex.Replace(name_en, "<[^>]+>", ""));
                            listViewItem.SubItems.Add(Regex.Replace(description_en, "<[^>]+>", ""));

                            //下载图片

                            string pichtml = Regex.Match(strhtml, @"<div class=""swiper-wrapper"">([\s\S]*?)<div class=""swiper-wrapper"">").Groups[1].Value;
                            MatchCollection picurls = Regex.Matches(pichtml, @"is-loading""><img src=""([\s\S]*?)""");


                            for (int a = 0; a < picurls.Count; a++)
                            {
                                string sPath = textBox1.Text + "\\" + removeValid(name) + "\\";
                                if (!Directory.Exists(sPath))
                                {
                                    Directory.CreateDirectory(sPath); //创建文件夹
                                }

                                method.downloadFile(picurls[a].Groups[1].Value, sPath,name_en+"_"+ a + ".jpg", "");

                            }
                           
                            
                          
                            while (this.zanting == false)
                            {
                                Application.DoEvents();//如果loader是false表明正在加载,,则Application.DoEvents()意思就是处理其他消息。阻止当前的队列继续执行。
                            }

                            Thread.Sleep(1000);



                        }

                    }



                }
                textBox2.Text += "抓取结束";

            }



            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        #endregion



        #region 去掉路径中非法字符
        public string removeValid(string illegal)
        {
            string invalid = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());

            foreach (char c in invalid)
            {
                illegal = illegal.Replace(c.ToString(), "");
            }
            return illegal;
        }

        #endregion


        #region 图片下载

        public void getimage(string dic,string url)
        {
            //string url = "https://www.parknshop.com/zh-hk/beer-12-can/p/BP_163144/showGalleryImages?&codeVarSel=163144";
            string cookie = "starbuysDisplay=true; scs=1; _dy_csc_ses=t; _dy_c_exps=; optimizelyEndUserId=oeu1569717848351r0.8872103555652517; optimizelySegments=%7B%226360070715%22%3A%22none%22%2C%226340792419%22%3A%22direct%22%2C%226342032533%22%3A%22gc%22%2C%226374150451%22%3A%22false%22%7D; optimizelyBuckets=%7B%7D; promoDisplay=true; crazyBannerDisplay=true; crazyBannerDisplayIsWm=true; _ga=GA1.2.261122262.1569717854; _gid=GA1.2.1393964393.1569717854; gaUserId=503b3bd5-d6e5-47e5-8cd3-b17fd3a032bb; _fbp=fb.1.1569717866681.1804581336; JSESSIONID=5F4E6BA36019D41301F7B9406CB81A24.phkpfa23; QueueITAccepted-SDFrts345E-V3_pnsprdhk=EventId%3Dpnsprdhk%26QueueId%3Db4d3acfe-1843-47ff-8294-daddd7d4af02%26RedirectType%3Dsafetynet%26IssueTime%3D1569719660%26Hash%3D84474cf9d22a4e438d13c00bfea5340be468b3b7a4e1f6253b8c5ebe2845ba87; ins-gaSSId=b8050312-a85e-b3d7-6dde-0f601dadc374_1569719674; _hjid=890a1193-1d78-4de8-b031-f8701c55ae68; _hjIncludedInSample=1; current-currency=HKD; insdrSV=21; lang=zt; ins-product-id=163144; _dy_ses_load_seq=70882%3A1569724185390; _dy_soct=242023.362820.1569724185; optimizelyPendingLogEvents=%5B%5D";
            string html = method.PostUrl(url,"",cookie,"utf-8");
            Match part = Regex.Match(html, @"圖片只供參考</p>([\s\S]*?)btn-next");

            MatchCollection images = Regex.Matches(part.Groups[1].Value, @"data-zoom-image=""([\s\S]*?)""");

            string sPath = textBox1.Text +"\\"+ dic+"\\";
             if (!Directory.Exists(sPath))
            {
                Directory.CreateDirectory(sPath); //创建文件夹
            }
            for (int i = 0; i < images.Count; i++)
            {
                string imageUrl = "https://www.parknshop.com"+images[i].Groups[1].Value;
                method.downloadFile(imageUrl,sPath,i+".jpg",cookie);

            }
        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text= AppDomain.CurrentDomain.BaseDirectory;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("请选择图片保存文件夹");
                return;
            }

            Thread thread = new Thread(new ThreadStart(run));
            Control.CheckForIllegalCrossThreadCalls = false;
            thread.Start();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            method.DataTableToExcel(method.listViewToDataTable(this.listView1), "Sheet1", true);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择所在文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }

                textBox1.Text = dialog.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            zanting = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            zanting = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            listView1.Items.Clear();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            
                Environment.Exit(0);
        
          
        }

    }
}
