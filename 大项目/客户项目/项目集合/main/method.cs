﻿using CsharpHttpHelper;
using CsharpHttpHelper.Enum;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotRas;
using System.ComponentModel;

namespace main
{
    class method
    {
        /// <summary>
     /// 创建或更新一个PPPOE连接(指定PPPOE名称)
     /// </summary>
        static void CreateOrUpdatePPPOE(string updatePPPOEname)
        {
            RasDialer dialer = new RasDialer();
            RasPhoneBook allUsersPhoneBook = new RasPhoneBook();
            string path = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
            allUsersPhoneBook.Open(path);
            // 如果已经该名称的PPPOE已经存在，则更新这个PPPOE服务器地址
            if (allUsersPhoneBook.Entries.Contains(updatePPPOEname))
            {
                allUsersPhoneBook.Entries[updatePPPOEname].PhoneNumber = " ";
                // 不管当前PPPOE是否连接，服务器地址的更新总能成功，如果正在连接，则需要PPPOE重启后才能起作用
                allUsersPhoneBook.Entries[updatePPPOEname].Update();
            }
            // 创建一个新PPPOE
            else
            {
                string adds = string.Empty;
                ReadOnlyCollection<RasDevice> readOnlyCollection = RasDevice.GetDevices();
                //                foreach (var col in readOnlyCollection)
                //                {
                //                    adds += col.Name + ":" + col.DeviceType.ToString() + "|||";
                //                }
                //                _log.Info("Devices are : " + adds);
                // Find the device that will be used to dial the connection.
                RasDevice device = RasDevice.GetDevices().Where(o => o.DeviceType == RasDeviceType.PPPoE).First();
                RasEntry entry = RasEntry.CreateBroadbandEntry(updatePPPOEname, device);    //建立宽带连接Entry
                entry.PhoneNumber = " ";
                allUsersPhoneBook.Entries.Add(entry);
            }
        }

        /// <summary>
        /// 断开 宽带连接
        /// </summary>
        public static void Disconnect()
        {
            ReadOnlyCollection<RasConnection> conList = RasConnection.GetActiveConnections();
            foreach (RasConnection con in conList)
            {
                con.HangUp();
            }
        }

        /// <summary>
        /// 宽带连接，成功返回true,失败返回 false
        /// </summary>
        /// <param name="PPPOEname">宽带连接名称</param>
        /// <param name="username">宽带账号</param>
        /// <param name="password">宽带密码</param>
        /// <returns></returns>
        public static bool Connect(string PPPOEname, string username, string password)
        {
            try
            {
                CreateOrUpdatePPPOE(PPPOEname);
                using (RasDialer dialer = new RasDialer())
                {
                    dialer.EntryName = PPPOEname;
                    dialer.AllowUseStoredCredentials = true;
                    dialer.Timeout = 1000;
                    dialer.PhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
                    dialer.Credentials = new NetworkCredential(username, password);
                    dialer.Dial();
                    return true;
                }
            }
            catch (RasException re)
            {
                MessageBox.Show(re.ErrorCode + " " + re.Message);
                return false;
            }
        }



        public static void sendEmail()
        {
          
                string host = "smtp.163.com";// 邮件服务器smtp.163.com表示网易邮箱服务器    
                string userName = "15764226619@163.com";// 发送端账号   
                string password = "password";// 发送端密码(这个客户端重置后的密码)




                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式    
                client.Host = host;//邮件服务器
                client.UseDefaultCredentials = true;
                client.Credentials = new System.Net.NetworkCredential(userName, password);//用户名、密码

                //////////////////////////////////////
                string strfrom = userName;
                string strto = "1073689549@qq.com";
               // string strcc = "2605625733@qq.com";//抄送


                string subject = "这是测试邮件标题5";//邮件的主题             
                string body = "测试邮件内容5";//发送的邮件正文  

                System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
                msg.From = new MailAddress(strfrom, "xyf");
                msg.To.Add(strto);
               // msg.CC.Add(strcc);

                msg.Subject = subject;//邮件标题   
                msg.Body = body;//邮件内容   
                msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
                msg.IsBodyHtml = true;//是否是HTML邮件   
                msg.Priority = MailPriority.High;//邮件优先级   


                try
                {
                    client.Send(msg);
                    Console.WriteLine("发送成功");
                }
                catch (System.Net.Mail.SmtpException ex)
                {
                    Console.WriteLine(ex.Message, "发送邮件出错");
                }
            
        }

            #region GET请求
            /// <summary>
            /// GET请求
            /// </summary>
            /// <param name="Url">网址</param>
            /// <returns></returns>
            public static string GetUrl(string Url, string charset)
            {
                try
            {
                string COOKIE = "";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);  //创建一个链接

                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.108 Safari/537.36";
                request.Referer = "https://shop145672826.taobao.com/index.htm?spm=2013.1.w5002-18853258787.2.2fc7258aIrZpr9";
                request.AllowAutoRedirect = true;
                request.Headers.Add("Cookie", COOKIE);
                request.KeepAlive = true;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;  //获取反馈
                
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(charset)); //reader.ReadToEnd() 表示取得网页的源码流 需要引用 using  IO

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

            #region GET使用代理IP请求
            /// <summary>
            /// GET请求
            /// </summary>
            /// <param name="Url">网址</param>
            /// <returns></returns>
            public static string GetUrlwithIP(string Url, string ip)
            {
                try
                {


                    string COOKIE = "BIDUPSID=6F7A1FB331DE7AD89632403D26928572; PSTM=1546067770; MCITY=-309%3A; BAIDUID=6F7A1FB331DE7AD89632403D26928572:SL=0:NR=10:FG=1; BDORZ=FFFB88E999055A3F8A630C64834BD6D0; __guid=171760688.3766717285699673600.1547615234549.5005; monitor_count=34";
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);  //创建一个链接
                    WebProxy proxy = new WebProxy(ip);
                    request.Proxy = proxy;
                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/63.0.3239.132 Safari/537.36";
                    request.AllowAutoRedirect = true;
                    request.Headers.Add("Cookie", COOKIE);
                    request.KeepAlive = false;
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;  //获取反馈
                request.Timeout = 5000;
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312")); //reader.ReadToEnd() 表示取得网页的源码流 需要引用 using  IO

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

        /// <summary>
        /// 苏菲论坛获取URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="COOKIE"></param>
        /// <returns></returns>
        /// 
        public static string gethtml(string url, string COOKIE,string charset)
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = url,
                Method = "GET",//URL     可选项 默认为Get  
                Encoding = Encoding.GetEncoding(charset),
                Timeout = 100000,//连接超时时间     可选项默认为100000  
                Allowautoredirect = true,
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000  

                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写  
                Cookie = COOKIE,
                UserAgent = "Mozilla/5.0 (iPhone; CPU iPhone OS 12_3_1 like Mac OS X) AppleWebKit/605.1.15 (KHTML, like Gecko) Version/12.1.1 Mobile/15E148 Safari/604.1",//用户的浏览器类型，版本，操作系统     可选项有默认值  
                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值  
                ContentType = "text/html",//返回类型    可选项有默认值  
                Referer = "",//来源URL     可选项  
                                                    //Allowautoredirect = False,//是否根据３０１跳转     可选项  
                                                    //AutoRedirectCookie = False,//是否自动处理Cookie     可选项  
                                                    //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数  
                                                    //Connectionlimit = 1024,//最大连接数     可选项 默认为1024  
                Postdata = "",//Post数据     可选项GET时不需要写  
                              //ProxyIp = "192.168.1.105：2020",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数  
                              //ProxyPwd = "123456",//代理服务器密码     可选项  
                              //ProxyUserName = "administrator",//代理服务器账户名     可选项  
                ResultType = ResultType.String,//返回数据类型，是Byte还是String  
            };
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            return html;

        }

            #region ！！！！如果之前的请求获取不到源码就用这个去获取,非常重要！！！！
        public static string GetHtmlSource(string url,string charset)
            {
                try
                {
                    Uri uri = new Uri(url);
                    HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(uri);
                    myReq.UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/71.0.3578.98 Safari/537.36";
                    myReq.Accept = "*/*";
                    myReq.KeepAlive = true;
                    myReq.Headers.Add("Accept-Language", "zh-cn,en-us;q=0.5");
                    HttpWebResponse result = (HttpWebResponse)myReq.GetResponse();
                    Stream receviceStream = result.GetResponseStream();
                    StreamReader readerOfStream = new StreamReader(receviceStream, System.Text.Encoding.GetEncoding(charset));
                    string strHTML = readerOfStream.ReadToEnd();
                    readerOfStream.Close();
                    receviceStream.Close();
                    result.Close();

                    return strHTML;
                }
                catch (Exception ex)
                {
                    throw new Exception("采集指定网址异常，" + ex.Message);
                }
            }
            #endregion

            #region GET请求带COOKIE
            /// <summary>
            /// GET请求带COOKIE
            /// </summary>
            /// <param name="Url">网址</param>
            /// <returns></returns>
            public static string GetUrlWithCookie(string Url, string COOKIE,string charset)
            {
                try
                {

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);  //创建一个链接

                    request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.108 Safari/537.36";

                    request.Headers.Add("Cookie", COOKIE);
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;  //获取反馈

                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(charset)); //reader.ReadToEnd() 表示取得网页的源码流 需要引用 using  IO

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

            #region 获取数据库58城市名称
            /// <summary>
            /// 获取数据库美团城市名称
            /// </summary>
            /// <param name="cob">数据绑定的下拉框</param>
            public static void get58CityName(ComboBox cob)
            {
                ArrayList list = new ArrayList();
                try
                {
                    string constr = "Host =116.62.62.62;Database=citys;Username=root;Password=zhoukaige";
                    string str = "SELECT cityname from city_58 ";
                    MySqlDataAdapter da = new MySqlDataAdapter(str, constr);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    DataTable dt = ds.Tables[0];
                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(dr[0].ToString().Trim());
                    }
                }
                catch (MySqlException ee)
                {
                    MessageBox.Show(ee.Message.ToString());
                }
                cob.DataSource = list;

            }
            #endregion

            #region  58获取数据库中城市名称对应的拼音

            public static string Get58pinyin(string city)
            {

                try
                {



                    string constr = "Host =116.62.62.62;Database=citys;Username=root;Password=zhoukaige";
                    MySqlConnection mycon = new MySqlConnection(constr);
                    mycon.Open();

                    MySqlCommand cmd = new MySqlCommand("select citycode from city_58 where cityname='" + city + "'  ", mycon);         //SQL语句读取textbox的值'"+skinTextBox1.Text+"'


                    MySqlDataReader reader = cmd.ExecuteReader();  //读取数据库数据信息，这个方法不需要绑定资源

                    reader.Read();

                    string citypinyin = reader["citycode"].ToString().Trim();
                    mycon.Close();
                    reader.Close();
                    return citypinyin;


                }

                catch (System.Exception ex)
                {
                    return ex.ToString();
                }


            }

            #endregion

            #region listview转datable
            /// <summary>
            /// listview转datable
            /// </summary>
            /// <param name="lv"></param>
            /// <returns></returns>
            public static DataTable listViewToDataTable(ListView lv)
            {
                int i, j;
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Clear();
                dt.Columns.Clear();
                //生成DataTable列头
                for (i = 0; i < lv.Columns.Count; i++)
                {
                    dt.Columns.Add(lv.Columns[i].Text.Trim(), typeof(String));
                }
            //每行内容
            for (i = 0; i < lv.Items.Count; i++)
            {
             

                    dr = dt.NewRow();
                    for (j = 0; j < lv.Columns.Count; j++)
                    {

                         dr[j] = lv.Items[i].SubItems[j].Text.Trim();
                     

                    }
                    dt.Rows.Add(dr);
                

            }

            return dt;
            }
            #endregion

            #region listview转多个datable
            /// <summary>
            /// listview转datable
            /// </summary>
            /// <param name="lv"></param>
            /// /// <param name="eachs">每次导出个数</param>
            /// <returns></returns>
            public static DataTable listViewToDataTable1(ListView lv, int biaos)
            {
                int i, j;
                DataTable dt = new DataTable();
                DataRow dr;
                dt.Clear();
                dt.Columns.Clear();


                //生成DataTable列头
                for (i = 0; i < lv.Columns.Count; i++)
                {
                    dt.Columns.Add(lv.Columns[i].Text.Trim(), typeof(String));
                }
                //每行内容
                for (i = 0; i < lv.Items.Count; i++)
                {
                    dr = dt.NewRow();
                    for (j = 0; j < lv.Columns.Count; j++)
                    {
                        dr[j] = lv.Items[i].SubItems[j].Text.Trim();
                    }
                    dt.Rows.Add(dr);
                }

                return dt;
            }
        #endregion

        

            #region NPOI导出表格
        public static int DataTableToExcel(DataTable data, string sheetName, bool isColumnWritten)
            {
                int i = 0;
                int j = 0;
                int count = 0;
                ISheet sheet = null;
                IWorkbook workbook = null;
                FileStream fs = null;
                // bool disposed;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "xlsx|*.xls|xlsx|*.xlsx";
                sfd.Title = "Excel文件导出";
                string fileName = "";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    fileName = sfd.FileName;
                }
                else
                    return -1;

                fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook();
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook();

                try
                {
                    if (workbook != null)
                    {
                        sheet = workbook.CreateSheet(sheetName);
                        ICellStyle style = workbook.CreateCellStyle();
                        style.FillPattern = FillPattern.SolidForeground;

                    }
                    else
                    {
                        return -1;
                    }

                    if (isColumnWritten == true) //写入DataTable的列名
                    {
                        IRow row = sheet.CreateRow(0);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);

                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }

                    for (i = 0; i < data.Rows.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                        }
                        ++count;
                    }
                    workbook.Write(fs); //写入到excel
                    workbook.Close();
                    fs.Close();
                    System.Diagnostics.Process[] Proc = System.Diagnostics.Process.GetProcessesByName("");
                    MessageBox.Show("数据导出完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.Message);
                    return -1;
                }
        }

        #endregion

               #region NPOI自动导出导出表格
        public static int DataTableToExcel2(DataTable data, string sheetName, bool isColumnWritten,string title)
        {
     
            string path = AppDomain.CurrentDomain.BaseDirectory;
            //string time = DateTime.Now.ToString("F").Replace(" ", "").Replace(":","");
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            IWorkbook workbook = null;
            FileStream fs = null;

            string fileName = path + title + "符合条件.xls";
            // string fileName = path + title+time + "符合条件.xls";

            fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                if (workbook != null)
                {
                    sheet = workbook.CreateSheet(sheetName);
                    ICellStyle style = workbook.CreateCellStyle();
                    style.FillPattern = FillPattern.SolidForeground;

                }
                else
                {
                    return -1;
                }

                if (isColumnWritten == true) //写入DataTable的列名
                {
                    IRow row = sheet.CreateRow(0);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);

                    }
                    count = 1;
                }
                else
                {
                    count = 0;
                }

                for (i = 0; i < data.Rows.Count; ++i)
                {
                    IRow row = sheet.CreateRow(count);
                    for (j = 0; j < data.Columns.Count; ++j)
                    {
                        row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                    }
                    ++count;
                }
                workbook.Write(fs); //写入到excel
                workbook.Close();
                fs.Close();
                System.Diagnostics.Process[] Proc = System.Diagnostics.Process.GetProcessesByName("");
                MessageBox.Show("数据导出完成！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }
        }

        #endregion

             #region  listview导出文本TXT
        public static void ListviewToTxt(ListView listview)
        {
            if (listview.Items.Count == 0)
            {
                MessageBox.Show("列表为空!");
            }
            else
            {
                List<string> list = new List<string>();
                foreach (ListViewItem item in listview.Items)
                {
                    string temp = item.SubItems[1].Text;
                    string temp1 = item.SubItems[2].Text;
                    list.Add(temp+ "-----"+temp1);
                }
                Thread thexp = new Thread(() => export(list)) { IsBackground = true };
                thexp.Start();
            }
        }


        private static void export(List<string> list)
        {
             string path = AppDomain.CurrentDomain.BaseDirectory + "url_" + Guid.NewGuid().ToString() + ".txt";
            
            StringBuilder sb = new StringBuilder();
            foreach (string tel in list)
            {
                sb.AppendLine(tel);
            }
            System.IO.File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
            MessageBox.Show("文件导出成功!文件地址:" + path);
        }



        #endregion

             #region 下载文件
        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="URLAddress">图片地址</param>
        /// <param name="subPath">图片所在文件夹</param>
        /// <param name="name">图片名称</param>
        public static void downloadFile(string URLAddress, string subPath, string name)
            {
                string path = System.IO.Directory.GetCurrentDirectory();

                WebClient client = new WebClient();

                if (false == System.IO.Directory.Exists(subPath))
                {
                    //创建pic文件夹
                    System.IO.Directory.CreateDirectory(subPath);
                }
                client.DownloadFile(URLAddress, subPath + "\\" + name);
            }

            #endregion

             #region 获取Mac地址
            /// <summary>
            /// 获取Mac地址
            /// </summary>
            /// <returns></returns>
            public static string GetMacAddress()
            {
                try
                {
                    string strMac = string.Empty;
                    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        if ((bool)mo["IPEnabled"] == true)
                        {
                            strMac = mo["MacAddress"].ToString();
                        }
                    }
                    moc = null;
                    mc = null;
                    return strMac;
                }
                catch
                {
                    return "unknown";
                }
            }

        #endregion




        /// <summary>
        /// 获取字符串的首字母
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>

         #region 获取公网IP
        public static string GetIP()
        {
            using (var webClient = new WebClient())
            {
                try
                {
                    webClient.Credentials = CredentialCache.DefaultCredentials;
                    byte[] pageDate = webClient.DownloadData("http://pv.sohu.com/cityjson?ie=utf-8");
                    String ip = Encoding.UTF8.GetString(pageDate);
                    webClient.Dispose();

                    Match rebool = Regex.Match(ip, @"\d{2,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                    return rebool.Value;
                }
                catch (Exception e)
                {
                    return e.ToString();
                }

            }
        }

        #endregion



          #region 获取IP地区
        /// <summary>
        /// 获取IP地区
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
            {
                try
                {

                    string html = GetUrl("https://ip.cn/", "utf-8");

                    MatchCollection match = Regex.Matches(html, @"<code>([\s\S]*?)</code>", RegexOptions.IgnoreCase | RegexOptions.Multiline);


                    return match[0].Groups[1].Value.Trim();

                }

                catch (Exception ex)
                {
                    ex.ToString();

                    return "获取IP错误";
                }

            }

        #endregion


        public enum IeVersion
        {
            IE7 = 7,
            IE8 = 8,
            IE9 = 9,
            IE10 = 10,
            IE11 = 11
        };

        /// <summary>  
        /// 修改注册表信息来兼容当前程序
        /// </summary>  
        public static void SetWebBrowserFeatures(IeVersion ieVersion)
        {
            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime) return;
            //获取程序及名称  
            string AppName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);

            //得到浏览器的模式的值  
            UInt32 ieMode = GeoEmulationModee((int)ieVersion);

            string featureControlRegKey = @"HKEY_CURRENT_USER\Software\Microsoft\Internet Explorer\Main\FeatureControl\";
            //设置浏览器对应用程序（appName）以什么模式（ieMode）运行  

            Registry.SetValue(featureControlRegKey + "FEATURE_BROWSER_EMULATION", AppName, ieMode, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", AppName, 1, RegistryValueKind.DWord);
            Registry.SetValue(featureControlRegKey + "FEATURE_AJAX_CONNECTIONEVENTS", AppName, 1, RegistryValueKind.DWord);
            Registry.SetValue(featureControlRegKey + "FEATURE_GPU_RENDERING", AppName, 1, RegistryValueKind.DWord);
            Registry.SetValue(featureControlRegKey + "FEATURE_WEBOC_DOCUMENT_ZOOM", AppName, 1, RegistryValueKind.DWord);
            Registry.SetValue(featureControlRegKey + "FEATURE_NINPUT_LEGACYMODE", AppName, 0, RegistryValueKind.DWord);
        }

        /// <summary>  
        /// 通过版本得到浏览器模式的值  
        /// </summary>  
        /// <param name="browserVersion"></param>  
        /// <returns></returns>  
        private static UInt32 GeoEmulationModee(int browserVersion)
        {
            UInt32 mode = 11000; // Internet Explorer 11. Webpages containing standards-based !DOCTYPE directives are displayed in IE11 Standards mode.   
            switch (browserVersion)
            {
                case 7:
                    mode = 7000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE7 Standards mode.   
                    break;
                case 8:
                    mode = 8000; // Webpages containing standards-based !DOCTYPE directives are displayed in IE8 mode.   
                    break;
                case 9:
                    mode = 9000; // Internet Explorer 9. Webpages containing standards-based !DOCTYPE directives are displayed in IE9 mode.                      
                    break;
                case 10:
                    mode = 10000; // Internet Explorer 10.  
                    break;
                case 11:
                    mode = 11000; // Internet Explorer 11  
                    break;
            }
            return mode;
        }


        #region datagriview转datatable
        public static DataTable DgvToTable(DataGridView dgv)
            {
                DataTable dt = new DataTable();
                // 列强制转换
                for (int count = 0; count < dgv.Columns.Count; count++)
                {
                    DataColumn dc = new DataColumn(dgv.Columns[count].Name.ToString());
                    dt.Columns.Add(dc);
                }
                // 循环行
                for (int count = 0; count < dgv.Rows.Count; count++)
                {
                    DataRow dr = dt.NewRow();
                    for (int countsub = 0; countsub < dgv.Columns.Count; countsub++)
                    {
                        dr[countsub] = Convert.ToString(dgv.Rows[count].Cells[countsub].Value);
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }

            #endregion


            #region 导出文本文件
            /// <summary>
            /// 导出文本文件
            /// </summary>
            /// <param name="dgv">需要导出的表格</param>
            public static void Txt(DataGridView dgv) //另存新档按钮   导出成.txt文件
            {

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "txt文件 (*.txt)|*.txt";
                //saveFileDialog.Filter = "词库文件 (*.ys)|*.ys";
                saveFileDialog.FilterIndex = 0;
                saveFileDialog.FileName = "采集器手机号码";
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.CreatePrompt = true;
                saveFileDialog.Title = "导出txt文件到";
                //saveFileDialog.ShowDialog();
                if (saveFileDialog.ShowDialog() != DialogResult.Cancel)
                {
                    Stream myStream;
                    myStream = saveFileDialog.OpenFile();
                    StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
                    try
                    {
                        //写内容
                        for (int j = 0; j < dgv.Rows.Count; j++)
                        {
                            string tempStr = string.Empty;
                            tempStr += dgv.Rows[j].Cells[2].Value + "\r\n";  //导出第二列
                            sw.Write(tempStr);
                        }
                        sw.Close();
                        myStream.Close();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    finally
                    {
                        sw.Close();
                        myStream.Close();
                    }
                }
            }

            #endregion

            /// <summary>
            /// unicode转字符串
            /// </summary>
            /// <param name="unicode"></param>
            /// <returns></returns>
            public static string UnicodeToStr(string str)
            {
                MatchCollection mc = Regex.Matches(str, "([\\w]+)|(\\\\u([\\w]{4}))");
                if (mc != null && mc.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (Match m2 in mc)
                    {
                        string v = m2.Value;
                        if (v.StartsWith("\\"))
                        {
                            string word = v.Substring(2);
                            byte[] codes = new byte[2];
                            int code = Convert.ToInt32(word.Substring(0, 2), 16);
                            int code2 = Convert.ToInt32(word.Substring(2), 16);
                            codes[0] = (byte)code2;
                            codes[1] = (byte)code;
                            sb.Append(Encoding.Unicode.GetString(codes));
                        }
                        else
                        {
                            sb.Append(v);
                        }
                    }
                    return sb.ToString();
                }
                else
                {
                    return str;
                }


            }

            #region 注册码随机生成函数
            /// <summary>
            /// 注册码随机生成函数
            /// </summary>
            /// <returns></returns>
            public static string Random(string mac)

            {


                //string[] array = {"A","B","C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"};
                string Hour = DateTime.Now.Hour.ToString();                    //获取当前小时
                string Hour2 = Math.Pow(Convert.ToDouble(Hour), 2).ToString();  //获取当前小时的平方
                                                                                //string zimu = array[DateTime.Now.Hour];                        //获取当前小时作为数组索引的字母
                string key = Hour + "1475" + (Hour2) + mac + "2479" + Hour;

                return key;



            }


            #endregion

            #region POST请求
            /// <summary>
            /// POST请求
            /// </summary>
            /// <param name="url">请求地址</param>
            /// <param name="postData">发送的数据包</param>
            /// <param name="COOKIE">cookie</param>
            /// <param name="charset">编码格式</param>
            /// <returns></returns>
            public static string PostUrl(string url, string postData, string COOKIE, string charset)
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "Post";
                request.ContentType = "application/x-www-form-urlencoded";
                //request.ContentType = "application/json";
                request.ContentLength = postData.Length;
                request.AllowAutoRedirect = true;
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.1.4322)";
                request.Headers.Add("Cookie", COOKIE);

                StreamWriter sw = new StreamWriter(request.GetRequestStream());
                sw.Write(postData);
                sw.Flush();


                WebResponse response = request.GetResponse();
                Stream s = response.GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.GetEncoding(charset));
                string html = sr.ReadToEnd();

                sw.Dispose();
                sw.Close();
                sr.Dispose();
                sr.Close();
                s.Dispose();
                s.Close();
                return html;
            }

        #endregion


        public static void expotTxt(ListView lv1)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件路径";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
               
                    StringBuilder sb = new StringBuilder();
                    foreach (ListViewItem item in lv1.Items)
                    {          
                            List<string> list = new List<string>();
                            string temp = item.SubItems[1].Text;
                    string temp1 = item.SubItems[2].Text;
                    string temp2 = item.SubItems[3].Text;
                    list.Add(temp+"-----"+temp1 + "-----" + temp2);
                            foreach (string tel in list)
                            {
                                sb.AppendLine(tel);
                            }

                    string path = "";
                  
                     path = dialog.SelectedPath + "\\导出结果.txt";
                    
                    System.IO.File.WriteAllText(path, sb.ToString(), Encoding.UTF8);

                }
                MessageBox.Show("导出完成");
            }

        }











    }


    }





