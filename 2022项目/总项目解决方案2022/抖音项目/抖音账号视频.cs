﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using myDLL;
using Microsoft.VisualBasic;
using System.Security.Cryptography;
using System.Web;

namespace 抖音项目
{
    public partial class 抖音账号视频 : Form
    {
        public 抖音账号视频()
        {
            InitializeComponent();
        }
        string path = AppDomain.CurrentDomain.BaseDirectory;
		string ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.88 Safari/537.36";
		
		#region GET请求
		public  string GetUrl(string Url, string charset)
		{
			
			string result;
			try
			{
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
				//request.Proxy = null;
				//request.AllowAutoRedirect = true;
				request.UserAgent = ua;
				request.Referer = "https://www.douyin.com/";
				request.Headers.Add("Cookie", COOKIE);
				request.Headers.Add("Accept-Encoding", "gzip");
				//request.KeepAlive = true;
				//request.Accept = "*/*";
				//request.Timeout = 5000;
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
			catch (Exception ex)
			{
				result = ex.ToString();
			}
			return result;
		}
		#endregion

		#region POST默认请求
		public static string PostUrlDefault(string url, string postData, string COOKIE)
		{
			string result;
			try
			{
				string charset = "utf-8";
				ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
				request.Method = "Post";
				
				//request.ContentType = "application/x-www-form-urlencoded";
				 request.ContentType = "application/json";
				request.ContentLength = (long)Encoding.UTF8.GetBytes(postData).Length;
				request.Headers.Add("Accept-Encoding", "gzip");
				request.AllowAutoRedirect = false;
				request.KeepAlive = true;
				request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36";
				request.Headers.Add("Cookie", COOKIE);
				request.Referer = "http://39.107.101.62:8111/docs";
				StreamWriter sw = new StreamWriter(request.GetRequestStream());
				sw.Write(postData);
				sw.Flush();
				HttpWebResponse response = request.GetResponse() as HttpWebResponse;
				response.GetResponseHeader("Set-Cookie");
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
				//result = ex.ToString();
				//400错误也返回内容
				using (var reader = new StreamReader(ex.Response.GetResponseStream()))
				{
					result = reader.ReadToEnd();
				}
			}
			return result;
		}
		#endregion

		public string getxbogus(string postdata)
        {
			string url = "http://39.107.101.62:8111/dy/xbogus/";
			string data = "{\"params\":\""+postdata+ "\", \"ua\":\"" + ua+"\",\"enc_type\": 1}";
			string html = PostUrlDefault(url,data,"");
			
			string xbogus= Regex.Match(html, @"""x_bogus"":""([\s\S]*?)""").Groups[1].Value.Trim();
			return xbogus;
		}
        private string inipath = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

      
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    
        public void IniWriteValue(string Section, string Key, string Value)
        {
          WritePrivateProfileString(Section, Key, Value, this.inipath);
        }

        
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder stringBuilder = new StringBuilder(500);
            int privateProfileString = GetPrivateProfileString(Section, Key, "", stringBuilder, 500, this.inipath);
            return stringBuilder.ToString();
        }

       
        public bool ExistINIFile()
        {
            return File.Exists(this.inipath);
        }
        public void jiance()
        {
            bool flag = this.ExistINIFile();
            if (flag)
            {
                string a = this.IniReadValue("values", "key");
                string text = this.IniReadValue("values", "secret");
                string[] array = text.Split(new string[]
                {
                    "asd147"
                }, StringSplitOptions.None);
                bool flag2 = Convert.ToInt32(array[1]) < Convert.ToInt32(method.GetTimeStamp());
                if (flag2)
                {
                    MessageBox.Show("激活已过期");
                    string text2 = Interaction.InputBox("您的机器码如下，请复制机器码提供到后台，输入激活码然后激活！", "激活软件", method.GetMD5(method.GetMacAddress()), -1, -1);
                    string[] array2 = text2.Split(new string[]
                    {
                        "asd"
                    }, StringSplitOptions.None);
                    bool flag3 = array2[0] == method.GetMD5(method.GetMD5(method.GetMacAddress()) + "siyiruanjian");
                    if (flag3)
                    {
                        this.IniWriteValue("values", "key", method.GetMD5(method.GetMacAddress()));
                        this.IniWriteValue("values", "secret", text2);
                        MessageBox.Show("激活成功");
                    }
                    else
                    {
                        MessageBox.Show("激活码错误");
                        Process.GetCurrentProcess().Kill();
                    }
                }
                else
                {
                    bool flag4 = array[0] != method.GetMD5(method.GetMD5(method.GetMacAddress()) + "siyiruanjian") || a != method.GetMD5(method.GetMacAddress());
                    if (flag4)
                    {
                        string text3 = Interaction.InputBox("您的机器码如下，请复制机器码提供到后台，输入激活码然后激活！", "激活软件", method.GetMD5(method.GetMacAddress()), -1, -1);
                        string[] array3 = text3.Split(new string[]
                        {
                            "asd147"
                        }, StringSplitOptions.None);
                        bool flag5 = array3[0] == method.GetMD5(method.GetMD5(method.GetMacAddress()) + "siyiruanjian");
                        if (flag5)
                        {
                            this.IniWriteValue("values", "key", method.GetMD5(method.GetMacAddress()));
                            this.IniWriteValue("values", "secret", text3);
                            MessageBox.Show("激活成功");
                        }
                        else
                        {
                            MessageBox.Show("激活码错误");
                            Process.GetCurrentProcess().Kill();
                        }
                    }
                }
            }
            else
            {
                string text4 = Interaction.InputBox("您的机器码如下，请复制机器码提供到后台，输入激活码然后激活！", "激活软件", method.GetMD5(method.GetMacAddress()), -1, -1);
                string[] array4 = text4.Split(new string[]
                {
                    "asd147"
                }, StringSplitOptions.None);
                bool flag6 = array4[0] == method.GetMD5(method.GetMD5(method.GetMacAddress()) + "siyiruanjian");
                if (flag6)
                {
                    this.IniWriteValue("values", "key", method.GetMD5(method.GetMacAddress()));
                    this.IniWriteValue("values", "secret", text4);
                    MessageBox.Show("激活成功");
                }
                else
                {
                    MessageBox.Show("激活码错误");
                    Process.GetCurrentProcess().Kill();
                }
            }
        }
        private void 抖音账号视频_Load(object sender, EventArgs e)
        {
			//jiance();
			
			StreamReader sr = new StreamReader(path+"cookie.txt", Encoding.UTF8);
            //一次性读取完 
           COOKIE= sr.ReadToEnd().Trim();
        }

        public string COOKIE = "";

		string max_cursor ="0";

		public static string GenerateXBogus(string url, string msToken)
		{
			// 解析URL参数
			var uri = new Uri(url);
			var queryParams = HttpUtility.ParseQueryString(uri.Query);

			// 排序并构造参数字符串
			var sortedParams = new StringBuilder();
			foreach (var key in queryParams.AllKeys.OrderBy(k => k))
			{
				sortedParams.Append($"{key}={queryParams[key]}&");
			}
			if (sortedParams.Length > 0) sortedParams.Length--; // 移除末尾的&

			// 构造基础字符串
			var baseStr = new StringBuilder();
			baseStr.Append(uri.AbsolutePath)
				   .Append("?")
				   .Append(sortedParams)
				   .Append($"&msToken={msToken}");

			// 添加时间戳和随机数
			var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
			var nonce = new Random().Next(1000, 10000); // 包含1000不包含10000
			baseStr.Append($"&timestamp={timestamp}&nonce={nonce}");

			// MD5哈希计算
			var md5 = MD5.Create();
			var hashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(baseStr.ToString()));

			// 转换为十六进制大写字符串
			var hexString = new StringBuilder();
			foreach (var b in hashBytes)
			{
				hexString.Append(b.ToString("X2"));
			}

			// 截取指定部分
			return hexString.ToString();
		}
		public void run()
        {


            // ArrayList lists = function .getusers();
            ArrayList lists = new ArrayList();
            lists.Add("MS4wLjABAAAA-1G1bXOz9qcLla3Bs4mBFo7cx8iHfQyIQhiAMfKhIbpKKy6NWtiULFYs7nzkN2bX");
            try
            {
                for (int a= 0; a < lists.Count; a++)
                {
					
						string uid = lists[a].ToString();
					
						textBox1.Text += DateTime.Now.ToString() + "正在读取：" + uid + "\r\n";
						
						string data = "device_platform=webapp&aid=6383&sec_user_id=" + uid + "&max_cursor="+ max_cursor + "&offset=0&count=18";
					//getxbogus(data);
					string xbogus = getxbogus(data);

						string url = "https://www.douyin.com/aweme/v1/web/aweme/favorite/?" + data + "&X-Bogus=" + xbogus;



						string html = GetUrl(url, "utf-8");


					

					//max_cursor = Regex.Match(html, @"""max_cursor"":([\s\S]*?),").Groups[1].Value;



					MatchCollection ahtmls = Regex.Matches(html, @"video_tag([\s\S]*?)danmaku_control");

						MatchCollection aweme_ids = Regex.Matches(html, @"""comment_gid"":([\s\S]*?),");
						MatchCollection photo_urls = Regex.Matches(html, @"""cover""([\s\S]*?)url_list"":\[""([\s\S]*?)""");
						MatchCollection durations = Regex.Matches(html, @"""video_duration"":([\s\S]*?),");

						MatchCollection authors = Regex.Matches(html, @"""author"":""([\s\S]*?)""");
						MatchCollection comment_counts = Regex.Matches(html, @"""comment_count"":([\s\S]*?),");
						MatchCollection share_counts = Regex.Matches(html, @"""share_count"":([\s\S]*?),");
						MatchCollection collect_counts = Regex.Matches(html, @"""collect_count"":([\s\S]*?),");

						MatchCollection digg_counts = Regex.Matches(html, @"""digg_count"":([\s\S]*?),");
						MatchCollection create_times = Regex.Matches(html, @"""create_time"":([\s\S]*?),");
						MatchCollection desc = Regex.Matches(html, @",""desc"":""([\s\S]*?)""");
						MatchCollection play_addrs = Regex.Matches(html, @"""bit_rate_audio""([\s\S]*?)url_list"":\[""([\s\S]*?)""");

						if (aweme_ids.Count == 0)
						{
							textBox1.Text += DateTime.Now.ToString() + uid + "喜欢列表加密" + "\r\n";
						continue;
						}


					JObject jsonObject = JObject.Parse(html);
					JArray people = (JArray)jsonObject["aweme_list"];
					//MessageBox.Show(aweme_ids.Count+"  " + desc.Count);
					for (int i = 0; i < aweme_ids.Count; i++)
						{
							try
							{
							//	string descs = desc[i-1].Groups[1].Value.Trim();
							//string aweme_url = "https://www.douyin.com/video/" + aweme_ids[i].Groups[1].Value.Trim();
							//!ahtmls[i].Groups[1].Value.Contains("main_arch_common") || 

							JObject person = (JObject)people[i];
							
							string aweme_id = person["aweme_id"].ToString();
							string descs = person["desc"].ToString();
						

							
							string aweme_url = "https://www.douyin.com/video/" + aweme_id.Trim();


							if (descs=="" || descs.Contains("#")|| descs.Length < 6)
								{
									textBox1.Text += DateTime.Now.ToString() + descs + "不是千川视频跳过...." + "\r\n";
									continue;
                                }

							




								
								string photo_url = function.Unicode2String(photo_urls[i].Groups[2].Value);
								string duration = durations[i].Groups[1].Value;
								string author_nickname = authors[i].Groups[1].Value;

								string comment_count = comment_counts[i].Groups[1].Value.Replace("}", "");
								string share_count = share_counts[i].Groups[1].Value.Replace("}", "");
								string collect_count = collect_counts[i].Groups[1].Value.Replace("}", "");
								string digg_count = digg_counts[i].Groups[1].Value.Replace("}", "");
								string create_time = create_times[i].Groups[1].Value;
								
								string created_at = function.ConvertStringToDateTime(create_times[i].Groups[1].Value).ToString("yyyy-MM-dd");
								string updated_at = created_at;
								string whoslike = uid;
								string author_sec_uid = "";
								string author_douyin_id = "";
								int dz_speed = 1;
								string create_date_time = DateTime.Now.ToString("yyyy-MM-dd hh:MM:ss");
								string unique_sign = "";
								string play_addr1 = function.Unicode2String(play_addrs[i].Groups[2].Value);

								string info = function.adddata(aweme_url, photo_url, duration, author_nickname, comment_count, share_count, collect_count, digg_count, create_time, descs, created_at, updated_at, whoslike, author_sec_uid, author_douyin_id, dz_speed, create_date_time, unique_sign, play_addr1);

								textBox1.Text += DateTime.Now.ToString() + descs + info + "\r\n";
								if (textBox1.Text.Length > 1000)
								{
									textBox1.Text = "";
								}
							}
							catch (Exception)
							{

								continue;
							}
						}



						Thread.Sleep(1000);
					
				}

			}
			catch (Exception ex)
            {

				textBox1.Text += ex.ToString()+"\r\n" ;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
			this.textBox1.SelectionStart = this.textBox1.Text.Length;
			this.textBox1.SelectionLength = 0;
			this.textBox1.ScrollToCaret();
		}
		Thread thread;
        private void button1_Click(object sender, EventArgs e)
        {

			
			if (DateTime.Now > Convert.ToDateTime("2025-05-11"))
			{
				return;
			}
			if (thread == null || !thread.IsAlive)
			{
				thread = new Thread(run);
				thread.Start();
				Control.CheckForIllegalCrossThreadCalls = false;
			}
		}

        private void button2_Click(object sender, EventArgs e)
        {

			string url = "https://www.douyin.com/aweme/v1/web/aweme/post/?device_platform=webapp";
			string userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36";

			string xBogus = GenerateXBogus(url, userAgent);
			MessageBox.Show($"生成的x-Bogus: {xBogus}");
			//thread.Abort();
        }
    }
}
