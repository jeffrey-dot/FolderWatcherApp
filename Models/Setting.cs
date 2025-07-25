using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FolderWatcherApp.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        // SMTP Server Settings
        public string SmtpServer { get; set; } = string.Empty;
        public int SmtpPort { get; set; } = 587; // Default port
        public string SmtpUser { get; set; } = string.Empty;
        public string SmtpPass { get; set; } = string.Empty;

        // Email Template Settings
        public string MailSubject { get; set; } = string.Empty;
        public string MailBody { get; set; } = string.Empty;

        // Navigation Properties for related data
        public virtual ICollection<MonitoredFolder> MonitoredFolders { get; set; } = new List<MonitoredFolder>();
        public virtual ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();
    }
}