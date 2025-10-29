using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DownloadManager
{
    public static class Logger
    {
        private static readonly string LogPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "log",
            "download.log");

        public static void Log(string message)
        {
            try
            {
                string directory = Path.GetDirectoryName(LogPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";

               
                File.AppendAllText(LogPath, logMessage + Environment.NewLine);
            }
            catch (Exception ex)
            {
                
            }
        }


        public static void logsize(string id)
        {
            FileStream fs1 = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "\\MB50.txt", FileMode.Append, FileAccess.Write);//创建写入文件 
            StreamWriter sw = new StreamWriter(fs1, Encoding.GetEncoding("UTF-8"));
            sw.WriteLine(id);
            sw.Close();
            fs1.Close();
            sw.Dispose();
        }






    }
}