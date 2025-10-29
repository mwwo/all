using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.WebRequestMethods;

namespace 视频号web端
{
    public partial class download : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                if (Request.HttpMethod == "POST" && Request.ContentType.Contains("application/json"))
                {

                    using (var reader = new StreamReader(Request.InputStream))
                    {


                        // 重置流位置（避免流已被读取导致内容为空）
                        Request.InputStream.Position = 0;
                        string json = reader.ReadToEnd();
                        JObject jObject = JObject.Parse(json);
                        string fileurl = jObject["fileurl"]?.ToString();
                        string filename = jObject["filename"]?.ToString();
                        string oss = Server.MapPath("~/") + "oss\\";
                        downloadFile(fileurl, oss, filename);
                        //Response.Write("{\"status\":\"0\",\"msg\":\"" + aa + "\"}");
                    }

                }


                else
                {
                    Response.Write("{\"status\":\"0\",\"msg\":\"网址输入有误\"}");
                }

            }
            catch (Exception ex)
            {
                Response.Write("{\"status\":\"0\",\"msg\":\"异常\"}" + ex.ToString());
            }
        }



        #region 下载文件
        public static void downloadFile(string URLAddress, string subPath, string name)
        {
            try
            {
                string path = Directory.GetCurrentDirectory();
                WebClient client = new WebClient();
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/78.0.3904.108 Safari/537.36");
                // client.Headers.Add("Cookie", COOKIE);
                client.Headers.Add("Referer", "https://m.mm131.net/chemo/89_5.html");
                bool flag = !Directory.Exists(subPath);
                if (flag)
                {
                    Directory.CreateDirectory(subPath);
                }
                client.DownloadFile(URLAddress, subPath + "\\" + name);
                //return "success";
            }
            catch (WebException ex)
            {
                //return ex.ToString()+URLAddress;
            }


        }
        #endregion
    }
}