﻿using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace 美团
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        public string cookie;
        bool zanting = true;
        public static string username = "";
        ArrayList tels = new ArrayList();
        private void Form1_Load(object sender, EventArgs e)
        {
           
          
            label3.Text = username;
           
        }

        bool status = true;
        

        #region GET请求
        public static string meituan_GetUrl(string Url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);  //创建一个链接
                //request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.11 (KHTML, like Gecko) Chrome/23.0.1271.97 Safari/537.11";
                request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 13_6_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 MicroMessenger/7.0.13(0x17000d2a) NetType/4G Language/zh_CN";
                WebHeaderCollection headers = request.Headers;
                headers.Add("uuid: E82ADB4FE4B6D0984D5B1BEA4EE9DE13A16B4B25F8A306260A976B724DF44576");
                headers.Add("open_id: oJVP50IRqKIIshugSqrvYE3OHJKQ");
                headers.Add("token: Vteo9CkJqIGMe30FC3iuvnvTr2YAAAAAygoAAMPHPyLNO16W1eYLn1hWsLhD40r-KnDdB70rrl9LN9OHUfVBGbTDt4PCDHH72xKkDA");
                
                request.Referer = "https://servicewechat.com/wxde8ac0a21135c07d/328/page-frame.html";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;  //获取反馈

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")); //reader.ReadToEnd() 表示取得网页的源码流 需要引用 using  IO

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

        #region GET请求2
        public static string GetUrl(string Url)
        {
            try
            {


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);  //创建一个链接
                string COOKIE = "__mta=216473129.1565052050597.1583818626848.1583818731643.25; _lxsdk_cuid=16bd4b88a38c8-0d297d6bd625d2-f353163-1fa400-16bd4b88a38c8; iuuid=F3B7CF367A381B6BDA09F29EB6CBD0809EA666DB1290BBF84995C104FBF57A65; _lxsdk=F3B7CF367A381B6BDA09F29EB6CBD0809EA666DB1290BBF84995C104FBF57A65; _hc.v=df1f9416-050a-6ffd-b8c9-62180be8d8d4.1564129261; webp=1; _ga=GA1.2.1573254713.1564129433; __mta=216473129.1565052050597.1572417349038.1572417355133.8; a2h=1; Hm_lvt_f66b37722f586a240d4621318a5a6ebe=1574064150; __utmz=74597006.1574219146.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); uuid=a4e700a6c1c84328a796.1583806511.1.0.0; rvct=184; lat=36.684199; lng=117.061944; JSESSIONID=1qkdmt10h0iksa0rzwxj5n6uy; IJSESSIONID=1qkdmt10h0iksa0rzwxj5n6uy; __utmc=74597006; ci=96; cityname=%E6%B5%8E%E5%8D%97; idau=1; __utma=74597006.1573254713.1564129433.1583807166.1583818213.5; i_extend=C133821980007841839627568335826373356080_b1_e2750553022450266608_v4837406441626188694_a%e4%b8%bd%e4%ba%ba_f181539753E126791890114317345528428946634289901095_v4837409378594186279_e3091222610447599364_a%e4%b8%bd%e4%ba%baGimthomepagecategory12H__a100005__b5; webloc_geo=33.960173%2C118.275532%2Cwgs84%2C-1; latlng=33.960173,118.275532,1583818614791; __utmb=74597006.4.10.1583818213; _lxsdk_s=170c2ebe787-369-736-b45%7C%7C14";
                request.UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 12_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Mobile/15E148 MicroMessenger/7.0.11(0x17000b21) NetType/4G Language/zh_CN";
                request.Headers.Add("Cookie", COOKIE);
                request.Headers.Add("token", "qayAmk04G6EvdItDp6GHVZUDg8gAAAAAagkAAJM_kYnReUzwieeFee3FiVFOGnq41qxDzT9WdNjTti_60YjsS4SwrMIdppEobrF2dw");
                request.Headers.Add("open_id", "oJVP50IRqKIIshugSqrvYE3OHJKQ");
                request.Headers.Add("uuid", "E82ADB4FE4B6D0984D5B1BEA4EE9DE13A16B4B25F8A306260A976B724DF44576");
                request.Referer = "https://servicewechat.com/wxde8ac0a21135c07d/350/page-frame.html";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;  //获取反馈

                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")); //reader.ReadToEnd() 表示取得网页的源码流 需要引用 using  IO

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

        #region  获取城市ID

        public string GetcityId(string city)
        {

            try
            {
                string url = "https://apimobile.meituan.com/group/v1/area/search/"+ System.Web.HttpUtility.UrlEncode(city);
                string html = GetUrl(url);
                Match cityId = Regex.Match(html, @"""cityId"":([\s\S]*?),");

                return cityId.Groups[1].Value;
            }

            catch (System.Exception ex)
            {
                return ex.ToString();
            }

        }

        #endregion

        #region  获取城市拼音缩写

        public string Getsuoxie(string city)
        {

            try
            {
                string url = "https://apimobile.meituan.com/group/v1/area/search/"+System.Web.HttpUtility.UrlEncode(city);
                string html = GetUrl(url);
                Match suoxie = Regex.Match(html, @"""cityAcronym"":""([\s\S]*?)""");

                return suoxie.Groups[1].Value;
            }

            catch (System.Exception ex)
            {
                return ex.ToString();
            }


        }

        #endregion


        #region 获取区域
        public ArrayList getareas(string city)
        {
            string Url = "https://"+city+".meituan.com/meishi/";

            string html = GetUrl(Url);  //定义的GetRul方法 返回 reader.ReadToEnd()

            MatchCollection areas = Regex.Matches(html, @"""subAreas"":\[\{""id"":([\s\S]*?),");
            ArrayList lists = new ArrayList();

            foreach (Match item in areas)
            {
                lists.Add(item.Groups[1].Value);
            }

            return lists;
        }

        #endregion


        #region  获取所有城市

        public ArrayList GetAllcityName()
        {
            ArrayList lists = new ArrayList();
            try
            {
                string url = "https://www.meituan.com/ptapi/getprovincecityinfo/";
                string html = GetUrl(url);
                MatchCollection cityId = Regex.Matches(html, @"""name"":""([\s\S]*?)""");
                foreach (Match match in cityId)
                {
                    lists.Add(match.Groups[1].Value);
                }
                return lists;
            }

            catch (System.Exception ex)
            {
                return lists;
            }

        }

        #endregion


        #region  主程序
        public void run()
        {

            try
            {
                 string[] citys = textBox1.Text.Trim().Split(new string[] { "," }, StringSplitOptions.None);
               
                if (textBox1.Text.Trim() == "")
                {
                    MessageBox.Show("请输入城市！");
                    return;
                }

                if (textBox2.Text == "")
                {
                    textBox2.Text =  "美食, 火锅, 烧烤, 麻辣烫, 面包, 蛋糕, 奶茶, 快餐, 面条, 西餐, 中餐,小吃, 自助餐, 烤鱼, 海鲜, 甜点, 炒菜" ;
                }

                string[] keywords = textBox2.Text.Trim().Split(new string[] { "," }, StringSplitOptions.None);

               
                foreach (string city in citys)
                {

                    ArrayList areas = getareas(Getsuoxie(city));

                    string cityId = GetcityId(city);
                    foreach (string areaId in areas)
                    {

                        foreach (string keyword in keywords)

                        {

                            for (int i = 0; i < 1000; i=i+15)

                            {


                                // string Url = "https://apimobile.meituan.com/group/v4/poi/search/"+cityId+"?riskLevel=71&optimusCode=10&cateId=-1&sort=default&userid=-1&offset="+i+"&limit=32&mypos=33.94108581542969%2C118.24807739257812&uuid=E82ADB4FE4B6D0984D5B1BEA4EE9DE13A16B4B25F8A306260A976B724DF44576&version_name=10.4.200&supportDisplayTemplates=itemA%2CitemB%2CitemJ%2CitemP%2CitemS%2CitemM%2CitemY%2CitemL&supportTemplates=default%2Chotel%2Cblock%2Cnofilter%2Ccinema&searchSource=miniprogram&ste=_b100000&q="+keyword.Trim()+"&requestType=filter&cityId="+cityId+"&areaId="+ areaId;

                                string Url = "https://apimobile.meituan.com/group/v4/poi/search/"+cityId+"?riskLevel=71&optimusCode=10&cateId=-1&sort=defaults&userid=-1&offset="+i+ "&limit=15&mypos=33.940975189208984%2C118.24801635742188&uuid=E82ADB4FE4B6D0984D5B1BEA4EE9DE13A16B4B25F8A306260A976B724DF44576&version_name=10.4.200&supportDisplayTemplates=itemA%2CitemB%2CitemJ%2CitemP%2CitemS%2CitemM%2CitemY%2CitemL&supportTemplates=default%2Chotel%2Cblock%2Cnofilter%2Ccinema&searchSource=miniprogram&ste=_b100000&q="+keyword.Trim()+"&cityId=" + cityId + "&areaId=" + areaId;

                                string html = GetUrl(Url); ;  //定义的GetRul方法 返回 reader.ReadToEnd()
                                
                                    MatchCollection all = Regex.Matches(html, @"\{""poiid"":([\s\S]*?),");
                               
                                    ArrayList lists = new ArrayList();
                                    foreach (Match NextMatch in all)
                                    {

                                    //https://apimobile.meituan.com/group/v1/poi/194905459?fields=areaName,frontImg,name,avgScore,avgPrice,addr,openInfo,wifi,phone,featureMenus,isWaimai,payInfo,chooseSitting,cates,lat,lng
                                    //lists.Add("https://mapi.meituan.com/general/platform/mtshop/poiinfo.json?poiid=" + NextMatch.Groups[1].Value);
                                    //lists.Add("http://i.meituan.com/poi/" + NextMatch.Groups[1].Value);
                                    //lists.Add("https://i.meituan.com/wrapapi/poiinfo?poiId=" + NextMatch.Groups[1].Value);
                                    lists.Add("https://i.meituan.com/wrapapi/allpoiinfo?riskLevel=71&optimusCode=10&poiId=" + NextMatch.Groups[1].Value + "&isDaoZong=true");  
                                    }


                               
                                if (lists.Count == 0)  //当前页没有网址数据跳过之后的网址采集，进行下个foreach采集

                                        break;

                                    string tm1 = DateTime.Now.ToString();  //获取系统时间

                                    toolStripStatusLabel1.Text = tm1 + "-->正在采集" + city +areaId+ keyword + "第" + i + "页";

                                    foreach (string list in lists)

                                    {

                                        string strhtml1 = meituan_GetUrl(list);  //定义的GetRul方法 返回 reader.ReadToEnd()
                                    MessageBox.Show(strhtml1);
                                    Match name = Regex.Match(strhtml1, @"name"":""([\s\S]*?)""");
                                        Match tel = Regex.Match(strhtml1, @"phone"":""([\s\S]*?)""");
                                        Match addr = Regex.Match(strhtml1, @"address"":""([\s\S]*?)""");
                                    Match score = Regex.Match(strhtml1, @"score"":([\s\S]*?),");
                                    if (!tels.Contains(tel.Groups[1].Value))
                                    {
                                        tels.Add(tel.Groups[1].Value);
                                      
                                            ListViewItem listViewItem = this.listView1.Items.Add((listView1.Items.Count + 1).ToString());
                                            listViewItem.SubItems.Add(name.Groups[1].Value);
                                            listViewItem.SubItems.Add(tel.Groups[1].Value);
                                            listViewItem.SubItems.Add(addr.Groups[1].Value);
                                            listViewItem.SubItems.Add(city);
                                        listViewItem.SubItems.Add(score.Groups[1].Value);


                                        while (this.zanting == false)
                                        {
                                            Application.DoEvents();//如果loader是false表明正在加载,,则Application.DoEvents()意思就是处理其他消息。阻止当前的队列继续执行。
                                        }
                                        if (status == false)
                                        {
                                            return;
                                        }

                                        
                                    }

                                    Thread.Sleep(1000);


                                }

                                Thread.Sleep(2000);
                            }




                        }

                    }


               }
            }





            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        

        }

        #endregion

        #region  自动全国
        public void run1()
        {

            try
            {
                ArrayList citys = GetAllcityName();

               

                if (textBox2.Text == "")
                {
                    textBox2.Text = "美食, 火锅, 烧烤, 麻辣烫, 面包, 蛋糕, 奶茶, 快餐, 面条, 西餐, 中餐,小吃, 自助餐, 烤鱼, 海鲜, 甜点, 炒菜";
                }

                string[] keywords = textBox2.Text.Trim().Split(new string[] { "," }, StringSplitOptions.None);


                foreach (string city in citys)
                {

                    ArrayList areas = getareas(Getsuoxie(city));

                    string cityId = GetcityId(city);
                    foreach (string areaId in areas)
                    {

                        foreach (string keyword in keywords)

                        {

                            for (int i = 0; i < 1000; i = i + 15)

                            {


                                string Url = "https://apimobile.meituan.com/group/v4/poi/search/" + cityId + "?riskLevel=71&optimusCode=10&cateId=-1&sort=default&userid=-1&offset=" + i + "&limit=15&mypos=33.94108581542969%2C118.24807739257812&uuid=E82ADB4FE4B6D0984D5B1BEA4EE9DE13A16B4B25F8A306260A976B724DF44576&version_name=10.4.200&supportDisplayTemplates=itemA%2CitemB%2CitemJ%2CitemP%2CitemS%2CitemM%2CitemY%2CitemL&supportTemplates=default%2Chotel%2Cblock%2Cnofilter%2Ccinema&searchSource=miniprogram&ste=_b100000&q=" + keyword.Trim() + "&requestType=filter&cityId=" + cityId + "&areaId=" + areaId;


                                string html = GetUrl(Url); ;  //定义的GetRul方法 返回 reader.ReadToEnd()

                                MatchCollection all = Regex.Matches(html, @"\{""poiid"":([\s\S]*?),");

                                ArrayList lists = new ArrayList();
                                foreach (Match NextMatch in all)
                                {

                                    //https://apimobile.meituan.com/group/v1/poi/194905459?fields=areaName,frontImg,name,avgScore,avgPrice,addr,openInfo,wifi,phone,featureMenus,isWaimai,payInfo,chooseSitting,cates,lat,lng
                                    //lists.Add("https://mapi.meituan.com/general/platform/mtshop/poiinfo.json?poiid=" + NextMatch.Groups[1].Value);
                                    //lists.Add("http://i.meituan.com/poi/" + NextMatch.Groups[1].Value);
                                    //lists.Add("https://i.meituan.com/wrapapi/poiinfo?poiId=" + NextMatch.Groups[1].Value);
                                    lists.Add("https://i.meituan.com/wrapapi/allpoiinfo?riskLevel=71&optimusCode=10&poiId=" + NextMatch.Groups[1].Value + "&isDaoZong=false");
                                }

                                if (lists.Count == 0)  //当前页没有网址数据跳过之后的网址采集，进行下个foreach采集

                                    break;

                                string tm1 = DateTime.Now.ToString();  //获取系统时间

                                toolStripStatusLabel1.Text = tm1 + "-->正在采集" + city + areaId + keyword + "第" + i + "页";

                                foreach (string list in lists)

                                {

                                    string strhtml1 = meituan_GetUrl(list);  //定义的GetRul方法 返回 reader.ReadToEnd()

                                    Match name = Regex.Match(strhtml1, @"name"":""([\s\S]*?)""");
                                    Match tell = Regex.Match(strhtml1, @"phone"":""([\s\S]*?)""");
                                    Match addr = Regex.Match(strhtml1, @"address"":""([\s\S]*?)""");
                                    Match score = Regex.Match(strhtml1, @"score"":([\s\S]*?),");
                                    if (!tels.Contains(tell.Groups[1].Value))
                                    {


                                        ListViewItem listViewItem = this.listView1.Items.Add((listView1.Items.Count + 1).ToString());
                                        listViewItem.SubItems.Add(name.Groups[1].Value);
                                        listViewItem.SubItems.Add(tell.Groups[1].Value);
                                        listViewItem.SubItems.Add(addr.Groups[1].Value);
                                        listViewItem.SubItems.Add(city);
                                        listViewItem.SubItems.Add(score.Groups[1].Value);


                                        while (this.zanting == false)
                                        {
                                            Application.DoEvents();//如果loader是false表明正在加载,,则Application.DoEvents()意思就是处理其他消息。阻止当前的队列继续执行。
                                        }
                                        if (status == false)
                                        {
                                            return;
                                        }

                                        Thread.Sleep(1000);
                                    }




                                }

                                Thread.Sleep(2000);
                            }




                        }

                    }


                }
            }





            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        #endregion

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dr = MessageBox.Show("确定要关闭吗？", "关闭", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                Environment.Exit(0);
            }
            else
            {

            }
        }

        private void LinkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //最小化
        }
        private Point mPoint = new Point();
        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mPoint.X = e.X;
            mPoint.Y = e.Y;

        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point myPosittion = MousePosition;
                myPosittion.Offset(-mPoint.X, -mPoint.Y);
                Location = myPosittion;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            SetWindowRegion();
        }

        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
            FormPath = GetRoundedRectPath(rect, 10);
            this.Region = new Region(FormPath);

        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = radius;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            // 左上角
            path.AddArc(arcRect, 180, 90);

            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);

            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);

            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();//闭合曲线
            return path;
        }


        private void Button1_Click(object sender, EventArgs e)
        {




            status = true;
            button1.Enabled = false;
            Thread search_thread = new Thread(new ThreadStart(run));
            Control.CheckForIllegalCrossThreadCalls = false;
            search_thread.Start();

            //status = true;
            //button1.Enabled = false;
            //Thread search_thread = new Thread(new ThreadStart(run1));
            //Control.CheckForIllegalCrossThreadCalls = false;
            //search_thread.Start();


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            zanting = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            zanting = true;
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            method.DataTableToExcel(method.listViewToDataTable(this.listView1), "Sheet1", true);
        }

     

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //System.Diagnostics.Process.Start("http://www.acaiji.com");
        }

        private void LinkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            listView1.Items.Clear();
        }

       

     

        private void LinkLabel11_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            button1.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            status = false;
            button1.Enabled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void LinkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            method.ListviewToTxt(listView1);
        }
    }
}
