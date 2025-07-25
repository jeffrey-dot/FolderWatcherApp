
namespace FolderWatcherApp
{
    partial class Form1
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
            this.rtbLog = new System.Windows.Forms.RichTextBox();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnToggleMonitoring = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtbLog
            // 
            this.rtbLog.Location = new System.Drawing.Point(12, 12);
            this.rtbLog.Name = "rtbLog";
            this.rtbLog.ReadOnly = true;
            this.rtbLog.Size = new System.Drawing.Size(460, 320);
            this.rtbLog.TabIndex = 0;
            this.rtbLog.Text = "";
            this.rtbLog.BackColor = System.Drawing.Color.Black;
            this.rtbLog.ForeColor = System.Drawing.Color.White;
            this.rtbLog.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(12, 350);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(120, 25);
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            // 
            // btnToggleMonitoring
            // 
            this.btnToggleMonitoring.Location = new System.Drawing.Point(352, 350);
            this.btnToggleMonitoring.Name = "btnToggleMonitoring";
            this.btnToggleMonitoring.Size = new System.Drawing.Size(120, 25);
            this.btnToggleMonitoring.Text = "Start Monitoring";
            this.btnToggleMonitoring.UseVisualStyleBackColor = true;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(150, 355);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(113, 15);
            this.lblStatus.Text = "Status: Not Running";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(484, 386);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnToggleMonitoring);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.rtbLog);
            this.Name = "Form1";
            this.Text = "Folder Watcher";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // 日志显示核心控件，用于实时展示程序运行状态和事件
        private System.Windows.Forms.RichTextBox rtbLog;
        // “设置”按钮，用于打开统一的设置窗口
        private System.Windows.Forms.Button btnSettings;
        // “开始/停止监控”按钮，用于控制监控服务的启停
        private System.Windows.Forms.Button btnToggleMonitoring;
        // 状态标签，用于显示“运行中”或“未运行”等状态
        private System.Windows.Forms.Label lblStatus;
    }
}
