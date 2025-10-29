using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace xueke_api
{
    public class method
    {

        public static string methodurl = method.Base64Decode(Encoding.GetEncoding("utf-8"), "aHR0cHM6Ly91c2VyLnp4eGsuY29tL2NyZWF0b3IvYXBpL3YxL2NyZWF0b3ItcmVzb3VyY2UvZ2V0LXJlc291cmNlLWRldGFpbD9yZXNvdXJjZUlkPQ==");


        


        #region 生成随机字符串
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly Random _random = new Random();

        // 生成指定长度的随机字符串
        public static string GenerateRandomString(int length)
        {
            if (length <= 0)
                throw new ArgumentException("长度必须大于0", nameof(length));

            StringBuilder result = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                // 随机从字符集中选择一个字符
                result.Append(Chars[_random.Next(Chars.Length)]);
            }

            return result.ToString();
        }

        #endregion

        #region GET请求带COOKIE
        public static string GetUrlWithCookie(string Url, string COOKIE)
        {
            string charset = "utf-8";
            string result;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.AllowAutoRedirect = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";
                request.Referer = Url;
                //WebHeaderCollection headers = request.Headers;
                //headers.Add("version:TYC-XCX-WX");
                request.Headers.Add("Cookie", COOKIE);
                request.Headers.Add("Accept-Encoding", "gzip");
                request.KeepAlive = true;
                request.Accept = "*/*";
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


        #region GET请求
        public static string GetUrl(string Url)
        {
            string charset = "utf-8";
            string result;
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.AllowAutoRedirect = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";
                request.Referer = Url;
                //WebHeaderCollection headers = request.Headers;
                //headers.Add("version:TYC-XCX-WX");
               
                request.Headers.Add("Accept-Encoding", "gzip");
                request.KeepAlive = true;
                request.Accept = "*/*";
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

        #region Base64编码
        public static string Base64Encode(Encoding encodeType, string source)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }
        #endregion


        #region Base64解码
        public static string Base64Decode(Encoding encodeType, string result)
        {
            string decode = string.Empty;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encodeType.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
        #endregion



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

        public static string cloud_getfileid(string id, string cookie)
        {
            try
            {

                string url = "https://cloud.189.cn/api/open/file/searchFiles.action?noCache=0.942218720555657&folderId=-11&pageSize=1&pageNum=1&filename=" + id + "&recursive=1&iconOption=5&mediaType=0&orderBy=lastOpTime&descending=true";
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
                string cookie = File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "cloud_cookies.txt").Trim();
                string fileId = cloud_getfileid(id, cookie);
                string url = "https://cloud.189.cn/api/open/file/getFileDownloadUrl.action?noCache=0.4189543716734623&fileId=" + fileId;
                string html = Request_cloud_189_cn(url, cookie);
                string fileurl = Regex.Match(html, @"""fileDownloadUrl"":""([\s\S]*?)""").Groups[1].Value.Trim();
                return fileurl;
            }
            catch (Exception ex)
            {

                return ex.ToString();
            }


        }

        #endregion

    }
}