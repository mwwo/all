using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace 学科网下载
{
    public class method
    {
        public static string constr = "Host =localhost;Database=xueke;Username=root;Password=YB29ts9bWM7nKnChvfd3";
       
        


       public static string methodurl = method.Base64Decode(Encoding.GetEncoding("utf-8"), "aHR0cHM6Ly91c2VyLnp4eGsuY29tL2NyZWF0b3IvYXBpL3YxL2NyZWF0b3ItcmVzb3VyY2UvZ2V0LXJlc291cmNlLWRldGFpbD9yZXNvdXJjZUlkPQ==");

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

        #region 下载文件
        public static string downloadFile(string URLAddress, string subPath, string name)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                WebClient client = new WebClient();
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36");
               // client.Headers.Add("Cookie", COOKIE);
                //client.Headers.Add("Referer", "https://m.mm131.net/chemo/89_5.html");
                bool flag = !Directory.Exists(subPath);
                if (flag)
                {
                    Directory.CreateDirectory(subPath);
                }
                client.DownloadFile(URLAddress, subPath + "\\" + name);
                return "";
            }
            catch (WebException ex)
            {
               return ex.ToString();
            }


        }
        #endregion


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


        #region 正规文件名 不包含特殊字符
       
        public static string CleanUrlKeepChinese(string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            // 正则表达式：保留中文、字母、数字和 -_.~ 符号，其他全部移除
            // \u4e00-\u9fa5 是中文Unicode范围
            return Regex.Replace(url, @"[^\u4e00-\u9fa5a-zA-Z0-9-_.]", "");
        }

        /// <summary>
        /// 给文件名添加数字后缀（保留原扩展名）
        /// </summary>
        /// <param name="originalFileName">原始文件名（如：今天.docx）</param>
        /// <param name="number">要添加的数字</param>
        /// <returns>处理后的文件名（如：今天-1561.docx）</returns>
        public static string AddIDToFileName(string originalFileName, string number)
        {
            if (string.IsNullOrEmpty(originalFileName))
                throw new ArgumentException("文件名不能为空", nameof(originalFileName));

            // 获取文件名（不含扩展名）
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(originalFileName);
            // 获取扩展名（含.）
            string extension = Path.GetExtension(originalFileName);

            // 拼接新文件名（格式：原文件名-数字.扩展名）
            return $"{fileNameWithoutExtension}_{number}{extension}";
        }


        #endregion

        #region  修改次数

        public static void editekey(string key)
        {

            try
            {

                MySqlConnection mycon = new MySqlConnection(method.constr);
                mycon.Open();

                MySqlCommand cmd = new MySqlCommand("update mykeys SET cishu = cishu - 1  where mykey='" + key + " ' ", mycon);         //SQL语句读取textbox的值'"+skinTextBox1.Text+"'

                int count = cmd.ExecuteNonQuery();  //count就是受影响的行数,如果count>0说明执行成功,如果=0说明没有成功.
                mycon.Close();
            }

            catch (System.Exception ex)
            {

            }
        }
        #endregion-


        #region 添加Ip等信息
        public static string addiplog(string key,string userIp)
        {
            try
            {

                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string area = getareaFromIp(userIp);
                MySqlConnection mycon = new MySqlConnection(method.constr);
                mycon.Open();

                string sql = "INSERT INTO logs (mykey,ip,time,area)VALUES(@key, @userIp, @time, @area)";


                MySqlCommand cmd = new MySqlCommand(sql, mycon);
                cmd.Parameters.AddWithValue("@key", key);
                cmd.Parameters.AddWithValue("@userIp", userIp);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@area", area);

                int count = cmd.ExecuteNonQuery();  //count就是受影响的行数,如果count>0说明执行成功,如果=0说明没有成功.

                mycon.Close();
                return area;

            }
            catch (Exception)
            {
                return "";

            }

        }

        public static string getareaFromIp(string ip)
        {
            string url = "https://www.ipshudi.com/" + ip + ".htm";
            string html = method.GetUrlWithCookie(url, "");
            string area = Regex.Match(html, @"归属地</td>([\s\S]*?)</span>").Groups[1].Value;
            area = Regex.Replace(area, "<[^>]+>", "").Replace(" ", "").Trim();
            return area;
        }


        #endregion

        #region  添加下载记录

        public static void adddownlog(string key, string link, string isvip,string cishu,string day,string price,string commercialLevel,string gradeId, string courseId,string stageId)
        {
            try
            {

                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                MySqlConnection mycon = new MySqlConnection(method.constr);
                mycon.Open();

                string sql = "INSERT INTO downlogs (mykey,link,isvip,downtime,cishu,day,price,commercialLevel,gradeId, courseId, stageId)VALUES(@key, @link, @isvip, @downtime,@cishu,@day,@price,@commercialLevel,@gradeId, @courseId, @stageId)";


                MySqlCommand cmd = new MySqlCommand(sql, mycon);
                cmd.Parameters.AddWithValue("@key", key);
                cmd.Parameters.AddWithValue("@link", link);
                cmd.Parameters.AddWithValue("@isvip", isvip);
                cmd.Parameters.AddWithValue("@downtime", time);
                cmd.Parameters.AddWithValue("@cishu", cishu);
                cmd.Parameters.AddWithValue("@day", day);
                cmd.Parameters.AddWithValue("@price", price);
                cmd.Parameters.AddWithValue("@commercialLevel", commercialLevel);
                cmd.Parameters.AddWithValue("@gradeId", gradeId);
                cmd.Parameters.AddWithValue("@courseId", courseId);
                cmd.Parameters.AddWithValue("@stageId", stageId);
                int count = cmd.ExecuteNonQuery();  //count就是受影响的行数,如果count>0说明执行成功,如果=0说明没有成功.


                mycon.Close();


            }
            catch (Exception)
            {


            }
        }
        #endregion

        #region  MD5加密
        public static string GetMD5(string txt)
        {
            string result;
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.Default.GetBytes(txt);
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                result = sb.ToString();
            }
            return result;
        }
        #endregion

        #region  修改时间

        public static void editetime(string key, string time)
        {

            try
            {

                MySqlConnection mycon = new MySqlConnection(method.constr);
                mycon.Open();



                MySqlCommand cmd = new MySqlCommand("update mykeys SET extime = '" + time + " '  where mykey='" + key + " ' ", mycon);         //SQL语句读取textbox的值'"+skinTextBox1.Text+"'

                int count = cmd.ExecuteNonQuery();  //count就是受影响的行数,如果count>0说明执行成功,如果=0说明没有成功.
                mycon.Close();
            }

            catch (System.Exception ex)
            {

            }
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

        #region 获取重定向网址
        public static string GetRedirectUrl(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "HEAD";
            req.AllowAutoRedirect = false;
            HttpWebResponse myResp = (HttpWebResponse)req.GetResponse();
            bool flag = myResp.StatusCode == HttpStatusCode.Found;
            if (flag)
            {
                url = myResp.GetResponseHeader("Location");
            }
            return url;
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
                request.Proxy = null;
                request.Timeout = 5000;
                //WebHeaderCollection headers = request.Headers;
                //headers.Add("version:TYC-XCX-WX");
                //request.ContentType = "application/x-www-form-urlencoded";
                request.ContentType = "application/json";
                request.ContentLength = (long)Encoding.UTF8.GetBytes(postData).Length;
                request.Headers.Add("Accept-Encoding", "gzip");
                request.AllowAutoRedirect = false;
                request.KeepAlive = true;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36";
                request.Headers.Add("Cookie", COOKIE);
                request.Referer = url;
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

        #region 请求异域下载

        public static void yiyu_download(string fileurl,string filename)
        {

            try
            {
                string url = "http://8.153.165.134/download.aspx";
                string postdata = "{\"fileurl\":\""+fileurl+"\",\"filename\":\""+filename+"\"}";
                PostUrlDefault(url,postdata,"");
            }
            catch (Exception)
            {

               
            }

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

        public static string cloud_getfileid(string id,string cookie)
        {
            try
            {
             
                string url = "https://cloud.189.cn/api/open/file/searchFiles.action?noCache=0.942218720555657&folderId=-11&pageSize=1&pageNum=1&filename="+id+"&recursive=1&iconOption=5&mediaType=0&orderBy=lastOpTime&descending=true";
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