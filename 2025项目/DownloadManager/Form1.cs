using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DownloadManager
{
    public partial class Form1 : Form
    {
        private DownloadManager downloadManager;
        private Config config;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            config = Config.Load();
            txtStartId.Text = config.LastId.ToString();
            txtDownloadPath.Text = config.DownloadPath;

            if (string.IsNullOrEmpty(txtDownloadPath.Text))
            {
                txtDownloadPath.Text = Path.Combine(Application.StartupPath, "Downloads");
            }
        }

        string path = AppDomain.CurrentDomain.BaseDirectory;
        private void btnStart_Click(object sender, EventArgs e)
        {
          
         
            if (!int.TryParse(txtStartId.Text, out int startId))
            {
                MessageBox.Show("请输入有效的起始ID");
                return;
            }

            if (!int.TryParse(txtEndId.Text, out int endId))
            {
                MessageBox.Show("请输入有效的结束ID");
                return;
            }

            if (startId > endId)
            {
                MessageBox.Show("起始ID不能大于结束ID");
                return;
            }

            if (!Directory.Exists(txtDownloadPath.Text))
            {
                try
                {
                    Directory.CreateDirectory(txtDownloadPath.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"创建下载目录失败：{ex.Message}");
                    return;
                }
            }

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            lblStatus.Text = "状态：下载中...";

            downloadManager = new DownloadManager(
                startId,
                endId,
                (int)numThreads.Value,
                txtDownloadPath.Text);

            downloadManager.OnLog += (message) =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<string>(s =>
                    {
                        txtLog.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {s}\r\n");
                        txtLog.ScrollToCaret();
                    }), message);
                }
                else
                {
                    txtLog.AppendText($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}\r\n");
                    txtLog.ScrollToCaret();
                }
            };

            downloadManager.OnProgress += (current, total) =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action<int, int>((c, t) =>
                    {
                        progressBar.Maximum = total;
                        progressBar.Value = c;
                        lblStatus.Text = $"状态：下载中 ({c}/{t})";
                    }), current, total);
                }
                else
                {
                    progressBar.Maximum = total;
                    progressBar.Value = current;
                    lblStatus.Text = $"状态：下载中 ({current}/{total})";
                }
            };

            downloadManager.OnCompleted += () =>
            {
                if (InvokeRequired)
                {
                    Invoke(new Action(() =>
                    {
                        btnStart.Enabled = true;
                        btnStop.Enabled = false;
                        lblStatus.Text = "状态：下载完成";
                        config.LastId = endId + 1;
                        config.DownloadPath = txtDownloadPath.Text;
                        config.Save();
                    }));
                }
                else
                {
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    lblStatus.Text = "状态：下载完成";
                    config.LastId = endId + 1;
                    config.DownloadPath = txtDownloadPath.Text;
                    config.Save();
                }
            };

            downloadManager.Start();
        }



        private void btnStop_Click(object sender, EventArgs e)
        {
            downloadManager?.Stop();
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblStatus.Text = "状态：已停止";

            if (int.TryParse(txtStartId.Text, out int startId))
            {
                config.LastId = startId;
                config.DownloadPath = txtDownloadPath.Text;
                config.Save();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                txtDownloadPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            downloadManager?.Stop();
            config.LastId = int.TryParse(txtStartId.Text, out int startId) ? startId : config.LastId;
            config.DownloadPath = txtDownloadPath.Text;
            config.Save();
            base.OnClosing(e);
        }

       
    }
}