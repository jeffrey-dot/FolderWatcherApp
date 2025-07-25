
using System.ComponentModel.DataAnnotations;

namespace FolderWatcherApp.Models
{
    public class MonitoredFolder
    {
        [Key]
        public int Id { get; set; }
        public string Path { get; set; } = string.Empty;
        public int SettingId { get; set; }
        public Setting? Setting { get; set; }
    }
}
