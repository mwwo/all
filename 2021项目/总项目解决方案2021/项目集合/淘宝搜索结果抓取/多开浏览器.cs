﻿using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 淘宝搜索结果抓取
{
    public partial class 多开浏览器 : Form
    {
        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern bool InternetGetCookieEx(string pchURL, string pchCookieName, StringBuilder pchCookieData, ref System.UInt32 pcchCookieData, int dwFlags, IntPtr lpReserved);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int InternetSetCookieEx(string lpszURL, string lpszCookieName, string lpszCookieData, int dwFlags, IntPtr dwReserved);
        public 多开浏览器()
        {
            InitializeComponent();
        }
      
        public static string tbName;
        public static string GetCookies(string url)
        {
            uint datasize = 256;
            StringBuilder cookieData = new StringBuilder((int)datasize);
            if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x2000, IntPtr.Zero))
            {
                if (datasize < 0)
                    return null;


                cookieData = new StringBuilder((int)datasize);
                if (!InternetGetCookieEx(url, null, cookieData, ref datasize, 0x00002000, IntPtr.Zero))
                    return null;
            }
            return cookieData.ToString();
        }
        public static void SetFeatures(UInt32 ieMode)
        {

            if (LicenseManager.UsageMode != LicenseUsageMode.Runtime)
            {
                throw new ApplicationException();
            }

            string appName = System.IO.Path.GetFileName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
            string featureControlRegKey = "HKEY_CURRENT_USER\\Software\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\";

            Registry.SetValue(featureControlRegKey + "FEATURE_BROWSER_EMULATION", appName, ieMode, RegistryValueKind.DWord);

            Registry.SetValue(featureControlRegKey + "FEATURE_ENABLE_CLIPCHILDREN_OPTIMIZATION", appName, 1, RegistryValueKind.DWord);
        }
        private void button1_Click(object sender, EventArgs e)
        {
          string  COOKIE = GetCookies("https://s.taobao.com/search?initiative_id=tbindexz_20170306&ie=utf8&spm=a21bo.2017.201856-taobao-item.2&sourceId=tb.index&search_type=item&ssid=s5-e&commend=all&imgfile=&q=%E6%89%8B%E6%9C%BA&suggest=history_7&_input_charset=utf-8&wq=&suggest_query=&source=suggest");
            if(COOKIE!="")
            {
                直通车抓取.cookielist.Add(COOKIE);
                MessageBox.Show("添加成功");
            }
            

        }

        private void 多开浏览器_Load(object sender, EventArgs e)
        {
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate("https://login.taobao.com/member/login.jhtml?redirectURL=http%3A%2F%2Fi.taobao.com%2Fmy_taobao.htm%3Fspm%3Da21bo.2017.1997525045.1.5af911d9bdM7Dk");
            SetFeatures(11000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //发送验证码后 清理cookie
            HtmlDocument document = webBrowser1.Document;
            document.ExecCommand("ClearAuthenticationCache", false, null);
            webBrowser1.Refresh();
            webBrowser1.Navigate("https://login.taobao.com/member/login.jhtml?redirectURL=http%3A%2F%2Fi.taobao.com%2Fmy_taobao.htm%3Fspm%3Da21bo.2017.1997525045.1.5af911d9bdM7Dk");
        }
    }
}
