﻿using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace main
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region GET请求
        /// <summary>
        /// GET请求
        /// </summary>
        /// <param name="Url">网址</param>
        /// <returns></returns>
        public static string GetUrl(string Url, string COOKIE)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);  //创建一个链接

                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.81 Safari/537.36";
                request.AllowAutoRedirect = true;
                request.Headers.Add("Cookie", COOKIE);
                request.KeepAlive = false;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;  //获取反馈

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gbk")); //reader.ReadToEnd() 表示取得网页的源码流 需要引用 using  IO

                string content = reader.ReadToEnd();
                reader.Close();
                response.Close();
                return content;

            }
            catch (System.Exception ex)
            {
                ex.ToString();

            }
            return "";
        }
        #endregion




        public static string COOKIE = "_uab_collina=155841531117990759884066; miid=1280586377935732790; thw=cn; cna=8QJMFUu4DhACATFZv2JYDtwd; t=549c883cd0d96a2361709464aafc2ef7; tg=0; hng=CN%7Czh-CN%7CCNY%7C156; _bl_uid=njj6hv4Xl4aqL0q7entvuUh8gtbv; _cc_=V32FPkk%2Fhw%3D%3D; enc=%2F%2B%2BMghI4XSh%2FGHJNSwgjpPsLgt0%2BE%2F%2BXQEBwokzOKQE3HkzETF8JiGAwWPNxItQwJOaD6V%2BBli0Wo0CeeA2X%2Fw%3D%3D; cookie2=1fe232f05a5136e28c32b1b505c5ea2e; _tb_token_=e3ea33e443e86; x=2992737907; _m_h5_tk=f8b0a562742b9fd05290ecc4ca22ac27_1558415881482; _m_h5_tk_enc=9ddf61ebe4a148329d6dd757b658cfc3; mt=ci=0_0; uc3=id2=&nk2=&lg2=; skt=ac295dae5743f3ef; sn=%E8%81%94%E9%80%9A%E7%BF%BC%E5%BE%B7%E9%80%9A%E4%BF%A1%E4%B8%93%E5%8D%96%E5%BA%97%3A%E6%A1%94%E5%AD%90; unb=3247690276; tracknick=; csg=baf127cb; v=0; x5sec=7b2274726164656d616e616765723b32223a226563336365336636373234393536313063333536643131343236336530646535434d32506a756346454e436e765a36346d634c443141456144444d794e4463324f5441794e7a59374d513d3d227d; uc1=cookie14=UoTZ7HEFr%2BLQZA%3D%3D&lng=zh_CN; l=bBreIJ14vWJ-OqdDBOfiVuIRGs7teIOb8sPzw4gGjICPOJfwSBbCWZt02rLeC3GVa6UWR3oWYJ1uBy8ivy4Eh; isg=BIuL2QUdU7GOmY_f_WBN1ptnGi-1iJfKQf9ZIv2JUEosHKl-hfMu82P-9lxXPPea";
        #region POST请求
        /// <summary>
        /// POST请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="postData">发送的数据包</param>
        /// <param name="COOKIE">cookie</param>
        /// <param name="charset">编码格式</param>
        /// <returns></returns>
        public static string PostUrl(string url,string POSTdata)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,//URL     必需项  
                Method = "POST",//URL     可选项 默认为Get  
                Timeout = 100000,//连接超时时间     可选项默认为100000  
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000  
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写  
                Cookie = COOKIE,
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",//用户的浏览器类型，版本，操作系统     可选项有默认值  
                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值  
                ContentType = "application/x-www-form-urlencoded",//返回类型    可选项有默认值  
                Referer = "https://trade.taobao.com/trade/itemlist/list_sold_items.htm?spm=687.8433302.201.1.296f226aJPgeJs&mytmenu=ymbb&utkn=g,ygvm3kgs5w24ftni2dc5pkgc6s26u1558407955652&scm=1028.1.1.201",//来源URL     可选项  
                //Allowautoredirect = False,//是否根据３０１跳转     可选项  
                //AutoRedirectCookie = False,//是否自动处理Cookie     可选项  
                //                           //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数  
                                           //Connectionlimit = 1024,//最大连接数     可选项 默认为1024  
                Postdata =POSTdata ,//Post数据     可选项GET时不需要写  
                                                                                                                                                                                                                                                                                                                                                   //ProxyIp = "192.168.1.105：2020",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数  
                                                                                                                                                                                                                                                                                                                                                   //ProxyPwd = "123456",//代理服务器密码     可选项  
                                                                                                                                                                                                                                                                                                                                                   //ProxyUserName = "administrator",//代理服务器账户名     可选项  
                ResultType = ResultType.String,//返回数据类型，是Byte还是String  
            };
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            return html;

        }

        #endregion






        public static string getToken()
        {
            string token = "";
            string[] keys = COOKIE.Split(new string[] { ";" }, StringSplitOptions.None);
            foreach (string key in keys)
            {

                if (key.Contains("token"))
                {
                    token = key;
                }
            }
            return token;

        }

        public static string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9A-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(
                source, x => string.Empty + Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)));
        }
        public void run()
        {
            if (COOKIE == "")
            {
                MessageBox.Show("请登录账号！");
                return;
            }
            listView1.Items.Clear();
            try
            {
                

              
                    string URL = "https://trade.taobao.com/trade/itemlist/list_sold_items.htm?action=itemlist/SoldQueryAction&event_submit_do_query=1&auctionStatus=PAID&tabCode=waitSend";
                    string postdata = "action=itemlist/SoldQueryAction&event_submit_do_query=1&auctionStatus=PAID&tabCode=waitSend";
                    string html = PostUrl(URL,postdata); ;
                    textBox1.Text = html;
                    MatchCollection IDs = Regex.Matches(html, @"tradeID=([\s\S]*?)&");
                    MatchCollection bianmas = Regex.Matches(html, @"u5546\\u5BB6\\u7F16\\u7801\\""\,\\""value\\"":\\""([\s\S]*?)\\");
                   
                    MatchCollection taocans = Regex.Matches(html, @"skuText\\"":\[{([\s\S]*?)}\]");
                MatchCollection times = Regex.Matches(html, @"createTime\\"":\\""([\s\S]*?)\\");
                MatchCollection users = Regex.Matches(html, @"\\""nick\\"":\\""([\s\S]*?)\\""");
                    

                 
                    for (int j = 0; j < IDs.Count; j++)
                    {

                    string strhtml = GetUrl("https://trade.tmall.com/detail/orderDetail.htm?bizOrderId="+ IDs[j].Groups[1].Value,COOKIE);

                    MatchCollection xs = Regex.Matches(strhtml, @"\[{""text"":""([\s\S]*?)""");
                    Match card = Regex.Match(strhtml,@"名""}\,{""content"":\[{""text"":""([\s\S]*?)""");

                    ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count + 1).ToString()); //使用Listview展示数据
                        lv1.SubItems.Add(IDs[j].Groups[1].Value);
                    lv1.SubItems.Add(bianmas[j].Groups[1].Value);
                    lv1.SubItems.Add(Unicode2String(taocans[j].Groups[1].Value));
                    lv1.SubItems.Add(times[j].Groups[1].Value);
                    lv1.SubItems.Add(Unicode2String(users[j].Groups[1].Value));
                    lv1.SubItems.Add(xs[0].Groups[1].Value);
                    lv1.SubItems.Add(xs[1].Groups[1].Value);
                    lv1.SubItems.Add(xs[7].Groups[1].Value);
                    lv1.SubItems.Add(card.Groups[1].Value);


                }
                Thread.Sleep(3000);
                }

            
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }



        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            webBrowser web = new webBrowser("https://login.taobao.com/member/login.jhtml");
            web.Show();
            //webBrowser web = new webBrowser("http://crm.58.com/");
            //web.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //textBox1.Text = webBrowser.cookie;
            //COOKIE = textBox1.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // timer1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            #region   读取注册码信息才能运行软件！

            RegistryKey rsg = Registry.CurrentUser.OpenSubKey("zhucema"); //true表可修改                
            if (rsg != null && rsg.GetValue("mac") != null)  //如果值不为空
            {
                Thread thread = new Thread(new ThreadStart(run));
                Control.CheckForIllegalCrossThreadCalls = false;
                thread.Start();

            }

            else
            {
                MessageBox.Show("请注册软件！");
                login lg = new login();
                lg.Show();
            }

            #endregion
        }
        public string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.GetEncoding("gb2312"));
                return reader.ReadToEnd();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(getToken());
          
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
