using System;
using System.IO;
using Newtonsoft.Json;

namespace DownloadManager
{
    public class Config
    {
        public int LastId { get; set; } = 54100001;
        public string DownloadPath { get; set; } = "";

        private static readonly string ConfigPath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "config.json");

        public static Config Load()
        {
            try
            {
                if (File.Exists(ConfigPath))
                {
                    string json = File.ReadAllText(ConfigPath);
                    return JsonConvert.DeserializeObject<Config>(json) ?? new Config();
                }
            }
            catch (Exception)
            {
                // 如果加载失败，返回默认配置
            }
            return new Config();
        }

        public void Save()
        {
            try
            {
                string directory = Path.GetDirectoryName(ConfigPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(ConfigPath, json);
            }
            catch (Exception)
            {
                // 保存失败，忽略错误
            }
        }
    }
}