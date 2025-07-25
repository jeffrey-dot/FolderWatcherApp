using FolderWatcherApp.Models;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderWatcherApp
{
    /// <summary>
    /// 应用程序的主窗口
    /// </summary>
    public partial class Form1 : Form
    {
        // 数据库上下文，用于与数据库交互
        private readonly AppDbContext _context;
        // 文件系统监视器列表，每个监视器对应一个文件夹
        private readonly List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();
        // 标记当前是否正在监控
        private bool _isMonitoring = false;

        /// <summary>
        /// 主窗口的构造函数
        /// </summary>
        /// <param name="context">共享的数据库上下文实例</param>
        public Form1(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            Log("Application initialized. Please configure settings and start monitoring.", Color.White);

            // 绑定按钮事件
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            this.btnToggleMonitoring.Click += new System.EventHandler(this.btnToggleMonitoring_Click);
        }

        /// <summary>
        /// 在日志窗口中记录一条带颜色的消息
        /// </summary>
        /// <param name="message">要记录的消息</param>
        /// <param name="color">消息的颜色</param>
        private void Log(string message, Color color)
        {
            // 如果是从非 UI 线程调用，则需要切换回 UI 线程
            if (rtbLog.InvokeRequired) { rtbLog.Invoke((Action)(() => Log(message, color))); return; }
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;
            rtbLog.SelectionColor = color;
            rtbLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
            rtbLog.ScrollToCaret(); // 自动滚动到最新日志
        }

        /// <summary>
        /// 开始监控所有已配置的文件夹
        /// </summary>
        private void StartMonitoring()
        {
            StopMonitoring(); // 开始前先确保停止所有旧的监控

            var setting = _context.Settings.Include(s => s.MonitoredFolders).FirstOrDefault();
            if (setting == null || !setting.MonitoredFolders.Any())
            {
                Log("Monitoring failed to start: No folders configured.", Color.White);
                MessageBox.Show("No folders configured to watch. Please add folders in Settings.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (var folder in setting.MonitoredFolders)
            {
                if (Directory.Exists(folder.Path))
                {
                    var watcher = new FileSystemWatcher(folder.Path) { NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName, EnableRaisingEvents = true };
                    watcher.Changed += OnFileSystemChanged;
                    watcher.Created += OnFileSystemChanged;
                    watcher.Deleted += OnFileSystemChanged;
                    watcher.Renamed += OnFileRenamed;
                    _watchers.Add(watcher);
                    Log($"Started watching: {folder.Path}", Color.Cyan);
                }
                else
                {
                    Log($"Failed to watch folder (not found): {folder.Path}", Color.White);
                }
            }

            if (_watchers.Any()) { _isMonitoring = true; Log("Monitoring started.", Color.White); }
            else { Log("Monitoring failed to start: No valid folders found to watch.", Color.White); }
            UpdateUIForMonitoringState();
        }

        /// <summary>
        /// 停止所有监控
        /// </summary>
        private void StopMonitoring()
        {
            if (!_watchers.Any()) return;
            foreach (var watcher in _watchers) { watcher.EnableRaisingEvents = false; watcher.Dispose(); Log($"Stopped watching: {watcher.Path}", Color.Gray); }
            _watchers.Clear();
            _isMonitoring = false;
            Log("Monitoring stopped.", Color.White);
            UpdateUIForMonitoringState();
        }

        /// <summary>
        /// 文件系统发生更改（创建、修改、删除）时触发
        /// </summary>
        private void OnFileSystemChanged(object sender, FileSystemEventArgs e)
        {
            if (e.Name != null) { Log($"Event: {e.ChangeType} | File: {e.Name}", Color.White); SendEmailWithPlaceholders(e.Name, Path.GetDirectoryName(e.FullPath) ?? string.Empty, e.ChangeType.ToString(), string.Empty); }
        }

        /// <summary>
        /// 文件被重命名时触发
        /// </summary>
        private void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            if (e.Name != null && e.OldName != null) { Log($"Event: {e.ChangeType} | From: {e.OldName} -> To: {e.Name}", Color.White); SendEmailWithPlaceholders(e.Name, Path.GetDirectoryName(e.FullPath) ?? string.Empty, e.ChangeType.ToString(), e.OldName); }
        }

        /// <summary>
        /// 使用占位符替换并发送邮件
        /// </summary>
        private void SendEmailWithPlaceholders(string fileName, string folderPath, string changeType, string oldFileName)
        {
            Task.Run(() =>
            {
                try
                {
                    var setting = _context.Settings.Include(s => s.Recipients).AsNoTracking().FirstOrDefault();
                    if (setting == null || string.IsNullOrEmpty(setting.SmtpServer) || !setting.Recipients.Any())
                    {
                        Log("Email not sent: SMTP settings or recipients are not configured.", Color.Yellow);
                        return;
                    }

                    string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string subject = setting.MailSubject.Replace("[FileName]", fileName).Replace("[FolderPath]", folderPath).Replace("[ChangeType]", changeType).Replace("[Timestamp]", timestamp).Replace("[OldFileName]", oldFileName);
                    string body = setting.MailBody.Replace("[FileName]", fileName).Replace("[FolderPath]", folderPath).Replace("[ChangeType]", changeType).Replace("[Timestamp]", timestamp).Replace("[OldFileName]", oldFileName);

                    var message = new MimeMessage();
                    message.From.Add(new MailboxAddress("Folder Watcher", setting.SmtpUser));
                    foreach (var recipient in setting.Recipients) { message.To.Add(MailboxAddress.Parse(recipient.EmailAddress)); }

                    message.Subject = subject;
                    message.Body = new TextPart("plain") { Text = body };

                    using (var client = new SmtpClient())
                    {
                        client.Connect(setting.SmtpServer, setting.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
                        client.Authenticate(setting.SmtpUser, setting.SmtpPass);
                        client.Send(message);
                        client.Disconnect(true);
                        Log($"Email sent successfully to: {string.Join(", ", setting.Recipients.Select(r => r.EmailAddress))}", Color.Green);
                    }
                }
                catch (Exception ex) { Log($"Failed to send email: {ex.Message}", Color.Red); }
            });
        }

        /// <summary>
        /// 打开设置窗口
        /// </summary>
        private void btnSettings_Click(object? sender, EventArgs e)
        {
            using (var form = new SettingsForm(_context))
            {
                form.StartPosition = FormStartPosition.CenterParent;
                form.ShowDialog(this);
            }
        }

        /// <summary>
        /// “开始/停止监控”按钮的点击事件
        /// </summary>
        private void btnToggleMonitoring_Click(object? sender, EventArgs e)
        {
            if (_isMonitoring) { StopMonitoring(); } else { StartMonitoring(); }
        }

        /// <summary>
        /// 根据监控状态更新 UI
        /// </summary>
        private void UpdateUIForMonitoringState()
        {
            btnSettings.Enabled = !_isMonitoring;
            btnToggleMonitoring.Text = _isMonitoring ? "Stop Monitoring" : "Start Monitoring";
            lblStatus.Text = _isMonitoring ? "Status: Running" : "Status: Not Running";
        }
    }
}
