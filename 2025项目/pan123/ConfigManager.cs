using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pan123
{
    public class ConfigManager
    {
        private readonly string _configFile = "api_123pan.txt";
        private Dictionary<string, string> _config;

        public ConfigManager()
        {
            LoadConfig();
        }

        public void LoadConfig()
        {
            _config = new Dictionary<string, string>();

            if (!File.Exists(_configFile))
            {
                CreateDefaultConfig();
                return;
            }

            var lines = File.ReadAllLines(_configFile);
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ':' }, 2);
                if (parts.Length == 2)
                {
                    _config[parts[0].Trim()] = parts[1].Trim();
                }
            }
        }

        public void SaveConfig()
        {
            var lines = _config.Select(kvp => $"{kvp.Key}:{kvp.Value}").ToArray();
            File.WriteAllLines(_configFile, lines);
        }

        public string GetValue(string key)
        {
            return _config.ContainsKey(key) ? _config[key] : null;
        }

        public void SetValue(string key, string value)
        {
            _config[key] = value;
        }

        private void CreateDefaultConfig()
        {
            _config = new Dictionary<string, string>
            {
                { "access_token", "" },
                { "expiredAt", DateTime.Now.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") },
                { "private_key", "" },
                { "uid", "1826905314" },
                { "client_123id", "57bfd73eafda484ba9fa51c1c4a5e277" },
                { "client_123secret", "44d008ff0e304a238d368551149399fb" }
            };
            SaveConfig();
        }










    }
}
