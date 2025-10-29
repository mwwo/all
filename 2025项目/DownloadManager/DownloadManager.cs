using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DownloadManager
{
    public class DownloadManager
    {
        private readonly int startId;
        private readonly int endId;
        private readonly int threadCount;
        private readonly string downloadPath;
        private readonly HttpClient httpClient;
        private CancellationTokenSource cancellationTokenSource;
        private int currentId;
        private int completedCount;

        public event Action<string> OnLog;
        public event Action<int, int> OnProgress;
        public event Action OnCompleted;

       

        public DownloadManager(int startId, int endId, int threadCount, string downloadPath)
        {
            this.startId = startId;
            this.endId = endId;
            this.threadCount = threadCount;
            this.downloadPath = downloadPath;
            this.httpClient = new HttpClient();
            this.httpClient.Timeout = TimeSpan.FromSeconds(300);
            this.currentId = startId;
            this.completedCount = 0;
        }

        public void Start()
        {
            cancellationTokenSource = new CancellationTokenSource();
            var tasks = new List<Task>();

            for (int i = 0; i < threadCount; i++)
            {
                tasks.Add(Task.Run(() => ProcessIds(cancellationTokenSource.Token)));
            }

            Task.WhenAll(tasks).ContinueWith(t =>
            {
                if (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    OnCompleted?.Invoke();
                }
            });
        }

        public void Stop()
        {
            cancellationTokenSource?.Cancel();
        }

        private async Task ProcessIds(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                int id = Interlocked.Increment(ref currentId) - 1;

                if (id > endId)
                {
                    break;
                }

                try
                {
                    await ProcessSingleId(id, cancellationToken);
                }
                catch (Exception ex)
                {
                    OnLog?.Invoke($"ID {id} 处理失败: {ex.Message}");
                    Logger.Log($"ID {id} 处理失败: {ex.Message}");
                }
                finally
                {
                    int completed = Interlocked.Increment(ref completedCount);
                    OnProgress?.Invoke(completed, endId - startId + 1);
                }
            }
        }

        private async Task ProcessSingleId(int id, CancellationToken cancellationToken)
        {
            // 第一步：检查文档ID
            string searchUrl = $"https://search.zxxk.com/api-kernel/v1/search-site/document/list?pageType=2&keyword={id}&page=1&pageIndex=1&pageSize=30&enableEBR=true&useCTR=false";

            //OnLog?.Invoke($"检查ID {id} 的文档信息");
           // Logger.Log($"检查ID {id} 的文档信息");

            string searchResponse = await httpClient.GetStringAsync(searchUrl);
            
            var searchData = JObject.Parse(searchResponse);

            var documents = searchData["data"]?["searchList"];
            if (documents == null)
            {
                OnLog?.Invoke($"ID {id} 未找到文档列表");
                Logger.Log($"ID {id} 未找到文档列表");
                return;
            }

            bool found = false;
            foreach (var doc in documents)
            {
                if (doc["documentId"]?.Value<int>() == id)
                {
                    found = true;
                    break;
                }
            }

            if (!found)
            {
                OnLog?.Invoke($"ID {id} 未找到匹配的文档");
                Logger.Log($"ID {id} 未找到匹配的文档");
                return;
            }

            // 第二步：获取资源详情
           

           // OnLog?.Invoke($"获取ID {id} 的资源详情");
           // Logger.Log($"获取ID {id} 的资源详情");

            string detailResponse = "";
            
            string detailUrl = $"http://110.40.191.114/xk.aspx?method=kd0ph8fynbhj&id={id}";
        
            var handler = new HttpClientHandler
            {
                CookieContainer = new System.Net.CookieContainer(),
                UseCookies = true // 确保使用CookieContainer中的Cookie
            };

            // 创建HttpClient
            using (var httpClient = new HttpClient(handler))
            {
               
                //// 创建一个Cookie并添加到CookieContainer
                //var cookie = new System.Net.Cookie("xk.passport.ticket.v2", COOKIES.Replace("xk.passport.ticket.v2=", ""), "/", "zxxk.com");
                //handler.CookieContainer.Add(new Uri(detailUrl), cookie);

                // 发送GET请求并获取响应字符串
                string response = await httpClient.GetStringAsync(detailUrl);
                detailResponse=(response);
            }
            
           ;
            
            string fileUrl = Regex.Match(detailResponse, @"""fileUrl"":""([\s\S]*?)""").Groups[1].Value.Trim();
            string fileName = Regex.Match(detailResponse, @"""fileName"":""([\s\S]*?)"",").Groups[1].Value.Trim();
            string fileSize = Regex.Match(detailResponse, @"""fileSize"":([\s\S]*?),").Groups[1].Value.Trim();

            if(fileSize!="")
            {
                double fz = Convert.ToDouble(fileSize) / 1000;
                if (fz>50)
                {
                    OnLog?.Invoke($"ID {id} 文件超过50MB");
                    Logger.logsize(id.ToString());
                    return;
                }
            }
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(fileUrl))
            {
                OnLog?.Invoke($"ID {id} 未找到文件名或文件URL");
                Logger.Log($"ID {id} 未找到文件名或文件URL");
                return;
            }

            // 第三步：下载文件
            //OnLog?.Invoke($"开始下载ID {id} 的文件: {fileName}");
            //Logger.Log($"开始下载ID {id} 的文件: {fileName}");

            string filePath = Path.Combine(downloadPath, id+"_"+fileName);

            using (var response = await httpClient.GetAsync(fileUrl, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                using (var stream = await response.Content.ReadAsStreamAsync())
                using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await stream.CopyToAsync(fileStream);
                }
            }

            OnLog?.Invoke($"成功下载ID {id} 的文件");
            //Logger.Log($"成功下载ID {id} 的文件: {fileName}");

            //var api = new OpenApi123Pan();
            ////上传文件
            //var fileId = api.CreateFile(filePath, "25242613");  //54100000上传到临时转存天翼云

            //if (fileId != null)
            //{
            //    OnLog?.Invoke($"文件上传成功: {fileId}");
            //    File.Delete(filePath);
            //}
        }
    }
}