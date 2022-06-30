﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPnetWeb应用程序空
{
    public partial class index627_admin : System.Web.UI.Page
    {
        //文件上传按钮click事件  
        protected void btnFileUpload_Click(object sender, EventArgs e)
        {

            if (FileUpLoad1.HasFile)
            {
                //判断文件是否小于10Mb  
                if (FileUpLoad1.PostedFile.ContentLength < 10485760)
                {
                    try
                    {
                        //上传文件并指定上传目录的路径  
                        //FileUpLoad1.PostedFile.SaveAs(Server.MapPath("~/image627/")
                        //    + FileUpLoad1.FileName);
                        /*注意->这里为什么不是:FileUpLoad1.PostedFile.FileName 
                        * 而是:FileUpLoad1.FileName? 
                        * 前者是获得客户端完整限定(客户端完整路径)名称 
                        * 后者FileUpLoad1.FileName只获得文件名. 
                        */

                        //当然上传语句也可以这样写(貌似废话):  
                        //FileUpLoad1.SaveAs(@"D:"+FileUpLoad1.FileName);  

                        FileUpLoad1.PostedFile.SaveAs(Server.MapPath("~/image627/")
                           + "a"+Request["changci"] + Path.GetExtension(FileUpLoad1.FileName));
                       


                        lblMessage.Text = "上传成功!";
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "出现异常,无法上传!";
                        //lblMessage.Text += ex.Message;  
                    }

                }
                else
                {
                    lblMessage.Text = "上传文件不能大于10MB!";
                }
            }
            else
            {
                lblMessage.Text = "尚未选择文件!";
            }
        }


        /// <summary>
        /// 读取本地图片
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Bitmap ReadImageFile(string path)
        {
            if (!File.Exists(path))
            {

                return null;//文件不存在
            }
            FileStream fs = File.OpenRead(path); //OpenRead
            int filelength = 0;
            filelength = (int)fs.Length; //获得文件长度 
            Byte[] image = new Byte[filelength]; //建立一个字节数组 
            fs.Read(image, 0, filelength); //按字节流读取 
            System.Drawing.Image result = System.Drawing.Image.FromStream(fs);
            fs.Close();
            fs.Dispose();
            Bitmap bit = new Bitmap(result);
            return bit;
        }

        /// <summary>
        /// 指定位置添加图片
        /// </summary>
        /// <param name="changci"></param>
        /// <param name="value"></param>
        public void run(string changci, string value)
        {


            try
            {
                string month = DateTime.Now.AddHours(-1).Month.ToString();
                string day = DateTime.Now.AddHours(-1).Day.ToString();

                if (DateTime.Now.Hour == 0 || DateTime.Now.Hour == 24)
                {
                    day = DateTime.Now.AddDays(-1).Day.ToString();
                }

                if (month.Length == 1)
                {
                    month = "0" + month;
                }
                if (day.Length == 1)
                {
                    day = "0" + day;
                }

                char[] monthchar = month.ToCharArray();



                char[] daychar = day.ToCharArray();


                System.Drawing.Image image_changci = ReadImageFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/场次/" + changci + ".png");



                System.Drawing.Image image_month = ReadImageFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/date/" + monthchar[0] + ".png");
                System.Drawing.Image image_month2 = ReadImageFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/date/" + monthchar[1] + ".png");
                System.Drawing.Image image_yue = ReadImageFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/date/月.png");
                System.Drawing.Image image_ri = ReadImageFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/date/日.png");
                System.Drawing.Image image_day = ReadImageFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/date/" + daychar[0] + ".png");
                System.Drawing.Image image_day2 = ReadImageFile(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/date/" + daychar[1] + ".png");







                Bitmap bitmap = new Bitmap(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/main7/" + value + ".jpg");
                Graphics device = Graphics.FromImage(bitmap);
                //如果picturebox1本身有内容，就先画到image上
                device.DrawImage(image_changci, 380, 80); //用你想要的位置画小图


                device.DrawImage(image_month, 140, 85);
                device.DrawImage(image_month2, 155, 85);

                device.DrawImage(image_day, 195, 85);
                device.DrawImage(image_day2, 210, 85);



                device.DrawImage(image_yue, 95, 46);
                device.DrawImage(image_ri, 165, 46);


                bitmap.Save(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/image627/" + changci + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);


                image_month.Dispose();
                image_month2.Dispose();
                image_yue.Dispose();
                image_ri.Dispose();
                image_day.Dispose();
                image_day2.Dispose();
                image_changci.Dispose();
                bitmap.Dispose();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());

            }

        }



        public void xieru(string path, string txt)
        {
            FileStream fs1 = new FileStream(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + path, FileMode.Create, FileAccess.Write);//创建写入文件 
            StreamWriter sw = new StreamWriter(fs1, Encoding.GetEncoding("UTF-8"));
            sw.Write(txt);
            sw.Close();
            fs1.Close();
            sw.Dispose();
        }

        public void duqu(string path)
        {
            StreamReader sr = new StreamReader(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + path, Encoding.GetEncoding("UTF-8"));
            //一次性读取完 
            string texts = sr.ReadToEnd();
            string[] text = texts.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            if (text.Length > 5)
            {
                Application["zuori"] = text[0];
                Application["a1"] = text[1];
                Application["a2"] = text[2];
                Application["a3"] = text[3];
                Application["a4"] = text[4];
                Application["a5"] = text[5];
                Application["a6"] = text[6];
                Application["a7"] = text[7];
            }

            sr.Close();  //只关闭流
            sr.Dispose();   //销毁流内存
        }
        protected void Page_Load(object sender, EventArgs e)
        {



            try
            {
                StreamReader sr = new StreamReader(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/index627.txt", Encoding.GetEncoding("UTF-8"));
                //一次性读取完 
                string texts = sr.ReadToEnd();
                string[] text = texts.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                Application["diyichang"] = text[0];
                Application["dierchang"] = text[1];
                Application["disanchang"] = text[2];
                sr.Close();  //只关闭流
                sr.Dispose();   //销毁流内存
            }
            catch (Exception ex)
            {

                ex.ToString();
            }


            if (Request["action"] == "reset")
            {
                if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/image627/1.jpg"))
                {
                    File.Delete(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/image627/1.jpg");
                }
                if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/image627/2.jpg"))
                {
                    File.Delete(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/image627/2.jpg");
                }
                if (File.Exists(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/image627/3.jpg"))
                {
                    File.Delete(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/image627/3.jpg");
                }
            }

            if (Request["changci"] == "1")
            {
                Application["changname"] = "第一场";
                duqu("/image627/chang1.txt");
            }
            if (Request["changci"] == "2")
            {
                Application["changname"] = "第二场";
                duqu("/image627/chang2.txt");
            }
            if (Request["changci"] == "3")
            {
                Application["changname"] = "第三场";
                duqu("/image627/chang3.txt");
            }
            if (HttpContext.Current.Request.RequestType == "POST")
            {


                string diyichang = Request["diyichang"];
                string dierchang = Request["dierchang"];
                string disanchang = Request["disanchang"];


                string zuori = Request["zuori"];
                string changname = Request["changname"];
                string a2 = Request["a2"];
                string a3 = Request["a3"];
                string a4 = Request["a4"];
                string a5 = Request["a5"];
                string a6 = Request["a6"];
                string a7 = Request["a7"];

                if (changname == "第一场")
                {
                    xieru("/image627/chang1.txt", zuori+ "\r\n" + changname + "\r\n" + a2 + "\r\n" + a3 + "\r\n" + a4 + "\r\n" + a5 + "\r\n" + a6 + "\r\n" + a7 + "\r\n");
                }
                if (changname == "第二场")
                {
                    xieru("/image627/chang2.txt", zuori + "\r\n" + changname + "\r\n" + a2 + "\r\n" + a3 + "\r\n" + a4 + "\r\n" + a5 + "\r\n" + a6 + "\r\n" + a7 + "\r\n");
                }
                if (changname == "第三场")
                {
                    xieru("/image627/chang3.txt", zuori + "\r\n" + changname + "\r\n" + a2 + "\r\n" + a3 + "\r\n" + a4 + "\r\n" + a5 + "\r\n" + a6 + "\r\n" + a7+ "\r\n");
                }


                FileStream fs1 = new FileStream(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "/index627.txt", FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter sw = new StreamWriter(fs1, Encoding.GetEncoding("UTF-8"));
                sw.WriteLine(diyichang + "\r\n" + dierchang + "\r\n" + disanchang);
                sw.Close();
                fs1.Close();
                sw.Dispose();


                Application["diyichang"] = diyichang;
                Application["dierchang"] = dierchang;
                Application["disanchang"] = disanchang;

                if (diyichang != "")
                {

                    run("1", diyichang);

                }

                if (dierchang != "")
                {

                    run("2", dierchang);

                }
                if (disanchang != "")
                {

                    run("3", disanchang);

                }


            }

        }


    }
}