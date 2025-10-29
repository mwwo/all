using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace pan123api
{
    public class OpenApi123Pan
    {

        private readonly string _baseUrl = "https://open-api.123pan.com";
        private Dictionary<string, string> _config;
        private Dictionary<string, string> _headers;

        // API endpoints
        private readonly string[] _apiEndpoints = {
            "/api/v1/access_token",           // 获取access_token
            "/api/v1/user/info",              // 获取用户信息
            "/upload/v1/file/mkdir",          // 创建目录
            "/upload/v1/file/create",         // 创建文件
            "/upload/v1/file/list_upload_parts", // 列举已上传分片
            "/upload/v1/file/get_upload_url", // 获取上传地址
            "/upload/v1/file/upload_complete", // 上传完毕
            "/upload/v1/file/upload_async_result", // 异步轮询获取上传结果
            "/api/v1/file/move",              // 移动文件
            "/api/v1/file/trash",             // 删除文件至回收站
            "/api/v1/file/recover",           // 从回收站恢复文件
            "/api/v1/file/delete",            // 彻底删除文件
            "/api/v1/file/list",              // 获取文件列表
            "/api/v1/share/create",           // 创建分享链接
            "/api/v1/offline/download",       // 创建离线下载
            "/api/v1/direct-link/enable",     // 启用直链空间
            "/api/v1/direct-link/disable",    // 禁用直链空间
            "/api/v1/direct-link/url",        // 获取直链链接
            "/api/v1/direct-link/queryTranscode", // 查询直链转码进度
            "/api/v1/direct-link/doTranscode", // 发起直链转码
            "/api/v1/direct-link/get/m3u8",   // 获取直链转码链接
            "/api/v1/file/rename",            // 重命名
            "/api/v1/file/detail",            // 获取文件详情
            "/api/v2/file/list",               // 获取文件列表（推荐）
            "/api/v1/file/download_info"       //下载

        };

        // Properties
        public string Authorization { get; private set; }
        public string ExpiredAt { get; private set; }
        public string PrivateKey { get; private set; }
        public long Uid { get; private set; }
        public string Client123Id { get; private set; }
        public string Client123Secret { get; private set; }

        // User info properties
        public string Nickname { get; private set; }
        public string HeadImage { get; private set; }
        public string Passport { get; private set; }
        public string Mail { get; private set; }
        public double SpaceUsed { get; private set; }
        public double SpacePermanent { get; private set; }
        public long SpaceTemp { get; private set; }
        public string SpaceTempExpr { get; private set; }
        public string DataTraceId { get; private set; }

        public  OpenApi123Pan()
        {
            ReadConfig();
            InitializeHeaders();
        }


        public string GetDirectLink(string fileId)
        {
            var directLinkUrl = _baseUrl + _apiEndpoints[17];
            var parameters = $"fileID={fileId}";

            try
            {
                var response = MakeRequest($"{directLinkUrl}?{parameters}", "GET", _headers);
                var responseData = JObject.Parse(response);
                var code = responseData["code"].Value<int>();
                var message = responseData["message"]?.ToString();

                if (code == 0)
                {
                    return responseData["data"]["url"].ToString();
                }
                else
                {
                    Console.WriteLine(message);
                    return null;
                }
            }
            catch (WebException e)
            {
                Console.WriteLine($"连接错误: {e.Message}");
                return null;
            }
        }


        private void ReadConfig()
        {
            try
            {
                _config = new Dictionary<string, string>();
                var lines = File.ReadAllLines("api_123pan.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split(new[] { ':' }, 2);
                    if (parts.Length == 2)
                    {
                        _config[parts[0].Trim()] = parts[1].Trim();
                    }
                }

                var currentTime = DateTime.Now;
                var expirationTime = DateTime.Parse(_config["expiredAt"]);

                if (currentTime > expirationTime)
                {
                    GetAccessToken();
                    // Reload config after token refresh
                    lines = File.ReadAllLines("api_123pan.txt");
                    _config.Clear();
                    foreach (var line in lines)
                    {
                        var parts = line.Split(new[] { ':' }, 2);
                        if (parts.Length == 2)
                        {
                            _config[parts[0].Trim()] = parts[1].Trim();
                        }
                    }
                }

                Authorization = _config["access_token"];
                ExpiredAt = _config["expiredAt"];
                PrivateKey = _config["private_key"];
                Uid = long.Parse(_config["uid"]);
                Client123Id = _config["client_123id"];
                Client123Secret = _config["client_123secret"];
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Read failed: {ex.Message}");
            }
        }

        private void InitializeHeaders()
        {
            _headers = new Dictionary<string, string>
            {
                { "Platform", "open_platform" },
                { "Authorization", Authorization },
                { "Content-Type", "application/json" }
            };
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileid"></param>
        /// <returns></returns>
        public string GetdownloadUrl(string fileid)
        {
            try
            {
                var Url = _baseUrl + _apiEndpoints[24];
                var data = new { fileId = fileid };

                var parameters = $"fileID={fileid}";
               
                var response = MakeRequest($"{Url}?{parameters}", "GET", _headers);
                var responseData = JObject.Parse(response);
                var code = responseData["code"].Value<int>();

                if (code == 0)
                {
                    var completeData = responseData["data"] as JObject;
                    var downloadUrl = completeData?["downloadUrl"]?.ToString();
                    return downloadUrl;
                }
                else
                {
                    return responseData.ToString();
                }

            }
            catch (Exception ex)
            {

                return ex.ToString();
            }
        }

        /// <summary>
        /// 获取token
        /// </summary>
        public void GetAccessToken()
        {
            try
            {
                var accessUrl = _baseUrl + _apiEndpoints[0];
                var headers = new Dictionary<string, string> { { "Platform", "open_platform" } };
                var data = new
                {
                    clientID = Client123Id,
                    clientSecret = Client123Secret
                };

                var response = MakeRequest(accessUrl, "POST", headers, data);
                var responseData = JObject.Parse(response);
                var dataObj = responseData["data"];

                var accessToken = dataObj["accessToken"].ToString();

                Console.WriteLine(dataObj["expiredAt"].ToString());
                var expiredAt = dataObj["expiredAt"].ToString().Replace("T", " ").Substring(0, 17);

                // Update config file
                var lines = File.ReadAllLines("api_123pan.txt").ToList();
                for (int i = 0; i < lines.Count; i++)
                {
                    if (lines[i].StartsWith("access_token:"))
                    {
                        lines[i] = $"access_token:{accessToken}";
                    }
                    else if (lines[i].StartsWith("expiredAt:"))
                    {
                        lines[i] = $"expiredAt:{expiredAt}";
                    }
                }
                File.WriteAllLines("api_123pan.txt", lines);

                // Update class properties
                Authorization = accessToken;
                ExpiredAt = expiredAt;
                InitializeHeaders(); // Refresh headers with new token
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.ToString());
            }
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="parentFileId"></param>
        /// <param name="maxRetries"></param>
        /// <returns></returns>

        public string CreateFile(string filePath, string parentFileId, int maxRetries = 2)
        {
            for (int retry = 0; retry < maxRetries; retry++)
            {
                var result = CreateFileInternal(filePath, parentFileId);
                if (result != null)
                {
                    return result;
                }
                Console.WriteLine($"进行第{retry + 2}次上传...");
            }
            return null;
        }

        /// <summary>
        /// 分片上传
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="parentFileId"></param>
        /// <returns></returns>
        private string CreateFileInternal(string filePath, string parentFileId)
        {
            var createUrl = _baseUrl + _apiEndpoints[3];
            var etag = CalculateMd5(filePath);
            var fileInfo = GetFileInfo(filePath);

            var data = new
            {
                parentFileID = parentFileId,
                filename = fileInfo.FileName,
                etag = etag,
                size = fileInfo.Size
            };

            var response = MakeRequest(createUrl, "POST", _headers, data);
            var responseData = JObject.Parse(response);
            var code = responseData["code"].Value<int>();
            var message = responseData["message"]?.ToString();

            if (code == 0)
            {
                var dataObj = responseData["data"];
                var fileId = dataObj["fileID"]?.ToString();
                var reuse = dataObj["reuse"]?.Value<bool>() ?? false;
                var preuploadId = dataObj["preuploadID"]?.ToString();
                var sliceSize = dataObj["sliceSize"]?.Value<long>() ?? 0;

                if (!reuse)
                {
                    var uploadDataParts = UploadSlices(filePath, sliceSize, fileInfo.Size, preuploadId);

                    if (fileInfo.Size > sliceSize)
                    {
                        var partsData = ListUploadParts(preuploadId);
                        if (!PartsDataEqual(partsData, uploadDataParts))
                        {
                            Console.WriteLine("云端分片与本地信息比对不相同");
                            return null;
                        }
                    }

                    try
                    {
                        var (completeFileId, completeData) = UploadComplete(preuploadId);
                        var resAsync = completeData["async"]?.Value<bool>() ?? false;
                        var completed = completeData["completed"]?.Value<bool>() ?? false;

                        if (completed)
                        {
                            if (completeFileId != null)
                            {
                                Console.WriteLine("文件上传完成");
                                return completeFileId;
                            }
                            Console.WriteLine("上传完成，但fileID获取失败");
                            return null;
                        }
                        else if (resAsync)
                        {
                            System.Threading.Thread.Sleep(3000);
                            var fileIdFromAsync = GetAsyncResult(preuploadId);
                            if (fileIdFromAsync != null)
                            {
                                Console.WriteLine("异步查询成功");
                                return fileIdFromAsync;
                            }
                            else
                            {
                                Console.WriteLine("异步查询有误，列表查询成功");
                                return null;
                            }
                        }
                        else
                        {
                            Console.WriteLine("未成功上传");
                            return null;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"上传结果获取: {e.Message}");
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("文件已秒传成功");
                    return fileId;
                }
            }
            else if (code == 1)
            {
                Console.WriteLine(message);
               
                return null;
            }
            else if (code == 401 && message == "token is expired")
            {
                GetAccessToken();
                return null;
            }
            else
            {
                Console.WriteLine("其他上传错误");
                return null;
            }
        }

        private string GetUploadUrl(Dictionary<string, object> dataSlices)
        {
            try
            {
                var uploadUrl = _baseUrl + _apiEndpoints[5];
                var response = MakeRequest(uploadUrl, "POST", _headers, dataSlices);
                var responseData = JObject.Parse(response);
                return responseData["data"]["presignedURL"].ToString();
            }
            catch (Exception e)
            {
                Console.WriteLine($"分片上传地址问题: {e.Message}");
                return null;
            }
        }

       

        private List<Dictionary<string, object>> UploadSlices(string filePath, long sliceSize, long fileSize, string preuploadId)
        {
            var uploadDataParts = new List<Dictionary<string, object>>();
            var numSlices = (int)Math.Ceiling((double)fileSize / sliceSize);

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                for (int i = 1; i <= numSlices; i++)
                {
                    var dataSlices = new Dictionary<string, object>
                    {
                        { "preuploadID", preuploadId },
                        { "sliceNo", i }
                    };

                    var presignedUrl = GetUploadUrl(dataSlices);
                    var chunk = new byte[sliceSize];
                    var bytesRead = fileStream.Read(chunk, 0, (int)sliceSize);

                    if (bytesRead == 0) break;

                    Array.Resize(ref chunk, bytesRead);
                    var md5 = CalculateMd5(chunk);

                    var response = PutRequest(presignedUrl, chunk);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        uploadDataParts.Add(new Dictionary<string, object>
                        {
                            { "partNumber", i.ToString() },
                            { "size", bytesRead },
                            { "etag", md5 }
                        });
                    }
                }
            }

            return uploadDataParts.Count > 0 ? uploadDataParts : null;
        }

        private List<Dictionary<string, object>> ListUploadParts(string preuploadId)
        {
            var uploadPartsUrl = _baseUrl + _apiEndpoints[4];
            var data = new { preuploadID = preuploadId };
            var response = MakeRequest(uploadPartsUrl, "POST", _headers, data);
            var responseData = JObject.Parse(response);
            return responseData["data"]["parts"].ToObject<List<Dictionary<string, object>>>();
        }

        private (string fileId, JObject data) UploadComplete(string preuploadId)
        {
            var uploadCompleteUrl = _baseUrl + _apiEndpoints[6];
            var data = new { preuploadID = preuploadId };

            var response = MakeRequest(uploadCompleteUrl, "POST", _headers, data, 3000, 5000);
            var responseData = JObject.Parse(response);
            var code = responseData["code"].Value<int>();

            if (code == 0)
            {
                var completeData = responseData["data"] as JObject;
                var fileId = completeData?["fileID"]?.ToString();
                return (fileId, completeData);
            }
            return (null, null);
        }

        private string GetAsyncResult(string preuploadId)
        {
            var asyncResultUrl = _baseUrl + _apiEndpoints[7];
            var data = new { preuploadID = preuploadId };
            var response = MakeRequest(asyncResultUrl, "POST", _headers, data);
            var responseData = JObject.Parse(response);
            var asyncData = responseData["data"];
            var completed = asyncData["completed"]?.Value<bool>() ?? false;
            var fileId = asyncData["fileID"]?.ToString();

            return completed ? fileId : null;
        }

    

    
      
     

        public string Searchfile(string searchData)
        {
            try
            {
                // 新版查询
                var limit = 1;
                var listUrl = _baseUrl + _apiEndpoints[23];
                var parameters = $"parentFileId=0&lastFileId=&limit={limit}&searchData={searchData}&searchMode=0";

                var response = MakeRequest($"{listUrl}?{parameters}", "GET", _headers);
                var responseData = JObject.Parse(response);
                var fileList = responseData["data"]["fileList"] as JArray;

                foreach (var item in fileList)
                {
                    return item["fileId"]?.ToString();
                }
                return "";
            }
            catch (Exception)
            {

                return "";
            }
        }

     

       

      

        // Utility methods
        private string CalculateMd5(string filePath)
        {
            using (var md5 = MD5.Create())
            using (var stream = File.OpenRead(filePath))
            {
                var hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        private string CalculateMd5(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        private (long Size, string FileName) GetFileInfo(string filePath)
        {
            var fileInfo = new FileInfo(filePath);
            return (fileInfo.Length, fileInfo.Name);
        }

       

        private bool PartsDataEqual(List<Dictionary<string, object>> parts1, List<Dictionary<string, object>> parts2)
        {
            if (parts1 == null || parts2 == null) return parts1 == parts2;
            if (parts1.Count != parts2.Count) return false;

            for (int i = 0; i < parts1.Count; i++)
            {
                var part1 = parts1[i];
                var part2 = parts2[i];

                if (part1["partNumber"].ToString() != part2["partNumber"].ToString() ||
                    Convert.ToInt64(part1["size"]) != Convert.ToInt64(part2["size"]) ||
                    part1["etag"].ToString() != part2["etag"].ToString())
                {
                    return false;
                }
            }
            return true;
        }

        // HTTP request helper methods
        private string MakeRequest(string url, string method, Dictionary<string, string> headers, object data = null, int timeoutMs = 0, int readWriteTimeoutMs = 0)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = method;
                request.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";

                if (timeoutMs > 0) request.Timeout = timeoutMs;
                if (readWriteTimeoutMs > 0) request.ReadWriteTimeout = readWriteTimeoutMs;

                foreach (var header in headers)
                {
                    if (header.Key == "Content-Type")
                        request.ContentType = header.Value;
                    else
                        request.Headers[header.Key] = header.Value;
                }

                if (data != null && (method == "POST" || method == "PUT"))
                {
                    var jsonData = JsonConvert.SerializeObject(data);
                    var byteData = Encoding.UTF8.GetBytes(jsonData);

                    request.ContentLength = byteData.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(byteData, 0, byteData.Length);
                    }
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {

               return ex.ToString();
            }
        }

        private HttpWebResponse PutRequest(string url, byte[] data)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            return (HttpWebResponse)request.GetResponse();
        }









    }
}
