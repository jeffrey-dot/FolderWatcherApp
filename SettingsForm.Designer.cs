namespace FolderWatcherApp
{
    partial class SettingsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabFolders = new System.Windows.Forms.TabPage();
            this.tabRecipients = new System.Windows.Forms.TabPage();
            this.tabEmailTemplate = new System.Windows.Forms.TabPage();
            this.tabSmtpServer = new System.Windows.Forms.TabPage();

            // Folder Tab
            this.dgvFolders = new System.Windows.Forms.DataGridView();
            this.btnAddFolder = new System.Windows.Forms.Button();

            // Recipients Tab
            this.dgvRecipients = new System.Windows.Forms.DataGridView();
            this.txtNewRecipient = new System.Windows.Forms.TextBox();
            this.btnAddRecipient = new System.Windows.Forms.Button();

            // Email Template Tab
            this.lblMailSubject = new System.Windows.Forms.Label();
            this.txtMailSubject = new System.Windows.Forms.TextBox();
            this.lblMailBody = new System.Windows.Forms.Label();
            this.txtMailBody = new System.Windows.Forms.TextBox();
            this.lblPlaceholders = new System.Windows.Forms.Label();

            // SMTP Server Tab
            this.lblSmtpServer = new System.Windows.Forms.Label();
            this.txtSmtpServer = new System.Windows.Forms.TextBox();
            this.lblSmtpPort = new System.Windows.Forms.Label();
            this.txtSmtpPort = new System.Windows.Forms.TextBox();
            this.lblSmtpUser = new System.Windows.Forms.Label();
            this.txtSmtpUser = new System.Windows.Forms.TextBox();
            this.lblSmtpPass = new System.Windows.Forms.Label();
            this.txtSmtpPass = new System.Windows.Forms.TextBox();

            // Init
            this.tabControl.SuspendLayout();
            this.tabFolders.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFolders)).BeginInit();
            this.tabRecipients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipients)).BeginInit();
            this.tabEmailTemplate.SuspendLayout();
            this.tabSmtpServer.SuspendLayout();
            this.SuspendLayout();

            // tabControl
            this.tabControl.Controls.Add(this.tabFolders);
            this.tabControl.Controls.Add(this.tabRecipients);
            this.tabControl.Controls.Add(this.tabEmailTemplate);
            this.tabControl.Controls.Add(this.tabSmtpServer);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;

            // --- Folders Tab ---
            this.tabFolders.Text = "Folders";
            this.tabFolders.Controls.Add(this.dgvFolders);
            this.tabFolders.Controls.Add(this.btnAddFolder);
            this.dgvFolders.Location = new System.Drawing.Point(10, 10);
            this.dgvFolders.Size = new System.Drawing.Size(440, 270);
            this.btnAddFolder.Location = new System.Drawing.Point(350, 285);
            this.btnAddFolder.Text = "Add Folder";

            // --- Recipients Tab ---
            this.tabRecipients.Text = "Recipients";
            this.tabRecipients.Controls.Add(this.dgvRecipients);
            this.tabRecipients.Controls.Add(this.txtNewRecipient);
            this.tabRecipients.Controls.Add(this.btnAddRecipient);
            this.txtNewRecipient.Location = new System.Drawing.Point(10, 10);
            this.txtNewRecipient.Size = new System.Drawing.Size(340, 23);
            this.btnAddRecipient.Location = new System.Drawing.Point(355, 9);
            this.btnAddRecipient.Size = new System.Drawing.Size(95, 25);
            this.btnAddRecipient.Text = "+ Add";
            this.dgvRecipients.Location = new System.Drawing.Point(10, 40);
            this.dgvRecipients.Size = new System.Drawing.Size(440, 270);

            // --- Email Template Tab ---
            this.tabEmailTemplate.Text = "Email Template";
            this.tabEmailTemplate.Controls.Add(this.lblMailSubject);
            this.tabEmailTemplate.Controls.Add(this.txtMailSubject);
            this.tabEmailTemplate.Controls.Add(this.lblMailBody);
            this.tabEmailTemplate.Controls.Add(this.txtMailBody);
            this.tabEmailTemplate.Controls.Add(this.lblPlaceholders);
            this.lblMailSubject.Location = new System.Drawing.Point(10, 15);
            this.lblMailSubject.Text = "Subject Template:";
            this.txtMailSubject.Location = new System.Drawing.Point(10, 35);
            this.txtMailSubject.Size = new System.Drawing.Size(440, 23);
            this.lblMailBody.Location = new System.Drawing.Point(10, 65);
            this.lblMailBody.Text = "Body Template:";
            this.txtMailBody.Location = new System.Drawing.Point(10, 85);
            this.txtMailBody.Size = new System.Drawing.Size(440, 180);
            this.txtMailBody.Multiline = true;
            this.lblPlaceholders.Location = new System.Drawing.Point(10, 270);
            this.lblPlaceholders.Text = "Placeholders: [FileName], [FolderPath], [ChangeType], [Timestamp], [OldFileName]";
            this.lblPlaceholders.AutoSize = true;

            // --- SMTP Server Tab ---
            this.tabSmtpServer.Text = "SMTP Server";
            this.tabSmtpServer.Controls.Add(this.lblSmtpServer);
            this.tabSmtpServer.Controls.Add(this.txtSmtpServer);
            this.tabSmtpServer.Controls.Add(this.lblSmtpPort);
            this.tabSmtpServer.Controls.Add(this.txtSmtpPort);
            this.tabSmtpServer.Controls.Add(this.lblSmtpUser);
            this.tabSmtpServer.Controls.Add(this.txtSmtpUser);
            this.tabSmtpServer.Controls.Add(this.lblSmtpPass);
            this.tabSmtpServer.Controls.Add(this.txtSmtpPass);
            this.lblSmtpServer.Location = new System.Drawing.Point(10, 15);
            this.lblSmtpServer.Text = "SMTP Server:";
            this.txtSmtpServer.Location = new System.Drawing.Point(120, 12);
            this.txtSmtpServer.Size = new System.Drawing.Size(310, 23);
            this.lblSmtpPort.Location = new System.Drawing.Point(10, 45);
            this.lblSmtpPort.Text = "Port:";
            this.txtSmtpPort.Location = new System.Drawing.Point(120, 42);
            this.lblSmtpUser.Location = new System.Drawing.Point(10, 75);
            this.lblSmtpUser.Text = "Username:";
            this.txtSmtpUser.Location = new System.Drawing.Point(120, 72);
            this.txtSmtpUser.Size = new System.Drawing.Size(310, 23);
            this.lblSmtpPass.Location = new System.Drawing.Point(10, 105);
            this.lblSmtpPass.Text = "Password:";
            this.txtSmtpPass.Location = new System.Drawing.Point(120, 102);
            this.txtSmtpPass.PasswordChar = '*';
            this.txtSmtpPass.Size = new System.Drawing.Size(310, 23);

            // SettingsForm
            this.ClientSize = new System.Drawing.Size(464, 341);
            this.Controls.Add(this.tabControl);
            this.Name = "SettingsForm";
            this.Text = "Settings";
            this.ResumeLayout(false);
        }

        #endregion

        // --- 核心控件 ---
        // 选项卡控件，用于组织不同的设置页面
        private System.Windows.Forms.TabControl tabControl;
        // “文件夹”选项卡页
        private System.Windows.Forms.TabPage tabFolders;
        // “收件人”选项卡页
        private System.Windows.Forms.TabPage tabRecipients;
        // “邮件模板”选项卡页
        private System.Windows.Forms.TabPage tabEmailTemplate;
        // “SMTP服务器”选项卡页
        private System.Windows.Forms.TabPage tabSmtpServer;

        // --- “文件夹”选项卡内的控件 ---
        // 用于显示和管理被监控文件夹列表的表格
        private System.Windows.Forms.DataGridView dgvFolders;
        // “添加文件夹”按钮
        private System.Windows.Forms.Button btnAddFolder;

        // --- “收件人”选项卡内的控件 ---
        // 用于显示和管理收件人列表的表格
        private System.Windows.Forms.DataGridView dgvRecipients;
        // 用于输入新收件人邮箱地址的文本框
        private System.Windows.Forms.TextBox txtNewRecipient;
        // “添加收件人”按钮
        private System.Windows.Forms.Button btnAddRecipient;

        // --- “邮件模板”选项卡内的控件 ---
        // “邮件主题”标签
        private System.Windows.Forms.Label lblMailSubject;
        // 用于编辑邮件主题的文本框
        private System.Windows.Forms.TextBox txtMailSubject;
        // “邮件正文”标签
        private System.Windows.Forms.Label lblMailBody;
        // 用于编辑邮件正文的文本框
        private System.Windows.Forms.TextBox txtMailBody;
        // 用于提示可用占位符的标签
        private System.Windows.Forms.Label lblPlaceholders;

        // --- “SMTP服务器”选项卡内的控件 ---
        // “SMTP服务器”标签
        private System.Windows.Forms.Label lblSmtpServer;
        // 用于输入SMTP服务器地址的文本框
        private System.Windows.Forms.TextBox txtSmtpServer;
        // “端口”标签
        private System.Windows.Forms.Label lblSmtpPort;
        // 用于输入SMTP端口的文本框
        private System.Windows.Forms.TextBox txtSmtpPort;
        // “用户名”标签
        private System.Windows.Forms.Label lblSmtpUser;
        // 用于输入SMTP用户名的文本框
        private System.Windows.Forms.TextBox txtSmtpUser;
        // “密码”标签
        private System.Windows.Forms.Label lblSmtpPass;
        // 用于输入SMTP密码的文本框
        private System.Windows.Forms.TextBox txtSmtpPass;
    }
}