using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace xueke_api
{
    public partial class api : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 设置响应头，支持跨域
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            Response.ContentType = "application/json";

            try
            {

                // 获取method参数
                string method = Request.QueryString["method"].Trim();
                string key = Request.QueryString["key"].Trim();
               
                

                if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(method))
                {
                    Response.Write("{\"status\":\"0\",\"msg\":\"服务异常，参数为空\"}");
                    return;
                }
                // 根据method参数调用对应的方法
                switch (method.ToLower())
                {

                    case "getkey":
                        getkey(key);
                        break;
                    case "getfile":
                        getfile(key);
                        break;
                    case "getallkeys":
                        GetAllKeys();
                        break;

                    default:
                        Response.Write("{\"status\":\"0\",\"msg\":\"服务异常，无效的请求\"}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.Write("{\"status\":\"0\",\"msg\":\"服务异常，请联系客服\"}"+ex.ToString());
            }
        }



        public void getfile(string key)
        {

            string link = Request.QueryString["link"].Trim();
            string fileid = Regex.Match(link, @"\d{6,}").Groups[0].Value;
            if (fileid == "")
            {
                Response.Write("{\"status\":\"0\",\"msg\":\"资料地址错误，请复制一篇资料网址，而不是专辑\"}");
                return;
            }
            DatabaseHelper db = new DatabaseHelper();
            DataTable dt = db.GetKey(key);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                string cishu = row["cishu"].ToString().Trim();
                string extime = row["extime"].ToString().Trim();
                string day = row["day"].ToString().Trim();
                string isvip = row["isvip"].ToString().Trim();


                if (Convert.ToInt32(cishu) <= 0)
                {
                    Response.Write("{\"status\":\"0\",\"msg\":\"剩余次数不足，请联系客服\"}");
                    return;
                }

                if (extime != "")
                {
                    if (Convert.ToDateTime(extime) < DateTime.Now)
                    {
                        Response.Write("{\"status\":\"0\",\"msg\":\"当前购买的秘钥已过期！\"}");
                        return;
                    }

                }

                //第一次筛选中职
                if (isvip == "0" || isvip == "")
                {
                    if (link.Contains("zhijiao"))
                    {
                        Response.Write("{\"status\":\"0\",\"msg\":\"当前购买的权限不能下载教辅及中职资料，请拍下对应选项的秘钥！（当前账户可下：精品、特供、普通、≤5储值的资料）\"}");
                        return;
                    }
                }

                //第一次下载
                if (extime == "")
                {
                    extime = DateTime.Now.AddDays(Convert.ToInt32(day)).ToString("yyyy-MM-dd HH:mm:ss");
                    db.editetime(key, extime);  //第一次登录

                }

                string url = "https://search.zxxk.com/api-kernel/v1/search-site/document/list?pageType=2&keyword="+fileid+"&page=1&pageIndex=1&pageSize=30&enableEBR=true&useCTR=false";
                string html = method.GetUrl(url);
                string price = Regex.Match(html, @"""displayPrice"":""([\s\S]*?)""").Groups[1].Value.Replace("储值", "").Trim();
                string title = Regex.Match(html, @"""title"":""([\s\S]*?)""").Groups[1].Value.Trim();
                string commercialLevelName = Regex.Match(html, @"""commercialLevelName"":""([\s\S]*?)""").Groups[1].Value.Trim();
                string stageId = Regex.Match(html, @"""stageId"":([\s\S]*?),").Groups[1].Value.Trim();

                if(title==""&&price=="")
                {
                    Response.Write("{\"status\":\"0\",\"msg\":\"请求不存在\"}");
                    return;
                }
                    
                if (commercialLevelName == "教辅" || stageId == "6")
                {
                    //账号不符合的跳过 //1202","普通""1203","精品""1204","特供" "1205","教辅" stageId=="6"中职
                    if (isvip == "0" || isvip == "")
                    {
                        Response.Write("{\"status\":\"0\",\"msg\":\"当前购买的权限不能下载教辅及中职，请拍对应选项（当前账户可下：精品、特供、普通、≤5储值的资料）\"}");
                        return;
                    }
                    //教辅的不超过10块
                    if (price != "")
                    {
                        if (Convert.ToDouble(price) > 5)
                        {
                            Response.Write("{\"status\":\"0\",\"msg\":\"下载大于5储值的教辅中职，请咨询客服\"}");
                            return;
                        }
                    }
                }

                //普通的不超过20
                if (price != "")
                {
                    if (Convert.ToDouble(price) > 10)
                    {
                        Response.Write("{\"status\":\"0\",\"msg\":\"当前购买的权限无法下载价格过高资料，请咨询客服\"}");
                        return;
                    }
                }
                int cishu_new = Convert.ToInt32(cishu) - 1;

                if (fileid.Substring(0, 3) == "541" || fileid.Substring(0, 3) == "542")
                {
                   bool status= download_189pan(fileid, cishu_new, extime, title);
                    if(status)
                     {
                        db.editecishu(key); //下载成功  减去次数
                      db.adddownlog(key, link, isvip, cishu, day, price, commercialLevelName, stageId); //下载成功  添加下载记录

                        return;
                    } 
                }
               bool ss= download_zxxk(fileid,cishu_new, extime);
                if (ss)
                {
                    db.editecishu(key); //下载成功  减去次数
                  db.adddownlog(key, link, isvip, cishu, day, price, commercialLevelName, stageId); //下载成功  添加下载记录

                }
                else
                {
                    Response.Write("{\"status\":\"0\",\"msg\":\"登录失效，请联系客服！\"}");
                }
               
            }

            else
            {
                Response.Write("{\"status\":\"0\",\"msg\":\"秘钥错误或不存在，请联系客服！\"}");


            }

        }



        public bool download_189pan(string fileid, int cishu_new, string extime, string filename)
        {
            var downurl = method.cloud189_download(fileid).Trim();

            if (downurl != "")
            {
                Response.Write("{\"status\":\"1\",  \"cishu\":\"" + cishu_new + "\", \"extime\":\"" + extime + "\",   \"filename\":\"" + filename + "\",\"fileurl\":\"" + downurl + "\",\"msg\":\"下载成功,请查看浏览器下载列表\"}");

                return true;
            }
            else
            {
                return false;
            }
        }

        public bool download_zxxk(string fileid,int cishu_new, string extime)
        {
            string url = "http://1.117.225.132/xk.aspx?method=kd0ph8fynbhj&id=" + fileid;

            string html = method.GetUrl(url);
            string fileurl = Regex.Match(html, @"""fileUrl"":""([\s\S]*?)""").Groups[1].Value.Trim();
            string filename = Regex.Match(html, @"""fileName"":""([\s\S]*?)"",").Groups[1].Value.Trim();

            //原网址下载
            string protocol = Request.Url.Scheme; // "http" 或 "https"
            string host = Request.Url.Host;       // 域名或IP
            int port = Request.Url.Port;
            string suiji = method.GenerateRandomString(10);

            string jiamifilekey = method.Base64Encode(Encoding.GetEncoding("utf-8"), fileurl).Replace("a", suiji);
            string jiamifileurl = protocol + "://" + host + ":" + port + "/download.aspx?filekey=" + jiamifilekey + "&suiji=" + suiji;

            if(fileurl!="")
            {
                Response.Write("{\"status\":\"1\",  \"cishu\":\"" + cishu_new + "\", \"extime\":\"" + extime + "\",   \"filename\":\"" + filename + "\",\"fileurl\":\"" + jiamifileurl + "\",\"msg\":\"下载成功,请查看浏览器下载列表\"}");
                return true;
            }
            else
            {

                return false;   
            }
        }

        private void getkey(string key)
        {

            DatabaseHelper db = new DatabaseHelper();
            DataTable dt = db.GetKey(key);
            ReturnData(dt);
        }


        private void GetAllKeys()
        {
            DatabaseHelper db = new DatabaseHelper();
            DataTable dt = db.GetAllKeys();
            ReturnData(dt);
        }



        // 返回JSON格式的数据
        private void ReturnData(DataTable dt)
        {
            var result = new
            {
                success = true,
                data = DataTableToDictionary(dt),
                count = dt.Rows.Count
            };

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string json = serializer.Serialize(result);
            Response.Write(json);
        }

        // 将DataTable转换为字典列表
        private List<Dictionary<string, object>> DataTableToDictionary(DataTable dt)
        {
            var list = new List<Dictionary<string, object>>();

            foreach (DataRow row in dt.Rows)
            {
                var dict = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    dict[col.ColumnName] = row[col] == DBNull.Value ? null : row[col];
                }
                list.Add(dict);
            }

            return list;
        }


















    }
}