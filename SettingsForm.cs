using FolderWatcherApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FolderWatcherApp
{
    /// <summary>
    /// 设置窗口，包含多个选项卡用于配置
    /// </summary>
    public partial class SettingsForm : Form
    {
        // 共享的数据库上下文实例
        private readonly AppDbContext _context;
        private Setting _setting;

        /// <summary>
        /// 设置窗口的构造函数
        /// </summary>
        /// <param name="context">共享的数据库上下文</param>
        public SettingsForm(AppDbContext context)
        {
            InitializeComponent();
            _context = context;
            // 加载设置，如果不存在则创建一个新的实例
            _setting = _context.Settings.Include(s => s.MonitoredFolders).Include(s => s.Recipients).FirstOrDefault() ?? new Setting();
            SetupDataGridViews();
            LoadSettings();

            // 绑定事件
            this.btnAddFolder.Click += new System.EventHandler(this.BtnAddFolder_Click);
            this.dgvFolders.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvFolders_CellContentClick);
            this.btnAddRecipient.Click += new System.EventHandler(this.BtnAddRecipient_Click);
            this.dgvRecipients.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvRecipients_CellContentClick);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
        }

        /// <summary>
        /// 初始化两个 DataGridView 控件的样式和列
        /// </summary>
        private void SetupDataGridViews()
        {
            SetupGridView(dgvFolders, "Path", "Folder Path");
            SetupGridView(dgvRecipients, "Email", "Email Address");
        }

        /// <summary>
        /// 配置单个 DataGridView 的通用方法
        /// </summary>
        /// <param name="dgv">要配置的 DataGridView 控件</param>
        /// <param name="columnName">数据列的名称</param>
        /// <param name="columnHeaderText">列头的显示文本</param>
        private void SetupGridView(DataGridView dgv, string columnName, string columnHeaderText)
        {
            dgv.AutoGenerateColumns = false;
            dgv.RowHeadersVisible = false; // 隐藏行头
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.None; // 移除单元格边框
            dgv.BackgroundColor = SystemColors.Window;
            dgv.DefaultCellStyle.SelectionBackColor = Color.LightBlue;
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv.AllowUserToResizeRows = false;
            dgv.AllowUserToAddRows = false; // 禁止用户添加新行，防止出现空行

            // 数据列 (显示路径或邮箱)
            var textColumn = new DataGridViewTextBoxColumn { Name = columnName, HeaderText = columnHeaderText, DataPropertyName = columnName, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true };
            dgv.Columns.Add(textColumn);

            // 删除按钮列
            var deleteButtonColumn = new DataGridViewButtonColumn { Name = "Delete", HeaderText = "", Text = "Delete", UseColumnTextForButtonValue = true, Width = 80, FlatStyle = FlatStyle.Flat };
            deleteButtonColumn.DefaultCellStyle.BackColor = Color.LightCoral;
            deleteButtonColumn.DefaultCellStyle.ForeColor = Color.White;
            dgv.Columns.Add(deleteButtonColumn);
        }

        /// <summary>
        /// 从数据库加载设置并填充到 UI 控件中
        /// </summary>
        private void LoadSettings()
        {
            // 加载文件夹列表
            foreach (var folder in _setting.MonitoredFolders) { dgvFolders.Rows.Add(folder.Path); }
            // 加载收件人列表
            foreach (var recipient in _setting.Recipients) { dgvRecipients.Rows.Add(recipient.EmailAddress); }

            // 加载邮件模板和 SMTP 设置
            txtMailSubject.Text = _setting.MailSubject;
            txtMailBody.Text = _setting.MailBody;
            txtSmtpServer.Text = _setting.SmtpServer;
            txtSmtpPort.Text = _setting.SmtpPort.ToString();
            txtSmtpUser.Text = _setting.SmtpUser;
            txtSmtpPass.Text = _setting.SmtpPass;

            // 如果邮件主题或正文为空，则提供默认的英文模板
            if (string.IsNullOrWhiteSpace(txtMailSubject.Text)) { txtMailSubject.Text = "Folder Watcher Alert: [ChangeType] in [FolderPath]"; }
            if (string.IsNullOrWhiteSpace(txtMailBody.Text)) { txtMailBody.Text = "Event: [ChangeType]\nFile: [FileName]\nFolder: [FolderPath]\nTime: [Timestamp]"; }
        }

        /// <summary>
        /// 将 UI 上的所有设置保存到数据库
        /// </summary>
        private void SaveSettings()
        {
            // 如果是新设置，则添加到数据库
            if (_setting.Id == 0) { _context.Settings.Add(_setting); }

            // 从 GridView 中获取当前的文件夹和收件人列表
            var foldersInGrid = dgvFolders.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["Path"].Value?.ToString()).Where(v => !string.IsNullOrEmpty(v)).ToList();
            var recipientsInGrid = dgvRecipients.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["Email"].Value?.ToString()).Where(v => !string.IsNullOrEmpty(v)).ToList();

            // 同步文件夹：找出需要删除和添加的
            var foldersToRemove = _setting.MonitoredFolders.Where(f => !foldersInGrid.Contains(f.Path)).ToList();
            _context.MonitoredFolders.RemoveRange(foldersToRemove);
            var foldersToAdd = foldersInGrid.Where(p => p != null && !_setting.MonitoredFolders.Any(f => f.Path.Equals(p, StringComparison.OrdinalIgnoreCase))).Select(p => new MonitoredFolder { Path = p ?? string.Empty, Setting = _setting });
            _context.MonitoredFolders.AddRange(foldersToAdd);

            // 同步收件人：找出需要删除和添加的
            var recipientsToRemove = _setting.Recipients.Where(r => !recipientsInGrid.Contains(r.EmailAddress)).ToList();
            _context.Recipients.RemoveRange(recipientsToRemove);
            var recipientsToAdd = recipientsInGrid.Where(e => e != null && !_setting.Recipients.Any(r => r.EmailAddress.Equals(e, StringComparison.OrdinalIgnoreCase))).Select(e => new Recipient { EmailAddress = e ?? string.Empty, Setting = _setting });
            _context.Recipients.AddRange(recipientsToAdd);

            // 保存邮件模板和 SMTP 设置
            _setting.MailSubject = txtMailSubject.Text;
            _setting.MailBody = txtMailBody.Text;
            if (int.TryParse(txtSmtpPort.Text, out int port) || string.IsNullOrEmpty(txtSmtpPort.Text)) { _setting.SmtpPort = port; }
            _setting.SmtpServer = txtSmtpServer.Text;
            _setting.SmtpUser = txtSmtpUser.Text;
            _setting.SmtpPass = txtSmtpPass.Text;

            _context.SaveChanges();
            Console.WriteLine("Settings saved automatically.");
        }

        /// <summary>
        /// “添加文件夹”按钮点击事件
        /// </summary>
        private void BtnAddFolder_Click(object? sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    // 检查是否重复
                    if (!dgvFolders.Rows.Cast<DataGridViewRow>().Any(r => r.Cells["Path"].Value?.ToString().Equals(fbd.SelectedPath, StringComparison.OrdinalIgnoreCase) ?? false))
                    {
                        dgvFolders.Rows.Add(fbd.SelectedPath);
                    }
                }
            }
        }

        /// <summary>
        /// “添加收件人”按钮点击事件
        /// </summary>
        private void BtnAddRecipient_Click(object? sender, EventArgs e)
        {
            var newEmail = txtNewRecipient.Text.Trim();
            if (!string.IsNullOrWhiteSpace(newEmail))
            {
                // 检查是否重复
                if (!dgvRecipients.Rows.Cast<DataGridViewRow>().Any(r => r.Cells["Email"].Value?.ToString().Equals(newEmail, StringComparison.OrdinalIgnoreCase) ?? false))
                {
                    dgvRecipients.Rows.Add(newEmail);
                    txtNewRecipient.Clear();
                }
            }
        }

        /// <summary>
        /// 文件夹列表的单元格点击事件 (用于处理删除)
        /// </summary>
        private void DgvFolders_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvFolders.Columns["Delete"].Index && e.RowIndex >= 0) { dgvFolders.Rows.RemoveAt(e.RowIndex); }
        }

        /// <summary>
        /// 收件人列表的单元格点击事件 (用于处理删除)
        /// </summary>
        private void DgvRecipients_CellContentClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvRecipients.Columns["Delete"].Index && e.RowIndex >= 0) { dgvRecipients.Rows.RemoveAt(e.RowIndex); }
        }

        /// <summary>
        /// 窗口关闭时自动保存设置
        /// </summary>
        private void SettingsForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            SaveSettings();
        }
    }
}