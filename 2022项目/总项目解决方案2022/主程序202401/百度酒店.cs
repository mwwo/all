using myDLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace 主程序202401
{
    public partial class 百度酒店 : Form
    {
        public 百度酒店()
        {
            InitializeComponent();
        }
        Thread thread;
        private void button1_Click(object sender, EventArgs e)
        {
            cloud189("");
            //status = true;
            //if (thread == null || !thread.IsAlive)
            //{
            //    thread = new Thread(run);
            //    thread.Start();
            //    Control.CheckForIllegalCrossThreadCalls = false;
            //}
        }

        #region GET请求带COOKIE
        public static string GetUrlWithCookie(string Url, string COOKIE, string charset)
        {
            string result;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.AllowAutoRedirect = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";
               
                request.Headers.Add("Cookie", COOKIE);
                request.Headers.Add("Accept-Encoding", "gzip");
                request.KeepAlive = true;
                request.Accept = "application/json";
                request.Timeout = 5000;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                bool flag = response.Headers["Content-Encoding"] == "gzip";
                string html;
                if (flag)
                {
                    GZipStream gzip = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                    StreamReader reader = new StreamReader(gzip, Encoding.GetEncoding(charset));
                    html = reader.ReadToEnd();
                    reader.Close();
                }
                else
                {
                    StreamReader reader2 = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(charset));
                    html = reader2.ReadToEnd();
                    reader2.Close();
                }
                response.Close();
                result = html;
            }
            catch (WebException ex)
            {
                result = ex.ToString();

            }
            return result;
        }
        #endregion



        #region 天翼云请求
        private static string Request_cloud_189_cn(string url,string cookie)
        {
          

            try
            {
                string charset= "utf-8";    
                string result;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

                request.KeepAlive = true;
                request.Headers.Add("sec-ch-ua-platform", @"""Windows""");
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36";
                request.Accept = "application/json;charset=UTF-8";
                request.Headers.Add("sec-ch-ua", @"""Chromium"";v=""140"", ""Not=A?Brand"";v=""24"", ""Google Chrome"";v=""140""");
                request.Headers.Add("Sign-Type", @"1");
                request.Headers.Add("sec-ch-ua-mobile", @"?0");
                request.Headers.Add("Sec-Fetch-Site", @"same-origin");
                request.Headers.Add("Sec-Fetch-Mode", @"cors");
                request.Headers.Add("Sec-Fetch-Dest", @"empty");
                request.Referer = "https://cloud.189.cn/web/main/file/folder/-11";

                request.Headers.Set(HttpRequestHeader.Cookie, cookie);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                bool flag = response.Headers["Content-Encoding"] == "gzip";
                string html;
                if (flag)
                {
                    GZipStream gzip = new GZipStream(response.GetResponseStream(), CompressionMode.Decompress);
                    StreamReader reader = new StreamReader(gzip, Encoding.GetEncoding(charset));
                    html = reader.ReadToEnd();
                    reader.Close();
                }
                else
                {
                    StreamReader reader2 = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(charset));
                    html = reader2.ReadToEnd();
                    reader2.Close();
                }
                response.Close();
                result = html;

                return result;
            }
            catch (WebException e)
            {
                return "";
            }
     
        }

        #endregion
        #region  云盘下载

        public  string cloud189(string link)
        {
            try
            {
                /* string cloud_cookie = File.ReadAllText(Server.MapPath("~/") + "cookie.txt").Trim(); */
                ;
                string fileId = "725321217015532937";
                string url = "https://cloud.189.cn/api/open/file/getFileDownloadUrl.action?noCache=0.4189543716734623&fileId=" + fileId;
                string cookie = "chinatelecomdata2015jssdkcross=%7B%22distinct_id%22%3A%22MTk3ODcxY2EwM2ZjNjEtMGZiNDQ5ZTJhNDdjNzM4LTI2MDExZTUxLTM2ODY0MDAtMTk3ODcxY2EwNDAyMmI5%22%2C%22first_id%22%3A%22%22%2C%22props%22%3A%7B%22%24latest_traffic_source_type%22%3A%22%E8%87%AA%E7%84%B6%E6%90%9C%E7%B4%A2%E6%B5%81%E9%87%8F%22%2C%22%24latest_search_keyword%22%3A%22%E6%9C%AA%E5%8F%96%E5%88%B0%E5%80%BC%22%2C%22%24latest_referrer%22%3A%22https%3A%2F%2Fwww.baidu.com%2F%22%2C%22_latest_utm_ch%22%3A%22100003%22%7D%2C%22login_type%22%3A%22%22%2C%22utms%22%3A%7B%7D%2C%22latest_utms%22%3A%7B%22_latest_utm_ch%22%3A%22100003%22%7D%2C%22%24device_id%22%3A%22197871ca03fc61-0fb449e2a47c738-26011e51-3686400-197871ca04022b9%22%7D; zhizhendata2015jssdkcross=%7B%22distinct_id%22%3A%22MTk3ODcxY2VlMWZiZTYtMDUyODNlNWRmOGQ2MTItMjYwMTFlNTEtMzY4NjQwMC0xOTc4NzFjZWUyMDJiMTU%3D%22%2C%22first_id%22%3A%22%22%2C%22props%22%3A%7B%7D%2C%22login_type%22%3A%22%22%2C%22utms%22%3A%7B%7D%2C%22%24device_id%22%3A%22197871cee1fbe6-05283e5df8d612-26011e51-3686400-197871cee202b15%22%7D; JSESSIONID=E24EDE7FA3E9162F82584D652CF13556; COOKIE_LOGIN_USER=90D3A9A6F2664FB7FDB5AE176BB5A4DB6C0DDE3A7E1E9CDFCC12365279199358DE5E617AA20490279857DD29A2B20BAE878A4A2319EA37A9D5BCC0AF";
                string html = Request_cloud_189_cn(url, cookie);

                string fileurl = Regex.Match(html, @"""fileDownloadUrl"":""([\s\S]*?)""").Groups[1].Value.Trim();
                MessageBox.Show(html);
                textBox1.Text = fileurl;
                return fileurl;
            }
            catch (Exception ex)
            {

                return "";
            }


        }

        #endregion
        bool zanting = true;
        bool status = true;
        public void run()
        {
            try
            {
              
                for (int i = 0; i < 100; i++)
                {

                    string code = citycode[comboBox1.Text];
                    string url = "https://hotel.pae.baidu.com/hotel/xcx/list?req_from=fw_xcx&xcx_from=fw_xcx&hotel_sid=&source=&courierId=&promotionCom=&xfrom=&qid=&fr=&platform=web&pn="+i+"&rn=10&startDate="+DateTime.Now.AddDays(1).ToString("yyyy-MM-dd")+ "&endDate="+DateTime.Now.AddDays(2).ToString("yyyy-MM-dd")+"&cityId="+ code + "&realCityId="+ code + "&query=%E9%85%92%E5%BA%97&bound=&_pfName=HOTEL_LIST_MAP";
                 
                    string html = method.GetUrl(url, "utf-8");
                   
           
                
                    MatchCollection uids = Regex.Matches(html, @"""uid"":""([\s\S]*?)""");
                    if (uids.Count == 0)
                    {
                        continue;
                    }

                    for (int a = 0; a < uids.Count; a++)
                    {
                        try
                        {
                            string aurl = "https://hotel.pae.baidu.com/hotel/xcx/detail?req_from=fw_xcx&xcx_from=fw_xcx&hotel_sid=&source=1081000910019156&courierId=&promotionCom=&xfrom=1081000910019156&qid=4471760522&fr=&platform=web&center_x=&center_y=&cityId=277&endDate=2024-01-08&realCityId=277&sfrom=&startDate=2024-01-07&uid=" + uids[a].Groups[1].Value +"&enterT=1704599055601&act=hotel_detail&type=hotel_detail&_pfName=HOTEL_DETAIL";
                            string ahtml = method.GetUrl(aurl,"utf-8");
                           
                           string name= Regex.Match(ahtml, @"""title"":""([\s\S]*?)""").Groups[1].Value;
                            string openYear = Regex.Match(ahtml, @"""openYear"":""([\s\S]*?)""").Groups[1].Value;
                            string phone = Regex.Match(ahtml, @"""phone"":""([\s\S]*?)""").Groups[1].Value;
                            string text = Regex.Match(ahtml, @"""text"":""([\s\S]*?)""").Groups[1].Value;
                            string price = Regex.Match(ahtml, @"""price"":([\s\S]*?),").Groups[1].Value;
                            ListViewItem lv1 = listView1.Items.Add((listView1.Items.Count + 1).ToString()); //使用Listview展示数据
                            lv1.SubItems.Add(name);
                            lv1.SubItems.Add(openYear);
                            lv1.SubItems.Add(phone);
                            lv1.SubItems.Add(text);
                            lv1.SubItems.Add(price);
                            if (listView1.Items.Count > 2)
                            {
                                this.listView1.Items[this.listView1.Items.Count - 1].EnsureVisible();
                            }
                            while (this.zanting == false)
                            {
                                Application.DoEvents();//如果loader是false表明正在加载,,则Application.DoEvents()意思就是处理其他消息。阻止当前的队列继续执行。
                            }
                            if (status == false)
                                return;
                        }
                        catch (Exception)
                        {

                            continue;
                        }
                    }



                    Thread.Sleep(Convert.ToInt32(textBox1.Text) * 1000);

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (zanting == false)
            {

                zanting = true;
            }
            else
            {
                zanting = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            status = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            method.DataTableToExcel(method.listViewToDataTable(this.listView1), "Sheet1", true);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        Dictionary<string, string> citycode= new Dictionary<string, string>();

        #region  获取城市ID

        public void GetcityId()
        {

            try
            {
                string url = "https://hotel.pae.baidu.com/hotel/xcx/city-list?req_from=fw_xcx&xcx_from=fw_xcx&hotel_sid=&source=1081000910019156&courierId=&promotionCom=&xfrom=1081000910019156&qid=8743974103&fr=&platform=web&act=hotel_city_list&_pfName=HOTEL_CITY_LIST";
                string html =method.GetUrl(url,"utf-8");
                MatchCollection cityIds = Regex.Matches(html, @"""code"":""([\s\S]*?)""");
                MatchCollection names = Regex.Matches(html, @"""name"":""([\s\S]*?)""");
                for (int i = 0; i < cityIds.Count; i++)
                {
                    comboBox1.Items.Add(names[i].Groups[1].Value);
                    citycode.Add(names[i].Groups[1].Value, cityIds[i].Groups[1].Value);
                }


            }

            catch (System.Exception ex)
            {
                ex.ToString();
            }

        }

        #endregion

        private void 百度酒店_Load(object sender, EventArgs e)
        {
            GetcityId();
        }
    }
}
