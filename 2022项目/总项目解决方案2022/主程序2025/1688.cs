﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using myDLL;
using System.IO;
using System.Web.UI.WebControls;
using System.Security.Policy;
using System.Web.UI;
using System.Runtime.InteropServices;
using System.Reflection.Emit;

namespace 主程序2025
{
    public partial class _1688 : Form
    {
        public _1688()
        {
            InitializeComponent();
        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //function.date = method.GetUrl("http://8.153.165.134/api/shuiguo.html", "utf-8").Trim();

            function.date = "2025-06-21";
            if (textBox1.Text == "")
            {
                MessageBox.Show("请导入关键词");
                return;
            }


           
            else
            {

                status = true;
                if (thread == null || !thread.IsAlive)
                {
                    thread = new Thread(run_mobile_sou);
                    thread.Start();
                    System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
                }
            }


        }



        Thread thread;
        bool status = true;

        private void button4_Click(object sender, EventArgs e)
        {
            method.DataTableToExcel(method.listViewToDataTable(this.listView1), "Sheet1", true);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            status = false;
        }

        private void _1688_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要关闭吗？", "关闭", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                // Environment.Exit(0);
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            else
            {
                e.Cancel = true;//点取消的代码 
            }
        }



        // string tk = "_m_h5_tk=97624facf78d7f3a426c8c64f0fb5c1f_1747920391562; _m_h5_tk_enc=8e0816a5bc0740da7ab9a836aa9a47dd;";
        string tk = "_m_h5_tk=1ac800c84da25489617f7703c415ff6b_1748152357506; _m_h5_tk_enc=62d87057306ba254c1aa934be3b953f4;";
        string x5 = "";
        string cookie = "";
        
        public string chuli(string shuzhi)
        {
            try
            {
                if (shuzhi != "-1")
                {
                    shuzhi = (Convert.ToDouble(shuzhi) * 100).ToString("F2") + "%";
                }
            }
            catch (Exception ex)
            {
             

            }
        return shuzhi;  
        }

       

       

        #region 手机端搜索直滑


        public void run_mobile_sou()
        {

          
            try
            {
              


                StreamReader sr = new StreamReader(textBox1.Text, method.EncodingType.GetTxtType(textBox1.Text));
                //一次性读取完 
                string texts = sr.ReadToEnd();
                string[] text = texts.Split(new string[] { "\r\n" }, StringSplitOptions.None);



                for (int i = 0; i < text.Length; i++)
                {
                    try
                    {
                        List<string> chongfulist = new List<string>();

                        for (int page = 0; page < 1000; page = page + 50)
                        {

                            cookie = tk + x5;

                            if (DateTime.Now > Convert.ToDateTime(function.date))
                            {
                                //function.TestForKillMyself();
                                return;
                            }




                            string keyword = text[i].Trim();
                            if (keyword.Trim() == "")
                                continue;
                            string time = function.GetTimeStamp();

                            string token = Regex.Match(cookie, @"_m_h5_tk=([\s\S]*?)_").Groups[1].Value;


                            string data = "{\"appId\":32517,\"params\":\"{\\\"appName\\\":\\\"findFactoryWap\\\",\\\"pageName\\\":\\\"findFactory\\\",\\\"searchScene\\\":\\\"factoryMTopSearch\\\",\\\"method\\\":\\\"getFactories\\\",\\\"keywords\\\":\\\"" + keyword + "\\\",\\\"startIndex\\\":" + page + ",\\\"asyncCount\\\":50,\\\"_wvUseWKWebView\\\":\\\"true\\\",\\\"tabCode\\\":\\\"findFactoryTab\\\",\\\"verticalProductFlag\\\":\\\"wapfactory\\\",\\\"_layoutMode_\\\":\\\"noSort\\\",\\\"source\\\":\\\"search_input\\\",\\\"searchBy\\\":\\\"input\\\",\\\"sessionId\\\":\\\"e3e6c641267b479bb8adef265c9df2c2\\\"}\"}";

                            string str = token + "&" + time + "&12574478&" + data;
                            string sign = function.Md5_utf8(str);

                            string url = "https://h5api.m.1688.com/h5/mtop.relationrecommend.wirelessrecommend.recommend/2.0/?jsv=2.5.1&appKey=12574478&t=" + time + "&sign=" + sign + "&api=mtop.relationrecommend.WirelessRecommend.recommend&v=2.0&jsonpIncPrefix=reqTppId_32517_getFactories&type=jsonp&dataType=jsonp&callback=mtopjsonpreqTppId_32517_getFactories1&data=" + System.Web.HttpUtility.UrlEncode(data);


                         

                            string html = function.GetUrlWithCookie(url, cookie, "utf-8");
                          




                            // textBox9.Text = html;

                           

                            if (html.Contains("挤爆啦"))
                            {
                                string capurl = Regex.Match(html, @"""url"":""([\s\S]*?)""").Groups[1].Value;

                                if (capurl.Contains("&action=captcha&"))//滑块不需要代理
                                {
                                    label1.Text = DateTime.Now.ToString("HH:mm:ss") + "：水果-" + page;
                                    
                                    yzm_yuanma.url = capurl;
                                    yzm_yuanma.run();
                                    x5 = yzm_yuanma.x5sec;

                                    cookie = tk + x5;
                                    html = function.GetUrlWithCookie(url, cookie, "utf-8");

                                }
                                
                                else if (capurl.Contains("&action=captchacapslidev2&")) //水果，换IP重新访问
                                {
                                    x5 = "";
                                    cookie = tk + x5;   
                                    html = function.GetUrlWithCookie_ip(url, cookie, textBox5.Text.Trim(), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim());

                                   
                                    label1.Text = DateTime.Now.ToString("HH:mm:ss") + "：水果-滑块";
                                    //调用滑块
                                    capurl = Regex.Match(html, @"""url"":""([\s\S]*?)""").Groups[1].Value;
                                    yzm_yuanma.url = capurl;
                                    yzm_yuanma.run();
                                    x5 = yzm_yuanma.x5sec;
                                    cookie = tk + x5;
                                    html = function.GetUrlWithCookie_ip(url, cookie, textBox5.Text.Trim(), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim());
                                   
                                }
                                else
                                {
                                    label1.Text =  html;
                                    x5 = "";
                                   

                                }
                                //else if (capurl.Contains("&action=captchacapslidev2&"))
                                // {
                                //     label1.Text = DateTime.Now.ToString() + "：被挤爆啦--水果";

                                //     shuiguo.url = capurl;
                                //     shuiguo.run();
                                //     x5 = shuiguo.x5sec;


                                // }

                                // else
                                // {
                                //     label1.Text = "禁止访问"+html;
                                //     page = page - 50;
                                //     yzm_yuanma.x5sec = "";
                                //     shuiguo.x5sec = "";
                                //     x5 = "";

                                // }


                                if (html.Contains("令牌过期"))
                                {
                                    label1.Text = "令牌过期";
                                    string cookiestr = function.getSetCookie(url, cookie);

                                    string _m_h5_tk = "_m_h5_tk=" + Regex.Match(cookiestr, @"_m_h5_tk=([\s\S]*?);").Groups[1].Value;
                                    string _m_h5_tk_enc = "_m_h5_tk_enc=" + Regex.Match(cookiestr, @"_m_h5_tk_enc=([\s\S]*?);").Groups[1].Value;
                                    tk = _m_h5_tk + ";" + _m_h5_tk_enc + ";";
                                    x5 = "";
                                    page = -50;
                                    continue;


                                }



                            }



                            MatchCollection facName = Regex.Matches(html, @"\\""facName\\"":\\""([\s\S]*?)\\""");
                            MatchCollection wangwang = Regex.Matches(html, @"\\""loginId\\"":\\""([\s\S]*?)\\""");
                            MatchCollection userId = Regex.Matches(html, @"\\""userId\\"":\\""([\s\S]*?)\\""");
                            MatchCollection complianceRate = Regex.Matches(html, @"\\""complianceRate\\"":\\""([\s\S]*?)\\"""); //履约率
                            MatchCollection repeatRate = Regex.Matches(html, @"\\""repeatRate\\"":\\""([\s\S]*?)\\"""); //回头率
                            MatchCollection wwResponseRate = Regex.Matches(html, @"\\""wwResponseRate\\"":\\""([\s\S]*?)\\""");  //响应率

                            if (facName.Count == 0)
                                break;

                            if (facName.Count > 0)
                            {
                                x5 = "";
                               
                            }
                            
                          
                            for (int j = 0; j < facName.Count; j++)
                            {


                                try
                                {



                                    if (chongfulist.Contains(userId[j].Groups[1].Value))
                                    {
                                        label1.Text = "重复，跳过：" + facName[j].Groups[1].Value;
                                        continue;
                                    }
                                    chongfulist.Add(userId[j].Groups[1].Value);




                                    if (complianceRate[j].Groups[1].Value != "")
                                    {
                                        if (Convert.ToDouble(complianceRate[j].Groups[1].Value) > Convert.ToDouble(textBox3.Text))
                                        {
                                            label1.Text = "履约率大于，跳过：" + facName[j].Groups[1].Value + "履约率：" + complianceRate[j].Groups[1].Value;
                                            continue;
                                        }

                                    }




                                    ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count).ToString()); //使用Listview展示数据 

                                    lv1.SubItems.Add("https://sale.1688.com/factory/card.html?__existtitle__=1&__removesafearea__=1&memberId=" + userId[j].Groups[1].Value);
                                    lv1.SubItems.Add(chuli(complianceRate[j].Groups[1].Value));
                                    lv1.SubItems.Add(chuli(repeatRate[j].Groups[1].Value));
                                    lv1.SubItems.Add(chuli(wwResponseRate[j].Groups[1].Value));
                                    lv1.SubItems.Add(keyword);
                                    lv1.SubItems.Add(facName[j].Groups[1].Value);
                                    lv1.SubItems.Add(wangwang[j].Groups[1].Value);




                                    if (status == false)
                                        return;
                                    if (listView1.Items.Count > 2)
                                    {
                                        this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                                    }
                                  

                                }
                                catch (Exception ex)
                                {
                                    //MessageBox.Show(ex.ToString());
                                    continue;
                                }
                            }


                            Thread.Sleep(1000);
                        }

                    }
                    catch (Exception ex)
                    {
                       
                        continue;
                    }



                }

                MessageBox.Show("完成");
            }
            catch (Exception ex)
            {

                //MessageBox.Show(ex.ToString());

            }

        }

        #endregion




        #region 手机端搜索直滑+水果


        public void run_mobile_sou2()
        {


            try
            {



                StreamReader sr = new StreamReader(textBox1.Text, method.EncodingType.GetTxtType(textBox1.Text));
                //一次性读取完 
                string texts = sr.ReadToEnd();
                string[] text = texts.Split(new string[] { "\r\n" }, StringSplitOptions.None);



                for (int i = 0; i < text.Length; i++)
                {

                    try
                    {
                        List<string> chongfulist = new List<string>();
                        string keyword = text[i].Trim();
                       
                        for (int page = 0; page < 1000; page = page + 50)
                        {

                            cookie = tk + x5;

                         
                            if (DateTime.Now > Convert.ToDateTime(function.date))
                            {
                                //function.TestForKillMyself();
                                return;
                            }





                            if (keyword.Trim() == "")
                                continue;
                            string time = function.GetTimeStamp();

                            string token = Regex.Match(cookie, @"_m_h5_tk=([\s\S]*?)_").Groups[1].Value;


                            string data = "{\"appId\":32517,\"params\":\"{\\\"appName\\\":\\\"findFactoryWap\\\",\\\"pageName\\\":\\\"findFactory\\\",\\\"searchScene\\\":\\\"factoryMTopSearch\\\",\\\"method\\\":\\\"getFactories\\\",\\\"keywords\\\":\\\"" + keyword + "\\\",\\\"startIndex\\\":" + page + ",\\\"asyncCount\\\":50,\\\"_wvUseWKWebView\\\":\\\"true\\\",\\\"tabCode\\\":\\\"findFactoryTab\\\",\\\"verticalProductFlag\\\":\\\"wapfactory\\\",\\\"_layoutMode_\\\":\\\"noSort\\\",\\\"source\\\":\\\"search_input\\\",\\\"searchBy\\\":\\\"input\\\",\\\"sessionId\\\":\\\"e3e6c641267b479bb8adef265c9df2c2\\\"}\"}";

                            string str = token + "&" + time + "&12574478&" + data;
                            string sign = function.Md5_utf8(str);

                            string url = "https://h5api.m.1688.com/h5/mtop.relationrecommend.wirelessrecommend.recommend/2.0/?jsv=2.5.1&appKey=12574478&t=" + time + "&sign=" + sign + "&api=mtop.relationrecommend.WirelessRecommend.recommend&v=2.0&jsonpIncPrefix=reqTppId_32517_getFactories&type=jsonp&dataType=jsonp&callback=mtopjsonpreqTppId_32517_getFactories1&data=" + System.Web.HttpUtility.UrlEncode(data);


                            label1.Text = "正在查询：" + page;



                            string html = function.GetUrlWithCookie(url, cookie, "utf-8");




                            // textBox9.Text = html;

                            //MessageBox.Show(html);

                            if (html.Contains("挤爆啦"))
                            {




                                string capurl = Regex.Match(html, @"""url"":""([\s\S]*?)""").Groups[1].Value;


                                if (capurl.Contains("&action=captcha&"))
                                {
                                    //function.log(DateTime.Now.ToString() + "：关键词：" + keyword + " 页码：" + page + "挤爆了滑块");
                                    label1.Text = DateTime.Now.ToString() + "：被挤爆啦--滑块" + page;

                                    yzm_yuanma.url = capurl;
                                    yzm_yuanma.run();
                                    x5 = yzm_yuanma.x5sec;

                                    cookie = tk + x5;
                                    html = function.GetUrlWithCookie(url, cookie, "utf-8");
                                }

                                else if (capurl.Contains("&action=captchacapslidev2&"))
                                {
                                   // function.log(DateTime.Now.ToString() + "：关键词：" + keyword + " 页码：" + page + "挤爆了水果");
                                    label1.Text = DateTime.Now.ToString() + "：被挤爆啦--水果" + page;

                                    shuiguo.url = capurl;

                                    var shuiguohelper = new shuiguo();
                                    shuiguohelper.GetX5Sec();
                                    x5 = shuiguo.x5sec;
                                    //MessageBox.Show(x5);

                                }

                                else
                                {
                                   // function.log(DateTime.Now.ToString() + "：关键词：" + keyword + " 页码：" + page + "挤爆了" + capurl);
                                    label1.Text = "禁止访问" + html;
                                    page = page - 50;
                                    yzm_yuanma.x5sec = "";
                                    shuiguo.x5sec = "";
                                    x5 = "";

                                }


                                if (html.Contains("令牌过期"))
                                {
                                    label1.Text = "令牌过期";
                                    string cookiestr = function.getSetCookie(url, cookie);

                                    string _m_h5_tk = "_m_h5_tk=" + Regex.Match(cookiestr, @"_m_h5_tk=([\s\S]*?);").Groups[1].Value;
                                    string _m_h5_tk_enc = "_m_h5_tk_enc=" + Regex.Match(cookiestr, @"_m_h5_tk_enc=([\s\S]*?);").Groups[1].Value;
                                    tk = _m_h5_tk + ";" + _m_h5_tk_enc + ";";
                                    x5 = "";
                                    page = 0;
                                    continue;


                                }



                            }



                            MatchCollection facName = Regex.Matches(html, @"\\""facName\\"":\\""([\s\S]*?)\\""");
                            MatchCollection wangwang = Regex.Matches(html, @"\\""loginId\\"":\\""([\s\S]*?)\\""");
                            MatchCollection userId = Regex.Matches(html, @"\\""userId\\"":\\""([\s\S]*?)\\""");
                            MatchCollection complianceRate = Regex.Matches(html, @"\\""complianceRate\\"":\\""([\s\S]*?)\\"""); //履约率
                            MatchCollection repeatRate = Regex.Matches(html, @"\\""repeatRate\\"":\\""([\s\S]*?)\\"""); //回头率
                            MatchCollection wwResponseRate = Regex.Matches(html, @"\\""wwResponseRate\\"":\\""([\s\S]*?)\\""");  //响应率

                            if (facName.Count == 0)
                                break;

                            //if (facName.Count > 0)
                            //{
                            //    x5 = "";
                            //}
                            for (int j = 0; j < facName.Count; j++)
                            {


                                try
                                {



                                    if (chongfulist.Contains(userId[j].Groups[1].Value))
                                    {
                                        label1.Text = "重复，跳过：" + facName[j].Groups[1].Value;
                                        continue;
                                    }
                                    chongfulist.Add(userId[j].Groups[1].Value);




                                    if (complianceRate[j].Groups[1].Value != "")
                                    {
                                        if (Convert.ToDouble(complianceRate[j].Groups[1].Value) > Convert.ToDouble(textBox3.Text))
                                        {
                                            label1.Text = "履约率大于，跳过：" + facName[j].Groups[1].Value + "履约率：" + complianceRate[j].Groups[1].Value;
                                            continue;
                                        }

                                    }




                                    ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count).ToString()); //使用Listview展示数据 

                                    lv1.SubItems.Add("https://sale.1688.com/factory/card.html?__existtitle__=1&__removesafearea__=1&memberId=" + userId[j].Groups[1].Value);
                                    lv1.SubItems.Add(chuli(complianceRate[j].Groups[1].Value));
                                    lv1.SubItems.Add(chuli(repeatRate[j].Groups[1].Value));
                                    lv1.SubItems.Add(chuli(wwResponseRate[j].Groups[1].Value));
                                    lv1.SubItems.Add(keyword);
                                    lv1.SubItems.Add(facName[j].Groups[1].Value);
                                    lv1.SubItems.Add(wangwang[j].Groups[1].Value);




                                    if (status == false)
                                        return;
                                    if (listView1.Items.Count > 2)
                                    {
                                        this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                                    }


                                }
                                catch (Exception ex)
                                {
                                    //MessageBox.Show(ex.ToString());
                                   // function.log(DateTime.Now.ToString() + ex.ToString());
                                    continue;
                                }
                            }


                            Thread.Sleep(2000);
                        }
                    }
                    catch (Exception)
                    {

                       continue;
                    }



                }

                MessageBox.Show("完成");
            }
            catch (Exception ex)
            {

                 //MessageBox.Show(ex.ToString());

            }

        }

        #endregion












        /// <summary>
        /// 主页最新商品
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public string getfahuo2(string memberid)
        {

            try
            {
                cookie = tk + x5;
                string time = function.GetTimeStamp();

                string token = Regex.Match(cookie, @"_m_h5_tk=([\s\S]*?)_").Groups[1].Value;

                string data = "{\"dataType\":\"moduleData\",\"argString\":\"{\\\"memberId\\\":\\\""+memberid+"\\\",\\\"appName\\\":\\\"pcmodules\\\",\\\"resourceName\\\":\\\"wpOfferColumn\\\",\\\"type\\\":\\\"view\\\",\\\"version\\\":\\\"1.0.0\\\",\\\"appdata\\\":{\\\"sortType\\\":\\\"timedown\\\"}}\"}";
                
                string str = token + "&" + time + "&12574478&" + data;
                string sign = function.Md5_utf8(str);

                string url = "https://h5api.m.1688.com/h5/mtop.1688.shop.data.get/1.0/?jsv=2.7.0&appKey=12574478&t="+time+"&sign="+sign+"&api=mtop.1688.shop.data.get&v=1.0&type=json&valueType=string&dataType=json&timeout=10000";
                string postdata = System.Web.HttpUtility.UrlEncode(data);
                string html = "";
                if (checkBox2.Checked)
                {
                    html = function.PostUrl_daili(url, "data=" + postdata, cookie, textBox5.Text.Trim(), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim());
                }
                else
                {
                    html = function.PostUrlDefault(url, "data=" + postdata, cookie);
                }

                // MessageBox.Show(html);

                if (html.Contains("令牌过期"))
                {
                    label1.Text = "令牌过期";
                    string cookiestr = function.getSetCookie(url, "");

                    string _m_h5_tk = "_m_h5_tk=" + Regex.Match(cookiestr, @"_m_h5_tk=([\s\S]*?);").Groups[1].Value;
                    string _m_h5_tk_enc = "_m_h5_tk_enc=" + Regex.Match(cookiestr, @"_m_h5_tk_enc=([\s\S]*?);").Groups[1].Value;
                    tk = _m_h5_tk + ";" + _m_h5_tk_enc + ";";



                }

              

                if (html.Contains("挤爆啦"))
                {
                    string capurl = Regex.Match(html, @"""url"":""([\s\S]*?)""").Groups[1].Value;

                    if (capurl.Contains("&action=captcha"))
                    {
                        label5.Text = DateTime.Now.ToString() + "：被挤爆啦";

                        yzm_yuanma.url = capurl;
                        yzm_yuanma.run();
                        x5 = yzm_yuanma.x5sec;

                        //MessageBox.Show(x5);
                    }
                    else
                    {
                        label1.Text = "禁止访问" + html;

                        yzm_yuanma.x5sec = "";
                        x5 = "";

                    }



                }



                MatchCollection agentInfo = Regex.Matches(html, @"""agentInfo""([\s\S]*?)""hasTUV""");
                MatchCollection id = Regex.Matches(html, @"object_id\@([\s\S]*?)\^");
                string fahuotime = "无";


                for (int i = 0; i < agentInfo.Count; i++)
                {

                    fahuotime = Regex.Match(agentInfo[i].Groups[1].Value, @"""fahuoTime"":""([\s\S]*?)""").Groups[1].Value;

                    if (fahuotime == "24")
                    {
                        //return "https://detail.1688.com/offer/" + id[i].Groups[1].Value + ".html";
                        return "24";
                    }

                }

                return fahuotime;
            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.ToString());
                return "ex";
            }

        }



        /// <summary>
        /// 主页供应商品
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public string getfahuo3(string memberid)
        {

            try
            {

                cookie = tk + x5;
                string time = function.GetTimeStamp();

                string token = Regex.Match(cookie, @"_m_h5_tk=([\s\S]*?)_").Groups[1].Value;

                string data = "{\"dataType\":\"moduleData\",\"argString\":\"{\\\"appName\\\":\\\"pcmodules\\\",\\\"resourceName\\\":\\\"wpOfferColumn\\\",\\\"type\\\":\\\"view\\\",\\\"version\\\":\\\"1.0.0\\\",\\\"memberId\\\":\\\""+memberid+"\\\",\\\"appdata\\\":{\\\"source\\\":\\\"index\\\",\\\"count\\\":\\\"100\\\",\\\"sortType\\\":\\\"tradenumdown\\\",\\\"catId\\\":\\\"-1\\\"}}\"}";
                string str = token + "&" + time + "&12574478&" + data;
                string sign = function.Md5_utf8(str);

                string url = "https://h5api.m.1688.com/h5/mtop.1688.shop.data.get/1.0/?jsv=2.7.0&appKey=12574478&t="+time+"&sign="+sign+"&api=mtop.1688.shop.data.get&v=1.0&type=json&valueType=string&dataType=json&timeout=10000";
                string postdata = System.Web.HttpUtility.UrlEncode(data);
                string html = "";
                if (checkBox2.Checked)
                {
                    html = function.PostUrl_daili(url, "data=" + postdata, cookie, textBox5.Text.Trim(), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim());
                }
                else
                {
                    html = function.PostUrlDefault(url, "data=" + postdata, cookie);
                }


               

                if (html.Contains("令牌过期"))
                {
                    label1.Text = "令牌过期";
                    string cookiestr = function.getSetCookie(url, "");

                    string _m_h5_tk = "_m_h5_tk=" + Regex.Match(cookiestr, @"_m_h5_tk=([\s\S]*?);").Groups[1].Value;
                    string _m_h5_tk_enc = "_m_h5_tk_enc=" + Regex.Match(cookiestr, @"_m_h5_tk_enc=([\s\S]*?);").Groups[1].Value;
                    tk = _m_h5_tk + ";" + _m_h5_tk_enc + ";";



                }



                if (html.Contains("挤爆啦"))
                {
                    string capurl = Regex.Match(html, @"""url"":""([\s\S]*?)""").Groups[1].Value;

                    if (capurl.Contains("&action=captcha"))
                    {
                        label5.Text = DateTime.Now.ToString() + "：被挤爆啦";

                        yzm_yuanma.url = capurl;
                        yzm_yuanma.run();
                        x5 = yzm_yuanma.x5sec;

                        //MessageBox.Show(x5);
                    }
                    else
                    {
                        label1.Text = "禁止访问" + html;

                        yzm_yuanma.x5sec = "";
                        x5 = "";

                    }



                }



                MatchCollection agentInfo = Regex.Matches(html, @"""agentInfo""([\s\S]*?)""hasTUV""");
                MatchCollection id = Regex.Matches(html, @"object_id\@([\s\S]*?)\^");
                string fahuotime = "无";

               
                for (int i = 0; i < agentInfo.Count; i++)
                {

                    fahuotime = Regex.Match(agentInfo[i].Groups[1].Value, @"""fahuoTime"":""([\s\S]*?)""").Groups[1].Value;

                    if (fahuotime == "24")
                    {
                        //return "https://detail.1688.com/offer/" + id[i].Groups[1].Value + ".html";
                        return "24";
                    }

                }

                return fahuotime;
            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.ToString());
                return "ex";
            }

        }

        /// <summary>
        /// 30348H揽收率履约率
        /// </summary>
        /// <param name="memberid"></param>
        /// <returns></returns>
        public string getlvyue30(string memberid)
        {

            try
            {

                
              
                cookie = tk + x5;
                string time = function.GetTimeStamp();

                string token = Regex.Match(cookie, @"_m_h5_tk=([\s\S]*?)_").Groups[1].Value;

                string data = "{\"componentKey\":\"wp_pc_shop_behavior\",\"params\":\"{\\\"memberId\\\":\\\""+memberid+"\\\"}\"}";
                string str = token + "&" + time + "&12574478&" + data;
                string sign = function.Md5_utf8(str);

                string url = "https://h5api.m.1688.com/h5/mtop.alibaba.alisite.cbu.server.pc.moduleasyncservice/1.0/?jsv=2.7.0&appKey=12574478&t=" + time + "&sign=" + sign + "&api=mtop.1688.shop.data.get&v=1.0&type=json&valueType=string&dataType=json&timeout=10000&callback=mtopjsonp2&data=" + System.Web.HttpUtility.UrlEncode(data); ;
                string postdata = System.Web.HttpUtility.UrlEncode(data);
                string html = "";
                if (checkBox2.Checked)
                {
                    html = function.GetUrlWithCookie_ip(url, cookie, textBox5.Text.Trim(), textBox6.Text.Trim(), textBox7.Text.Trim(), textBox8.Text.Trim());
                }
                else
                {
                    html = function.GetUrlWithCookie(url,  cookie,"utf-8");
                }




              

                if (html.Contains("挤爆啦"))
                {
                    string capurl = Regex.Match(html, @"""url"":""([\s\S]*?)""").Groups[1].Value;

                    if (capurl.Contains("&action=captcha"))
                    {
                        label5.Text = DateTime.Now.ToString() + "：被挤爆啦";

                        yzm_yuanma.url = capurl;
                        yzm_yuanma.run();
                        x5 = yzm_yuanma.x5sec;
                    }
                    else
                    {
                        label1.Text = "禁止访问" + html;

                        yzm_yuanma.x5sec = "";
                        x5 = "";

                    }



                }
                if (html.Contains("令牌过期"))
                {
                    label1.Text = "令牌过期";
                    string cookiestr = function.getSetCookie(url, "");

                    string _m_h5_tk = "_m_h5_tk=" + Regex.Match(cookiestr, @"_m_h5_tk=([\s\S]*?);").Groups[1].Value;
                    string _m_h5_tk_enc = "_m_h5_tk_enc=" + Regex.Match(cookiestr, @"_m_h5_tk_enc=([\s\S]*?);").Groups[1].Value;
                    tk = _m_h5_tk + ";" + _m_h5_tk_enc + ";";
                   

                }
               

                string dingdan= "";
                string lanshou = "";
                string lvyue = "";


                dingdan = Regex.Match(html, @"""shop_nps_pay_ord_cnt_1m_001"":([\s\S]*?),").Groups[1].Value.Replace("\"","").Replace("}", "").Replace("null", "0");
                lanshou = Regex.Match(html, @"""shop_nps_lgt_48h_got_rate_30d"":([\s\S]*?),").Groups[1].Value.Replace("\"", "").Replace("null", "0.00");
                lvyue = Regex.Match(html, @"""shop_lgt_fulfill_got_rate_30d"":([\s\S]*?),").Groups[1].Value.Replace("\"", "").Replace("null", "0.00");



                string aaa = dingdan + "," + lanshou + "," + lvyue;
               
                return aaa;
            }
            catch (Exception ex)
            {

                // MessageBox.Show(ex.ToString());
                return "ex";
            }

        }



        private void _1688_Load(object sender, EventArgs e)
        {
            timer1.Start();
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\log.txt"))
            {
                File.Create(AppDomain.CurrentDomain.BaseDirectory + "\\log.txt");
            }

          
           
        }



       
        private void button6_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            getlvyue30("");
            listView1.Items.Clear();

            //yzm_yuanma.url = "https://ditu.amap.com/detail/get/detail?id=B00155L3DHo";
            //yzm_yuanma.run();
            //MessageBox.Show(yzm_yuanma.x5sec);

            //var shuiguohelper = new shuiguo();
            //shuiguohelper.GetX5Sec();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems.Count == 0)
                return;

            try
            {
                System.Diagnostics.Process.Start(listView1.SelectedItems[0].SubItems[8].Text);
            } 
            catch (Exception)
            {

               
            }
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
            function.date = method.GetUrl("http://8.153.165.134/api/shuiguo.html", "utf-8").Trim();


            if (textBox4.Text == "")
            {
                MessageBox.Show("请导入表格");
                return;
            }



            else
            {

                status = true;
                if (thread == null || !thread.IsAlive)
                {
                    thread = new Thread(shaixuan);
                    thread.Start();
                    System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Title = "打开excel文件";
            openFileDialog1.Filter = "excel03文件(*.xls)|*.xls|excel07文件(*.xlsx)|*.xlsx";
            openFileDialog1.Filter = "excel07文件(*.xlsx)|*.xlsx";
            openFileDialog1.InitialDirectory = @"C:\Users\Administrator\Desktop";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //打开文件对话框选择的文件
                textBox4.Text = openFileDialog1.FileName;
              

            }
        }




        public void shaixuan()
        {
            try
            {
                DataTable dt =function.ExcelToDataTable(textBox4.Text,true);

              
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (DateTime.Now > Convert.ToDateTime(function.date))
                    {
                      
                        return;
                    }
                    try
                    {

                        string userid = Regex.Match(dt.Rows[i][1].ToString(), @"memberId=.*").Groups[0].Value.Replace("memberId=", "");

                       
                       string item24 = getfahuo3(userid);

                        if(item24=="无")
                        {
                            item24 = getfahuo3(userid);
                        }

                        ListViewItem lv1 = listView2.Items.Add((listView2.Items.Count).ToString()); //使用Listview展示数据 

                        label5.Text = "正在筛选：" + dt.Rows[i][6].ToString();
                        lv1.SubItems.Add(dt.Rows[i][1].ToString());
                        lv1.SubItems.Add(dt.Rows[i][2].ToString());
                        lv1.SubItems.Add(dt.Rows[i][3].ToString());
                        lv1.SubItems.Add(dt.Rows[i][4].ToString());
                        lv1.SubItems.Add(dt.Rows[i][5].ToString());
                        lv1.SubItems.Add(dt.Rows[i][6].ToString());
                        lv1.SubItems.Add(dt.Rows[i][7].ToString());

                        lv1.SubItems.Add(item24);

                        //string bbb = getlvyue30(userid);
                        //string[] aaa = bbb.Split(new string[] { "," }, StringSplitOptions.None);

                        //string a= aaa[0];   
                        //string b= aaa[1];
                        //string c= aaa[2];
                        //if (b!="" && c!=""&&b!="null" && c != "null")
                        //{
                        //    b = (Convert.ToDouble(b) * 100).ToString() + "%";
                        //    c= (Convert.ToDouble(c) * 100).ToString() + "%";

                        //}
                        //lv1.SubItems.Add(a);
                        //lv1.SubItems.Add(b);
                        //lv1.SubItems.Add(c);

                        if (status == false)
                            return;
                        if (listView2.Items.Count > 2)
                        {
                            this.listView2.Items[this.listView2.Items.Count - 1].EnsureVisible();
                        }
                    }
                    catch (Exception ex)
                    {
                       
                        continue;
                    }


                   

                }
            }
            catch (Exception ex)
            {

               MessageBox.Show(ex.ToString());
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            status = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            method.DataTableToExcel(method.listViewToDataTable(this.listView2), "Sheet1", true);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            function.date = method.GetUrl("http://8.153.165.134/api/shuiguo.html", "utf-8").Trim();
        }
    }
}
