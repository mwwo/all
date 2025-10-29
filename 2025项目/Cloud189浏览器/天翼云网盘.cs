using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System;
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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cloud189浏览器
{
    public partial class 天翼云网盘 : Form
    {
        public 天翼云网盘()
        {
            InitializeComponent();
            InitializeWebView2();
            InitializeTimer();
        }

        private void 天翼云网盘_Load(object sender, EventArgs e)
        {

        }



      
      

        private async void InitializeWebView2()
        {
            try
            {
              
                await webView21.EnsureCoreWebView2Async();

                // 导航到天翼云网盘登录页面
                webView21.Source = new Uri("https://cloud.189.cn/");

                // 监听导航完成事件
                webView21.NavigationCompleted += WebView_NavigationCompleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"WebView2初始化失败: {ex.Message}");
            }
        }


        System.Windows.Forms.Timer cookieTimer;
        private void InitializeTimer()
        {
            // 创建定时器，每隔10秒获取一次Cookie[citation:5]
            cookieTimer = new System.Windows.Forms.Timer();
            //cookieTimer.Interval =Convert.ToInt32(textBox1.Text)*60*1000; // 
            cookieTimer.Interval = 10000; // 
            cookieTimer.Tick += CookieTimer_Tick;
        }

        private void WebView_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            // 页面加载完成后启动定时器
            if (e.IsSuccess)
            {
                cookieTimer.Start();
                this.Text = "天翼云网盘 - 已启动Cookie监控";
            }
        }


        private async void CookieTimer_Tick(object sender, EventArgs e)
        {
            cookieTimer.Interval = Convert.ToInt32(textBox1.Text) * 60 * 1000; // 
            webView21.Reload();
            if (webView21?.CoreWebView2?.CookieManager == null)
                return;

            try
            {
                // 获取所有Cookie[citation:1]
                var cookies = await webView21.CoreWebView2.CookieManager.GetCookiesAsync("https://cloud.189.cn");

                // 将Cookie保存到文件
                await SaveCookiesToFile(cookies);

                // 显示最新获取时间
                this.Text = $"天翼云网盘 - 最后更新: {DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"获取Cookie失败: {ex.Message}");
            }
        }

        private async System.Threading.Tasks.Task SaveCookiesToFile(System.Collections.Generic.IList<Microsoft.Web.WebView2.Core.CoreWebView2Cookie> cookies)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string parentDirectory = Path.GetDirectoryName(currentDirectory);
            string cookieFile = Path.Combine(parentDirectory, "cloud_cookies.txt");

            try
            {
                using (StreamWriter writer = new StreamWriter(cookieFile, append: false))
                {
                    StringBuilder sb = new StringBuilder();

                    foreach (var cookie in cookies)
                    {
                        sb.Append(cookie.Name+"="+ cookie.Value+";");
                      
                    }

                    await writer.WriteLineAsync(sb.ToString());
                }

                // 可选：在控制台显示保存成功信息
                Console.WriteLine($"Cookie已保存到: {cookieFile}, 共{cookies.Count}个Cookie");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"保存Cookie文件失败: {ex.Message}", "错误",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        #region 天翼云请求
        private static string Request_cloud_189_cn(string url, string cookie)
        {


            try
            {
                string charset = "utf-8";
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
        
        #region  天翼云盘获取文件ID

        public static string cloud_getfileid(string id,string cookie)
        {
            try
            {
                string url = "https://cloud.189.cn/api/open/file/searchFiles.action?noCache=0.942218720555657&folderId=-11&pageSize=60&pageNum=1&filename=" + id + "&recursive=1&iconOption=5&mediaType=0&orderBy=lastOpTime&descending=true";
                string html = Request_cloud_189_cn(url, cookie);
               
                string fileurl = Regex.Match(html, @"""id"":""([\s\S]*?)""").Groups[1].Value.Trim();
                return fileurl;
            }
            catch (Exception ex)
            {

                return "";
            }


        }

        #endregion

        #region  天翼云盘下载

        public static string cloud189_download(string id)
        {
            try
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string parentDirectory = Path.GetDirectoryName(currentDirectory);
                string cookie = File.ReadAllText(parentDirectory + "\\cloud_cookies.txt").Trim();
                MessageBox.Show(cookie);
                string fileId = cloud_getfileid(id,cookie);
                string url = "https://cloud.189.cn/api/open/file/getFileDownloadUrl.action?noCache=0.4189543716734623&fileId=" + fileId;
                string html = Request_cloud_189_cn(url, cookie);
               
                string fileurl = Regex.Match(html, @"""fileDownloadUrl"":""([\s\S]*?)""").Groups[1].Value.Trim();
                return fileurl;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return "";
            }


        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
          MessageBox.Show(cloud189_download("54100001"));
        }
    }
}
