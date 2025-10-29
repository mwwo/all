using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pan123api
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //// 创建分享链接
            //var shareLink = api.CreateShareLink(new List<string> { fileId }, "测试分享", 7);
            //Console.WriteLine($"分享链接: {shareLink}");

            //// 获取直链
            //var directLink = api.GetAuthDirectLinkFromFileId(fileId);
            //Console.WriteLine($"直链: {directLink}");




            var api = new OpenApi123Pan();
            ////上传文件
            //var fileId = api.CreateFile(@"C:\Users\Administrator\Desktop\muban\help.mp4", "25209886");

            //if (fileId != null)
            //{
            //    Console.WriteLine($"文件上传成功: {fileId}");

            //}





            var fileId2 = api.Searchfile("54142561");
            //var downurl = api.GetdownloadUrl(fileId2);
            var downurl = api.GetDirectLink(fileId2);
            Console.WriteLine(downurl);
        }
    }
}
